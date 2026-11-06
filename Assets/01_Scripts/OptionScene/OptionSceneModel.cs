using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static LastHit.ConfigData;

/// <summary>
/// オプション画面で編集中の設定データを保持するクラス
/// </summary>
public class OptionSceneModel : MonoBehaviour
{
    public Config configData { get; private set; }

    /// <summary> 変更された設定を設定データに反映するメソッド </summary>
    /// <param name="gamePlay"> ゲーム内設定 </param>
    /// <param name="camera"> カメラ設定 </param>
    /// <param name="sound"> サウンド設定 </param>
    /// <param name="display"> 画面設定 </param>
    public void UpdateConfig(GamePlayConfig? gamePlay = null, CameraConfig? camera = null, SoundConfig? sound = null, DisplayConfig? display = null)
    {
        var config = configData;
        config.gamePlay = gamePlay ?? config.gamePlay;
        config.camera = camera ?? config.camera;
        config.sound = sound ?? config.sound;
        config.display = display ?? config.display;
        SetConfigData(config);
    }

    /// <summary> 現在の設定と編集中の設定に違いがあるか判定するメソッド </summary>
    public bool HasChanges(Config _currentConfig) => !configData.Equals(_currentConfig);

    public void SetConfigData(Config _configData) => configData = _configData;

    /// <summary> 選択したアクションに指定したキーを設定するメソッド </summary>
    /// <param name="action"> 変更するアクション </param>
    /// <param name="inputName"> キーの名前 </param>
    public void SetOperateBinding(OperateGameAction action, string inputName)
    {
        var config = configData;
        switch (action)
        {
            case OperateGameAction.MoveForward: config.operate.moveForward = inputName; break;
            case OperateGameAction.MoveBackward: config.operate.moveBackward = inputName; break;
            case OperateGameAction.MoveRight: config.operate.moveRight = inputName; break;
            case OperateGameAction.MoveLeft: config.operate.moveLeft = inputName; break;
            case OperateGameAction.Jump: config.operate.jump = inputName; break;
            case OperateGameAction.Crouch: config.operate.crouch = inputName; break;
            case OperateGameAction.Dodge: config.operate.dodge = inputName; break;
            default: return;
        }

        SetConfigData(config);
    }

    /// <summary> キー入力を待機し、入力されたキーの名前を返すメソッド </summary>
    /// <returns> 入力されたキー </returns>
    public async UniTask<string> WaitForInputAsync()
    {
        string inputName = null;
        await UniTask.WaitUntil(() =>
        {
            inputName = GetInputName();
            return inputName != null;
        });
        return inputName;
    }

    /// <summary> キーが入力されたら何が押されたか調べて返すメソッド </summary>
    /// <returns> 入力したキーの名前 </returns>
    private string GetInputName()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            foreach (var key in keyboard.allKeys)
            {
                if (key.wasPressedThisFrame)
                    return key.keyCode.ToString();
            }
        }

        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame) return "MouseLeft";
            if (mouse.rightButton.wasPressedThisFrame) return "MouseRight";
            if (mouse.middleButton.wasPressedThisFrame) return "MouseMiddle";
            if (mouse.backButton.wasPressedThisFrame) return "MouseBack";
            if (mouse.forwardButton.wasPressedThisFrame) return "MouseForward";
        }

        return null;
    }
}

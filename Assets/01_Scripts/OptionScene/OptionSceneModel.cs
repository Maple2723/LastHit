using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
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

}

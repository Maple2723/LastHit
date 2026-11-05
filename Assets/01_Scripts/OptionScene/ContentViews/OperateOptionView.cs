using System;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary>
/// 操作設定のUI表示を管理するクラス
/// </summary>
public class OperateOptionView : MonoBehaviour
{
    [Header("操作設定のUI")]
    [SerializeField] private TMP_Text moveForwardText;
    [SerializeField] private TMP_Text moveBackwardText;
    [SerializeField] private TMP_Text moveRightText;
    [SerializeField] private TMP_Text moveLeftText;
    [SerializeField] private TMP_Text jumpText;
    [SerializeField] private TMP_Text crouchText;
    [SerializeField] private TMP_Text dodgeText;

    [Header("キー入力パネル")]
    [SerializeField] private Button[] keyButtons;
    [SerializeField] private GameObject keyInputPanel;

    /// <summary> 設定データを画面上に反映させるメソッド </summary>
    /// <param name="_config"> 画面の設定データ </param>
    public void SetConfigData(OperateConfig _config)
    {
        moveForwardText.text = GetKeyName(_config.moveForward, _config.moveForwardMouse);
        moveBackwardText.text = GetKeyName(_config.moveBackward, _config.moveBackwardMouse);
        moveRightText.text = GetKeyName(_config.moveRight, _config.moveRightMouse);
        moveLeftText.text = GetKeyName(_config.moveLeft, _config.moveLeftMouse);
        jumpText.text = GetKeyName(_config.jump, _config.jumpMouse);
        crouchText.text = GetKeyName(_config.crouch, _config.crouchMouse);
        dodgeText.text = GetKeyName(_config.dodge, _config.dodgeMouse);
    }

    /// <summary> 操作設定の操作を一括で通知 </summary>
    public IObservable<int> KeyButtonClicked =>
        Observable.Merge(keyButtons.Select((button, index) =>
            button.OnClickAsObservable().Select(_ => index)));

    /// <summary> キーコードを文字に変換するメソッド </summary>
    /// <param name="_key"> キーコード </param>
    /// <returns> キーの名称 </returns>
    private string GetKeyName(Key _key, MouseBinding mouse)
    {
        if (mouse != MouseBinding.None)
        {
            return mouse switch
            {
                MouseBinding.Left => "左クリック",
                MouseBinding.Right => "右クリック",
                MouseBinding.Middle => "中央クリック",
                MouseBinding.Back => "マウス戻る",
                MouseBinding.Forward => "マウス進む",
                _ => "未設定"
            };
        }


        if (_key == Key.None) return "未設定";
        return Keyboard.current[_key].displayName;
    }

    /// <summary>
    /// キー設定画面を表示するメソッド;
    /// </summary>
    public void ShowKeyInputPanel() => keyInputPanel.SetActive(true);

    public void HideKeyInputPanel() => keyInputPanel.SetActive(false);
}

using System;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary>
/// 操作設定のUI表示を管理するクラス
/// </summary>
public class OperateOptionView : MonoBehaviour
{
    [Serializable]
    private struct KeyButtonBinding
    {
        public OperateGameAction action;
        public Button button;
    }

    [Header("操作設定のUI")]
    [SerializeField] private TMP_Text moveForwardText;
    [SerializeField] private TMP_Text moveBackwardText;
    [SerializeField] private TMP_Text moveRightText;
    [SerializeField] private TMP_Text moveLeftText;
    [SerializeField] private TMP_Text jumpText;
    [SerializeField] private TMP_Text crouchText;
    [SerializeField] private TMP_Text dodgeText;

    [Header("キー入力パネル")]
    [SerializeField] private KeyButtonBinding[] keyButtons;
    [SerializeField] private GameObject keyInputPanel;

    /// <summary> 設定データを画面上に反映させるメソッド </summary>
    /// <param name="_config"> 操作の設定データ </param>
    public void SetConfigData(OperateConfig _config)
    {
        moveForwardText.text =_config.moveForward;
        moveBackwardText.text = _config.moveBackward;
        moveRightText.text = _config.moveRight;
        moveLeftText.text = _config.moveLeft;
        jumpText.text = _config.jump;
        crouchText.text = _config.crouch;
        dodgeText.text = _config.dodge;
    }

    /// <summary> キー割り当てを変更する操作の選択を通知 </summary>
    public IObservable<OperateGameAction> KeyButtonClicked =>
        Observable.Merge(keyButtons.Select(binding =>
            binding.button.OnClickAsObservable().Select(_ => binding.action)));

    /// <summary> キー設定画面を表示するメソッド </summary>
    public void ShowKeyInputPanel() => keyInputPanel.SetActive(true);

    /// <summary> キー設定画面を非表示にするメソッド </summary>
    public void HideKeyInputPanel() => keyInputPanel.SetActive(false);
}

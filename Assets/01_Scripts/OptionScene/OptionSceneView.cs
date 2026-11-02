using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public enum OptionPanelType
{
    GamePlay,
    Camera,
    Sound,
    Display,
    Operate
}

public class OptionSceneView : MonoBehaviour
{
    [Serializable]
    private class ContentPanel
    {
        public OptionPanelType panelType;
        public GameObject panel;
    }

    [Header("設定カテゴリに対応した表示パネル")]
    [SerializeField] private ContentPanel[] contentPanels;

    [Header("設定カテゴリ設定ボタン")]
    [SerializeField] private Button gamePlayConfigButton;
    [SerializeField] private Button cameraConfigButton;
    [SerializeField] private Button soundConfigButton;
    [SerializeField] private Button displayConfigButton;
    [SerializeField] private Button operateConfigButton;

    [Header("設定下部に常に表示されるボタン")]
    [SerializeField] private Button footerCloseButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button applyButton;

    [Header("設定画面を閉じるボタン")]
    [SerializeField] private Button CloseButton;

    private void Awake()
    {
        ShowCategory(OptionPanelType.GamePlay); // 最初はゲームプレイパネルを表示する
    }

    /// <summary> 指定されたパネルを表示するメソッド </summary>
    /// <param name="_panelType"> 表示するパネル </param>
    public void ShowCategory(OptionPanelType _panelType)
    {
        foreach (var content in contentPanels)
        {
            content.panel.SetActive(content.panelType == _panelType);
        }
    }

    // 各ボタンのクリック通知を送信
    public IObservable<Unit> GamePlayConfigButtonClick => gamePlayConfigButton.OnClickAsObservable();
    public IObservable<Unit> CameraConfigButtonClick => cameraConfigButton.OnClickAsObservable();
    public IObservable<Unit> SoundConfigButtonClick => soundConfigButton.OnClickAsObservable();
    public IObservable<Unit> DisplayConfigButtonClick => displayConfigButton.OnClickAsObservable();
    public IObservable<Unit> OperateConfigButtonClick => operateConfigButton.OnClickAsObservable();
    public IObservable<Unit> FooterCloseButtonClick => footerCloseButton.OnClickAsObservable();
    public IObservable<Unit> ResetButtonClick => resetButton.OnClickAsObservable();
    public IObservable<Unit> ApplyButtonClick => applyButton.OnClickAsObservable();
    public IObservable<Unit> CloseButtonClick => CloseButton.OnClickAsObservable();
}

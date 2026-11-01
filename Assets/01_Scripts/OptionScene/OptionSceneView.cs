using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class OptionSceneView : MonoBehaviour
{
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

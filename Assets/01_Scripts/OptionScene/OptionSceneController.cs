using System;
using UniRx;
using UnityEngine;

/// <summary>
/// OptionSceneの処理を制御するクラス
/// </summary>
public class OptionSceneController : MonoBehaviour
{
    [Header("Viewを登録")]
    [SerializeField] private OptionSceneView view;

    [Header("ボタンの連打防止時間を設定")]
    [SerializeField, Range(0.01f, 1f)] private double clickBlockSecond = 0.1;

    private void Start()
    {
        var interval = TimeSpan.FromSeconds(clickBlockSecond);

        view.GamePlayConfigButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnGamePlayConfigButtonClicked())
            .AddTo(this);

        view.CameraConfigButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnCameraConfigButtonClicked())
            .AddTo(this);

        view.SoundConfigButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnSoundConfigButtonClicked())
            .AddTo(this);

        view.DisplayConfigButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnDisplayConfigButtonClicked())
            .AddTo(this);

        view.OperateConfigButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnOperateConfigButtonClicked())
            .AddTo(this);

        view.FooterCloseButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnCloseButtonClicked())
            .AddTo(this);

        view.ResetButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnResetButtonClicked())
            .AddTo(this);

        view.ApplyButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnApplyButtonClicked())
            .AddTo(this);

        view.CloseButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnCloseButtonClicked())
            .AddTo(this);
    }

    private void OnGamePlayConfigButtonClicked()
    {
        Debug.Log("ゲームプレイ設定ボタンが押されました");
    }

    private void OnCameraConfigButtonClicked()
    {
        Debug.Log("カメラ設定ボタンが押されました");
    }

    private void OnSoundConfigButtonClicked()
    {
        Debug.Log("サウンド設定ボタンが押されました");
    }

    private void OnDisplayConfigButtonClicked()
    {
        Debug.Log("ディスプレイ設定ボタンが押されました");
    }

    private void OnOperateConfigButtonClicked()
    {
        Debug.Log("操作設定ボタンが押されました");
    }

    private void OnResetButtonClicked()
    {
        Debug.Log("初期設定に戻すボタンが押されました");
    }

    private void OnApplyButtonClicked()
    {
        Debug.Log("適用ボタンが押されました");
    }

    private void OnCloseButtonClicked()
    {
        Debug.Log("閉じるボタンが押されました");
    }
}

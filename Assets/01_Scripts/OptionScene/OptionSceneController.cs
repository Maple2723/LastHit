using System;
using UniRx;
using UnityEngine;
using static LastHit.SceneNames;

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

    /// <summary> ゲームプレイ設定ボタンが押された際の処理を行うメソッド </summary>
    private void OnGamePlayConfigButtonClicked()
    {
        view.ShowCategory(OptionPanelType.GamePlay);
    }

    /// <summary> カメラ設定ボタンが押された際の処理を行うメソッド </summary>
    private void OnCameraConfigButtonClicked()
    {
        view.ShowCategory(OptionPanelType.Camera);
    }

    /// <summary> サウンド設定ボタンが押された際の処理を行うメソッド </summary>
    private void OnSoundConfigButtonClicked()
    {
        view.ShowCategory(OptionPanelType.Sound);
    }

    /// <summary> ディスプレイ設定ボタンが押された際の処理を行うメソッド </summary>
    private void OnDisplayConfigButtonClicked()
    {
        view.ShowCategory(OptionPanelType.Display);
    }

    /// <summary> 操作設定ボタンが押された際の処理を行うメソッド </summary>
    private void OnOperateConfigButtonClicked()
    {
        view.ShowCategory(OptionPanelType.Operate);
    }

    /// <summary> 初期設定に戻すボタンが押された際の処理を行うメソッド </summary>
    private void OnResetButtonClicked()
    {
        Debug.Log("初期設定に戻すボタンが押されました");
    }

    /// <summary> 適用ボタンが押された際の処理を行うメソッド </summary>
    private void OnApplyButtonClicked()
    {
        Debug.Log("適用ボタンが押されました");
    }

    /// <summary> 閉じるボタンが押された際の処理を行うメソッド </summary>
    private void OnCloseButtonClicked()
    {
        SceneTransitionController.Instance.UnloadScene(OPTION_SCENE);
    }
}

using System;
using UniRx;
using UnityEngine;
using static LastHit.ConfigData;
using static LastHit.SceneNames;

/// <summary>
/// OptionSceneの処理を制御するクラス
/// </summary>
public class OptionSceneController : MonoBehaviour
{
    [Header("Model,Viewを登録")]
    [SerializeField] private OptionSceneView view;
    [SerializeField] private OptionSceneModel model;

    [Header("子オブジェクトのカテゴリViewを登録")]
    [SerializeField] private GamePlayOptionView gamePlayView;
    [SerializeField] private CameraOptionView cameraView;
    [SerializeField] private SoundOptionView soundView;
    [SerializeField] private DisplayOptionView displayView;
    [SerializeField] private OperateOptionView operateView;

    [Header("ボタンの連打防止時間を設定")]
    [SerializeField, Range(0.01f, 1f)] private double clickBlockSecond = 0.1;

    private void Start()
    {
        model.SetConfigData(ConfigDataController.Instance.GetConfigData());
        RefreshConfigViews();
        SubscribeConfigChanges();

        RegisterButtonEvents();
    }

    /// <summary> ボタンのイベントを登録するメソッド </summary>
    private void RegisterButtonEvents()
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

    /// <summary> 設定データを画面上に反映するメソッド </summary>
    private void RefreshConfigViews()
    {
        var config = model.GetConfigData();
        gamePlayView.SetConfigData(config.gamePlay);
        cameraView.SetConfigData(config.camera);
        soundView.SetConfigData(config.sound);
        displayView.SetConfigData(config.display);
        operateView.SetConfigData(config.operate);
    }

    /// <summary> 各設定を変更時に設定データに反映させるメソッド </summary>
    private void SubscribeConfigChanges()
    {
        gamePlayView.ConfigChanged.Subscribe(_config =>
        {
            var config = model.GetConfigData();
            config.gamePlay = _config;
            model.SetConfigData(config);
        }).AddTo(this);

        cameraView.ConfigChanged.Subscribe(_config =>
        {
            var config = model.GetConfigData();
            config.camera = _config;
            model.SetConfigData(config);
        }).AddTo(this);

        soundView.ConfigChanged.Subscribe(_config =>
        {
            var config = model.GetConfigData();
            config.sound = _config;
            model.SetConfigData(config);
        }).AddTo(this);

        displayView.FullScreenChanged.Subscribe(_config =>
        {
            var config = model.GetConfigData();
            config.display.isFullScreen = _config;
            model.SetConfigData(config);
        }).AddTo(this);

        displayView.PreviousSizeClicked.Subscribe(_ => ChangeWindowSize(-1)).AddTo(this);
        displayView.NextSizeClicked.Subscribe(_ => ChangeWindowSize(1)).AddTo(this);
    }

    /// <summary> 画面サイズを変更時に設定データに反映させるメソッド </summary>
    /// <param name="_direction"> サイズを大きくするか小さくするか </param>
    private void ChangeWindowSize(int _direction)
    {
        var config = model.GetConfigData();
        int count = Enum.GetValues(typeof(WindowSize)).Length;
        int nextIndex = (int)config.display.windowSize + _direction;
        config.display.windowSize = (WindowSize)Mathf.Clamp(nextIndex, 0, count - 1);
        model.SetConfigData(config);
        displayView.SetConfigData(config.display);
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
        model.SetConfigData(ConfigDataController.Instance.GetDefaultConfigData());
        RefreshConfigViews();
    }

    /// <summary> 適用ボタンが押された際の処理を行うメソッド </summary>
    private void OnApplyButtonClicked()
    {
        ConfigDataController.Instance.ChangeConfigData(model.GetConfigData());
        ConfigDataController.Instance.SaveConfigData();
    }

    /// <summary> 閉じるボタンが押された際の処理を行うメソッド </summary>
    private void OnCloseButtonClicked()
    {
        ConfigDataController.Instance.SaveConfigData();
        SceneTransitionController.Instance.UnloadScene(OPTION_SCENE);
    }
}

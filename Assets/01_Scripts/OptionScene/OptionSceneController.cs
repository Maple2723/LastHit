using Cysharp.Threading.Tasks;
using LastHit;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
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

        operateView.KeyButtonClicked
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(index => OnKeyConfigButtonClicked(index))
            .AddTo(this);
    }

    /// <summary> 設定データを画面上に反映するメソッド </summary>
    private void RefreshConfigViews()
    {
        gamePlayView.SetConfigData(model.configData.gamePlay);
        cameraView.SetConfigData(model.configData.camera);
        soundView.SetConfigData(model.configData.sound);
        displayView.SetConfigData(model.configData.display);
        operateView.SetConfigData(model.configData.operate);
    }

    /// <summary> 各設定を変更時に設定データに反映させるメソッド </summary>
    private void SubscribeConfigChanges()
    {
        gamePlayView.ConfigChanged.Subscribe(config => model.UpdateConfig(gamePlay: config)).AddTo(this);
        cameraView.ConfigChanged.Subscribe(config => model.UpdateConfig(camera: config)).AddTo(this);
        soundView.ConfigChanged.Subscribe(config => model.UpdateConfig(sound: config)).AddTo(this);
        displayView.ConfigChanged.Subscribe(config => model.UpdateConfig(display: config)).AddTo(this);
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
        ConfigDataController.Instance.ChangeConfigData(model.configData);
        ConfigDataController.Instance.SaveConfigData();
    }

    /// <summary> 閉じるボタンが押された際の処理を行うメソッド </summary>
    private void OnCloseButtonClicked()
    {
        ConfigDataController.Instance.SaveConfigData();
        SceneTransitionController.Instance.UnloadScene(OPTION_SCENE);
    }

    private async void OnKeyConfigButtonClicked()
    {
        operateView.ShowKeyInputPanel();
    }
}

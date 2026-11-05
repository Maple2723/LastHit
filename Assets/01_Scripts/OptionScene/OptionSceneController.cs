using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using static LastHit.SceneNames;
using static LastHit.ConfigData;

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

        RegisterCategoryButton(view.GamePlayConfigButtonClick, OptionPanelType.GamePlay, interval);
        RegisterCategoryButton(view.CameraConfigButtonClick, OptionPanelType.Camera, interval);
        RegisterCategoryButton(view.SoundConfigButtonClick, OptionPanelType.Sound, interval);
        RegisterCategoryButton(view.DisplayConfigButtonClick, OptionPanelType.Display, interval);
        RegisterCategoryButton(view.OperateConfigButtonClick, OptionPanelType.Operate, interval);

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
            .Subscribe(action => OnKeyConfigButtonClicked(action))
            .AddTo(this);
    }

    /// <summary> ボタンを押したときに対応するカテゴリを表示する </summary>
    private void RegisterCategoryButton(IObservable<Unit> buttonClick, OptionPanelType category, TimeSpan interval)
    {
        buttonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => view.ShowCategory(category))
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


    /// <summary> 初期設定に戻すボタンが押された際の処理を行うメソッド </summary>
    private void OnResetButtonClicked()
    {
        model.SetConfigData(ConfigDataController.Instance.GetDefaultConfigData());
        RefreshConfigViews();
    }

    /// <summary> 適用ボタンが押された際の処理を行うメソッド </summary>
    private void OnApplyButtonClicked()
    {
        ConfigDataController.Instance.ApplyConfig(model.configData);
    }

    /// <summary> 閉じるボタンが押された際の処理を行うメソッド </summary>
    private void OnCloseButtonClicked()
    {
        SceneTransitionController.Instance.UnloadScene(OPTION_SCENE);
    }

    /// <summary> キー設定を変更するメソッド </summary>
    /// <param name="action"> 変更したいアクション </param>
    private async void OnKeyConfigButtonClicked(OperateAction action)
    {
        var inputModule = EventSystem.current?.currentInputModule as InputSystemUIInputModule;
        inputModule.enabled = false;
        // キー入力待ち
        operateView.ShowKeyInputPanel();
        string inputName = await model.WaitForInputAsync();

        // 入力後、設定を更新
        if(inputName != "Escape")
        {
            model.SetOperateBinding(action, inputName);
            operateView.SetConfigData(model.configData.operate);
        }

        operateView.HideKeyInputPanel();
        inputModule.enabled = true;
    }
}

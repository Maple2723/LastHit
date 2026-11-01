using UnityEngine;
using System;
using UniRx;
using UnityEngine.SceneManagement;
using static LastHit.SceneNames;

/// <summary>
/// TitleSceneの処理を制御するクラス
/// </summary>
public class TitleSceneController : MonoBehaviour
{
    [Header("Model,Viewを登録")]
    [SerializeField] private TitleSceneView view;
    [SerializeField] private TitleSceneModel model;

    [Header ("ボタンの連打防止時間を設定")]
    [SerializeField, Range(0.01f, 1f)] private double clickBlockSecond;

    private void Start()
    {
        var interval = TimeSpan.FromSeconds(clickBlockSecond);     // 待機時間 

        // クリック時に呼び出される関数を登録
        view.StartButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => StartButtonClicked())
            .AddTo(this);

        view.TrainingButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => TrainingButtonClicked())
            .AddTo(this);

        view.OptionButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OptionButtonClicked())
            .AddTo(this);

        view.QuitButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => QuitButtonClicked())
            .AddTo(this);
    }

    /// <summary> 開始ボタンが押された際の処理を行うメソッド </summary>
    private void StartButtonClicked()
    {
        SceneTransitionController.Instance.LoadScene(STAGE_SELECT_SCENE, _isFadeEnabled: true);
    }

    /// <summary> トレーニングボタンが押された際の処理を行うメソッド </summary>
    private void TrainingButtonClicked()
    {
        SceneTransitionController.Instance.LoadScene(STAGE_SELECT_SCENE, _isFadeEnabled: true);
    }

    /// <summary> 設定ボタンが押された際の処理を行うメソッド </summary>
    private void OptionButtonClicked()
    {
        SceneTransitionController.Instance.LoadScene(OPTION_SCENE, LoadSceneMode.Additive);
    }

    /// <summary> 終了ボタンが押された際の処理を行うメソッド </summary>
    private void QuitButtonClicked()
    {
        model.QuitGame();
    }
}

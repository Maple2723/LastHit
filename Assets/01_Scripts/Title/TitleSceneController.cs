using UnityEngine;
using System;
using UniRx;

/// <summary>
/// TitleSceneの処理を制御するクラス
/// </summary>
public class TitleSceneController : MonoBehaviour
{
    [Header ("Viewを登録")]
    [SerializeField] private TitleSceneView view;
    //[SerializeField] private TitleSceneModel model;

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
        Debug.Log("Startボタンが押されました");
    }

    /// <summary> トレーニングボタンが押された際の処理を行うメソッド </summary>
    private void TrainingButtonClicked()
    {
        Debug.Log("Trainingボタンが押されました");
    }

    /// <summary> 設定ボタンが押された際の処理を行うメソッド </summary>
    private void OptionButtonClicked()
    {
        Debug.Log("Optionボタンが押されました");
    }

    /// <summary> 終了ボタンが押された際の処理を行うメソッド </summary>
    private void QuitButtonClicked()
    {
        Debug.Log("Quitボタンが押されました");
    }
}
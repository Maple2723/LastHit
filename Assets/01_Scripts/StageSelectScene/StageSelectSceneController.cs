using System;
using UniRx;
using UnityEngine;
using static LastHit.SceneNames;

/// <summary>
/// StageSelectSceneの処理を制御するクラス
/// </summary>
public class StageSelectSceneController : MonoBehaviour
{
    [Header("Viewを登録")]
    [SerializeField] private StageSelectSceneView view;

    [Header("ボタンの連打防止時間を設定")]
    [SerializeField, Range(0.01f, 1f)] private double clickBlockSecond = 0.1;

    private void Start()
    {
        // ボタンにイベントを登録する
        var interval = TimeSpan.FromSeconds(clickBlockSecond);

        view.GameStartButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnBackButtonClicked())
            .AddTo(this);

        view.BackButtonClick
            .ThrottleFirst(interval, Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => OnGameStartButtonClicked())
            .AddTo(this);
    }

    /// <summary> 戻るボタンが押された際の処理を行うメソッド </summary>
    private void OnBackButtonClicked()
    {
        SceneTransitionController.Instance.LoadScene(INGAME_SCENE, _isFadeEnabled: true);
    }

    /// <summary> ゲーム開始ボタンが押された際の処理を行うメソッド </summary>
    private void OnGameStartButtonClicked()
    {
        SceneTransitionController.Instance.LoadScene(TITLE_SCENE, _isFadeEnabled: true);
    }
}

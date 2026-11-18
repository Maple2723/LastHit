using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>StageSelectSceneのボタン操作を通知するクラス</summary>
public class StageSelectSceneView : MonoBehaviour
{
    [Header("画面上のボタンの登録")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button gameStartButton;

    public IObservable<Unit> GameStartButtonClick => gameStartButton.OnClickAsObservable();
    public IObservable<Unit> BackButtonClick => backButton.OnClickAsObservable();
}

using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

/// <summary>
/// TitleSceneの描画情報を定義するクラス
/// </summary>
public class TitleSceneView : MonoBehaviour
{
    [Header ("画面上のボタンの登録")]
    [SerializeField] private Button startButton;    // 開始ボタン
    [SerializeField] private Button trainingButton; // トレーニングボタン
    [SerializeField] private Button optionButton;   // 設定ボタン
    [SerializeField] private Button quitButton;     // 終了ボタン


    // 各ボタンのクリック通知を送信
    public IObservable<Unit> StartButtonClick => startButton.OnClickAsObservable();
    public IObservable<Unit> TrainingButtonClick => trainingButton.OnClickAsObservable();
    public IObservable<Unit> OptionButtonClick => optionButton.OnClickAsObservable();
    public IObservable<Unit> QuitButtonClick => quitButton.OnClickAsObservable();
}

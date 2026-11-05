using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary>
/// 画面設定のUI表示を管理するクラス
/// </summary>
public class DisplayOptionView : MonoBehaviour
{
    [Header("画面設定のUI")]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Button previousSizeButton;
    [SerializeField] private Button nextSizeButton;
    [SerializeField] private TMP_Text windowSizeText;

    private static readonly int WindowSizeCount = Enum.GetValues(typeof(WindowSize)).Length;
    private WindowSize windowSize;

    /// <summary> 設定データを画面上に反映させるメソッド </summary>
    /// <param name="_config"> 画面の設定データ </param>
    public void SetConfigData(DisplayConfig _config)
    {
        fullScreenToggle.SetIsOnWithoutNotify(_config.isFullScreen);
        SetWindowSize(_config.windowSize);
    }

    /// <summary> 画面設定の変更を一括で通知 </summary>
    public IObservable<DisplayConfig> ConfigChanged => Observable.Merge(
        fullScreenToggle.OnValueChangedAsObservable().Skip(1).AsUnitObservable(),
        previousSizeButton.OnClickAsObservable().Do(_ => ChangeWindowSize(-1)),
        nextSizeButton.OnClickAsObservable().Do(_ => ChangeWindowSize(1)))
        .Select(_ => new DisplayConfig
        {
            isFullScreen = fullScreenToggle.isOn,
            windowSize = windowSize
        });

    /// <summary> 画面サイズの設定値を変更するメソッド </summary>
    /// <param name="_direction"> 変更値 </param>
    private void ChangeWindowSize(int _direction)
    {
        int nextIndex = Mathf.Clamp((int)windowSize + _direction, 0, WindowSizeCount - 1);
        SetWindowSize((WindowSize)nextIndex);
    }

    /// <summary> UI上に設定する画面サイズを描画 </summary>
    /// <param name="_windowSize"> 画面サイズ </param>
    private void SetWindowSize(WindowSize _windowSize)
    {
        windowSize = _windowSize;

        // enumの定義からSizeを削除して表示する
        const string TRIM_TEXT = "Size";
        windowSizeText.text = windowSize.ToString().Replace(TRIM_TEXT, "");
    }
}

using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

public class DisplayOptionView : MonoBehaviour
{
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Button previousSizeButton;
    [SerializeField] private Button nextSizeButton;
    [SerializeField] private TMP_Text windowSizeText;

    public void SetConfigData(DisplayConfig _config)
    {
        fullScreenToggle.SetIsOnWithoutNotify(_config.isFullScreen);
        windowSizeText.text = _config.windowSize.ToString();
    }

    public IObservable<bool> FullScreenChanged => fullScreenToggle.OnValueChangedAsObservable().Skip(1);
    public IObservable<Unit> PreviousSizeClicked => previousSizeButton.OnClickAsObservable();
    public IObservable<Unit> NextSizeClicked => nextSizeButton.OnClickAsObservable();
}

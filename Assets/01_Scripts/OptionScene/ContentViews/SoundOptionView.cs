using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary>
/// サウンド設定のUI表示を管理するクラス
/// </summary>
public class SoundOptionView : MonoBehaviour
{
    [Header("サウンド設定のUI")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;

    /// <summary> 設定データを画面上に反映させるメソッド </summary>
    /// <param name="_config"> 画面の設定データ </param>
    public void SetConfigData(SoundConfig _config)
    {
        masterVolumeSlider.SetValueWithoutNotify(_config.masterVolume);
        bgmVolumeSlider.SetValueWithoutNotify(_config.bgmVolume);
        seVolumeSlider.SetValueWithoutNotify(_config.seVolume);
        effectVolumeSlider.SetValueWithoutNotify(_config.effectVolume);
    }

    /// <summary> サウンド設定の変更を一括で通知 </summary>
    public IObservable<SoundConfig> ConfigChanged => Observable.Merge(
        masterVolumeSlider.OnValueChangedAsObservable().Skip(1).AsUnitObservable(),
        bgmVolumeSlider.OnValueChangedAsObservable().Skip(1).AsUnitObservable(),
        seVolumeSlider.OnValueChangedAsObservable().Skip(1).AsUnitObservable(),
        effectVolumeSlider.OnValueChangedAsObservable().Skip(1).AsUnitObservable())
        .Select(_ => new SoundConfig
        {
            masterVolume = Mathf.RoundToInt(masterVolumeSlider.value),
            bgmVolume = Mathf.RoundToInt(bgmVolumeSlider.value),
            seVolume = Mathf.RoundToInt(seVolumeSlider.value),
            effectVolume = Mathf.RoundToInt(effectVolumeSlider.value)
        });
}

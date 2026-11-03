using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary> SoundConfigの表示と変更通知を担当する </summary>
public class SoundOptionView : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;

    public void SetConfigData(SoundConfig _config)
    {
        masterVolumeSlider.SetValueWithoutNotify(_config.masterVolume);
        bgmVolumeSlider.SetValueWithoutNotify(_config.bgmVolume);
        seVolumeSlider.SetValueWithoutNotify(_config.seVolume);
        effectVolumeSlider.SetValueWithoutNotify(_config.effectVolume);
    }

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

using System;
using System.Globalization;
using TMPro;
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
    [SerializeField] private TMP_InputField masterVolumeInput;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private TMP_InputField bgmVolumeInput;
    [SerializeField] private Slider seVolumeSlider;
    [SerializeField] private TMP_InputField seVolumeInput;
    [SerializeField] private Slider effectVolumeSlider;
    [SerializeField] private TMP_InputField effectVolumeInput;

    private void Awake()
    {
        // スライダーとインプットフィールドを紐づける
        BindSlider(masterVolumeSlider, masterVolumeInput);
        BindSlider(bgmVolumeSlider, bgmVolumeInput);
        BindSlider(seVolumeSlider, seVolumeInput);
        BindSlider(effectVolumeSlider, effectVolumeInput);
    }

    /// <summary> スライダーとインプットフィールドを紐づけるメソッド </summary>
    /// <param name="_slider"> 紐づけるスライド </param>
    /// <param name="_input"> 紐づけるインプットフィールド </param>
    private void BindSlider(Slider _slider, TMP_InputField _input)
    {
        // スライダーが少数ならインプットフィールドも少数に対応させる
        _input.contentType = _slider.wholeNumbers ? TMP_InputField.ContentType.IntegerNumber : TMP_InputField.ContentType.DecimalNumber;

        // スライダーとインプットフィールドどちらがが変更されたらどちらも変更する
        _slider.onValueChanged.AsObservable().Subscribe(value => SetSliderValue(_slider, _input, value)).AddTo(this);
        _input.onEndEdit.AsObservable().Subscribe(text =>
        {
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal value))
                _slider.value = (float)(decimal.Truncate(value * 100m) / 100m);

            SetSliderValue(_slider, _input, _slider.value);
        }).AddTo(this);
    }

    /// <summary> スライダーとインプットフィールドの数値を同じにするメソッド </summary>
    /// <param name="_slider"> 変更するスライダー </param>
    /// <param name="_input"> 変更するインプットフィールド </param>
    /// <param name="_value"> 変更したい値 </param>
    private void SetSliderValue(Slider _slider, TMP_InputField _input, float _value)
    {
        if (!_slider.wholeNumbers) _value = (float)(decimal.Truncate((decimal)_value * 100m) / 100m);
        _slider.SetValueWithoutNotify(_value);
        _input.SetTextWithoutNotify(_slider.value.ToString("0.##", CultureInfo.InvariantCulture));
    }

    /// <summary> 設定データを画面上に反映させるメソッド </summary>
    /// <param name="_config"> 音量の設定データ </param>
    public void SetConfigData(SoundConfig _config)
    {
        SetSliderValue(masterVolumeSlider, masterVolumeInput, _config.masterVolume);
        SetSliderValue(bgmVolumeSlider, bgmVolumeInput, _config.bgmVolume);
        SetSliderValue(seVolumeSlider, seVolumeInput, _config.seVolume);
        SetSliderValue(effectVolumeSlider, effectVolumeInput, _config.effectVolume);
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

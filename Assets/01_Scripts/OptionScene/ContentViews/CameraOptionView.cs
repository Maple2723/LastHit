using System;
using System.Globalization;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary> 
/// カメラ設定のUI表示を管理するクラス
/// </summary>
public class CameraOptionView : MonoBehaviour
{
    [Header("カメラ設定のUI")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TMP_InputField sensitivityInput;
    [SerializeField] private Slider fieldOfViewSlider;
    [SerializeField] private TMP_InputField fieldOfViewInput;
    [SerializeField] private Toggle invertXToggle;
    [SerializeField] private Toggle invertYToggle;

    private void Awake()
    {
        // スライダーとインプットフィールドを紐づける
        BindSlider(sensitivitySlider, sensitivityInput);
        BindSlider(fieldOfViewSlider, fieldOfViewInput);
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
    /// <param name="_config"> カメラの設定データ </param>
    public void SetConfigData(CameraConfig _config)
    {
        SetSliderValue(sensitivitySlider, sensitivityInput, _config.sensitivity);
        SetSliderValue(fieldOfViewSlider, fieldOfViewInput, _config.fieldOfView);
        invertXToggle.SetIsOnWithoutNotify(_config.isInvertX);
        invertYToggle.SetIsOnWithoutNotify(_config.isInvertY);
    }

    /// <summary> カメラ設定の変更を一括で通知 </summary>
    public IObservable<CameraConfig> ConfigChanged => Observable.Merge(
        sensitivitySlider.OnValueChangedAsObservable().Skip(1).AsUnitObservable(),
        fieldOfViewSlider.OnValueChangedAsObservable().Skip(1).AsUnitObservable(),
        invertXToggle.OnValueChangedAsObservable().Skip(1).AsUnitObservable(),
        invertYToggle.OnValueChangedAsObservable().Skip(1).AsUnitObservable())
        .Select(_ => new CameraConfig
        {
            sensitivity = sensitivitySlider.value,
            fieldOfView = Mathf.RoundToInt(fieldOfViewSlider.value),
            isInvertX = invertXToggle.isOn,
            isInvertY = invertYToggle.isOn
        });
}

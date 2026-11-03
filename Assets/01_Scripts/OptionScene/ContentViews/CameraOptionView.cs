using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary> CameraConfigの表示と変更通知を担当する </summary>
public class CameraOptionView : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider fieldOfViewSlider;
    [SerializeField] private Toggle invertXToggle;
    [SerializeField] private Toggle invertYToggle;

    public void SetConfigData(CameraConfig _config)
    {
        sensitivitySlider.SetValueWithoutNotify(_config.sensitivity);
        fieldOfViewSlider.SetValueWithoutNotify(_config.fieldOfView);
        invertXToggle.SetIsOnWithoutNotify(_config.isInvertX);
        invertYToggle.SetIsOnWithoutNotify(_config.isInvertY);
    }

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

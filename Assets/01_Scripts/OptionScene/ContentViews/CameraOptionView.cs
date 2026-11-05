using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary> 
/// カメラ設定のUI表示を管理するクラス
/// </summary>
public class CameraOptionView : MonoBehaviour
{
    [Header ("カメラ設定のUI")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider fieldOfViewSlider;
    [SerializeField] private Toggle invertXToggle;
    [SerializeField] private Toggle invertYToggle;

    /// <summary> 設定データを画面上に反映させるメソッド </summary>
    /// <param name="_config"> カメラの設定データ </param>
    public void SetConfigData(CameraConfig _config)
    {
        sensitivitySlider.SetValueWithoutNotify(_config.sensitivity);
        fieldOfViewSlider.SetValueWithoutNotify(_config.fieldOfView);
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

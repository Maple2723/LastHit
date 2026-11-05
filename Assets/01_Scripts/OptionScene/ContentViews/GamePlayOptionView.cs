using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary>
/// ゲーム内設定のUI表示を管理するクラス
/// </summary>
public class GamePlayOptionView : MonoBehaviour
{
    [Header("ゲーム内設定のUI")]
    [SerializeField] private Toggle viewControlHintToggle;

    /// <summary> 設定データを画面上に反映させるメソッド </summary>
    /// <param name="_config"> 画面の設定データ </param>
    public void SetConfigData(GamePlayConfig _config)
    {
        viewControlHintToggle.SetIsOnWithoutNotify(_config.isViewControlHint);
    }

    /// <summary> ゲーム内設定の変更を一括で通知 </summary>
    public IObservable<GamePlayConfig> ConfigChanged => Observable.Merge(
        viewControlHintToggle.OnValueChangedAsObservable().Skip(1).AsUnitObservable())
        .Select(_ => new GamePlayConfig
        {
            isViewControlHint = viewControlHintToggle.isOn
        });
}

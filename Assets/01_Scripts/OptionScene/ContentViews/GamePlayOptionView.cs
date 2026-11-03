using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static LastHit.ConfigData;

/// <summary> GamePlayConfigの表示と変更通知を担当する </summary>
public class GamePlayOptionView : MonoBehaviour
{
    [SerializeField] private Toggle viewControlHintToggle;

    public void SetConfigData(GamePlayConfig _config)
    {
        viewControlHintToggle.SetIsOnWithoutNotify(_config.isViewControlHint);
    }

    public IObservable<GamePlayConfig> ConfigChanged => Observable.Merge(
        viewControlHintToggle.OnValueChangedAsObservable().Skip(1).AsUnitObservable())
        .Select(_ => new GamePlayConfig
        {
            isViewControlHint = viewControlHintToggle.isOn
        });
}

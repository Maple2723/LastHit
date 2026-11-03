using UnityEngine;
using static LastHit.ConfigData;

/// <summary>
/// オプション画面で編集中の設定データを保持するクラス
/// </summary>
public class OptionSceneModel : MonoBehaviour
{
    [SerializeField] private Config configData;

    public Config GetConfigData() => configData;

    public void SetConfigData(Config _configData) => configData = _configData;

    /// <summary> 現在の設定と編集中の設定に違いがあるか判定するメソッド </summary>
    public bool HasChanges(Config _currentConfig) => !configData.Equals(_currentConfig);
}

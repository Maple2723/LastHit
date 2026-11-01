using UnityEngine;
using static LastHit.ConfigData;


/// <summary>
/// 設定の初期値を定義するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "DefaultConfig", menuName = "LastHit/DefaultConfig")]
public class ConfigSO : ScriptableObject
{
    [SerializeField]
    private Config defaultConfig;   // 設定の初期値
}

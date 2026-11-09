using UnityEngine;
using UnityEngine.InputSystem;
using static LastHit.ConfigData;

public class ConfigDataController : MonoBehaviour
{
    [SerializeField] public ConfigDataModel model;

    public static ConfigDataController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        model.LoadConfigData();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Config GetConfigData() => model.GetConfigData();

    /// <summary> 編集した設定を確定してファイルに保存する </summary>
    public void ApplyConfig(Config config)
    {
        model.SetConfigData(config);
        model.SaveConfigData();
    }

    public Config GetDefaultConfigData() => model.defaultConfigData.defaultConfig;
}

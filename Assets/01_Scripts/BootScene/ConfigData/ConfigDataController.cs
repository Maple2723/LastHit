using UnityEngine;
using UnityEngine.InputSystem;
using static LastHit.ConfigData;

public class ConfigDataController : MonoBehaviour
{
    [SerializeField] private ConfigDataModel model;
    [SerializeField] private Key saveKey = Key.F5;

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

    private void Update()
    {
        if (Keyboard.current[saveKey].wasPressedThisFrame)
        {
            model.SaveConfigData();
        }
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

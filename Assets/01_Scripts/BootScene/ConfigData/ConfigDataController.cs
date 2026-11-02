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
        model.LoadConfigData();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (Keyboard.current[saveKey].wasPressedThisFrame)
        {
            model.SaveConfigData();
        }
    }

    public void ChangeConfigData(Config _fixData)
    {
        model.SetConfigData(_fixData);
    }

    public void ResetConfiData()
    {
        model.SetConfigData(model.defaultConfigData.defaultConfig);
    }
}

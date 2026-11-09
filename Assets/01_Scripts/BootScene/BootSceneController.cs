using UnityEngine;
using UnityEngine.InputSystem;
using static LastHit.SceneNames;

/// <summary>
/// BootSceneの処理を制御するクラス
/// </summary>
public class BootSceneController : MonoBehaviour
{
    [SerializeField] private SceneTransitionController sceneTransition;
    [SerializeField] private InputActionAsset inputActions;

    private void Start()
    {
        var configController = ConfigDataController.Instance;
        var config = configController.GetConfigData();

        SetKeyBind.ApplyAllKeyBindings(config.operate, inputActions);
        SetAudioVolume.ApplyAllVolumes(config.sound);
        
        sceneTransition.LoadScene(TITLE_SCENE, _isFadeEnabled: true);
    }
}

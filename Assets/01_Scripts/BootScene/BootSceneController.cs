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
        SetKeyBind.ApplyAllKeyBindings(ConfigDataController.Instance.GetConfigData().operate, inputActions);
        sceneTransition.LoadScene(TITLE_SCENE, _isFadeEnabled: true);
    }
}

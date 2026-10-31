using UnityEngine;
using static LastHit.SceneNames;

/// <summary>
/// BootSceneの処理を制御するクラス
/// </summary>
public class BootSceneController : MonoBehaviour
{
    [SerializeField] private SceneTransitionController sceneTransition;

    private void Start()
    {
        sceneTransition.LoadScene(TITLE_SCENE, _isFadeEnabled: true);
    }
}

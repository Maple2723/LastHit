using UnityEngine;
using UnityEngine.SceneManagement;
using static LastHit.SceneNames;

/// <summary>
/// ゲーム開始時にBootSceneを呼び出すクラス
/// </summary>
public class BootScene : MonoBehaviour
{
    /// <summary> ゲーム開始時にBootSceneを呼び出すメソッド </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadBootScene()
    {
        if (SceneManager.GetActiveScene().name == BOOT_SCENE)
            return;

        SceneManager.LoadScene(BOOT_SCENE);
    }
}

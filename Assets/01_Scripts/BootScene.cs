using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム開始時にBootSceneを呼び出すクラス
/// </summary>
public class BootScene : MonoBehaviour
{
    private const string BOOT_SCENE_NAME = "00_BootScene";      // 呼び出すシーンの名前

    /// <summary> ゲーム開始時にBootSceneを呼び出すメソッド </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadBootScene()
    {
        if (SceneManager.GetActiveScene().name == BOOT_SCENE_NAME)
            return;

        SceneManager.LoadScene(BOOT_SCENE_NAME);
    }
}

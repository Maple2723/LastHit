using UnityEngine;
using UnityEngine.SceneManagement;
using static LastHit.SceneNames;

/// <summary>
/// StageSelectSceneの処理を制御するクラス
/// </summary>
public class StageSelectSceneController : MonoBehaviour
{
    private void Start()
    {
        // デバッグ用
        SceneManager.LoadSceneAsync(INGAME_SCENE);
    }
}

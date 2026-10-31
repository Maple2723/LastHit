using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// BootSceneの処理を制御するクラス
/// </summary>
public class BootSceneController : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("01_TitleScene");
    }
}

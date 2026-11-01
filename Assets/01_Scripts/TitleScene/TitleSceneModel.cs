using UnityEngine;

public class TitleSceneModel : MonoBehaviour
{
    /// <summary> ゲームを終了するメソッド </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

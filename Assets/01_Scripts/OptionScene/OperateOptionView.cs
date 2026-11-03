using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static LastHit.ConfigData;

/// <summary> 操作設定のキー名の表示を担当する </summary>
public class OperateOptionView : MonoBehaviour
{
    [SerializeField] private TMP_Text moveForwardText;
    [SerializeField] private TMP_Text moveBackwardText;
    [SerializeField] private TMP_Text moveRightText;
    [SerializeField] private TMP_Text moveLeftText;
    [SerializeField] private TMP_Text jumpText;
    [SerializeField] private TMP_Text crouchText;
    [SerializeField] private TMP_Text dodgeText;

    public void SetConfigData(OperateConfig _config)
    {
        moveForwardText.text = GetKeyName(_config.moveForward);
        moveBackwardText.text = GetKeyName(_config.moveBackward);
        moveRightText.text = GetKeyName(_config.moveRight);
        moveLeftText.text = GetKeyName(_config.moveLeft);
        jumpText.text = GetKeyName(_config.jump);
        crouchText.text = GetKeyName(_config.crouch);
        dodgeText.text = GetKeyName(_config.dodge);
    }

    private string GetKeyName(Key _key)
    {
        if (_key == Key.None) return "未設定";
        var keyboard = Keyboard.current;
        return keyboard != null ? keyboard[_key].displayName : _key.ToString();
    }
}

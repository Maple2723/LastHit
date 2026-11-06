using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using static LastHit.ConfigData;

public static class SetKeyBind
{
    /// <summary>操作設定に含まれるすべてのキー割り当てを適用する。</summary>
    public static void AllKeyBindSeting(OperateConfig config, InputActionAsset actions = null)
    {
        KeyBindSeting(OperateGameAction.MoveForward, config.moveForward, actions);
        KeyBindSeting(OperateGameAction.MoveBackward, config.moveBackward, actions);
        KeyBindSeting(OperateGameAction.MoveLeft, config.moveLeft, actions);
        KeyBindSeting(OperateGameAction.MoveRight, config.moveRight, actions);
        KeyBindSeting(OperateGameAction.Jump, config.jump, actions);
        KeyBindSeting(OperateGameAction.Crouch, config.crouch, actions);
        KeyBindSeting(OperateGameAction.Dodge, config.dodge, actions);
    }

    /// <summary> 指定された操作にキー割り当てを設定するメソッド </summary>
    /// <param name="operation"> 設定する操作 </param>
    /// <param name="inputName"> 登録するキー </param>
    /// <param name="actions"> 適用先のアクションアセット </param>
    public static void KeyBindSeting(OperateGameAction operation, string inputName, InputActionAsset actions = null)
    {
        // アクション名を取得する
        var (actionName, direction) = operation switch
        {
            OperateGameAction.MoveForward => ("Move", "up"),
            OperateGameAction.MoveBackward => ("Move", "down"),
            OperateGameAction.MoveLeft => ("Move", "left"),
            OperateGameAction.MoveRight => ("Move", "right"),
            OperateGameAction.Jump => ("Jump", null),
            OperateGameAction.Crouch => ("Squat", null),
            OperateGameAction.Dodge => ("Dodge", null),
            _ => ((string)null, (string)null),
        };
        if (actionName == null || string.IsNullOrWhiteSpace(inputName))
            return;

        // InputSystemを取得、設定する値をInputSystem用に変換する
        if (actions == null)
        {
            var inputModule = EventSystem.current?.currentInputModule as InputSystemUIInputModule;
            actions = inputModule?.actionsAsset;
        }

        var action = actions?.FindActionMap("InGame")?.FindAction(actionName);
        if (action == null || action.bindings.Count == 0)
            return;
        var controlPath = GetControlPath(inputName);

        // アクションをInputSystemに反映する
        int bindingIndex = 0;
        if (direction != null)
            bindingIndex = action.GetBindingIndex(new InputBinding { name = direction });

        if (bindingIndex >= 0)
            action.ApplyBindingOverride(bindingIndex, controlPath);
    }


    /// <summary> キーの名前をInputSystemのパスに変換するメソッド </summary>
    /// <param name="inputName"> キーの名前 </param>
    /// <returns></returns>
    private static string GetControlPath(string inputName)
    {
        // マウスが設定されている場合は先に変換して返す
        var mousePath = inputName switch
        {
            "MouseLeft" => "<Mouse>/leftButton",
            "MouseRight" => "<Mouse>/rightButton",
            "MouseMiddle" => "<Mouse>/middleButton",
            "MouseBack" => "<Mouse>/backButton",
            "MouseForward" => "<Mouse>/forwardButton",
            _ => null,
        };
        if (mousePath != null)
            return mousePath;

        // 数字キーの場合指定の形に変換しておく
        if (inputName.StartsWith("Digit", StringComparison.Ordinal))
            inputName = inputName.Substring("Digit".Length);

        return "<Keyboard>/" + inputName;
    }
}

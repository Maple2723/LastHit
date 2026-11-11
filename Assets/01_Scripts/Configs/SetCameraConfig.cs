using Unity.Cinemachine;
using UnityEngine;
using static LastHit.ConfigData;

/// <summary>
/// カメラ設定を実際に反映するクラス
/// </summary>
public static class SetCameraConfig
{
    private const string X_AXIS_NAME = "Look X (Pan)";
    private const string Y_AXIS_NAME = "Look Y (Tilt)";
    private const float STANDARD_SENSITIVITY = 0.015f;

    /// <summary> カメラの設定を反映するメソッド </summary>
    /// <param name="_config">カメラ設定</param>
    public static void ApplyCameraConfig(CameraConfig _config)
    {
        var camera = Object.FindFirstObjectByType<CinemachineCamera>();
        if (camera == null)
        {
            Debug.LogWarning("ChinemaChineが存在しないため設定できません。");
            return;
        }

        // 視野角の設定
        camera.Lens.FieldOfView = Mathf.Clamp(_config.fieldOfView, 60, 120);

        var inputController = camera.GetComponent<CinemachineInputAxisController>();

        // 感度・操作反転の設定
        float gain = Mathf.Clamp(_config.sensitivity, 0f, 20f) * STANDARD_SENSITIVITY;

        var horizontal = inputController.GetController(X_AXIS_NAME);
        horizontal.Input.Gain = _config.isInvertX ? -gain : gain;

        var vertical = inputController.GetController(Y_AXIS_NAME);
        vertical.Input.Gain = _config.isInvertY ? gain : -gain;
    }
}

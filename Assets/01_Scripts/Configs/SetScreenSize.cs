using UnityEngine;
using static LastHit.ConfigData;

/// <summary>画面設定をゲームのウィンドウに反映するクラス</summary>
public static class SetScreenSize
{
    /// <summary>表示モードを切り替え、ウィンドウモードでは保存されたサイズを適用する。</summary>
    public static void ApplyScreenSize(DisplayConfig _config)
    {
        // フルスクリーン中にウィンドウサイズだけ変更した場合は表示を維持する。
        if (_config.isFullScreen)
        {
            if (Screen.fullScreenMode != FullScreenMode.FullScreenWindow)
            {
                var display = Screen.mainWindowDisplayInfo;
                Screen.SetResolution(display.width, display.height, FullScreenMode.FullScreenWindow);
            }

            return;
        }

        var (width, height) = _config.windowSize switch
        {
            WindowSize.Size1600x900 => (1600, 900),
            WindowSize.Size1920x1080 => (1920, 1080),
            _ => (1280, 720)
        };

        if (Screen.fullScreenMode != FullScreenMode.Windowed || Screen.width != width || Screen.height != height)
            Screen.SetResolution(width, height, FullScreenMode.Windowed);
    }
}

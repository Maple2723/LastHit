using System;
using UnityEngine;

namespace LastHit
{
    /// <summary>
    /// 設定のデータ構造を定義するクラス
    /// </summary>
    public static class ConfigData
    {
        [Serializable]
        public struct GamePlayConfig        // ゲーム設定の定義
        {
            [Header ("操作方法のヒントを表示")]
            public bool isViewControlHint;  // 操作方法のヒントを表示するか
        }

        [Serializable]
        public struct CameraConfig                      // カメラ設定の定義
        {
            [Header ("マウス感度")]
            [Range(0f, 20f)] public float sensitivity;  // マウス感度の設定

            [Header ("視野角")]
            [Range(60f, 120f)] public int fieldOfView;  // 視野角の設定
            [Header("カメラの反転設定X")]
            public bool isInvertX;                      // カメラの反転設定X
            [Header("カメラの反転設定Y")]
            public bool isInvertY;                      // カメラの反転設定Y
        }

        [Serializable]
        public struct SoundConfig                       // 音量設定の定義
        {
            [Header("全体音量設定")]
            [Range(0, 100)] public int masterVolume;    // 全体音量の設定
            [Header("BGM音量設定")]
            [Range(0, 100)] public int bgmVolume;       // BGM音量の設定
            [Header("SE音量設定")]
            [Range(0, 100)] public int seVolume;        // SE音量の設定
            [Header("効果音音量設定")]
            [Range(0, 100)] public int effectVolume;    // 効果音音量の設定
        }

        [Serializable]
        public enum WindowSize      // ウィンドウサイズの定義
        {
            Size1920x1080,
            Size1600x900,
            Size1280x720,
        }

        [Serializable]
        public struct DisplayConfig
        {
            [Header("フルスクリーン設定")]
            public bool isFullScreen;       // フルスクリーン設定
            [Header("ウィンドウサイズ設定")]
            public WindowSize windowSize;   // ウィンドウサイズ設定
        }

        [Serializable]
        public struct OperateConfig   // 操作キーの定義
        {
            public string test;     // 仮置き
        }

        [Serializable]
        public struct Config    // 設定のデータ構造をまとめた構造体
        {
            [Header("ゲームプレイ設定")]
            public GamePlayConfig gamePlay;
            [Header("カメラ設定")]
            public CameraConfig camera;
            [Header("音量設定")]
            public SoundConfig sound;
            [Header("ディスプレイ設定")]
            public DisplayConfig display;
            [Header("操作設定")]
            public OperateConfig operate;

        }
    }
}

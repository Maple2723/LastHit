using UnityEngine;
using UnityEngine.Audio;
using static LastHit.ConfigData;

/// <summary> 
/// 設定の音量をAudioMixerへ反映するクラス
/// </summary>
public static class SetAudioVolume
{
    private static AudioMixer audioMixer;

    /// <summary> マスター・BGM・SE・Effectの音量を一括反映する </summary>
    /// <param name="_config"> 音量設定 </param>
    public static void ApplyAllVolumes(SoundConfig _config)
    {
        SetMasterVolume(_config.masterVolume);
        SetBgmVolume(_config.bgmVolume);
        SetSeVolume(_config.seVolume);
        SetEffectVolume(_config.effectVolume);
    }

    public static void SetMasterVolume(int volume) => SetVolume("MasterVolume", volume);
    public static void SetBgmVolume(int volume) => SetVolume("BgmVolume", volume);
    public static void SetSeVolume(int volume) => SetVolume("SeVolume", volume);
    public static void SetEffectVolume(int volume) => SetVolume("EffectVolume", volume);

    private static void SetVolume(string parameter, int volume)
    {
        if (audioMixer == null)
            audioMixer = Resources.Load<AudioMixer>("Audio/LastHitAudio");

        float value = Mathf.Clamp(volume, 0, 100) / 100f;
        float decibels = value > 0f ? Mathf.Log10(value) * 20f : -80f;
        audioMixer.SetFloat(parameter, decibels);
    }
}

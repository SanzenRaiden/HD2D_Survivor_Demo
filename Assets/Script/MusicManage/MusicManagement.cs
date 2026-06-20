using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicManagement : MonoBehaviour
{
    public AudioClip titleBGM;
    public AudioClip battleBGM;
    public AudioClip UIbuttenAudio;
    public AudioClip gainobjectAudio;

    public AudioSource BGMAudioPlayer;
    public AudioSource SEAudioPlayer;

    //音量
    public float BGMvolume = 0.5f;
    public float SEvolume = 0.5f;

    /// <summary>
    /// BGM音量与其音量滑块同步
    /// </summary>
    /// <param name="bgm">音量滑块</param>
    public void BGMAudioVolumeSlider(Slider bgm)
    {
        BGMvolume = bgm.value;
        BGMAudioPlayer.volume = BGMvolume;
    }

    /// <summary>
    /// SE音量与其音量滑块同步
    /// </summary>
    /// <param name="se">音量滑块</param>
    public void SEAudioVolumeSlider(Slider se)
    {
        SEvolume = se.value;
        SEAudioPlayer.volume = SEvolume;
    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="BGM">播放的音乐</param>
    public void PlayBGM(AudioClip BGM)
    {
        BGMAudioPlayer.volume = BGMvolume;
        BGMAudioPlayer.clip = BGM;
        BGMAudioPlayer.Play();
    }

    /// <summary>
    /// 播放UI点击音效
    /// </summary>
    public void PlayUIAudio()
    {
        SEAudioPlayer.volume = SEvolume;
        SEAudioPlayer.clip = UIbuttenAudio;
        SEAudioPlayer.Play();
    }

    /// <summary>
    /// 播放获得物品的音效
    /// </summary>
    public void PlayGainObjectAudio()
    {
        SEAudioPlayer.volume = SEvolume;
        SEAudioPlayer.clip = gainobjectAudio;
        SEAudioPlayer.Play();
    }

    /// <summary>
    /// 播放花费音效
    /// </summary>
    /// <param name="SE"></param>
    public void PlayCustomAudio(AudioClip SE)
    {
        SEAudioPlayer.volume = SEvolume;
        SEAudioPlayer.clip = SE;
        SEAudioPlayer.Play();
    }
} 

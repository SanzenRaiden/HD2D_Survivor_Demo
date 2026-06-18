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

    public float BGMvolume = 0.5f;
    public float SEvolume = 0.5f;

    public void BGMAudioVolumeSlider(Slider bgm)
    {
        BGMvolume = bgm.value;
        BGMAudioPlayer.volume = BGMvolume;
    }

    public void SEAudioVolumeSlider(Slider se)
    {
        SEvolume = se.value;
        SEAudioPlayer.volume = SEvolume;
    }

    public void PlayBGM(AudioClip BGM)
    {
        BGMAudioPlayer.volume = BGMvolume;
        BGMAudioPlayer.clip = BGM;
        BGMAudioPlayer.Play();
    }

    public void PlayUIAudio()
    {
        SEAudioPlayer.volume = SEvolume;
        SEAudioPlayer.clip = UIbuttenAudio;
        SEAudioPlayer.Play();
    }

    public void PlayGainObjectAudio()
    {
        SEAudioPlayer.volume = SEvolume;
        SEAudioPlayer.clip = gainobjectAudio;
        SEAudioPlayer.Play();
    }

    public void PlayCustomAudio(AudioClip SE)
    {
        SEAudioPlayer.volume = SEvolume;
        SEAudioPlayer.clip = SE;
        SEAudioPlayer.Play();
    }
} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefAudioManager : MonoBehaviour
{
    public void PlaySE(AudioClip audioClip)
    {
        AudioManager.instance.PlaySE(audioClip);
    }

    public void PlayBGM(AudioClip audioClip)
    {
        AudioManager.instance.PlayBGM(audioClip);
    }

    public void StopBGM() { AudioManager.instance.StopBGM();}
}

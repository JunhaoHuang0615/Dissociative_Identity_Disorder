using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager
{
    private GameManager gameManager;

    public AudioSourceManager(GameManager gameManager){
        this.gameManager = gameManager;
    }

    //控制音量
    public void ChangeVolume(int value){
        gameManager.audioSource.volume = (float) value/100; //因为在调整时是以百分比调整的
    }

    //切换声音
    public void ChangeBGM(AudioClip clip){
        gameManager.audioSource.clip = clip;
        gameManager.audioSource.Play();
    }

    //mute
    public void Mute(){
        gameManager.audioSource.Stop();
    }
}

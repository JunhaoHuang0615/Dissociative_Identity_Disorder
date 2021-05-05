/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：声音系统，由GameMananger初始化，由它管理
 *
 * Date:2021/
 * Version:0.1v
 * Coder:Junhao Huang
 *
 *
 */

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

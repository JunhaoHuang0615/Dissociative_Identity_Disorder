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
    private Dictionary<string,AudioClip> adDic = new Dictionary<string, AudioClip>(); //缓存音乐
    private Dictionary<string,AudioClip> aefDic = new Dictionary<string, AudioClip>(); //缓存音效


    public AudioSourceManager(GameManager gameManager){
        this.gameManager = gameManager;
    }

    //控制音量
    public void ChangeBGMVolume(int value){
        gameManager.BGMAudioSource.volume = (float) value/100; //因为在调整时是以百分比调整的
    }

    //切换声音
    public void ChangeBGM(string clipName,bool isloop=true,bool cache=false){
        AudioClip changeToAu = null;
        if(adDic.ContainsKey(clipName)){
            changeToAu = adDic[clipName];
        }else{
            changeToAu = Resources.Load<AudioClip>("Sounds/"+clipName);
            if(cache)
            adDic.Add(clipName,changeToAu);
        }
        //确认加载的音乐是否是现在已经播放了的音乐
        if(gameManager.BGMAudioSource.clip ==null || gameManager.BGMAudioSource.clip.name != changeToAu.name){
            gameManager.BGMAudioSource.clip = changeToAu;
            gameManager.BGMAudioSource.Play();
            gameManager.BGMAudioSource.loop = isloop;
        }
    }
    public void PlaySoundEffect(string clipName,bool cache =true,bool isloop=false){
        AudioClip changeToAu = null;
        if(aefDic.ContainsKey(clipName)){
            changeToAu = aefDic[clipName];
        }else{
            changeToAu = Resources.Load<AudioClip>("Sounds/"+clipName);
            if(cache)
            aefDic.Add(clipName,changeToAu);

        }
        //确认加载的音乐是否是现在已经播放了的音乐
        if(gameManager.gameeffectAudioSource.clip ==null && gameManager.gameeffectAudioSource.clip.name != changeToAu.name){
            gameManager.gameeffectAudioSource.clip = changeToAu;
            gameManager.gameeffectAudioSource.Play();
            gameManager.gameeffectAudioSource.loop = isloop;
        }
    }

    //mute
    public void MuteBGM(){
        gameManager.BGMAudioSource.Stop();
    }
}

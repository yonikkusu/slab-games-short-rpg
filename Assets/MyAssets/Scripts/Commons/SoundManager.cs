using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// サウンドマネージャー
/// Prefab上でbgmClips, seClipsにBGM,SEを登録して使う
/// TODO: AudioClipを追加する度にPrefabをいじらなくていいようにしたい
/// </summary>
//--------------------------------------------------------------------------/
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] seClips;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="bgmName">BGM ID</param>
    //--------------------------------------------------------------------------/
    public void PlayBgm(Bgm bgmName)
    {
        bgmSource.PlayOneShot(bgmClips[(int)bgmName]);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// SEを再生する
    /// </summary>
    /// <param name="seName">SE名</param>
    //--------------------------------------------------------------------------/
    public void PlaySe(Se seName)
    {
        seSource.PlayOneShot(seClips[(int)seName]);
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// BGM名
/// </summary>
//--------------------------------------------------------------------------/
public enum Bgm
{
    Title,
}

//--------------------------------------------------------------------------/
/// <summary>
/// SE名
/// </summary>
//--------------------------------------------------------------------------/
public enum Se
{
    Tap,
}

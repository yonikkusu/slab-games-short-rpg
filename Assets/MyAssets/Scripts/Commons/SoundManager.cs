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
    [SerializeField] private AudioSource bgmSource = default;
    [SerializeField] private AudioSource seSource = default;
    [SerializeField] private AudioClip[] bgmClips = default;
    [SerializeField] private AudioClip[] seClips = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="bgmName">BGM ID</param>
    //--------------------------------------------------------------------------/
    public void PlayBgm(Bgm bgmName)
    {
        StopBgm();
        bgmSource.PlayOneShot(bgmClips[(int)bgmName]);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// BGMを停止する
    /// </summary>
    //--------------------------------------------------------------------------/
    public void StopBgm()
    {
        bgmSource.Stop();
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

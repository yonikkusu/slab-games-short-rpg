using UnityEngine;
using UniRx.Async;

/// <summary>
/// サウンドマネージャー
/// Prefab上でbgmClips, seClipsにBGM,SEを登録して使う
/// TODO: AudioClipを追加する度にPrefabをいじらなくていいようにしたい
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    ///<summary>デフォルトBGM音量</summary>
    private const float DefaultBgmVolume = 0.5f;

    [SerializeField] private AudioSource bgmSource = default;
    [SerializeField] private AudioSource seSource = default;
    [SerializeField] private AudioClip[] bgmClips = default;
    [SerializeField] private AudioClip[] seClips = default;

    private Bgm currentBgmName;
    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="bgmName">BGM ID</param>
    /// <param name="volume">音量</param>
    public void PlayBgm(Bgm bgmName, float volume = DefaultBgmVolume)
    {
        // 鳴っているBGMが同じなら何もしない
        if(bgmSource.isPlaying && bgmName == currentBgmName) return;

        StopBgm();
        if(bgmName != Bgm.None) {
            bgmSource.volume = volume;
            bgmSource.PlayOneShot(bgmClips[(int)bgmName]);
        }
        currentBgmName = bgmName;
    }

    /// <summary>
    /// BGMを停止する
    /// </summary>
    public void StopBgm()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// BGMをフェードアウトする
    /// </summary>
    /// <param name="frame">フェードさせるフレーム数</param>
    public async UniTask FadeOutBgmAsync(int frame = 10)
    {
        // BGMが鳴ってないなら何もしない
        if(!bgmSource.isPlaying || bgmSource.volume <= 0f) return;

        var delta = bgmSource.volume / frame;
        for(var i = 0; i < frame; i++) {
            bgmSource.volume -= delta;
            await UniTask.DelayFrame(1);
        }
        bgmSource.volume = 0f;
        StopBgm();
    }

    /// <summary>
    /// BGMをフェードインする
    /// </summary>
    /// <param name="frame">フェードさせるフレーム数</param>
    public async UniTask FadeInBgmAsync(int frame = 10)
    {
        // BGMが鳴ってるなら何もしない
        if(bgmSource.isPlaying || bgmSource.volume > 0f) return;

        PlayBgm(currentBgmName, 0f);
        var delta = DefaultBgmVolume / frame;
        for(var i = 0; i < frame; i++) {
            bgmSource.volume += delta;
            await UniTask.DelayFrame(1);
        }
        bgmSource.volume = DefaultBgmVolume;
    }

    /// <summary>
    /// SEを再生する
    /// </summary>
    /// <param name="seName">SE名</param>
    public void PlaySe(Se seName)
    {
        seSource.PlayOneShot(seClips[(int)seName]);
    }
}

/// <summary>
/// BGM名
/// </summary>
public enum Bgm
{
    None = -1,
    Title,
    House,
}

/// <summary>
/// SE名
/// </summary>
public enum Se
{
    Tap,
}

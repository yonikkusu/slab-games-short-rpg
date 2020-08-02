using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

//--------------------------------------------------------------------------/
/// <summary>
/// 画面マネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class DisplayManager : SingletonMonoBehaviour<DisplayManager>
{
    [SerializeField] private Image fadeImage = default;
    [SerializeField] private GameObject menuObject = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 画面をフェードアウトする
    /// </summary>
    /// <param name="frame">フェードさせるフレーム数</param>
    //--------------------------------------------------------------------------/
    public async UniTask FadeOutDisplayAsync(int frame = 10)
    {
        var originalColor = fadeImage.color;
        var delta = 1f / frame;
        for(var i = 0; i < frame; i++) {
            var newColor = fadeImage.color;
            newColor.a += delta;
            fadeImage.color = newColor;
            await UniTask.DelayFrame(1);
        }
        fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 画面をフェードインする
    /// </summary>
    /// <param name="frame">フェードさせるフレーム数</param>
    //--------------------------------------------------------------------------/
    public async UniTask FadeInDisplayAsync(int frame = 10)
    {
        var originalColor = fadeImage.color;
        var delta = 1f / frame;
        for(var i = 0; i < frame; i++) {
            var newColor = fadeImage.color;
            newColor.a -= delta;
            fadeImage.color = newColor;
            await UniTask.DelayFrame(1);
        }
        fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// メニューの表示/非表示を切り替える
    /// </summary>
    /// <param name="value">表示するならtrue、非表示ならfalse</param>
    //--------------------------------------------------------------------------/
    public void SetActiveMenu(bool value) => menuObject.SetActive(value);
}

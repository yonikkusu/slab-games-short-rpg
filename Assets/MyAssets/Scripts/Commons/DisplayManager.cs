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
    [SerializeField] private Menu menu = default;
    [SerializeField] private ItemPanel itemPanel = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    protected override void Awake()
    {
        base.Awake();

        // 各オブジェクトの表示状態を初期化
        fadeImage.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(false);
    }

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
    /// インゲームで使う表示物を表示する
    /// </summary>
    //--------------------------------------------------------------------------/
    public void ShowInGameDisplayObjects()
    {
        menu.Initialize();
        itemPanel.Initialize();
        menu.gameObject.SetActive(true);
        itemPanel.gameObject.SetActive(true);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// インゲームで使う表示物を非表示にする
    /// </summary>
    //--------------------------------------------------------------------------/
    public void HideInGameDisplayObjects()
    {
        menu.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(false);
    }
}

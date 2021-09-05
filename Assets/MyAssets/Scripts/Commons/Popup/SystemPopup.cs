using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;

/// <summary>
/// システムポップアップ
/// </summary>
public class SystemPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupObject = default;
    [SerializeField] private Button closeBackgroundButton = default;
    [SerializeField] private Text messageText = default;
    [SerializeField] private SystemPopupInput systemPopupInput = default;

    private UniTaskCompletionSource source;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
        systemPopupInput.OnCloseKeyDown.Subscribe(_ => source?.TrySetResult()).AddTo(this);
        closeBackgroundButton.OnClickAsObservable()
            .Subscribe(_ => source?.TrySetResult())
            .AddTo(this);
    }

    /// <summary>
    /// ポップアップを表示する
    /// </summary>
    /// <param name="message">表示するメッセージ</param>
    /// <returns>UniTask</returns>
    public async UniTask ShowAsync(string message)
    {
        initialize(message);
        show();
        await waitHideAsync();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="message">表示するメッセージ</param>
    private void initialize(string message)
    {
        InputService.Instance.ChangeMode(InputMode.SystemPopup);
        messageText.text = message;
    }

    /// <summary>
    /// ポップアップを表示する
    /// </summary>
    private void show() => popupObject.gameObject.SetActive(true);

    /// <summary>
    /// ポップアップを非表示にする
    /// </summary>
    private void hide() => popupObject.gameObject.SetActive(false);

    /// <summary>
    /// 閉じられるのを待つ
    /// </summary>
    /// <returns>UniTask</returns>
    private async UniTask waitHideAsync()
    {
        source = new UniTaskCompletionSource();
        await source.Task;
        source = null;
        InputService.Instance.ChangeMode(InputMode.Normal);
        hide();
    }
}

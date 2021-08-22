using System.Threading;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// システムポップアップ
/// </summary>
public class SystemPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupObject = default;
    [SerializeField] private Button closeBackgroundButton = default;
    [SerializeField] private Text messageText = default;

    private UniTaskCompletionSource source;
    private CancellationTokenSource cancellationTokenSource;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
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
        cancellationTokenSource = new CancellationTokenSource();
        waitKeyInputAsync(cancellationTokenSource.Token).Forget();
        await waitHideAsync();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="message">表示するメッセージ</param>
    private void initialize(string message) => messageText.text = message;

    /// <summary>
    /// ポップアップを表示する
    /// </summary>
    private void show() => popupObject.gameObject.SetActive(true);

    /// <summary>
    /// ポップアップを非表示にする
    /// </summary>
    private void hide() => popupObject.gameObject.SetActive(false);

    /// <summary>
    /// キー入力を待つ
    /// </summary>
    /// <param name="cancellationToken">キャンセル通知</param>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid waitKeyInputAsync(CancellationToken cancellationToken)
    {
        // NOTE: 1秒間は入力をブロック
        await UniTask.Delay(1000, cancellationToken: cancellationToken);
        await UniTask.WaitUntil(() => Input.GetKey(KeyCode.Return), cancellationToken: cancellationToken);
        source?.TrySetResult();
    }

    /// <summary>
    /// 閉じられるのを待つ
    /// </summary>
    /// <returns>UniTask</returns>
    private async UniTask waitHideAsync()
    {
        source = new UniTaskCompletionSource();
        await source.Task;
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        source = null;
        hide();
    }
}

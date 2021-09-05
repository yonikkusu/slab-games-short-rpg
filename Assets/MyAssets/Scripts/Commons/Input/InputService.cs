using UniRx.Async;

/// <summary>
/// 入力 Serviceクラス
/// </summary>
public class InputService : SingletonMonoBehaviour<InputService>
{
    /// <summary>現在のモード</summary>
    public InputMode CurrentMode { get; private set; }

    /// <summary>
    /// 入力モードを変更する
    /// </summary>
    /// <param name="mode">入力モード</param>
    public void ChangeMode(InputMode mode)
    {
        changeModeAsync(mode).Forget();
    }

    /// <summary>
    /// 入力モードを変更する
    /// </summary>
    /// <param name="mode">入力モード</param>
    /// <returns>UniTask</returns>
    private async UniTask changeModeAsync(InputMode mode)
    {
        await UniTask.DelayFrame(1);
        CurrentMode = mode;
    }
}

/// <summary>
/// 入力モード
/// </summary>
public enum InputMode
{
    None = 0,
    Normal,
    SystemPopup,
}

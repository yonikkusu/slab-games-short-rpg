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
    public void ChangeMode(InputMode mode) => CurrentMode = mode;
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

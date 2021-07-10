using UniRx.Async;

/// <summary>
/// 自動イベント
/// 継承先でautoEventIdとexecuteAsync()をoverrideして処理内容を記述する
/// </summary>
public abstract class AutoEvent : MapEvent
{
    protected abstract AutoEventId autoEventId { get; }

    /// <summary>
    /// 自動イベントが発動するかチェックする
    /// </summary>
    public async UniTask CheckAutoEventAsync()
    {
        if(PlayerData.Instance.FlagManager.HasAutoEventSwitch(autoEventId)) return;
        await executeAsync();
        PlayerData.Instance.FlagManager.SetAutoEventSwitchOn(autoEventId);
    }

    /// <summary>
    /// 実行する
    /// </summary>
    /// <returns>UniTask</returns>
    protected abstract UniTask executeAsync();
}

/// <summary>
/// 自動イベントID
/// </summary>
public enum AutoEventId
{
    None = 0,
    Opening = 1,
}

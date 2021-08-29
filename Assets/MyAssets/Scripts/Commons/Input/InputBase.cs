using UnityEngine;

/// <summary>
/// 入力処理 ベースクラス
/// </summary>
public abstract class InputBase : MonoBehaviour
{
    /// <summary>使用する入力モード</summary>
    protected abstract InputMode usedMode { get; }

    /// <summary>
    /// 更新する
    /// </summary>
    void Update()
    {
        if(InputService.Instance.CurrentMode != usedMode) return;
        checkInput();
    }

    /// <summary>
    /// 入力をチェックする
    /// </summary>
    protected abstract void checkInput();
}

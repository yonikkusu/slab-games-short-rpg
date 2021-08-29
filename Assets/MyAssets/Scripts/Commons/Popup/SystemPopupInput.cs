using System;
using UnityEngine;
using UniRx;

/// <summary>
/// システムポップアップ 入力クラス
/// </summary>
public class SystemPopupInput : InputBase
{
    /// <summary>使用する入力モード</summary>
    protected override InputMode usedMode => InputMode.SystemPopup;

    /// <summary>閉じるキー入力通知</summary>
    public IObservable<Unit> OnCloseKeyDown => onCloseKeyDownSubject;
    private Subject<Unit> onCloseKeyDownSubject = new Subject<Unit>();

    /// <summary>
    /// 入力をチェックする
    /// </summary>
    protected override void checkInput()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            onCloseKeyDownSubject.OnNext(Unit.Default);
        }
    }
}

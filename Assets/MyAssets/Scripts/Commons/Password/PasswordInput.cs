using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class PasswordInput : InputBase
{
    /// <summary>使用する入力モード</summary>
    protected override InputMode usedMode => InputMode.InputPassword;

    /// <summary>リターンキー入力通知</summary>
    public IObservable<Unit> OnReturnKeyDown => onReturnKeyDownSubject;
    private Subject<Unit> onReturnKeyDownSubject = new Subject<Unit>();

    /// <summary>
    /// 入力をチェックする
    /// </summary>
    protected override void checkInput()
    {
        if( Input.GetKeyDown(KeyCode.Return) )
        {
            onReturnKeyDownSubject.OnNext(Unit.Default);
        }
    }
}

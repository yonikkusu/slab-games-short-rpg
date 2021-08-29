using System;
using UnityEngine;
using UniRx;

/// <summary>
/// アイテムパネル 入力クラス
/// </summary>
public sealed class ItemPanelInput : InputBase
{
    /// <summary>使用する入力モード</summary>
    protected override InputMode usedMode => InputMode.Normal;

    /// <summary>マウスホイール変更通知(ホイール移動量(正:上、負:下)を通知)</summary>
    public IObservable<float> OnWheelValueChanged => onWheelValueChangedSubject;
    private Subject<float> onWheelValueChangedSubject = new Subject<float>();

    /// <summary>
    /// 入力をチェックする
    /// </summary>
    protected override void checkInput()
    {
        var wheelValue = Input.GetAxis("Mouse ScrollWheel");
        onWheelValueChangedSubject.OnNext(wheelValue);
    }
}

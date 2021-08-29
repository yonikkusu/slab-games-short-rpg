using System;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤー 入力クラス
/// </summary>
public class PlayerInput : InputBase
{
    /// <summary>使用する入力モード</summary>
    protected override InputMode usedMode => InputMode.Normal;

    /// <summary>プレイヤー移動通知(移動方向ベクターを通知)</summary>
    public IObservable<Vector2> OnMovePlayer => onMovePlayerSubject;
    private Subject<Vector2> onMovePlayerSubject = new Subject<Vector2>();
    /// <summary>調べるキー入力通知</summary>
    public IObservable<Unit> OnInspectKeyDown => onInspectKeyDownSubject;
    private Subject<Unit> onInspectKeyDownSubject = new Subject<Unit>();
    /// <summary>アイテム使用キー入力通知</summary>
    public IObservable<Unit> OnUseItemKeyDown => onUseItemKeyDownSubject;
    private Subject<Unit> onUseItemKeyDownSubject = new Subject<Unit>();

    /// <summary>
    /// 入力をチェックする
    /// </summary>
    protected override void checkInput()
    {
        // 移動チェック
        var playerDirectionVector = getDirectionVector();
        if(playerDirectionVector != Vector2.zero) {
            onMovePlayerSubject.OnNext(playerDirectionVector);
        }
        // 調べるイベントチェック
        if(Input.GetKeyDown(KeyCode.Return)) {
            onInspectKeyDownSubject.OnNext(Unit.Default);
        }
        // アイテム使用イベントチェック
        if(Input.GetKeyDown(KeyCode.I)) {
            onUseItemKeyDownSubject.OnNext(Unit.Default);
        }
    }

    /// <summary>
    /// 移動方向ベクターを取得する(斜めは無効)
    /// </summary>
    /// <returns>移動方向ベクター</returns>
    private Vector2 getDirectionVector()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }
}

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

    /// <summary>プレイヤー移動通知(移動方向を通知)</summary>
    public IObservable<PLAYER_DIRECTION> OnMovePlayer => onMovePlayerSubject;
    private Subject<PLAYER_DIRECTION> onMovePlayerSubject = new Subject<PLAYER_DIRECTION>();
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
        var playerDirection = getDirection();
        if(playerDirection != PLAYER_DIRECTION.NONE) {
            onMovePlayerSubject.OnNext(playerDirection);
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
    /// 移動方向を取得する(斜めは無効)
    /// </summary>
    /// <returns>移動方向</returns>
    private PLAYER_DIRECTION getDirection()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        var directionVector = new Vector2(x, y).normalized;
        if(directionVector == Vector2.left) return PLAYER_DIRECTION.LEFT;
        if(directionVector == Vector2.right) return PLAYER_DIRECTION.RIGHT;
        if(directionVector == Vector2.up) return PLAYER_DIRECTION.UP;
        if(directionVector == Vector2.down) return PLAYER_DIRECTION.DOWN;
        return PLAYER_DIRECTION.NONE;
    }
}

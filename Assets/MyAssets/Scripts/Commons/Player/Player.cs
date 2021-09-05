using System;
using UnityEngine;
using UniRx;
using UniRx.Async;

/// <summary>
/// プレイヤー管理クラス
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary> 移動アニメーションのフレーム数(2の累乗にする必要がある)</summary>
    private const int MOVE_ANIMATION_FRAME = 4;

    [SerializeField] private Rigidbody2D rigidBody = default;
    [SerializeField] private Animator anim = default;
    [SerializeField] private PlayerInput playerInput = default;

    /// <summary>プレイヤーが調べるを行った通知(調べた座標を渡す)</summary>
    public IObservable<Vector2> OnInspect => onInspectSubject;
    private Subject<Vector2> onInspectSubject = new Subject<Vector2>();
    /// <summary>プレイヤーがアイテムを使った通知(使った座標とアイテムIDを渡す)</summary>
    public IObservable<(Vector2 checkedPosition, ItemID usedItemId)> OnUseItem => onUseItemSubject;
    private Subject<(Vector2 checkedPosition, ItemID usedItemId)> onUseItemSubject = new Subject<(Vector2 checkedPosition, ItemID usedItemId)>();
    /// <summary>プレイヤー移動後通知(移動後のプレイヤー情報を渡す)</summary>
    public IObservable<PlayerModel> OnMoved => onMovedSubject;
    private Subject<PlayerModel> onMovedSubject = new Subject<PlayerModel>();

    // プレイヤー情報
    private PlayerModel playerModel;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="startPosition">初期位置</param>
    /// <param name="startDirection">初期方向</param>
    public void Initialize(Vector3 startPosition, PLAYER_DIRECTION startDirection)
    {
        playerModel = new PlayerModel(startPosition, startDirection);
        transform.position = MapSceneBase.OffScreenPos + startPosition;
        playerInput.OnMovePlayer.Subscribe(direction => checkMoveAsync(direction).Forget()).AddTo(this);
        playerInput.OnInspectKeyDown.Subscribe(_ => onInspectSubject.OnNext(getOneSquareAheadPosition())).AddTo(this);
        playerInput.OnUseItemKeyDown
            .Subscribe(_ => onUseItemSubject.OnNext((getOneSquareAheadPosition(), ItemPanel.Instance.SelectedItem?.ID ?? ItemID.None)))
            .AddTo(this);
        move(startDirection, startPosition);
    }

    /// <summary>
    /// 移動可能なら移動する
    /// </summary>
    /// <param name="direction">プレイヤーの移動方向</param>
    private async UniTask checkMoveAsync(PLAYER_DIRECTION direction)
    {
        // 移動中またはシーン移動中なら何もしない
        if(playerModel.IsMoving || SceneManagerExtension.IsMoving) return;

        // 移動処理
        playerModel.UpdateIsMoving(isMoving: true);
        var directionVector = direction.ToVector2();
        var movedPosition = rigidBody.position + directionVector;
        var prevPosition = rigidBody.position;
        while(true) {
            move(direction, rigidBody.position + (directionVector / MOVE_ANIMATION_FRAME));
            await UniTask.DelayFrame(1);
            if(isFinishedMove()) break;
            prevPosition = rigidBody.position;
        }

        // 床イベントチェック
        onMovedSubject.OnNext(playerModel);

        // 移動中フラグを下ろす
        playerModel.UpdateIsMoving(isMoving: false);

        // 移動終了したか
        bool isFinishedMove()
        {
            var currentPosition = rigidBody.position;
            // 目的地についたらtrue
            if(float.Equals(prevPosition.x, currentPosition.x)
            && float.Equals(prevPosition.y, currentPosition.y)) return true;
            // 目的地よりも過ぎていた場合もtrue
            if(direction == PLAYER_DIRECTION.LEFT) return currentPosition.x <= movedPosition.x;
            if(direction == PLAYER_DIRECTION.RIGHT) return currentPosition.x >= movedPosition.x;
            if(direction == PLAYER_DIRECTION.UP) return currentPosition.y >= movedPosition.y;
            if(direction == PLAYER_DIRECTION.DOWN) return currentPosition.y <= movedPosition.y;
            return false;
        }
    }

    /// <summary>
    /// プレイヤーが向いてる方向の1マス先の位置を取得する
    /// </summary>
    private Vector2 getOneSquareAheadPosition()
    {
        switch(playerModel.CurrentDirection) {
            case PLAYER_DIRECTION.LEFT: return new Vector2(transform.position.x - 1, transform.position.y);
            case PLAYER_DIRECTION.RIGHT: return new Vector2(transform.position.x + 1, transform.position.y);
            case PLAYER_DIRECTION.UP: return new Vector2(transform.position.x, transform.position.y + 1);
            case PLAYER_DIRECTION.DOWN: return new Vector2(transform.position.x, transform.position.y - 1);
            default:
                DebugLogger.Log("存在しない方向を調べようとしています。");
                return default;
        }
    }

    /// <summary>
    /// 移動する
    /// </summary>
    /// <param name="direction">移動方向</param>
    /// <param name="movePosition">移動座標</param>
    private void move(PLAYER_DIRECTION direction, Vector2 movePosition)
    {
        moveAnimation(direction);
        rigidBody.MovePosition(movePosition);
        playerModel.UpdatePosition(movePosition);
    }

    /// <summary>
    /// 移動アニメーションを行う
    /// </summary>
    /// <param name="direction">移動方向</param>
    private void moveAnimation(PLAYER_DIRECTION direction)
    {
        playerModel.UpdateDirection(direction);

        switch(direction) {
            case PLAYER_DIRECTION.LEFT:
                anim.SetTrigger("left");
                break;
            case PLAYER_DIRECTION.RIGHT:
                anim.SetTrigger("right");
                break;
            case PLAYER_DIRECTION.UP:
                anim.SetTrigger("up");
                break;
            case PLAYER_DIRECTION.DOWN:
                anim.SetTrigger("down");
                break;
            default:
                break;
        }
    }
}

using UnityEngine;
using UniRx.Async;

//--------------------------------------------------------------------------/
/// <summary>
/// プレイヤー管理クラス
/// </summary>
//--------------------------------------------------------------------------/
public class Player : MonoBehaviour
{
    /// <summary> 移動アニメーションのフレーム数(2の累乗にする必要がある)</summary>
    private const int MOVE_ANIMATION_FRAME = 4;

    /// <summary> プレイヤーの向き</summary>
    public enum DIRECTION {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
    };

    [SerializeField] private Rigidbody2D rigidBody = default;
    [SerializeField] private Animator anim = default;
    [SerializeField] private ItemPanel itemPanel = default;

    /// <summary>現在のシーン</summary>
    private MapSceneBase mapScene;

    // プレイヤー情報
    private PlayerModel playerModel;

    // 移動処理中か
    private bool isMoving;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Awake()
    {
        mapScene = FindObjectOfType<MapSceneBase>();
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アップデート処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Update()
    {
        // キャラクターの移動チェック
        checkMoveAsync().Forget();

        // 調べるイベントチェック
        if(Input.GetKeyDown(KeyCode.Return)) {
            switch(playerModel.CurrentDirection) {
                case DIRECTION.LEFT:
                    mapScene.CheckInspectEvents(new Vector2(transform.position.x - 1, transform.position.y));
                    break;
                case DIRECTION.RIGHT:
                    mapScene.CheckInspectEvents(new Vector2(transform.position.x + 1, transform.position.y));
                    break;
                case DIRECTION.UP:
                    mapScene.CheckInspectEvents(new Vector2(transform.position.x, transform.position.y + 1));
                    break;
                case DIRECTION.DOWN:
                    mapScene.CheckInspectEvents(new Vector2(transform.position.x, transform.position.y - 1));
                    break;
                default:
                    break;
            }
        }

        // アイテム使用キーが押された場合
        if(Input.GetKeyDown(KeyCode.I)) {
            PlayerData.Instance.ItemManager.UseItem(itemPanel.CurrentItemIndex);
            itemPanel.UpdateItemList();
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="startPosition">初期位置</param>
    /// <param name="startDirection">初期方向</param>
    //--------------------------------------------------------------------------/
    public void Initialize(Vector3 startPosition, DIRECTION startDirection)
    {
        playerModel = new PlayerModel(startPosition, startDirection);
        transform.position = MapSceneBase.OffScreenPos + startPosition;
        move(startDirection, startPosition);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アイテムを所持品に追加する
    /// </summary>
    /// <param name="itemId">追加するアイテムID</param>
    //--------------------------------------------------------------------------/
    public void AddItem(int itemId)
    {
        PlayerData.Instance.AddItem((ItemID)itemId);
        itemPanel.UpdateItemList();
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 移動可能なら移動する
    /// </summary>
    /// <param name="direction">方向Vector</param>
    //--------------------------------------------------------------------------/
    private async UniTask checkMoveAsync()
    {
        // 移動中またはシーン移動中なら何もしない
        if(isMoving || SceneManagerExtension.IsMoving) return;

        // 入力データから移動方向を求める
        var directionVector = getDirectionVector();
        var direction = getDirection(directionVector);

        // 移動キーが押されてないなら何もしない
        if(direction == DIRECTION.NONE) return;

        // 移動処理
        isMoving = true;
        var movedPosition = rigidBody.position + directionVector;
        var prevPosition = rigidBody.position;
        var i = 0;
        while(true) {
            move(direction, rigidBody.position + (directionVector / MOVE_ANIMATION_FRAME));
            await UniTask.DelayFrame(1);
            if(isFinishedMove()) break;
            prevPosition = rigidBody.position;
            i++;
        }

        // 床イベントチェック
        mapScene.CheckFloorEvents(playerModel);

        // 移動中フラグを下ろす
        isMoving = false;

        // 移動終了したか
        bool isFinishedMove()
        {
            var currentPosition = rigidBody.position;
            // 目的地についたらtrue
            if(float.Equals(prevPosition.x, currentPosition.x)
            && float.Equals(prevPosition.y, currentPosition.y)) return true;
            // 目的地よりも過ぎていた場合もtrue
            if(direction == DIRECTION.LEFT) return currentPosition.x <= movedPosition.x;
            if(direction == DIRECTION.RIGHT) return currentPosition.x >= movedPosition.x;
            if(direction == DIRECTION.UP) return currentPosition.y >= movedPosition.y;
            if(direction == DIRECTION.DOWN) return currentPosition.y <= movedPosition.y;
            return false;
        }
    }


    //--------------------------------------------------------------------------/
    /// <summary>
    /// 入力データから方向ベクトルを取得する
    /// </summary>
    /// <returns>方向Vector</returns>
    //--------------------------------------------------------------------------/
    private Vector2 getDirectionVector()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 移動方向を取得する(斜めは無効)
    /// </summary>
    /// <<param name="directionVector">移動ベクトル</param>
    /// <returns>移動方向</returns>
    //--------------------------------------------------------------------------/
    private DIRECTION getDirection(Vector2 directionVector)
    {
        if(directionVector == Vector2.left) return DIRECTION.LEFT;
        if(directionVector == Vector2.right) return DIRECTION.RIGHT;
        if(directionVector == Vector2.up) return DIRECTION.UP;
        if(directionVector == Vector2.down) return DIRECTION.DOWN;

        return DIRECTION.NONE;
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 移動する
    /// </summary>
    /// <param name="direction">移動方向</param>
    /// <param name="movePosition">移動座標</param>
    //--------------------------------------------------------------------------/
    private void move(DIRECTION direction, Vector2 movePosition)
    {
        moveAnimation(direction);
        rigidBody.MovePosition(movePosition);
        playerModel.UpdatePosition(movePosition);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 移動アニメーションを行う
    /// </summary>
    /// <param name="direction">移動方向</param>
    //--------------------------------------------------------------------------/
    private void moveAnimation(DIRECTION direction)
    {
        playerModel.UpdateDirection(direction);

        switch(direction) {
            case DIRECTION.LEFT:
                anim.SetTrigger("left");
                break;
            case DIRECTION.RIGHT:
                anim.SetTrigger("right");
                break;
            case DIRECTION.UP:
                anim.SetTrigger("up");
                break;
            case DIRECTION.DOWN:
                anim.SetTrigger("down");
                break;
            default:
                break;
        }
    }
}

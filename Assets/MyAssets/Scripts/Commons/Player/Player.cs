using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// プレイヤー管理クラス
/// </summary>
//--------------------------------------------------------------------------/
public class Player : MonoBehaviour
{
    /// <summary>アニメーション速度</summary>
    private const float PLAYER_SPEED = 10f;

    /// <summary> プレイヤーの向き</summary>
    public enum DIRECTION {
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
    /// <summary>現在の向き</summary>
    private DIRECTION currentDirection;


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
        // プレイヤーの移動
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        var direction = new Vector2(x, y).normalized;
        rigidBody.velocity = direction * PLAYER_SPEED;

        // 調べるイベントチェック
        if(Input.GetKeyDown(KeyCode.Return)) {
            switch(currentDirection) {
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

        // 移動中なら下記の処理を行う
        if(direction != Vector2.zero) {
            // 歩行アニメーション
            if(direction.x < 0) {
                moveAnimation(DIRECTION.LEFT);
            } else if(direction.x > 0) {
                moveAnimation(DIRECTION.RIGHT);
            } else if(direction.y > 0) {
                moveAnimation(DIRECTION.UP);
            } else if(direction.y < 0) {
                moveAnimation(DIRECTION.DOWN);
            }

            // 床イベントチェック
            mapScene.CheckFloorEvents(transform.position);
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
        transform.position = MapSceneBase.OffScreenPos + startPosition;
        moveAnimation(startDirection);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// プレイヤーの開始位置と向きをセットする
    /// </summary>
    /// <param name="position">開始位置</param>
    /// <param name="direction">向き</param>
    //--------------------------------------------------------------------------/
    public static void SetStartPotisionAndDirection(Vector2 position, DIRECTION direction)
    {
        startPosition = new Vector3(position.x, position.y, 0f);
        startDirection = direction;
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
    /// 移動アニメーションを行う
    /// </summary>
    /// <param name="direction">移動する向き</param>
    //--------------------------------------------------------------------------/
    private void moveAnimation(DIRECTION direction)
    {
        switch(direction) {
            case DIRECTION.LEFT:
                anim.SetTrigger("left");
                currentDirection = DIRECTION.LEFT;
                break;
            case DIRECTION.RIGHT:
                anim.SetTrigger("right");
                currentDirection = DIRECTION.RIGHT;
                break;
            case DIRECTION.UP:
                anim.SetTrigger("up");
                currentDirection = DIRECTION.UP;
                break;
            case DIRECTION.DOWN:
                anim.SetTrigger("down");
                currentDirection = DIRECTION.DOWN;
                break;
            default:
                break;
        }
    }
}

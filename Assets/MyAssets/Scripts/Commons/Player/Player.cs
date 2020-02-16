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
    public enum DIRECTOIN {
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
    private DIRECTOIN currentDirection;

    private static Vector3 startPosition;
    private static DIRECTOIN startDirection;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        mapScene = FindObjectOfType<MapSceneBase>();
        transform.position = startPosition;
        moveAnimation(startDirection);
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
                case DIRECTOIN.LEFT:
                    mapScene.CheckInspectEvents(new Vector2(transform.position.x - 1, transform.position.y));
                    break;
                case DIRECTOIN.RIGHT:
                    mapScene.CheckInspectEvents(new Vector2(transform.position.x + 1, transform.position.y));
                    break;
                case DIRECTOIN.UP:
                    mapScene.CheckInspectEvents(new Vector2(transform.position.x, transform.position.y + 1));
                    break;
                case DIRECTOIN.DOWN:
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
                moveAnimation(DIRECTOIN.LEFT);
            } else if(direction.x > 0) {
                moveAnimation(DIRECTOIN.RIGHT);
            } else if(direction.y > 0) {
                moveAnimation(DIRECTOIN.UP);
            } else if(direction.y < 0) {
                moveAnimation(DIRECTOIN.DOWN);
            }

            // 床イベントチェック
            mapScene.CheckFloorEvents(transform.position);
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// プレイヤーの開始位置と向きをセットする
    /// </summary>
    /// <param name="position">開始位置</param>
    /// <param name="direction">向き</param>
    //--------------------------------------------------------------------------/
    public static void SetStartPotisionAndDirection(Vector2 position, DIRECTOIN direction)
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
    private void moveAnimation(DIRECTOIN direction)
    {
        switch(direction) {
            case DIRECTOIN.LEFT:
                anim.SetTrigger("left");
                currentDirection = DIRECTOIN.LEFT;
                break;
            case DIRECTOIN.RIGHT:
                anim.SetTrigger("right");
                currentDirection = DIRECTOIN.RIGHT;
                break;
            case DIRECTOIN.UP:
                anim.SetTrigger("up");
                currentDirection = DIRECTOIN.UP;
                break;
            case DIRECTOIN.DOWN:
                anim.SetTrigger("down");
                currentDirection = DIRECTOIN.DOWN;
                break;
            default:
                break;
        }
    }
}

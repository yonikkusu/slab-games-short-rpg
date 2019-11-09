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

    private MapSceneBase mapScene;
    private DIRECTOIN currentDirection;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
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

        // 移動中なら下記の処理を行う
        if(direction != Vector2.zero) {
            // 歩行アニメーション
            if(direction.x < 0) {
                anim.SetTrigger("left");
                currentDirection = DIRECTOIN.LEFT;
            } else if(direction.x > 0) {
                anim.SetTrigger("right");
                currentDirection = DIRECTOIN.RIGHT;
            } else if(direction.y > 0) {
                anim.SetTrigger("up");
                currentDirection = DIRECTOIN.UP;
            } else if(direction.y < 0) {
                anim.SetTrigger("down");
                currentDirection = DIRECTOIN.DOWN;
            }

            // 床イベントチェック
            mapScene.CheckFloorEvents(transform.position);
        }
    }
}

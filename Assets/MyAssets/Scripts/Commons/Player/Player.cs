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
    /// <summary>イベント座標</summary>
    private readonly Vector2 EVENT_COORD = new Vector2(4.5f, -1.5f);

    /// <summary> プレイヤーの向き</summary>
    public enum DIRECTOIN {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    };

    [SerializeField] private Rigidbody2D rigidBody = default;
    [SerializeField] private Text mainText = default;
    [SerializeField] private GameObject mainTextPanel = default;
    [SerializeField] private Animator anim = default;

    private DIRECTOIN currentDirection;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        mainText.text = "";
        mainTextPanel.SetActive(false);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アップデート処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        var direction = new Vector2(x, y).normalized;
        rigidBody.velocity = direction * PLAYER_SPEED;

        // アニメーショントリガー
        // プレイヤー向き取得
        if( direction.x < 0 ) {
            anim.SetTrigger("left");
            currentDirection = DIRECTOIN.LEFT;
        } else if( direction.x > 0 ) {
            anim.SetTrigger("right");
            currentDirection = DIRECTOIN.RIGHT;
        } else if( direction.y > 0 ) {
            anim.SetTrigger("up");
            currentDirection = DIRECTOIN.UP;
        }  else if( direction.y < 0 ) {
            anim.SetTrigger("down");
            currentDirection = DIRECTOIN.DOWN;
        }

        if(Input.GetKeyDown(KeyCode.Return)) {
            if( ( ( transform.position.x > EVENT_COORD.x - 1.5 && transform.position.x < EVENT_COORD.x - 0.5 ) && ( transform.position.y > EVENT_COORD.y - 0.5 && transform.position.y < EVENT_COORD.y + 0.5 ) && ( currentDirection == DIRECTOIN.RIGHT ) ) ||
                ( ( transform.position.x > EVENT_COORD.x + 0.5 && transform.position.x < EVENT_COORD.x + 1.5 ) && ( transform.position.y > EVENT_COORD.y - 0.5 && transform.position.y < EVENT_COORD.y + 0.5 ) && ( currentDirection == DIRECTOIN.LEFT  ) ) ||
                ( ( transform.position.x > EVENT_COORD.x - 0.5 && transform.position.x < EVENT_COORD.x + 0.5 ) && ( transform.position.y > EVENT_COORD.y - 1.5 && transform.position.y < EVENT_COORD.y - 0.5 ) && ( currentDirection == DIRECTOIN.UP    ) ) ||
                ( ( transform.position.x > EVENT_COORD.x - 0.5 && transform.position.x < EVENT_COORD.x + 0.5 ) && ( transform.position.y > EVENT_COORD.y + 0.5 && transform.position.y < EVENT_COORD.y + 1.5 ) && ( currentDirection == DIRECTOIN.DOWN  ) ) ) {
                switchTextActive();
            }
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// テキストの表示切り替え
    /// </summary>
    //--------------------------------------------------------------------------/
    private void switchTextActive()
    {
        if( mainTextPanel.activeSelf ) {
            mainText.text = "";
            mainTextPanel.SetActive(false);

        } else {
            mainText.text = "test";
            mainTextPanel.SetActive(true);
        }
    }
}

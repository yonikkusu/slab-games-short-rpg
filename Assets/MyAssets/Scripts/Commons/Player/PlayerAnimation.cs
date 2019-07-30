using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// プレイヤーのアニメーション管理クラス
/// </summary>
//--------------------------------------------------------------------------/
public class PlayerAnimation : MonoBehaviour
{
    /// <summary>アニメーション速度</summary>
    private const float speed = 10f;

    [SerializeField] private Rigidbody2D rigidBody = default;
    [SerializeField] private Text mainText = default;
    [SerializeField] private GameObject mainTextPanel = default;

    enum DIRECTOIN
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    };

    private Animator anim;
    private int currentDirection;
    private Vector2 eventCoord = new Vector2((float)4.5, (float)-1.5);

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        anim = GetComponent<Animator>();
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
        rigidBody.velocity = direction * speed;

        // アニメーショントリガー
        // プレイヤー向き取得
        if( direction.x < 0 )
        {
            anim.SetTrigger("left");
            currentDirection = (int)DIRECTOIN.LEFT;
        }
        else if( direction.x > 0 )
        {
            anim.SetTrigger("right");
            currentDirection = (int)DIRECTOIN.RIGHT;
        }
        else if( direction.y > 0 )
        {
            anim.SetTrigger("up");
            currentDirection = (int)DIRECTOIN.UP;
        }
        else if( direction.y < 0 )
        {
            anim.SetTrigger("down");
            currentDirection = (int)DIRECTOIN.DOWN;
        }

        if( Input.GetKeyDown(KeyCode.Return) )
        {
            if( ( ( transform.position.x > eventCoord.x - 1.5 && transform.position.x < eventCoord.x - 0.5 ) && ( transform.position.y > eventCoord.y - 0.5 && transform.position.y < eventCoord.y + 0.5 ) && ( currentDirection == 1 ) ) ||
                ( ( transform.position.x > eventCoord.x + 0.5 && transform.position.x < eventCoord.x + 1.5 ) && ( transform.position.y > eventCoord.y - 0.5 && transform.position.y < eventCoord.y + 0.5 ) && ( currentDirection == 0 ) ) ||
                ( ( transform.position.x > eventCoord.x - 0.5 && transform.position.x < eventCoord.x + 0.5 ) && ( transform.position.y > eventCoord.y - 1.5 && transform.position.y < eventCoord.y - 0.5 ) && ( currentDirection == 2 ) ) ||
                ( ( transform.position.x > eventCoord.x - 0.5 && transform.position.x < eventCoord.x + 0.5 ) && ( transform.position.y > eventCoord.y + 0.5 && transform.position.y < eventCoord.y + 1.5 ) && ( currentDirection == 3 ) ) )
            {
                switchTextActive();
            }
        }
    }

    // テキストのアクティブ切り替え
    private void switchTextActive()
    {
        //Debug.Log(mainText.text);
        if( mainTextPanel.activeSelf )
        {
            mainText.text = "";
            mainTextPanel.SetActive(false);

        }
        else
        {
            mainText.text = "test";
            mainTextPanel.SetActive(true);
        }
    }
}

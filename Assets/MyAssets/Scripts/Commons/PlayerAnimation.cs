using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// プレイヤーのアニメーション管理クラス
/// </summary>
//--------------------------------------------------------------------------/
public class PlayerAnimation : MonoBehaviour
{
    /// <summary>アニメーション速度</summary>
    private const float speed = 30f;

    [SerializeField] private Rigidbody2D rigidBody;

    private Animator anim;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        anim = GetComponent<Animator>();
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
        if(Input.GetKey(KeyCode.LeftArrow)) { 
            anim.SetTrigger("left");
        }
        else if(Input.GetKey(KeyCode.RightArrow)) {
            anim.SetTrigger("right");
        }
        else if(Input.GetKey(KeyCode.UpArrow)) {
            anim.SetTrigger("up");
        }
        else if(Input.GetKey(KeyCode.DownArrow)) {
            anim.SetTrigger("down");
        }
    }
}

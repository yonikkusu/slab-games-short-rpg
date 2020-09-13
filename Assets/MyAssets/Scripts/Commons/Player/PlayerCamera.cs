using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// プレイヤーカメラ
/// </summary>
//--------------------------------------------------------------------------/
public class PlayerCamera : MonoBehaviour
{
    private GameObject player;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アップデート処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}

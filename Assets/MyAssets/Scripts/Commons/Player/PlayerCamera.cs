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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // プレイヤーの位置をセットする(データがないならデバッグ用に作成する)
        if(PlayerData.Instance.CurrentData == null) {
            PlayerData.Instance.Create("デバッグプレイヤー");
        }
        var playerData = PlayerData.Instance.CurrentData;
        player.transform.position = new Vector2(playerData.PlayerPositionX, playerData.PlayerPositionY);
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

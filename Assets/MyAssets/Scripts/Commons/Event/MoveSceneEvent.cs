using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// シーン移動 イベント
/// </summary>
//--------------------------------------------------------------------------/
public class MoveSceneEvent : MapEvent
{
    [SerializeField] private SceneName sceneName = default;
    [SerializeField] private Vector2 destinationCoord = default;
    [SerializeField] private Player.DIRECTION playerDirection = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 踏まれた時の処理
    /// </summary>
    //--------------------------------------------------------------------------/
    protected override void onStepped()
    {
        Player.SetStartPotisionAndDirection(destinationCoord, playerDirection);
        SceneManagerExtension.LoadScene(sceneName);
    }
}

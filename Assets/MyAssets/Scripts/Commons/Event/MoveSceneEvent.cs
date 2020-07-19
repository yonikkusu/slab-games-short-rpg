using UnityEngine;
using UniRx.Async;

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
        var parameter = new MapSceneParameter(destinationCoord, playerDirection);
        SceneManagerExtension.LoadSceneAsync(sceneName, parameter).Forget();
    }
}

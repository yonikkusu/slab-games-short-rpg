using UnityEngine;
using UniRx.Async;

/// <summary>
/// シーン移動 イベント
/// </summary>
public class MoveSceneEvent : MapEvent
{
    [SerializeField] private SceneName sceneName = default;
    [SerializeField] private Vector2 destinationCoord = default;
    [SerializeField] private PLAYER_DIRECTION playerDirection = default;

    /// <summary>
    /// 踏まれた時の処理
    /// </summary>
    /// <param name="playerModel">プレイヤー情報</param>
    protected override void onStepped(IReadOnlyPlayerModel playerModel)
    {
        var direction = playerDirection == PLAYER_DIRECTION.NONE ? playerModel.CurrentDirection : playerDirection;
        var parameter = new MapSceneParameter(destinationCoord, direction);
        SceneManagerExtension.LoadSceneAsync(sceneName, parameter).Forget();
    }
}

//#define DEBUG_LOG

using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// マップシーン基底クラス
/// </summary>
//--------------------------------------------------------------------------/
public class MapSceneBase : MonoBehaviour
{
    [SerializeField] private MapEvent[] mapEvents = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 調べるイベントを発動させるかチェックする
    /// </summary>
    /// <param name="checkedPosition">調べた位置</param>
    //--------------------------------------------------------------------------/
    public void CheckInspectEvents(Vector2 checkedPosition)
    {
#if DEBUG_LOG
        Debug.Log($"調べた位置({checkedPosition.x}, {checkedPosition.y})");
#endif
        foreach(var mapEvent in mapEvents) {
            mapEvent.CheckInspectEvent(checkedPosition);
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 床イベントを発動させるかチェックする
    /// </summary>
    /// <param name="playerPosition">プレイヤーの位置</param>
    //--------------------------------------------------------------------------/
    public void CheckFloorEvents(Vector2 playerPosition)
    {
#if DEBUG_LOG
        Debug.Log($"踏んだ位置({playerPosition.x}, {playerPosition.y})");
#endif
        foreach(var mapEvent in mapEvents) {
            mapEvent.CheckFloorEvent(playerPosition);
        }
    }
}

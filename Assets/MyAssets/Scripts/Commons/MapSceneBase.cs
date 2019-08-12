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
}

using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// マップイベント
/// </summary>
//--------------------------------------------------------------------------/
public class MapEvent : MonoBehaviour
{
    [SerializeField] private Vector2 eventCoord = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 調べるイベントを発動させるかチェックする
    /// </summary>
    /// <param name="checkedPosition">調べた位置</param>
    //--------------------------------------------------------------------------/
    public void CheckInspectEvent(Vector2 checkedPosition)
    {
        if(isSamePosition(checkedPosition)) {
            onInspected();
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 床イベントを発動させるかチェックする
    /// </summary>
    /// <param name="playerPosition">プレイヤーの位置</param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public void CheckFloorEvent(Vector2 playerPosition)
    {
        if(isSamePosition(playerPosition)) {
            onStepped();
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 調べられた時の処理(継承先で中身を定義する)
    /// </summary>
    //--------------------------------------------------------------------------/
    protected virtual void onInspected() { }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 踏まれた時の処理(継承先で中身を定義する)
    /// </summary>
    //--------------------------------------------------------------------------/
    protected virtual void onStepped() { }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 指定した座標に対応するイベントがあるか
    /// </summary>
    /// <param name="position">座標</param>
    /// <returns>あればtrue</returns>
    //--------------------------------------------------------------------------/
    private bool isSamePosition(Vector2 position)
    {
        if(eventCoord.x - 0.5 <= position.x && position.x <= eventCoord.x + 0.5 && 
           eventCoord.y - 0.5 <= position.y && position.y <= eventCoord.y + 0.5) {
            return true;
        }

        return false;
    }
}

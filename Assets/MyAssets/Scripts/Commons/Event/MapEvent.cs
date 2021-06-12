using UnityEngine;

/// <summary>
/// マップイベント
/// </summary>
public abstract class MapEvent : MonoBehaviour
{
    private RectTransform rectTransform;

    /// <summary>
    /// 起動時処理
    /// </summary>
    public virtual void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// 調べるイベントを発動させるかチェックする
    /// </summary>
    /// <param name="checkedPosition">調べた位置</param>
    public void CheckInspectEvent(Vector2 checkedPosition)
    {
        if(isSamePosition(checkedPosition)) {
            onInspected();
        }
    }

    /// <summary>
    /// 床イベントを発動させるかチェックする
    /// </summary>
    /// <param name="playerModel">プレイヤー情報</param>
    public void CheckFloorEvent(IReadOnlyPlayerModel playerModel)
    {
        if(isSamePosition(playerModel.CurrentPosition)) {
            onStepped(playerModel);
        }
    }

    /// <summary>
    /// アイテム使用イベントを発動させるかチェックする
    /// </summary>
    /// <param name="checkedPosition">使用する位置</param>
    /// <param name="usedItemId">使用するアイテムのID</param>
    public void CheckUseItemEvent(Vector2 checkedPosition, ItemID usedItemId)
    {
        if(isSamePosition(checkedPosition)) {
            onUsedItem(usedItemId);
        }
    }

    /// <summary>
    /// 調べられた時の処理(継承先で中身を定義する)
    /// </summary>
    protected virtual void onInspected() { }

    /// <summary>
    /// 踏まれた時の処理(継承先で中身を定義する)
    /// </summary>
    /// <param name="playerModel">プレイヤー情報</param>
    protected virtual void onStepped(IReadOnlyPlayerModel playerModel) { }

    /// <summary>
    /// アイテム使用時の処理(継承先で中身を定義する)
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    protected virtual void onUsedItem(ItemID usedItemId) { }

    /// <summary>
    /// 現在のMapEventの中心座標を取得する
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    protected Vector2 getCurrentCenterPosition()
    {
        return rectTransform.CenterPosition();
    }

    /// <summary>
    /// 指定した座標に対応するイベントがあるか
    /// </summary>
    /// <param name="position">座標</param>
    /// <returns>あればtrue</returns>
    private bool isSamePosition(Vector2 position)
    {
        var eventCoord = getCurrentCenterPosition();

        if(eventCoord.x - 0.5 <= position.x && position.x <= eventCoord.x + 0.5 && 
           eventCoord.y - 0.5 <= position.y && position.y <= eventCoord.y + 0.5) {
            return true;
        }

        return false;
    }
}

using UnityEngine;

/// <summary>
/// プレイヤーの向き
/// </summary>
public enum PLAYER_DIRECTION {
    NONE,
    LEFT,
    RIGHT,
    UP,
    DOWN,
};

/// <summary>
/// PLAYER_DIRECTION 拡張クラス
/// </summary>
public static class PlayerDirectionExtention
{
    /// <summary>
    /// 移動方向ベクターに変換する(斜めはVector2.zero扱い)
    /// </summary>
    /// <<param name="directionVector">移動方向</param>
    /// <returns>移動方向ベクター</returns>
    public static Vector2 ToVector2(this PLAYER_DIRECTION direction)
    {
        switch(direction) {
            case PLAYER_DIRECTION.LEFT: return Vector2.left;
            case PLAYER_DIRECTION.RIGHT: return Vector2.right;
            case PLAYER_DIRECTION.UP: return Vector2.up;
            case PLAYER_DIRECTION.DOWN: return Vector2.down;
            default: return Vector2.zero;
        }
    }
}

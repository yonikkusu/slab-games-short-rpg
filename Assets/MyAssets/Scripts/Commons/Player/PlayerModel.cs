using UnityEngine;

/// <summary>
/// プレイヤー モデル
/// </summary>
public class PlayerModel : IReadOnlyPlayerModel
{
    /// <summary>現在の位置</summary>
    public Vector2 CurrentPosition { get; private set; }

    /// <summary>現在の向き</summary>
    public PLAYER_DIRECTION CurrentDirection { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="direction">向き</param>
    public PlayerModel(Vector2 position, PLAYER_DIRECTION direction)
    {
        CurrentPosition = position;
        CurrentDirection = direction;
    }

    /// <summary>
    /// 位置情報を更新
    /// </summary>
    /// <param name="position">位置</param>
    public void UpdatePosition(Vector2 position) => CurrentPosition = position;
    
    /// <summary>
    /// 向き情報を更新
    /// </summary>
    /// <param name="direction">向き</param>
    public void UpdateDirection(PLAYER_DIRECTION direction) => CurrentDirection = direction;
}

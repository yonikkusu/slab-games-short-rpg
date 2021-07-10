using UnityEngine;

/// <summary>
/// プレイヤー モデル(読み取り専用)
/// </summary>
public interface IReadOnlyPlayerModel
{
    /// <summary>現在の位置</summary>
    Vector2 CurrentPosition { get; }

    /// <summary>現在の向き</summary>
    Player.DIRECTION CurrentDirection { get; }
}

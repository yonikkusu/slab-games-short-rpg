using UnityEngine;

/// <summary>
/// RectTransform 拡張
/// </summary>
public static class RectTransformExtension
{
    /// <summary>
    /// 中心座標を取得する
    /// </summary>
    public static Vector2 CenterPosition(this RectTransform self)
    {
        var position = self.position;
        var diff = new Vector3(
            Mathf.Lerp(-self.rect.size.x / 2f, self.rect.size.x / 2f, self.pivot.x) * self.transform.lossyScale.x,
            Mathf.Lerp(-self.rect.size.y / 2f, self.rect.size.y / 2f, self.pivot.y) * self.transform.lossyScale.y
        );
        return position - diff;
    }
}

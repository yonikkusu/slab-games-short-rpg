using UnityEngine;

/// <summary>
/// ポップアップ生成クラス
/// </summary>
public class PopupCreator : SingletonMonoBehaviour<PopupCreator>
{
    [SerializeField] private SystemPopup systemPopup;

    /// <summary>
    /// ポップアップを生成する
    /// </summary>
    /// <returns>ポップアップ</returns>
    public SystemPopup CreatePopup() => systemPopup;
}

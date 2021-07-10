using UnityEngine;
using UniRx.Async;

/// <summary>
/// 自動イベント オープニング
/// </summary>
public class AutoEventOpening : AutoEvent
{
    [SerializeField] private GameObject[] otherCharacters;

    protected override AutoEventId autoEventId => AutoEventId.Opening;

    /// <summary>
    /// 実行する
    /// </summary>
    /// <returns>UniTask</returns>
    protected override async UniTask executeAsync()
    {
        // 他キャラを表示する
        foreach(var character in otherCharacters) {
            character.SetActive(true);
        }
        DisplayManager.Instance.HideInGameDisplayObjects();
        await FlowchartHelper.Instance.PlayAdvAsync($"AutoEvent_Opening_1");
        await DisplayManager.Instance.FadeOutDisplayAsync();
        // 他キャラを消す
        foreach(var character in otherCharacters) {
            character.SetActive(false);
        }
        await UniTask.Delay(3000);
        await DisplayManager.Instance.FadeInDisplayAsync();
        await FlowchartHelper.Instance.PlayAdvAsync($"AutoEvent_Opening_2");
        DisplayManager.Instance.ShowInGameDisplayObjects();
    }
}

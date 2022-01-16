using UnityEngine.SceneManagement;
using UniRx.Async;

/// <summary>
/// シーンマネージャー 拡張
/// </summary>
public class SceneManagerExtension
{
    /// <summary>シーンパラメータ</summary>
    public static MapSceneParameter SceneParameter;

    /// <summary>シーン遷移中か</summary>
    public static bool IsMoving;

    /// <summary>
    /// シーンを読み込む
    /// </summary>
    /// <param name="scene">読み込むシーン名</param>
    /// <param name="parameter">読み込み時に使うパラメータ</param>
    public static async UniTask LoadSceneAsync(SceneName scene, MapSceneParameter paramter = null)
    {
        IsMoving = true;
        // 画面フェードアウト
        await DisplayManager.Instance.FadeOutDisplayAsync();
        // シーン遷移
        SceneParameter = paramter;
        await SceneManager.LoadSceneAsync(scene.ToString());
        IsMoving = false;
    }

    /// <summary>
    /// アクティブなシーンを取得する
    /// </summary>
    public static SceneName GetCurrentSceneName()
    {
        var scene = SceneManager.GetActiveScene();
        return scene.IsValid() ? scene.name.ToEnum<SceneName>() : SceneName.None;
    }
}

/// <summary>
/// シーン名
/// </summary>
public enum SceneName
{
    None,
    Title,
    GrassMap,
    Corridor2F,
    GuestRoom1,
    GuestRoom2,
    GuestRoom3,
    GuestRoom4,
    Bedroom,
    Study,
    KidsRoom,
    Corridor1F,
    DressingRoom,
    BathRoom,
    Toilet,
    Dining,
    SekkanRoom,
    Library,
    Garden,
    Kitchen,
}

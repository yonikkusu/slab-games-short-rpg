using UnityEngine.SceneManagement;

//--------------------------------------------------------------------------/
/// <summary>
/// シーンマネージャー 拡張
/// </summary>
//--------------------------------------------------------------------------/
public class SceneManagerExtension
{
    //--------------------------------------------------------------------------/
    /// <summary>
    /// シーンを読み込む
    /// </summary>
    /// <param name="scene">読み込むシーン名</param>
    //--------------------------------------------------------------------------/
    public static void LoadScene(SceneName scene) => SceneManager.LoadSceneAsync(scene.ToString());

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アクティブなシーンを取得する
    /// </summary>
    //--------------------------------------------------------------------------/
    public static SceneName GetCurrentSceneName()
    {
        var scene = SceneManager.GetActiveScene();
        return scene.IsValid() ? scene.name.ToEnum<SceneName>() : SceneName.None;
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// シーン名
/// </summary>
//--------------------------------------------------------------------------/
public enum SceneName
{
    None,
    Title,
    MainMenu,
    Field,
    GrassMap,
    Corridor2F,
    GuestRoom1,
    GuestRoom2,
    GuestRoom3,
    GuestRoom4,
}

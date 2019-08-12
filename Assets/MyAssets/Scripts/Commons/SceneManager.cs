using SceneManagement = UnityEngine.SceneManagement;

//--------------------------------------------------------------------------/
/// <summary>
/// シーンマネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    /// <summary>現在表示しているシーン</summary>
    public Scene CurrentScene { get; private set; }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// シーンを読み込む
    /// </summary>
    /// <param name="scene">読み込むシーン名</param>
    //--------------------------------------------------------------------------/
    public void LoadScene(Scene scene)
    {
        SceneManagement.SceneManager.LoadSceneAsync(scene.ToString());
        CurrentScene = scene;
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// シーン名
/// </summary>
//--------------------------------------------------------------------------/
public enum Scene
{
    Title,
    MainMenu,
    Field,
    GrassMap,
}

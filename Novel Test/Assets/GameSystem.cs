
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Fungus;

public class GameSystem : MonoBehaviour {

    public void GameStart() {
        SceneManager.LoadScene ("Game");
    }

    public void EndCredits()
    {
        SceneManager.LoadScene("EndCredits");
    }

    //　ゲーム終了ボタンを押したら実行する
    public void GameEnd() {
	#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
	#elif UNITY_WEBPLAYER
		Application.OpenURL("http://www.yahoo.co.jp/");
	#else
		Application.Quit();
	#endif
    }


}
 
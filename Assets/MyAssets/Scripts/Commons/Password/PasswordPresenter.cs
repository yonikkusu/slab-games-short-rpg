using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

public class PasswordPresenter : SingletonMonoBehaviour<PasswordPresenter>
{
    [SerializeField] private Image PasswordPanel;
    [SerializeField] private PasswordCtrlText passwordCtrlText;

    public void Initialize()
    {
        Debug.Log("presenter initialize");
        PasswordPanel.gameObject.SetActive(true);
        passwordCtrlText.Initialize();
    }

    public int getPassword()
    {
        while( !Input.GetKey(KeyCode.Return) )
        {
            ;
        }

        Debug.Log("get password");
        return passwordCtrlText.getPassword();
    }

    public void hide()
    {
        Debug.Log("hide presenter");
        PasswordPanel.gameObject.SetActive(false);
    }
  
}

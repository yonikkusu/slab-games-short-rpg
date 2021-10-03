using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

public class InputPassword : MapEvent
{
    
    /// <summary>
    /// 調べられた時の処理
    /// </summary>
    /// <returns>UniTask</returns>
    protected override async UniTask onInspectedAsync()
    {
        //Debug.Log("input password");
        PasswordPresenter.Instance.Initialize();
        var password = await PasswordPresenter.Instance.getPassword();
        PasswordPresenter.Instance.hide();

        Debug.Log(password.ToString());
    }

}

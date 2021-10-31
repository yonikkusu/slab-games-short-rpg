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
        var popup = PopupCreator.Instance.CreatePopup();
        await popup.ShowAsync(MessageCreator.Create(MessageId.FindLock));
        //Debug.Log("input password");
        PasswordPresenter.Instance.Initialize();
        var password = await PasswordPresenter.Instance.getPassword();
        
        PasswordPresenter.Instance.hide();

        if( password == 0 )
        {
            await popup.ShowAsync(MessageCreator.Create(MessageId.OpenLock));
        }
        Debug.Log(password.ToString());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using UniRx;

public class PasswordPresenter : SingletonMonoBehaviour<PasswordPresenter>
{
    [SerializeField] private Image PasswordPanel;
    [SerializeField] private PasswordCtrlText passwordCtrlText;
    [SerializeField] private PasswordInput passwordInput;

    private UniTaskCompletionSource source;

    private void Start()
    {
        passwordInput.OnReturnKeyDown.Subscribe(_ => source?.TrySetResult()).AddTo(this);
    }

    public void Initialize()
    {
        Debug.Log("presenter initialize");
        InputService.Instance.ChangeMode(InputMode.InputPassword);
        PasswordPanel.gameObject.SetActive(true);
        passwordCtrlText.Initialize();
    }

    public async UniTask<int> getPassword()
    {
        source = new UniTaskCompletionSource();
        await source.Task;

        Debug.Log("get password");
        return passwordCtrlText.getPassword();
    }

    public void hide()
    {
        Debug.Log("hide presenter");
        InputService.Instance.ChangeMode(InputMode.Normal);
        PasswordPanel.gameObject.SetActive(false);
    }
  
}

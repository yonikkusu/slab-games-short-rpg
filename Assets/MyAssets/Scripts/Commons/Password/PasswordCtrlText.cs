using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PasswordCtrlText : MonoBehaviour
{
    [SerializeField] private Button[] upButton;
    [SerializeField] private Button[] downButton;
    [SerializeField] private Text[] passwordText;
    
    private const int max_ctrl_num = 4;
    private int[] text_num = new int[max_ctrl_num];

    public IObservable<Unit> OnTapUpButton_0 => upButton[0].OnClickAsObservable();
    public IObservable<Unit> OnTapUpButton_1 => upButton[1].OnClickAsObservable();
    public IObservable<Unit> OnTapUpButton_2 => upButton[2].OnClickAsObservable();
    public IObservable<Unit> OnTapUpButton_3 => upButton[3].OnClickAsObservable();
    public IObservable<Unit> OnTapDownButton_0 => downButton[0].OnClickAsObservable();
    public IObservable<Unit> OnTapDownButton_1 => downButton[1].OnClickAsObservable();
    public IObservable<Unit> OnTapDownButton_2 => downButton[2].OnClickAsObservable();
    public IObservable<Unit> OnTapDownButton_3 => downButton[3].OnClickAsObservable();

    // Start is called before the first frame update
    public void Initialize()
    {
        Debug.Log("ctrlText initialize");
        OnTapUpButton_0.Subscribe(_ =>
        {
            text_num[0]++;
            TextUpdate(0);
        }).AddTo(this);

        OnTapDownButton_0.Subscribe(_ =>
        {
            text_num[0]--;
            TextUpdate(0);
        }).AddTo(this);

        OnTapUpButton_1.Subscribe(_ =>
        {
            text_num[1]++;
            TextUpdate(1);
        }).AddTo(this);

        OnTapDownButton_1.Subscribe(_ =>
        {
            text_num[1]--;
            TextUpdate(1);
        }).AddTo(this);

        OnTapUpButton_2.Subscribe(_ =>
        {
            text_num[2]++;
            TextUpdate(2);
        }).AddTo(this);

        OnTapDownButton_2.Subscribe(_ =>
        {
            text_num[2]--;
            TextUpdate(2);
        }).AddTo(this);

        OnTapUpButton_3.Subscribe(_ =>
        {
            text_num[3]++;
            TextUpdate(3);
        }).AddTo(this);

        OnTapDownButton_3.Subscribe(_ =>
        {
            text_num[3]--;
            TextUpdate(3);
        }).AddTo(this);

        textInit(passwordText);
    }

    // Update is called once per frame
    void TextUpdate(int target)
    {
        text_num[target] = CtrlTextNum(text_num[target]);
        passwordText[target].text = text_num[target].ToString();
    }

    public void textInit(Text[] text )
    {
        for (int i = 0; i < max_ctrl_num; i++ )
        {
            text_num[i] = 0;
            text[i].text = text_num[i].ToString();
            text[i].fontSize = 80;
            text[i].alignment = TextAnchor.MiddleCenter;
        }
        Debug.Log("ctrlText Init: " + text_num[0].ToString() + text_num[1].ToString() + text_num[2].ToString() + text_num[3].ToString());
    }

    public int getPassword()
    {
        Debug.Log("ctrlText getPassword: " + text_num[0].ToString() + text_num[1].ToString() + text_num[2].ToString() + text_num[3].ToString());
        return text_num[0] * 1000 + text_num[1] * 100 + text_num[2] * 10 + text_num[3];
    }

    private int CtrlTextNum(int num )
    {
        if( num > 9 )
        {
            num = 0;
        }
        else if(num < 0 )
        {
            num = 9;
        }
        return num;
    }
}

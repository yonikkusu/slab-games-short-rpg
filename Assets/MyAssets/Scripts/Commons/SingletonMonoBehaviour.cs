﻿using UnityEngine;
using System;

//--------------------------------------------------------------------------/
/// <summary>
/// シングルトン
/// </summary>
//--------------------------------------------------------------------------/
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>自身のインスタンス</summary>
    public static T Instance
    {
        get
        {
            if (instance == null) {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null) {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }
    private static T instance;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    virtual protected void Awake()
    {
        CheckInstance();
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 他のゲームオブジェクトにアタッチされているか調べる
    /// アタッチされている場合は破棄する
    /// </summary>
    //--------------------------------------------------------------------------/
    protected bool CheckInstance()
    {
        if(instance == null) {
            instance = this as T;
            return true;
        }else if(Instance == this) {
            return true;
        }
        Destroy(this);
        return false;
    }
}

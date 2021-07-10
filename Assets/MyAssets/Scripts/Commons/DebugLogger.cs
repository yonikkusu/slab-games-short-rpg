﻿// NOTE: コメントアウトを外すことで各所に記述したデバッグ用ログを表示できる
//#define DEBUG_LOG

using UnityEngine;

/// <summary>
/// デバッグログ表示クラス
/// </summary>
public class DebugLogger
{
    /// <summary>
    /// 通常のログを表示する
    /// </summary>
    /// <param name="message">表示内容</param>
    public static void Log(object message)
    {
        Debug.Log(message);
    }

    /// <summary>
    /// 警告ログを表示する
    /// </summary>
    /// <param name="message">表示内容</param>
    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }

    /// <summary>
    /// エラーログを表示する
    /// </summary>
    /// <param name="message">表示内容</param>
    public static void LogError(object message)
    {
        Debug.LogError(message);
    }
}

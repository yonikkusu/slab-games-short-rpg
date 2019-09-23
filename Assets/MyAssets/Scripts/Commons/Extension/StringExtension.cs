using System;

//--------------------------------------------------------------------------/
/// <summary>
/// string 拡張
/// </summary>
//--------------------------------------------------------------------------/
public static class StringExtension
{
    //--------------------------------------------------------------------------/
    /// <summary>
    /// 文字列をEnum型に変換する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str"></param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public static T ToEnum<T>(this string str) where T : struct, IComparable, IFormattable, IConvertible
    {
        return (T)Enum.Parse(typeof(T), str);
    }
}

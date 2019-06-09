using System;

//--------------------------------------------------------------------------/
/// <summary>
/// ゲームで使えそうなユーティリティ
/// </summary>
//--------------------------------------------------------------------------/
public class GameUtility
{
    /// <summary>UnixTimeの起点時刻</summary>
    private static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 現在のUnixTimeを取得する
    /// </summary>
    /// <returns>UnixTime</returns>
    //--------------------------------------------------------------------------/
    public static long GetUnixTime()
    {
        return (DateTimeOffset.Now - UnixEpoch).Ticks / 10000000;
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// UnixTimeをDateTimeOffsetに変換する
    /// </summary>
    /// <param name="unixTime">変換したいUnixTime</param>
    /// <returns>DateTimeOffset</returns>
    //--------------------------------------------------------------------------/
    public static DateTimeOffset UnixTimeToDateTimeOffset(long unixTime)
    {
        return new DateTimeOffset(unixTime * 10000000 + UnixEpoch.Ticks, TimeSpan.Zero);
    }
}

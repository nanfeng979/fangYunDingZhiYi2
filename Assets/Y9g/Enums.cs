namespace Y9g
{
    public enum Move4Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    public enum Move8Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
    }

    public sealed class EnumCalculate
    {
        /// <summary>
        /// 获取其他枚举。
        /// </summary>
        /// <typeparam name="T"> 枚举类型 </typeparam>
        /// <param name="enumValue"> 枚举值 </param>
        /// <param name="step"> 步数 </param>
        /// <returns> 其他枚举 </returns>
        public static T GetOtherEnum<T>(T enumValue, int step) where T : System.Enum
        {
            int enumInt = (int)(object)enumValue;
            int enumCount = System.Enum.GetValues(typeof(T)).Length;
            enumInt = (enumInt + step + enumCount) % enumCount;
            return (T)(object)enumInt;
        }

        public static T StringToEnum<T>(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value);
        }
    }
}
namespace _game.Scripts.Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format">E2, e2, E</param>
        /// <returns></returns>
        public static string ToScientificNotation(this float value, string format = "E2")
        {
            value.ToString("");
            return value.ToString(format);
        }

        //TODO: implement properly
        public static string ToShortFormat(this float value)
        {
            if(value > 1000)
                return (value / 1000).ToString("F2") + "K";
            if(value > 1000000)
                return (value / 1000000).ToString("F2") + "M";
            if(value > 1000000000)
                return (value / 1000000000).ToString("F2") + "B";
            if(value > 1000000000000)
                return (value / 1000000000000).ToString("F2") + "T";
            if(value > 1000000000000000)
                return (value / 1000000000000000).ToString("F2") + "Q";
            if(value > 1000000000000000000)
                return (value / 1000000000000000000).ToString("F2") + "QQ";
            return value.ToString("F2");
        }
        
        
    }
}
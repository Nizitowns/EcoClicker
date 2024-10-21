using UnityEngine;

namespace _game.Scripts.Extensions
{
    

    
    public static class FloatExtensions
    {
        public static string[] IndexToMagnitude = new string[] {"", "K", "M", "B", "T", "q", "Q"};
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format">E2, e2, E</param>
        /// <returns></returns>
        public static string ToScientificNotation(this float value, string format = "E2")
        {
            return value.ToString(format);
        }

        //TODO: implement properly
        public static string ToShortFormat(this float value)
        {

            
            if(value >= 1000000000000000000 && value < 1000000000000000000)
                return (value / 1000000000000000000).ToString("F2") + "QQ";
            if(value is >= 1000000000000000 and < 1000000000000000000)
                return (value / 1000000000000000).ToString("F2") + "Q";
            if(value is >= 1000000000000 and < 1000000000000000)
                return (value / 1000000000000).ToString("F2") + "T";
            if(value is >= 1000000000 and < 1000000000000)
                return (value / 1000000000).ToString("F2") + "B";
            if(value is >= 1000000 and < 1000000000)
                return (value / 1000000).ToString("F2") + "M";
            if(value is >= 1000 and < 1000000)
                return (value / 1000).ToString("F2") + "K";
            return value.ToString("F2");
        }
        
        
    }
}
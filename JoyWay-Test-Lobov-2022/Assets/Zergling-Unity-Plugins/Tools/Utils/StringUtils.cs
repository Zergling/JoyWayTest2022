using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Tools.Utils
{
    public static class StringUtils
    {
        public static T ParseEnum<T>(string value, T defaultType, bool ignoreCase = true) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, ignoreCase);
            }
            catch
            {
                return defaultType;
            }
        }
    }
}

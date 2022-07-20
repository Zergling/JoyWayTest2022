using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZerglingPlugins.Windows
{
    public abstract class WindowSetup
    {
        public class Empty : WindowSetup
        {
            public static readonly Empty Instance = new Empty();
        }
    }
}

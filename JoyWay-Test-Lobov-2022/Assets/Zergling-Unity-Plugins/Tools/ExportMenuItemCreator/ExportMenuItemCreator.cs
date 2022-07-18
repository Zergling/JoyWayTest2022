using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using ZerglingPlugins.Tools.Log;

namespace ZerglingPlugins.Tools.Editor
{
    public static class ExportMenuItemCreator
    {
        [MenuItem("Zergling Plugins/Create export menu item", false, 100)]
        public static void Create()
        {
            var classFilePath = $"{Application.dataPath}/Zergling-Unity-Plugins/Tools/ExportMenuItemCreator/ExportMenuItemClass.txt";
            var lines = File.ReadAllLines(classFilePath);

            var productName = Application.productName;
            var pluginName = productName.Split('_')[1];
            
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (!line.Contains("{0}"))
                    continue;

                lines[i] = string.Format(line, pluginName, pluginName);
            }
            
            var outputFilePath = $"{Application.dataPath}/Zergling-Unity-Plugins/{pluginName}/ExportMenuItem.cs";
            File.WriteAllLines(outputFilePath, lines);
        }
    }
}
#endif

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// using Iso;

namespace Zocat
{
    public static class EnumGenerator
    {
#if UNITY_EDITOR
        private static string _EnumPath => Application.dataPath + "/Zocat/Runtime/Core/_Etc/Audio/";
        private static string _ResourcesPath => Application.dataPath + "/Zocat/Runtime/Core/Audio/Resources/";

        public static void Create(string _Name)
        {
            var _TempList = new List<string>();
            _TempList.Clear();
            var fullPath = _ResourcesPath + _Name;
            // IsoHelper.Log(fullPath);
            var dirInfo = new DirectoryInfo(fullPath);
            foreach (var file in dirInfo.GetFiles())
                if (file.Name.Contains(".mp3") && !file.Name.Contains(".meta"))
                {
                    var _Temp = file.Name.Replace(".mp3", "");
                    _TempList.Add(_Temp);
                }
                else if (file.Name.Contains(".wav") && !file.Name.Contains(".meta"))
                {
                    var _Temp = file.Name.Replace(".wav", "");
                    _TempList.Add(_Temp);
                }

                else if (file.Name.Contains(".ogg") && !file.Name.Contains(".meta"))
                {
                    var _Temp = file.Name.Replace(".ogg", "");
                    _TempList.Add(_Temp);
                }

            Create(_EnumPath, _Name, _TempList);
        }

        public static void Create(string _enumPath, string _enumName, List<string> _EnumEntries)
        {
            if (File.Exists(_enumPath + _enumName + ".cs"))
            {
                Write(_enumPath, _enumName, _EnumEntries);
            }
            else
            {
                using var fs = File.Create(_enumPath + _enumName + ".cs");
                fs.Dispose();
                Write(_enumPath, _enumName, _EnumEntries);
            }

            AssetDatabase.Refresh();
        }

        private static void Write(string _enumPath, string _EnumName, List<string> _EnumEntries)
        {
            using (var streamWriter = new StreamWriter(_enumPath + _EnumName + ".cs"))
            {
                streamWriter.WriteLine("public enum " + _EnumName + "");
                streamWriter.WriteLine("{");
                for (var i = 0; i < _EnumEntries.Count; i++) streamWriter.WriteLine("\t" + _EnumEntries[i] + ",");

                streamWriter.WriteLine("}");

                streamWriter.Flush();
                streamWriter.Close();
            }
        }
#endif
    }
}
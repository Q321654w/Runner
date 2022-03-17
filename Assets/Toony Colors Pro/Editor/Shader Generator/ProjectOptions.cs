using System.Collections.Generic;
using System.IO;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    public static class ProjectOptions
    {
        [System.Serializable]
        public class Data
        {
            public bool AutoNames = true;
            public bool SubFolders = true;
            public bool OverwriteConfig = false;
            public bool LoadAllShaders = false;
            public string CustomOutputPath = ShaderGenerator2.OUTPUT_PATH;
            public string LastImplementationExportImportPath = Application.dataPath;
            public List<string> OpenedFoldouts = new List<string>();
            public bool UseCustomFont = false;
            public Font CustomFont = null;

            public bool CustomFontInitialized = false;
        }
        static Data _data;
        public static Data data
        {
            get
            {
                if (_data == null)
                {
                    LoadProjectOptions();
                }
                return _data;
            }
        }

        static string GetPath()
        {
            return Application.dataPath.Replace(@"\","/") + "/../ProjectSettings/ToonyColorsPro.json";
        }

        public static void LoadProjectOptions()
        {
            _data = new Data();
            string path = GetPath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                EditorJsonUtility.FromJsonOverwrite(json, _data);
            }
        }

        public static void SaveProjectOptions()
        {
            string path = GetPath();
            string json = EditorJsonUtility.ToJson(_data, true);
            File.WriteAllText(path, json);
        }
    }
}
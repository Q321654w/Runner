using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_KeywordString : UIFeature
    {
        string keyword;
        string defaultValue;
        bool forceValue;

        internal UIFeature_KeywordString(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            switch(key)
            {
                case "kw": keyword = value; break;
                case "default": defaultValue = value.Trim('"'); break;
                case "forceKeyword": forceValue = bool.Parse(value); break;
                default: base.ProcessProperty(key, value); break;
            }
        }

        internal override void ForceValue(Config config)
        {
            if (forceValue && Enabled(config) && !config.HasKeyword(keyword))
            {
                config.SetKeyword(keyword, defaultValue);
            }
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            EditorGUI.BeginChangeCheck();
            string value = config.GetKeyword(keyword);
            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }
            string newValue = EditorGUI.TextField(position, GUIContent.none, value);
            if (EditorGUI.EndChangeCheck())
            {
                if (newValue != value)
                {
                    config.SetKeyword(keyword, newValue);
                }
            }
        }

        internal override bool Highlighted(Config config)
        {
            var value = config.GetKeyword(this.keyword);
            return !string.IsNullOrEmpty(value) && value != defaultValue;
        }
    }
}
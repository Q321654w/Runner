using System.Collections.Generic;
using System.Globalization;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Int : UIFeature
    {
        string keyword;
        int defaultValue;
        int min = int.MinValue;
        int max = int.MaxValue;

        internal UIFeature_Int(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "kw")
                keyword = value;
            else if(key == "default")
                defaultValue = int.Parse(value, CultureInfo.InvariantCulture);
            else if(key == "min")
                min = int.Parse(value, CultureInfo.InvariantCulture);
            else if(key == "max")
                max = int.Parse(value, CultureInfo.InvariantCulture);
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            var currentValueStr = config.GetKeyword(keyword);
            var currentValue = defaultValue;
            if(!int.TryParse(currentValueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out currentValue))
            {
                currentValue = defaultValue;

                //Only enforce keyword if feature is enabled
                if (Enabled(config))
                {
                    config.SetKeyword(keyword, currentValue.ToString(CultureInfo.InvariantCulture));
                }
            }

            EditorGUI.BeginChangeCheck();
            var newValue = currentValue;
            newValue = Mathf.Clamp(EditorGUI.IntField(position, currentValue), min, max);
            if(EditorGUI.EndChangeCheck())
            {
                if(newValue != currentValue)
                {
                    config.SetKeyword(keyword, newValue.ToString(CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Globalization;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Float : UIFeature
    {
        string keyword;
        float defaultValue;
        float min = float.MinValue;
        float max = float.MaxValue;

        internal UIFeature_Float(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "kw")
                keyword = value;
            else if(key == "default")
                defaultValue = float.Parse(value, CultureInfo.InvariantCulture);
            else if(key == "min")
                min = float.Parse(value, CultureInfo.InvariantCulture);
            else if(key == "max")
                max = float.Parse(value, CultureInfo.InvariantCulture);
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            var currentValueStr = config.GetKeyword(keyword);
            var currentValue = defaultValue;
            if(!float.TryParse(currentValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out currentValue))
            {
                currentValue = defaultValue;

                //Only enforce keyword if feature is enabled
                if (Enabled(config))
                {
                    config.SetKeyword(keyword, currentValue.ToString("0.0###############", CultureInfo.InvariantCulture));
                }
            }

            EditorGUI.BeginChangeCheck();
            var newValue = currentValue;
            newValue = Mathf.Clamp(EditorGUI.FloatField(position, currentValue), min, max);
            if(EditorGUI.EndChangeCheck())
            {
                if(newValue != currentValue)
                {
                    config.SetKeyword(keyword, newValue.ToString("0.0###############", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
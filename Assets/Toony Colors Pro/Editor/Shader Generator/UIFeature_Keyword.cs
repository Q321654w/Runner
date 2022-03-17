using System;
using System.Collections.Generic;
using System.Globalization;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Keyword : UIFeature
    {
        string keyword;
        string[] labels;
        string[] values;
        int defaultValue;
        bool forceValue;

        internal UIFeature_Keyword(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "kw")
                keyword = value;
            else if(key == "default")
                defaultValue = int.Parse(value, CultureInfo.InvariantCulture);
            else if(key == "forceKeyword")
                forceValue = bool.Parse(value);
            else if(key == "values")
            {
                var data = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                labels = new string[data.Length];
                values = new string[data.Length];

                for(var i = 0; i < data.Length; i++)
                {
                    var lbl_feat = data[i].Split('|');
                    if(lbl_feat.Length != 2)
                    {
                        Debug.LogWarning("[UIFeature_Keyword] Invalid data:" + data[i]);
                        continue;
                    }

                    labels[i] = lbl_feat[0];
                    values[i] = lbl_feat[1];
                }
            }
            else
                base.ProcessProperty(key, value);
        }

        internal override void ForceValue(Config config)
        {
            var selectedValue = GetSelectedValue(config);
            if(selectedValue < 0)
                selectedValue = defaultValue;

            if(forceValue && Enabled(config) && !config.HasKeyword(keyword))
            {
                config.SetKeyword(keyword, values[selectedValue]);
            }
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            var selectedValue = GetSelectedValue(config);
            if(selectedValue < 0)
            {
                selectedValue = defaultValue;
                if(forceValue && Enabled(config))
                {
                    config.SetKeyword(keyword, values[defaultValue]);
                }
            }

            EditorGUI.BeginChangeCheck();
            selectedValue = EditorGUI.Popup(position, selectedValue, labels);
            if(EditorGUI.EndChangeCheck())
            {
                if(string.IsNullOrEmpty(values[selectedValue]))
                    config.RemoveKeyword(keyword);
                else
                    config.SetKeyword(keyword, values[selectedValue]);
            }
        }

        int GetSelectedValue(Config config)
        {
            var currentValue = config.GetKeyword(keyword);
            for(var i = 0; i < values.Length; i++)
            {
                if(currentValue == values[i])
                    return i;
            }

            return -1;
        }

        internal override bool Highlighted(Config config)
        {
            var feature = GetSelectedValue(config);
            return feature != defaultValue;
        }
    }
}
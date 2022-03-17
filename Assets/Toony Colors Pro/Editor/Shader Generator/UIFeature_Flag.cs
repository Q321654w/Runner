using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Flag : UIFeature
    {
        bool negative;
        string keyword;
        string block = "pragma_surface_shader";
        string[] toggles;    //features forced to be toggled when this flag is enabled

        internal UIFeature_Flag(List<KeyValuePair<string, string>> list, bool negative) : base(list)
        {
            this.negative = negative;
            showHelp = false;
        }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "kw")
                keyword = value;
            else if(key == "block")
                block = value.Trim('"');
            else if(key == "toggles")
                toggles = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            var flag = Highlighted(config);
            EditorGUI.BeginChangeCheck();
            flag = EditorGUI.Toggle(position, flag);
            if (labelClicked)
            {
                flag = !flag;
                GUI.changed = true;
            }

            if(EditorGUI.EndChangeCheck())
            {
                UpdateConfig(config, flag);
            }
        }

        internal override bool Highlighted(Config config)
        {
            bool hasFlag = config.HasFlag(block, keyword);
            return negative ? !hasFlag : hasFlag;
        }

        void UpdateConfig(Config config, bool flag)
        {
            config.ToggleFlag(block, keyword, negative ? !flag : flag);

            if (toggles != null)
            {
                foreach (var t in toggles)
                {
                    config.ToggleFeature(t, negative ? !flag : flag);
                }
            }
        }
    }
}
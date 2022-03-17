using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Single : UIFeature
    {
        string keyword;
        string[] toggles;    //features forced to be toggled when this feature is enabled

        internal UIFeature_Single(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "kw")
                keyword = value;
            else if(key == "toggles")
                toggles = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            var feature = Highlighted(config);
            EditorGUI.BeginChangeCheck();
            feature = EditorGUI.Toggle(position, feature);
            if (labelClicked)
            {
                feature = !feature;
                GUI.changed = true;
            }
            if(EditorGUI.EndChangeCheck())
            {
                config.ToggleFeature(keyword, feature);

                if(toggles != null)
                {
                    foreach(var t in toggles)
                        config.ToggleFeature(t, feature);
                }
            }
        }

        internal override bool Highlighted(Config config)
        {
            return config.HasFeature(keyword);
        }
    }
}
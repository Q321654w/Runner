using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Multiple : UIFeature
    {
        string[] labels;
        string[] features;
        string[] toggles;    //features forced to be toggled when this feature is enabled

        internal UIFeature_Multiple(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "kw")
            {
                var data = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                labels = new string[data.Length];
                features = new string[data.Length];

                for(var i = 0; i < data.Length; i++)
                {
                    var lbl_feat = data[i].Split('|');
                    if(lbl_feat.Length != 2)
                    {
                        Debug.LogWarning("[UIFeature_Multiple] Invalid data:" + data[i]);
                        continue;
                    }

                    labels[i] = lbl_feat[0];
                    features[i] = lbl_feat[1];
                }
            }
            else if(key == "toggles")
                toggles = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            var feature = GetSelectedFeature(config);
            if(feature < 0) feature = 0;

            EditorGUI.BeginChangeCheck();
            feature = EditorGUI.Popup(position, feature, labels);
            if(EditorGUI.EndChangeCheck())
            {
                ToggleSelectedFeature(config, feature);
            }
        }

        int GetSelectedFeature(Config config)
        {
            for(var i = 0; i < features.Length; i++)
            {
                if(config.HasFeature(features[i]))
                    return i;
            }

            return -1;
        }

        internal override bool Highlighted(Config config)
        {
            var feature = GetSelectedFeature(config);
            return feature > 0;
        }

        protected override void OnEnabledChangedState(Config config, bool newState)
        {
            var feature = -1;
            if(newState)
            {
                feature = GetSelectedFeature(config);
                if(feature < 0) feature = 0;
            }

            ToggleSelectedFeature(config, feature);
        }

        void ToggleSelectedFeature(Config config, int selectedFeature)
        {
            for(var i = 0; i < features.Length; i++)
            {
                var enable = (i == selectedFeature);
                config.ToggleFeature(features[i], enable);
            }

            if(toggles != null)
            {
                foreach(var t in toggles)
                    config.ToggleFeature(t, selectedFeature > 0);
            }
        }
    }
}
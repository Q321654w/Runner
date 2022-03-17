using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_MultipleFixedFunction : UIFeature
    {
        string keyword;
        string[] labels;
        string[] fixedFunctionValues;
        string shaderPropertyName;
        ShaderProperty shaderProperty;

        internal UIFeature_MultipleFixedFunction(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            if (key == "kw")
            {
                keyword = value;
            }
            else if (key == "options")
            {
                var data = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                labels = new string[data.Length];
                fixedFunctionValues = new string[data.Length];

                for (var i = 0; i < data.Length; i++)
                {
                    var lbl_feat = data[i].Split('|');
                    if (lbl_feat.Length != 2)
                    {
                        Debug.LogWarning("[UIFeature_MultipleFixedFunction] Invalid data:" + data[i]);
                        continue;
                    }

                    labels[i] = lbl_feat[0];
                    fixedFunctionValues[i] = lbl_feat[1];
                }
            }
            else if (key == "shader_property")
            {
                shaderPropertyName = value.Replace("\"", "");
            }
            else
            {
                base.ProcessProperty(key, value);
            }
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            // Fetch embedded Shader Property
            bool highlighted = Highlighted(config);
            if (shaderProperty == null && highlighted) // the SP only exists if the feature is enabled
            {
                var match = Array.Find(config.AllShaderProperties, sp => sp.Name == shaderPropertyName);
                if (match == null)
                {
                    Debug.LogError(ShaderGenerator2.ErrorMsg("Can't find matching embedded Shader Property with name: '" + shaderPropertyName + "'"));
                }
                shaderProperty = match;
            }

            int feature = highlighted ? (shaderProperty.implementations[0] as ShaderProperty.Imp_Enum).ValueType + 1 : 0;
            if (feature < 0) feature = 0;

            EditorGUI.BeginChangeCheck();
            feature = EditorGUI.Popup(position, feature, labels);
            if (EditorGUI.EndChangeCheck())
            {
                config.ToggleFeature(keyword, feature > 0);

                // Update Fixed Function value type
                var ffv = fixedFunctionValues[feature];
                if (feature > 0 && !string.IsNullOrEmpty(ffv) && shaderProperty != null)
                {
                    (shaderProperty.implementations[0] as ShaderProperty.Imp_Enum).SetValueTypeFromString(ffv);
                    shaderProperty.CheckHash();
                    shaderProperty.CheckErrors();
                }
            }

            // Show embedded Shader Property UI
            if (highlighted && shaderProperty != null)
            {
                if (shaderProperty.Type != ShaderProperty.VariableType.fixed_function_enum)
                {
                    EditorGUILayout.HelpBox("Embedded Shader Property should be a Fixed Function enum.", MessageType.Error);
                }
                else
                {
                    var imp = shaderProperty.implementations[0] as ShaderProperty.Imp_Enum;
                    if (imp == null)
                    {
                        EditorGUILayout.HelpBox("First implementation of enum Shader Property isn't an Imp_Enum.", MessageType.Error);
                    }
                    else
                    {
                        EditorGUI.BeginChangeCheck();
                        {
                            imp.EmbeddedGUI(28, 170);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            shaderProperty.CheckHash();
                            shaderProperty.CheckErrors();
                        }
                    }
                }
            }
        }

        internal override bool Highlighted(Config config)
        {
            return config.HasFeature(keyword);
        }

        /*
			protected override void OnEnabledChangedState(Config config, bool newState)
			{
				var feature = -1;
				if (newState)
				{
					feature = GetSelectedFeature(config);
					if (feature < 0) feature = 0;
				}

				ToggleSelectedFeature(config, feature);
			}
			*/
    }
}
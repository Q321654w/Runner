using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_SubHeader : UIFeature
    {
        internal UIFeature_SubHeader(List<KeyValuePair<string, string>> list) : base(list)
        {
            customGUI = true;
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            if (this.helpTopic != null)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    TCP2_GUI.HelpButtonSG2(this.helpTopic);
                    TCP2_GUI.SubHeaderGray(label);
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                TCP2_GUI.SubHeaderGray(label);
            }
        }
    }
}
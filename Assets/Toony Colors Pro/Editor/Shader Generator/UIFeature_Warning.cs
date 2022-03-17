using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Warning : UIFeature
    {
        MessageType msgType = MessageType.Warning;

        internal UIFeature_Warning(List<KeyValuePair<string, string>> list) : base(list)
        {
            customGUI = true;
        }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "msgType")
                msgType = (MessageType)Enum.Parse(typeof(MessageType), value, true);
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            if(Enabled(config))
            {
                //EditorGUILayout.HelpBox(this.label, msgType);
                TCP2_GUI.HelpBoxLayout(label, msgType);
            }
        }
    }
}
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Header : UIFeature
    {
        internal UIFeature_Header(List<KeyValuePair<string, string>> list) : base(list)
        {
            customGUI = true;
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            TCP2_GUI.Header(label);
        }
    }
}
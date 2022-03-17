using System.Collections.Generic;
using System.Globalization;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Space : UIFeature
    {
        float space = 8f;

        internal UIFeature_Space(List<KeyValuePair<string, string>> list) : base(list)
        {
            customGUI = true;
        }

        protected override void ProcessProperty(string key, string value)
        {
            if(key == "space")
                space = float.Parse(value, CultureInfo.InvariantCulture);
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            if(Enabled(config))
                GUILayout.Space(space);
        }
    }
}
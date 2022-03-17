using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_Separator : UIFeature
    {
        internal UIFeature_Separator() : base(null)
        {
            customGUI = true;
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            TCP2_GUI.SeparatorSimple();
        }
    }
}
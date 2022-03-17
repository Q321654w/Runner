using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_Header : GUICommand
    {
        public string label { get; set; }
        GUIContent guiContent;

        public override void OnGUI()
        {
            if (guiContent == null)
                guiContent = new GUIContent(label);

            if (MaterialInspector_Hybrid.ShowNextProperty)
                GUILayout.Label(guiContent, SGUILayout.Styles.OrangeBoldLabel);
        }
    }
}
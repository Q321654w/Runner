using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_Label : GUICommand
    {
        public string label { get; set; }
        GUIContent guiContent;

        public override void OnGUI()
        {
            if (guiContent == null)
                guiContent = new GUIContent(label);

            if (MaterialInspector_Hybrid.ShowNextProperty)
                GUILayout.Label(guiContent);
        }
    }
}
using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_Space : GUICommand { public override void OnGUI() { if (MaterialInspector_Hybrid.ShowNextProperty) GUILayout.Space(8); } }
}
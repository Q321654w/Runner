using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_Separator : GUICommand { public override void OnGUI() { if (MaterialInspector_Hybrid.ShowNextProperty) TCP2_GUI.SeparatorSimple(); } }
}
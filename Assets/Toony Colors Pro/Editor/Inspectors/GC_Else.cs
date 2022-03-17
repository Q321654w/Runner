using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_Else : GUICommand
    {
        public override void OnGUI()
        {
            bool invertCondition = !MaterialInspector_Hybrid.ShowNextProperty;
            MaterialInspector_Hybrid.PopShowProperty();
            MaterialInspector_Hybrid.PushShowProperty(invertCondition);
        }
    }
}
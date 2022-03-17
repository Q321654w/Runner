using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_IfURP : GUICommand
    {
        bool needUrp;

        public GC_IfURP(bool urp = true)
        {
            needUrp = urp;
        }

        public override void OnGUI()
        {
            bool show = MaterialInspector_Hybrid._isURP;
            if (!needUrp) show = !show;
            MaterialInspector_Hybrid.PushShowProperty(show);
        }
    }
}
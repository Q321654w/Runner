using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_HelpBox : GUICommand
    {
        public string message { get; set; }
        public MessageType messageType { get; set; }

        public override void OnGUI()
        {
            if (MaterialInspector_Hybrid.ShowNextProperty)
                TCP2_GUI.HelpBoxLayout(message, messageType);
        }
    }
}
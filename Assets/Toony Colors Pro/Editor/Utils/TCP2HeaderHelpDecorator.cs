using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2HeaderHelpDecorator : MaterialPropertyDrawer
    {
        protected readonly string header;
        protected readonly string help;

        public TCP2HeaderHelpDecorator(string header)
        {
            this.header = header;
            help = null;
        }
        public TCP2HeaderHelpDecorator(string header, string help)
        {
            this.header = header;
            this.help = help;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            TCP2_GUI.HeaderAndHelp(position, header, null, help);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return 18f;
        }
    }
}
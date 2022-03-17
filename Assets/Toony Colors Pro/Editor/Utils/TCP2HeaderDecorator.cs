using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2HeaderDecorator : MaterialPropertyDrawer
    {
        protected readonly string header;

        public TCP2HeaderDecorator(string header)
        {
            this.header = header;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            position.y += 2;
            TCP2_GUI.Header(position, header);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return 18f;
        }
    }
}
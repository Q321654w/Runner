using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2SeparatorDecorator : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            position.y += 4;
            position.height -= 4;
            TCP2_GUI.Separator(position);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return 12f;
        }
    }
}
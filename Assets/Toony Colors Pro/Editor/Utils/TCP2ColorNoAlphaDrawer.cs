using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2ColorNoAlphaDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            //Code from ColorPropertyInternal, but with alpha turned off
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            bool hdr = (prop.flags & MaterialProperty.PropFlags.HDR) != MaterialProperty.PropFlags.None;
            bool showAlpha = false;
#if UNITY_2018_1_OR_NEWER
            Color colorValue = EditorGUI.ColorField(position, label, prop.colorValue, true, showAlpha, hdr);
#else
				Color colorValue = EditorGUI.ColorField(position, label, prop.colorValue, true, showAlpha, hdr, null);
#endif
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.colorValue = colorValue;
            }
        }
    }
}
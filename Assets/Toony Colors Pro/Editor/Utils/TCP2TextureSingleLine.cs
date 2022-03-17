using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    internal class TCP2TextureSingleLine : MaterialPropertyDrawer
    {
        private static bool IsPropertyTypeSuitable(MaterialProperty prop)
        {
            return prop.type == MaterialProperty.PropType.Texture;
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            float result;
            if (!IsPropertyTypeSuitable(prop))
            {
                result = 40f;
            }
            else
            {
                result = base.GetPropertyHeight(prop, label, editor);
            }
            return result;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (!IsPropertyTypeSuitable(prop))
            {
                EditorGUI.HelpBox(position, "TextureSingleLine used on a non-texture property: " + prop.name, MessageType.Warning);
            }
            else
            {
                EditorGUI.showMixedValue = prop.hasMixedValue;
                editor.TexturePropertyMiniThumbnail(position, prop, label.text, label.tooltip);
                EditorGUI.showMixedValue = false;
            }
        }
    }
}
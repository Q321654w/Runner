using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    internal class TCP2UVScrolling : MaterialPropertyDrawer
    {
        private static readonly GUIContent[] s_UVLabels = new GUIContent[]
        {
            new GUIContent("U"),
            new GUIContent("V")
        };

        private static readonly float[] s_UVValues = new float[]
        {
            0,
            0
        };

        private static bool IsPropertyTypeSuitable(MaterialProperty prop)
        {
            return prop.type == MaterialProperty.PropType.Vector;
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
                position = EditorGUI.PrefixLabel(position, label);
                EditorGUI.BeginChangeCheck();
                s_UVValues[0] = prop.vectorValue.x;
                s_UVValues[1] = prop.vectorValue.y;
                EditorGUI.MultiFloatField(position, s_UVLabels, s_UVValues);
                if (EditorGUI.EndChangeCheck())
                {
                    var v4 = prop.vectorValue;
                    v4.x = s_UVValues[0];
                    v4.y = s_UVValues[1];
                    prop.vectorValue = v4;
                }

                EditorGUI.showMixedValue = false;
            }
        }
    }
}
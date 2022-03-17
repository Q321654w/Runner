using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    internal class TCP2ToggleNoKeywordDrawer : MaterialPropertyDrawer
    {
        private static bool IsPropertyTypeSuitable(MaterialProperty prop)
        {
            return prop.type == MaterialProperty.PropType.Float || prop.type == MaterialProperty.PropType.Range;
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
                EditorGUI.HelpBox(position, "Toggle used on a non-float property: " + prop.name, MessageType.Warning);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                var flag = Mathf.Abs(prop.floatValue) > 0.001f;
                EditorGUI.showMixedValue = prop.hasMixedValue;
                flag = EditorGUI.Toggle(position, label, flag);
                EditorGUI.showMixedValue = false;
                if (EditorGUI.EndChangeCheck())
                {
                    prop.floatValue = ((!flag) ? 0f : 1f);
                }
            }
        }
    }
}
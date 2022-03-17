using System;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    internal class TCP2HeaderToggleDrawer : MaterialPropertyDrawer
    {
        protected readonly string keyword;

        public TCP2HeaderToggleDrawer()
        {
            keyword = null;
        }

        public TCP2HeaderToggleDrawer(string keyword)
        {
            this.keyword = keyword;
        }

        private static bool IsPropertyTypeSuitable(MaterialProperty prop)
        {
            return prop.type == MaterialProperty.PropType.Float || prop.type == MaterialProperty.PropType.Range;
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            if (!IsPropertyTypeSuitable(prop))
            {
                return 40f;
            }
            return base.GetPropertyHeight(prop, label, editor);
        }

        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (!IsPropertyTypeSuitable(prop))
            {
                EditorGUI.HelpBox(position, "Toggle used on a non-float property: " + prop.name, MessageType.Warning);
                return;
            }
            EditorGUI.BeginChangeCheck();
            bool value = Math.Abs(prop.floatValue) > 0.001f;
            EditorGUI.showMixedValue = prop.hasMixedValue;

            var guiColor = GUI.color;
            var guiColorA = guiColor;
            guiColorA.a = 0.5f;
            GUI.color = value ? guiColor : guiColorA;
            Rect toggleRect = EditorGUI.PrefixLabel(position, label, SGUILayout.Styles.OrangeBoldLabel);
            GUI.color = guiColor;
            value = EditorGUI.Toggle(toggleRect, GUIContent.none, value);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = ((!value) ? 0f : 1f);
                SetKeyword(prop, value);
            }
        }

        public override void Apply(MaterialProperty prop)
        {
            base.Apply(prop);
            if (IsPropertyTypeSuitable(prop) && !prop.hasMixedValue)
            {
                SetKeyword(prop, Math.Abs(prop.floatValue) > 0.001f);
            }
        }

        protected void SetKeyword(MaterialProperty prop, bool on)
        {
            if (string.IsNullOrEmpty(keyword)) return;

            UnityEngine.Object[] targets = prop.targets;
            for (int i = 0; i < targets.Length; i++)
            {
                Material material = (Material)targets[i];
                if (on)
                {
                    material.EnableKeyword(keyword);
                }
                else
                {
                    material.DisableKeyword(keyword);
                }
            }
        }
    }
}
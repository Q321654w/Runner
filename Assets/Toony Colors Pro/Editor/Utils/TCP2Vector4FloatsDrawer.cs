using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2Vector4FloatsDrawer : MaterialPropertyDrawer
    {
        const float spacing = 2f;

        bool expanded;
        string[] labels;
        float[] min;
        float[] max;
        bool useSlider;
        protected int channelsCount;

        public TCP2Vector4FloatsDrawer(string labelX, string labelY, string labelZ, string labelW)
        {
            labels = new[] { labelX, labelY, labelZ, labelW };
            min = new[] { float.MinValue, float.MinValue, float.MinValue, float.MinValue };
            max = new[] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };
            useSlider = false;
            expanded = false;
            channelsCount = 4;
        }
        public TCP2Vector4FloatsDrawer(string labelX, string labelY, string labelZ, string labelW, float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float minW, float maxW)
        {
            labels = new[] { labelX, labelY, labelZ, labelW };
            min = new[] { SignedValue(minX), SignedValue(minY), SignedValue(minZ), SignedValue(minW) };
            max = new[] { SignedValue(maxX), SignedValue(maxY), SignedValue(maxZ), SignedValue(maxW) };
            useSlider = true;
            expanded = false;
            channelsCount = 4;
        }

        //hacky workaround because adding a minus sign in a material drawer argument will break the shader
        float SignedValue(float val)
        {
            return val > 90000 ? 90000 - val : val;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            var lineRect = position;
            lineRect.x += 12;
            lineRect.width -= 12;
            lineRect.height = EditorGUIUtility.singleLineHeight;

            var values = prop.vectorValue;
            EditorGUI.BeginChangeCheck();
            expanded = EditorGUI.Foldout(lineRect, expanded, label, true);
            if (expanded)
            {
                lineRect.y += lineRect.height + spacing;

                var lw = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = position.width - 200f;

                if (channelsCount > 0)
                {
                    values.x = useSlider ? EditorGUI.Slider(lineRect, labels[0], values.x, min[0], max[0]) : EditorGUI.FloatField(lineRect, labels[0], values.x);
                    lineRect.y += lineRect.height + spacing;
                }

                if (channelsCount > 1)
                {
                    values.y = useSlider ? EditorGUI.Slider(lineRect, labels[1], values.y, min[1], max[1]) : EditorGUI.FloatField(lineRect, labels[1], values.y);
                    lineRect.y += lineRect.height + spacing;
                }

                if (channelsCount > 2)
                {
                    values.z = useSlider ? EditorGUI.Slider(lineRect, labels[2], values.z, min[2], max[2]) : EditorGUI.FloatField(lineRect, labels[2], values.z);
                    lineRect.y += lineRect.height + spacing;
                }

                if (channelsCount > 3)
                {
                    values.w = useSlider ? EditorGUI.Slider(lineRect, labels[3], values.w, min[3], max[3]) : EditorGUI.FloatField(lineRect, labels[3], values.w);
                }

                EditorGUIUtility.labelWidth = lw;
            }
            if (EditorGUI.EndChangeCheck())
            {
                prop.vectorValue = values;
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return (EditorGUIUtility.singleLineHeight+spacing) * (expanded ? (channelsCount+1) : 1) - spacing;
        }
    }
}
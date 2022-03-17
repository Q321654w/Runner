using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    internal class MaterialTCP2EnumDrawer : MaterialPropertyDrawer
    {
        private readonly GUIContent[] names;
        private readonly float[] values;

        public MaterialTCP2EnumDrawer(string n1, float v1) : this(new string[] { n1 }, new float[] { v1 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2) : this(new string[] { n1, n2 }, new float[] { v1, v2 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3) : this(new string[] { n1, n2, n3 }, new float[] { v1, v2, v3 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4) : this(new string[] { n1, n2, n3, n4 }, new float[] { v1, v2, v3, v4 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5) : this(new string[] { n1, n2, n3, n4, n5 }, new float[] { v1, v2, v3, v4, v5 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6) : this(new string[] { n1, n2, n3, n4, n5, n6 }, new float[] { v1, v2, v3, v4, v5, v6 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7) : this(new string[] { n1, n2, n3, n4, n5, n6, n7 }, new float[] { v1, v2, v3, v4, v5, v6, v7 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9, string n10, float v10) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9, string n10, float v10, string n11, float v11) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9, string n10, float v10, string n11, float v11, string n12, float v12) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9, string n10, float v10, string n11, float v11, string n12, float v12, string n13, float v13) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9, string n10, float v10, string n11, float v11, string n12, float v12, string n13, float v13, string n14, float v14) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9, string n10, float v10, string n11, float v11, string n12, float v12, string n13, float v13, string n14, float v14, string n15, float v15) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15 }) { }
        public MaterialTCP2EnumDrawer(string n1, float v1, string n2, float v2, string n3, float v3, string n4, float v4, string n5, float v5, string n6, float v6, string n7, float v7, string n8, float v8, string n9, float v9, string n10, float v10, string n11, float v11, string n12, float v12, string n13, float v13, string n14, float v14, string n15, float v15, string n16, float v16) : this(new string[] { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16 }, new float[] { v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16 }) { }

        public MaterialTCP2EnumDrawer(string[] enumNames, float[] vals)
        {
            this.names = new GUIContent[enumNames.Length];
            for (int i = 0; i < enumNames.Length; i++)
            {
                this.names[i] = new GUIContent(enumNames[i]);
            }
            this.values = new float[vals.Length];
            for (int j = 0; j < vals.Length; j++)
            {
                this.values[j] = vals[j];
            }
        }
        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            float result;
            if (prop.type != MaterialProperty.PropType.Float && prop.type != MaterialProperty.PropType.Range)
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
            if (prop.type != MaterialProperty.PropType.Float && prop.type != MaterialProperty.PropType.Range)
            {
                EditorGUI.HelpBox(position, "Enum used on a non-float property: " + prop.name, MessageType.Warning);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                EditorGUI.showMixedValue = prop.hasMixedValue;
                float floatValue = prop.floatValue;
                int selectedIndex = -1;
                for (int i = 0; i < this.values.Length; i++)
                {
                    float num = this.values[i];
                    if (num == floatValue)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
                int num2 = EditorGUI.Popup(position, label, selectedIndex, this.names);
                EditorGUI.showMixedValue = false;
                if (EditorGUI.EndChangeCheck())
                {
                    prop.floatValue = this.values[num2];
                }
            }
        }
    }
}
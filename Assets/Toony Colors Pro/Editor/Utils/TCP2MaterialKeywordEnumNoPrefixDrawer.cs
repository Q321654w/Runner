using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    internal class TCP2MaterialKeywordEnumNoPrefixDrawer : MaterialPropertyDrawer
    {
        private readonly GUIContent[] labels;
        private readonly string[] keywords;

        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1) : this(new[] { lbl1 }, new[] { kw1 }) { }
        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1, string lbl2, string kw2) : this(new[] { lbl1, lbl2 }, new[] { kw1, kw2 }) { }
        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1, string lbl2, string kw2, string lbl3, string kw3) : this(new[] { lbl1, lbl2, lbl3 }, new[] { kw1, kw2, kw3 }) { }
        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1, string lbl2, string kw2, string lbl3, string kw3, string lbl4, string kw4) : this(new[] { lbl1, lbl2, lbl3, lbl4 }, new[] { kw1, kw2, kw3, kw4 }) { }
        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1, string lbl2, string kw2, string lbl3, string kw3, string lbl4, string kw4, string lbl5, string kw5) : this(new[] { lbl1, lbl2, lbl3, lbl4, lbl5 }, new[] { kw1, kw2, kw3, kw4, kw5 }) { }
        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1, string lbl2, string kw2, string lbl3, string kw3, string lbl4, string kw4, string lbl5, string kw5, string lbl6, string kw6) : this(new[] { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6 }, new[] { kw1, kw2, kw3, kw4, kw5, kw6 }) { }
        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1, string lbl2, string kw2, string lbl3, string kw3, string lbl4, string kw4, string lbl5, string kw5, string lbl6, string kw6, string lbl7, string kw7) : this(new[] { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7 }, new[] { kw1, kw2, kw3, kw4, kw5, kw6, kw7 }) { }
        public TCP2MaterialKeywordEnumNoPrefixDrawer(string lbl1, string kw1, string lbl2, string kw2, string lbl3, string kw3, string lbl4, string kw4, string lbl5, string kw5, string lbl6, string kw6, string lbl7, string kw7, string lbl8, string kw8) : this(new[] { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8 }, new[] { kw1, kw2, kw3, kw4, kw5, kw6, kw7, kw8 }) { }

        public TCP2MaterialKeywordEnumNoPrefixDrawer(string[] labels, string[] keywords)
        {
            this.labels= new GUIContent[keywords.Length];
            this.keywords = new string[keywords.Length];
            for (int i = 0; i < keywords.Length; ++i)
            {
                this.labels[i] = new GUIContent(labels[i]);
                this.keywords[i] = keywords[i];
            }
        }

        static bool IsPropertyTypeSuitable(MaterialProperty prop)
        {
            return prop.type == MaterialProperty.PropType.Float || prop.type == MaterialProperty.PropType.Range;
        }

        void SetKeyword(MaterialProperty prop, int index)
        {
            for (int i = 0; i < keywords.Length; ++i)
            {
                string keyword = GetKeywordName(prop.name, keywords[i]);
                foreach (Material material in prop.targets)
                {
                    if (keyword == "_")
                    {
                        continue;
                    }

                    if (index == i)
                        material.EnableKeyword(keyword);
                    else
                        material.DisableKeyword(keyword);
                }
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            if (!IsPropertyTypeSuitable(prop))
            {
                return EditorGUIUtility.singleLineHeight * 2.5f;
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

            EditorGUI.showMixedValue = prop.hasMixedValue;
            var value = (int)prop.floatValue;
            value = EditorGUI.Popup(position, label, value, labels);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value;
                SetKeyword(prop, value);
            }
        }

        public override void Apply(MaterialProperty prop)
        {
            base.Apply(prop);
            if (!IsPropertyTypeSuitable(prop))
                return;

            if (prop.hasMixedValue)
                return;

            SetKeyword(prop, (int)prop.floatValue);
        }

        // Final keyword name: property name + "_" + display name. Uppercased,
        // and spaces replaced with underscores.
        private static string GetKeywordName(string propName, string name)
        {
            // Just return the supplied name
            return name;

            // Original code:
            /*
				string n = propName + "_" + name;
				return n.Replace(' ', '_').ToUpperInvariant();
				*/
        }
    }
}
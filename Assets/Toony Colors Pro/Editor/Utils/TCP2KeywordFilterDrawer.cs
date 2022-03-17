using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2KeywordFilterDrawer : MaterialPropertyDrawer
    {
        protected readonly string[] keywords;

        public TCP2KeywordFilterDrawer(string keyword)
        {
            keywords = keyword.Split(',');
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            if (IsValid(editor))
            {
                EditorGUI.indentLevel++;
                editor.DefaultShaderProperty(prop, label);
                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            //There's still a small space if we return 0, -2 seems to get rid of that
            return -2f;
        }

        bool IsValid(MaterialEditor editor)
        {
            var valid = false;
            if (editor.target != null && editor.target is Material)
            {
                foreach (var kw in keywords)
                    valid |= (editor.target as Material).IsKeywordEnabled(kw);
            }
            return valid;
        }
    }
}
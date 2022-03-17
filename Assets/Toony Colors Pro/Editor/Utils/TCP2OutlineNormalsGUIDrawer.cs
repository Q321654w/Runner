using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2OutlineNormalsGUIDrawer : MaterialPropertyDrawer
    {
        readonly GUIContent[] labels = {
            new GUIContent("Regular", "Use regular vertex normals"),
            new GUIContent("Vertex Colors", "Use vertex colors as normals (with smoothed mesh)"),
            new GUIContent("Tangents", "Use tangents as normals (with smoothed mesh)"),
            new GUIContent("UV2", "Use second texture coordinates as normals (with smoothed mesh)")
        };
        readonly string[] keywords = { "TCP2_NONE", "TCP2_COLORS_AS_NORMALS", "TCP2_TANGENT_AS_NORMALS", "TCP2_UV2_AS_NORMALS" };

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            TCP2_GUI.Header("Outline Normals Source", "Defines where to take the vertex normals from to draw the outline.\nChange this when using a smoothed mesh to fill the gaps shown in hard-edged meshes.");

            var r = EditorGUILayout.GetControlRect();
            r = EditorGUI.IndentedRect(r);
            var index = GetCurrentIndex(prop);
            EditorGUI.BeginChangeCheck();
            index = TCP2_GUI.RadioChoiceHorizontal(r, index, labels);
            if (EditorGUI.EndChangeCheck())
            {
                SetKeyword(prop, index);
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return 0f;
        }

        int GetCurrentIndex(MaterialProperty prop)
        {
            var index = 0;
            var targets = prop.targets;
            foreach (var t in targets)
            {
                var m = (Material)t;
                for (var i = 0; i < keywords.Length; i++)
                {
                    if (m.IsKeywordEnabled(keywords[i]))
                    {
                        return i;
                    }
                }
            }
            return index;
        }

        private void SetKeyword(MaterialProperty prop, int index)
        {
            var label = prop.targets.Length > 1 ? string.Format("modify Outline Normals of {0} Materials", prop.targets.Length) : string.Format("modify Outline Normals of {0}", prop.targets[0].name);
            Undo.RecordObjects(prop.targets, label);
            for (var i = 0; i < keywords.Length; i++)
            {
                var keywordName = keywords[i];
                var targets = prop.targets;
                for (var j = 0; j < targets.Length; j++)
                {
                    var material = (Material)targets[j];
                    if (index == i)
                    {
                        material.EnableKeyword(keywordName);
                    }
                    else
                    {
                        material.DisableKeyword(keywordName);
                    }
                }
            }
        }
    }
}
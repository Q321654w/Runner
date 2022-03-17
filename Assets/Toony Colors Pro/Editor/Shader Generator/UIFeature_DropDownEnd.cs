using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_DropDownEnd : UIFeature
    {
        internal UIFeature_DropDownEnd() : base(null)
        {
            customGUI = true;
            ignoreVisibility = true;
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            FoldoutStack.Pop();

            EditorGUILayout.EndVertical();
        }
    }
}
// Toony Colors Pro+Mobile 2
// (c) 2014-2020 Jean Moreno

using Toony_Colors_Pro.Scripts.Runtime;
using UnityEditor;

// Script to get the water height from a specific world position
// Useful to easily make objects float on water for example

namespace Toony_Colors_Pro.Editor.Inspectors
{
	namespace Inspector
	{
		[CustomEditor(typeof(TCP2_GetPosOnWater)), CanEditMultipleObjects]
		public class TCP2_GetPosOnWater_Editor : UnityEditor.Editor
		{
			public override void OnInspectorGUI()
			{
				//base.OnInspectorGUI();

				EditorGUILayout.HelpBox("Use this script with a Water Template-generated shader to get the water height at a specific world point.\n\nMake sure that the shader has the following features enabled:\n- Custom Time\n- Vertex Waves\n- World-based Position\n\nMake sure to also use the TCP2_ShaderUpdateUnityTime script!", MessageType.Info);
				base.OnInspectorGUI();
			}
		}
	}
}
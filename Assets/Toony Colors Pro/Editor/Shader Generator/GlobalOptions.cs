// Toony Colors Pro 2
// (c) 2014-2020 Jean Moreno

using UnityEditor;

// Represents the global options for the Shader Generator, using the EditorPrefs API

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
	namespace ShaderGenerator
	{
		// Global Options shared across all Unity projects
		public static class GlobalOptions
		{
			[System.Serializable]
			public class Data
			{
				public bool ShowOptions = true;
				public bool ShowDisabledFeatures = true;
				public bool SelectGeneratedShader = true;
				public bool ShowContextualHelp = true;
				public bool DockableWindow = false;
			}
			static Data _data;
			public static Data data
			{
				get
				{
					if (_data == null)
					{
						LoadUserPrefs();
					}
					return _data;
				}
			}

			public static void LoadUserPrefs()
			{
				string dataStr = EditorPrefs.GetString("TCP2_GlobalOptions", null);
				_data = new Data();
				if (!string.IsNullOrEmpty(dataStr))
				{
					EditorJsonUtility.FromJsonOverwrite(dataStr, _data);
				}
			}

			public static void SaveUserPrefs()
			{
				EditorPrefs.SetString("TCP2_GlobalOptions", EditorJsonUtility.ToJson(data));
			}
		}

		// Project Options only saved for this Unity project
	}
}
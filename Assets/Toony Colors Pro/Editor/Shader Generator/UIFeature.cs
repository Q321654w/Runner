// Toony Colors Pro 2
// (c) 2014-2020 Jean Moreno

using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
	namespace ShaderGenerator
	{
		// Utility to generate custom Toony Colors Pro 2 shaders with specific features

		//--------------------------------------------------------------------------------------------------
		// UI from Template System

		internal class UIFeature
		{
			protected const float LABEL_WIDTH = 210f;
			static Rect LastPositionInline;
			static float LastLowerBoundY;
			static float LastIndentY;
			static int LastIndent;
			static bool LastVisible;

			static GUIContent tempContent = new GUIContent();
			protected static GUIContent TempContent(string label, string tooltip = null)
			{
				tempContent.text = label;
				tempContent.tooltip = tooltip;
				return tempContent;
			}

			protected string label;
			protected string tooltip;
			protected string[] requires;    //features required for this feature to be enabled (AND)
			protected string[] requiresOr;  //features required for this feature to be enabled (OR)
			protected string[] excludes;   //features required to be OFF for this feature to be enabled
			protected string[] excludesAll;   //features required to be OFF for this feature to be enabled
			protected string[] visibleIf;   //features required to be ON for this feature to be visible
			protected bool showHelp = false;
			protected int indentLevel;
			protected string helpTopic;
			protected bool customGUI;   //complete custom GUI that overrides the default behaviors (e.g. separator)
			protected bool ignoreVisibility;   //ignore the current visible state and force the UI element to be drawn
			bool wasEnabled;    //track when the Enabled flag changes
			bool inline;        //draw next to previous position
			bool halfWidth;     //draw in half space of the position (for inline)

			UIFeature parent; // simple hierarchy system to handle visibility and vertical/horizontal line hierarchy drawing

			protected static Stack<bool> FoldoutStack = new Stack<bool>();
			internal static void ClearFoldoutStack()
			{
				UIFeature_DropDownStart.ClearDropDownsList();
				FoldoutStack.Clear();
			}

			//Initialize a UIFeature given a list of arbitrary properties
			internal UIFeature(List<KeyValuePair<string, string>> list)
			{
				if(list != null)
				{
					foreach(var kvp in list)
					{
						ProcessProperty(kvp.Key, kvp.Value);
					}
				}
			}

			//Process a property from the Template in the form key=value
			protected virtual void ProcessProperty(string key, string value)
			{
				//Direct inline properties, no need for a value
				if(string.IsNullOrEmpty(value))
				{
					switch(key)
					{
						case "nohelp": showHelp = false; break;
						case "indent": indentLevel = 1; break;
						case "inline": inline = true; break;
						case "half": halfWidth = true; break;
						case "help": showHelp = true; break;
					}
				}
				else
				{
					//Common properties to all UIFeature classes
					switch(key)
					{
						case "lbl": label = value.Replace("  ", "\n").Trim('"'); break;
						case "tt": tooltip = value.Replace(@"\n", "\n").Replace("  ", "\n").Trim('"'); break;
						case "help": showHelp = true; helpTopic = value; break;
						case "indent": indentLevel = int.Parse(value); break;
						case "needs": requires = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries); break;
						case "needsOr": requiresOr = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries); break;
						case "excl": excludes = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries); break;
						case "exclAll": excludesAll = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries); break;
						case "visibleIf": visibleIf = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries); break;
						case "inline": inline = bool.Parse(value); break;
						case "half": halfWidth = bool.Parse(value); break;
					}
				}
			}

			static Rect HeaderRect(ref Rect lineRect, float width)
			{
				var rect = lineRect;
				rect.width = width;

				lineRect.x += rect.width;
				lineRect.width -= rect.width;

				return rect;
			}

			// temp state between each DrawGUI, so that children don't have to
			// re-fetch them with the Enabled() and Highlighted() methods
			bool enabled;
			bool highlighted;
			internal void DrawGUI(Config config)
			{
				bool guiEnabled = GUI.enabled;

				// update states
				this.enabled = Enabled(config);
				this.highlighted = Highlighted(config);

				GUI.enabled = enabled;

				// by default, only show if top-level
				bool visible = indentLevel == 0;
				// if set, show all
				if (GlobalOptions.data.ShowDisabledFeatures)
				{
					visible = true;
				}
				// else, show only if parent is enabled & highlighted
				else if (indentLevel > 0 && parent != null)
				{
					if (visibleIf != null && visibleIf.Length > 0)
					{
						visible = config.HasFeaturesAll(visibleIf);
					}
					else
					{
						visible = parent.enabled && parent.highlighted;
					}
				}

				if(inline)
					visible = LastVisible;

				visible &= (FoldoutStack.Count > 0) ? FoldoutStack.Peek() : true;

				ForceValue(config);

				if(customGUI)
				{
					if(visible || ignoreVisibility)
					{
						DrawGUI(new Rect(0, 0, EditorGUIUtility.currentViewWidth, 0), config, false);
						return;
					}
				}
				else if(visible)
				{
					//Total line rect
					Rect position;
					position = inline ? LastPositionInline : EditorGUILayout.GetControlRect();

					if(halfWidth)
					{
						position.width = (position.width/2f) - 8f;
					}

					//LastPosition is already halved
					if(inline)
					{
						position.x += position.width + 16f;
					}

					//Last Position for inlined properties
					LastPositionInline = position;

					if(!inline)
					{
						//Help
						if(showHelp)
						{
							var helpRect = HeaderRect(ref position, 20f);
							TCP2_GUI.HelpButtonSG2(helpRect, label, string.IsNullOrEmpty(helpTopic) ? label : helpTopic);
						}
						else
						{
							HeaderRect(ref position, 20f);
						}

						const float barIndent = 2;	// pixels for vertical bar indent
						const float uiIndent = 8;	// pixels per indent level for UI

						var horizontalRect = position;
						var lineColor = Color.black * (EditorGUIUtility.isProSkin ? 0.3f : 0.2f);
						for (int i = 1; i <= indentLevel; i++)
						{
							// vertical bar to the left of indented lines
							horizontalRect = position;
							if (indentLevel > 0 && Event.current.type == EventType.Repaint)
							{
								var verticalRect = position;
								verticalRect.width = 1;
								verticalRect.x += barIndent;
								verticalRect.yMax -= 7;
								verticalRect.yMin = (indentLevel <= 0 || i > LastIndent) ? LastLowerBoundY : LastIndentY;
								EditorGUI.DrawRect(verticalRect, lineColor);
							}

							// indent
							HeaderRect(ref position, uiIndent);

							// horizontal bar
							horizontalRect.width = horizontalRect.width - position.width;
							horizontalRect.height = 1;
							horizontalRect.xMin += barIndent + 1;
							horizontalRect.y += position.height/2;
							if (indentLevel > 0 && i == indentLevel && Event.current.type == EventType.Repaint)
							{
								EditorGUI.DrawRect(horizontalRect, lineColor);
							}
						}

						LastLowerBoundY = position.yMax;
						LastIndentY = horizontalRect.y;
						LastIndent = indentLevel;
					}

					//Label
					var guiContent = TempContent(label, tooltip);
					var labelPosition = HeaderRect(ref position, inline ? (EditorStyles.label.CalcSize(guiContent)).x + 8f : LABEL_WIDTH - position.x);
					TCP2_GUI.SubHeader(labelPosition, guiContent, this.highlighted && this.enabled);

					//Actual property
					bool labelClicked = Event.current.type == EventType.MouseUp && Event.current.button == 0 && labelPosition.Contains(Event.current.mousePosition);
					if (labelClicked)
					{
						Event.current.Use();
					}
					DrawGUI(position, config, labelClicked);

					LastVisible = visible;
				}

				GUI.enabled = guiEnabled;
			}

			//Internal DrawGUI: actually draws the feature
			protected virtual void DrawGUI(Rect position, Config config, bool labelClicked)
			{
				GUI.Label(position, "Unknown feature type for: " + label);
			}

			//Defines if the feature is selected/toggle/etc. or not
			internal virtual bool Highlighted(Config config)
			{
				return false;
			}

			//Called when processing this UIFeature, in case any forced value needs to be set even if the UI component isn't visible
			internal virtual void ForceValue(Config config)
			{

			}

			//Called when Enabled(config) has changed state
			//Originally used to force Multiple UI to enable the default feature, if any
			protected virtual void OnEnabledChangedState(Config config, bool newState)
			{

			}

			internal bool Enabled(Config config)
			{
				var enabled = true;
				if(requiresOr != null)
				{
					enabled = false;
					enabled |= config.HasFeaturesAny(requiresOr);
				}
				if(excludesAll != null)
					enabled &= !config.HasFeaturesAll(excludesAll);
				if(requires != null)
					enabled &= config.HasFeaturesAll(requires);
				if(excludes != null)
					enabled &= !config.HasFeaturesAny(excludes);

				if(wasEnabled != enabled)
				{
					OnEnabledChangedState(config, enabled);
				}
				wasEnabled = enabled;

				return enabled;
			}

			//Parses a #FEATURES text block
			internal static UIFeature[] GetUIFeatures(string[] lines, ref int i, Template template)
			{
				var uiFeaturesList = new List<UIFeature>();
				string subline;
				do
				{
					subline = lines[i];
					i++;

					//Empty line
					if(string.IsNullOrEmpty(subline))
						continue;

					var data = subline.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

					//Skip empty or comment # lines
					if(data == null || data.Length == 0 || (data.Length > 0 && data[0].StartsWith("#")))
						continue;

					var kvpList = new List<KeyValuePair<string, string>>();
					for(var j = 1; j < data.Length; j++)
					{
						var sdata = data[j].Split('=');
						if(sdata.Length == 2)
							kvpList.Add(new KeyValuePair<string, string>(sdata[0], sdata[1]));
						else if(sdata.Length == 1)
							kvpList.Add(new KeyValuePair<string, string>(sdata[0], null));
						else
							Debug.LogError("Couldn't parse UI property from Template:\n" + data[j]);
					}

					// Discard the UIFeature if not for this template:
					var templateCompatibility = kvpList.Find(kvp => kvp.Key == "templates");
					if (templateCompatibility.Key == "templates")
					{
						if (!templateCompatibility.Value.Contains(template.id))
						{
							continue;
						}
					}

					UIFeature feature = null;
					switch(data[0])
					{
						case "---": feature = new UIFeature_Separator(); break;
						case "space": feature = new UIFeature_Space(kvpList); break;
						case "flag": feature = new UIFeature_Flag(kvpList, false); break;
						case "nflag": feature = new UIFeature_Flag(kvpList, true); break;
						case "float": feature = new UIFeature_Float(kvpList); break;
						case "int": feature = new UIFeature_Int(kvpList); break;
						case "subh": feature = new UIFeature_SubHeader(kvpList); break;
						case "header": feature = new UIFeature_Header(kvpList); break;
						case "warning": feature = new UIFeature_Warning(kvpList); break;
						case "sngl": feature = new UIFeature_Single(kvpList); break;
						case "gpu_inst_opt": feature = new UIFeature_Single(kvpList); break;
						case "mult": feature = new UIFeature_Multiple(kvpList); break;
						case "mult_flags": feature = new UIFeature_MultFlags(kvpList); break;
						case "keyword": feature = new UIFeature_Keyword(kvpList); break;
						case "keyword_str": feature = new UIFeature_KeywordString(kvpList); break;
						case "dd_start": feature = new UIFeature_DropDownStart(kvpList); break;
						case "dd_end": feature = new UIFeature_DropDownEnd(); break;
						case "mult_fs": feature = new UIFeature_MultipleFixedFunction(kvpList); break;

						default: feature = new UIFeature(kvpList); break;
					}

					uiFeaturesList.Add(feature);
				}
				while(subline != "#END" && i < lines.Length);

				var uiFeaturesArray = uiFeaturesList.ToArray();

				// Build hierarchy from the parsed UIFeatures
				// note: simple hierarchy, where only a top-level element can be parent (one level only)
				UIFeature lastParent = null;
				for (int j = 0; j < uiFeaturesArray.Length; j++)
				{
					var uiFeature = uiFeaturesArray[j];
					if (uiFeature.indentLevel == 0 && !(uiFeature is UIFeature_Header) && !(uiFeature is UIFeature_Warning) && !uiFeature.inline)
					{
						lastParent = uiFeature;
					}
					else if (uiFeature.indentLevel > 0)
					{
						uiFeature.parent = lastParent;
					}
				}

				return uiFeaturesList.ToArray();
			}
		}

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// SINGLE FEATURE TOGGLE

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// FEATURES COMBOBOX

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// MULT FLAGS: enum flags-like interface to select multiple flags

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// FEATURES COMBOBOX for FIXED FUNCTION STATES
		// Embeds some UI from the corresponding Shader Property to easily change the states in the Features tab

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// KEYWORD COMBOBOX

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// KEYWORD STRING

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// SURFACE SHADER / GENERIC FLAG

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// FIXED FLOAT

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// FIXED INTEGER

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// DECORATORS
	}
}
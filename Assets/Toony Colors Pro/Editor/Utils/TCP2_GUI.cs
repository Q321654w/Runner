// Toony Colors Pro+Mobile 2
// (c) 2014-2020 Jean Moreno

using System.Collections.Generic;
using System.IO;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using UnityEditor;
using UnityEngine;

// Graphical User Interface helper functions

namespace Toony_Colors_Pro.Editor.Utils
{
	namespace Utilities
	{
		public static class TCP2_GUI
		{
			static GUIContent tempGuiContent = new GUIContent();

			public static GUIContent TempContent(string label, Texture2D icon)
			{
				tempGuiContent.text = label;
				tempGuiContent.image = icon;
				tempGuiContent.tooltip = null;
				return tempGuiContent;
			}

			public static GUIContent TempContent(string label, string tooltip = null, Texture2D icon = null)
			{
				tempGuiContent.text = label;
				tempGuiContent.image = icon;
				tempGuiContent.tooltip = tooltip;
				return tempGuiContent;
			}

			private static Dictionary<string, Texture2D> CustomEditorTextures = new Dictionary<string, Texture2D>();
			public static Texture2D GetCustomTexture(string name)
			{
				var uiName = name + (EditorGUIUtility.isProSkin ? "pro" : "");

				if (CustomEditorTextures.ContainsKey(uiName))
					return CustomEditorTextures[uiName];

				var rootPath = Utils.FindReadmePath(true);

				Texture2D texture = null;

				//load pro version
				if (EditorGUIUtility.isProSkin)
					texture = AssetDatabase.LoadAssetAtPath(rootPath + "/Editor/Icons/" + name + "_Pro.png", typeof(Texture2D)) as Texture2D;

				//load default version
				if (texture == null)
					texture = AssetDatabase.LoadAssetAtPath(rootPath + "/Editor/Icons/" + name + ".png", typeof(Texture2D)) as Texture2D;

				if (texture != null)
				{
					CustomEditorTextures.Add(uiName, texture);
					return texture;
				}

				return null;
			}

			private static GUIStyle _EnabledLabel;
			private static GUIStyle EnabledLabel
			{
				get
				{
					if (_EnabledLabel == null)
					{
						_EnabledLabel = new GUIStyle(EditorStyles.label);
						_EnabledLabel.normal.background = GetCustomTexture("TCP2_EnabledBg");
					}
					return _EnabledLabel;
				}
			}

			private static GUIStyle _ContextMenuButton;
			public static GUIStyle ContextMenuButton
			{
				get
				{
					if (_ContextMenuButton == null)
					{
						_ContextMenuButton = new GUIStyle();
						_ContextMenuButton.fixedWidth = 16;
						_ContextMenuButton.fixedHeight = 16;
						_ContextMenuButton.normal.background = GetCustomTexture("TCP2_Context");
					}
					return _ContextMenuButton;
				}
			}

			private static GUIStyle _ContextualHelpBox;
			public static GUIStyle ContextualHelpBox
			{
				get
				{
					if (_ContextualHelpBox == null)
					{
						_ContextualHelpBox = new GUIStyle(EditorStyles.helpBox);
						_ContextualHelpBox.normal.background = GetCustomTexture("TCP2_ContextualHelpBox");
						_ContextualHelpBox.normal.textColor = EditorGUIUtility.isProSkin ? new Color32(150, 170, 200, 255) : new Color32(80, 90, 100, 255);
						_ContextualHelpBox.richText = true;
						_ContextualHelpBox.alignment = TextAnchor.MiddleLeft;
						_ContextualHelpBox.padding = new RectOffset(6, 6, 4, 4);
					}
					return _ContextualHelpBox;
				}
			}

			private static GUIStyle _ContextualHelpBoxHover;
			public static GUIStyle ContextualHelpBoxHover
			{
				get
				{
					if (_ContextualHelpBoxHover == null)
					{
						_ContextualHelpBoxHover = new GUIStyle(ContextualHelpBox);
						_ContextualHelpBoxHover.hover.background = GetCustomTexture("TCP2_ContextualHelpBoxHover");
						_ContextualHelpBoxHover.hover.textColor = _ContextualHelpBoxHover.normal.textColor;
					}
					return _ContextualHelpBoxHover;
				}
			}

			private static GUIStyle _EnabledPropertyHelpBoxExp;
			public static GUIStyle EnabledPropertyHelpBoxExp
			{
				get
				{
					if (_EnabledPropertyHelpBoxExp == null)
					{
						_EnabledPropertyHelpBoxExp = new GUIStyle(EditorStyles.helpBox);
						_EnabledPropertyHelpBoxExp.normal.background = GetCustomTexture("TCP2_EnabledBgPropertyExpanded");
						var border = _EnabledPropertyHelpBoxExp.border;
						border.top += 24;
						_EnabledPropertyHelpBoxExp.border = border;
					}
					return _EnabledPropertyHelpBoxExp;
				}
			}

			private static GUIStyle _EnabledPropertyHelpBox;
			public static GUIStyle EnabledPropertyHelpBox
			{
				get
				{
					if (_EnabledPropertyHelpBox == null)
					{
						_EnabledPropertyHelpBox = new GUIStyle(EditorStyles.helpBox);
						_EnabledPropertyHelpBox.normal.background = GetCustomTexture("TCP2_EnabledBgProperty");
					}
					return _EnabledPropertyHelpBox;
				}
			}

			private static GUIStyle _ErrorPropertyHelpBoxExp;
			public static GUIStyle ErrorPropertyHelpBoxExp
			{
				get
				{
					if (_ErrorPropertyHelpBoxExp == null)
					{
						_ErrorPropertyHelpBoxExp = new GUIStyle(EditorStyles.helpBox);
						_ErrorPropertyHelpBoxExp.normal.background = GetCustomTexture("TCP2_ErrorBgPropertyExpanded");
						var border = _ErrorPropertyHelpBoxExp.border;
						border.top += 24;
						_ErrorPropertyHelpBoxExp.border = border;
					}
					return _ErrorPropertyHelpBoxExp;
				}
			}

			private static GUIStyle _ErrorPropertyHelpBox;
			public static GUIStyle ErrorPropertyHelpBox
			{
				get
				{
					if (_ErrorPropertyHelpBox == null)
					{
						_ErrorPropertyHelpBox = new GUIStyle(EditorStyles.helpBox);
						_ErrorPropertyHelpBox.normal.background = GetCustomTexture("TCP2_ErrorBgProperty");
					}
					return _ErrorPropertyHelpBox;
				}
			}

			private static GUIStyle _Tab;
			public static GUIStyle Tab
			{
				get
				{
					if (_Tab == null)
					{
						_Tab = new GUIStyle(EditorStyles.toolbarButton);
						_Tab.normal.background = GetCustomTexture("TCP2_TabOff");
						_Tab.focused.background = GetCustomTexture("TCP2_TabOff");
						_Tab.active.background = GetCustomTexture("TCP2_TabOff");

						_Tab.onNormal.background = GetCustomTexture("TCP2_Tab");
						_Tab.onFocused.background = GetCustomTexture("TCP2_Tab");
						_Tab.onActive.background = GetCustomTexture("TCP2_Tab");

						_Tab.margin = new RectOffset(4, 4, 0, 0);
					}
					return _Tab;
				}
			}

			static GUIStyle ShurikenMiniButtonBorder(GUIStyle source)
			{
				var style = new GUIStyle(source)
				{
					border = new RectOffset(5, 5, 5, 5),
					margin = new RectOffset(0, 0, 0, 0),
				};

				style.onActive.background = style.onNormal.background;
				style.onActive.scaledBackgrounds = style.onNormal.scaledBackgrounds;
				return style;
			}

			private static GUIStyle _ShurikenMiniButton;
			public static GUIStyle ShurikenMiniButton
			{
				get
				{
					if (_ShurikenMiniButton == null) _ShurikenMiniButton = ShurikenMiniButtonBorder(EditorStyles.miniButton);
					return _ShurikenMiniButton;
				}
			}

			private static GUIStyle _ShurikenMiniButtonLeft;
			public static GUIStyle ShurikenMiniButtonLeft
			{
				get
				{
					if (_ShurikenMiniButtonLeft == null) _ShurikenMiniButtonLeft = ShurikenMiniButtonBorder(EditorStyles.miniButtonLeft);
					return _ShurikenMiniButtonLeft;
				}
			}

			private static GUIStyle _ShurikenMiniButtonMid;
			public static GUIStyle ShurikenMiniButtonMid
			{
				get
				{
					if (_ShurikenMiniButtonMid == null) _ShurikenMiniButtonMid = ShurikenMiniButtonBorder(EditorStyles.miniButtonMid);
					return _ShurikenMiniButtonMid;
				}
			}

			private static GUIStyle _ShurikenMiniButtonRight;
			public static GUIStyle ShurikenMiniButtonRight
			{
				get
				{
					if (_ShurikenMiniButtonRight == null) _ShurikenMiniButtonRight = ShurikenMiniButtonBorder(EditorStyles.miniButtonRight);
					return _ShurikenMiniButtonRight;
				}
			}


			private static GUIStyle _HelpIcon;
			private static GUIStyle _HelpIcon2;
			public static bool UseNewHelpIcon;
			public static GUIStyle HelpIcon
			{
				get
				{
					if (_HelpIcon == null)
					{
						_HelpIcon = new GUIStyle(EditorStyles.label);
						_HelpIcon.fixedWidth = 16;
						_HelpIcon.fixedHeight = 16;

						_HelpIcon.normal.background = GetCustomTexture("TCP2_HelpIcon");
						_HelpIcon.active.background = GetCustomTexture("TCP2_HelpIcon_Down");
					}

					if (_HelpIcon2 == null)
					{
						_HelpIcon2 = new GUIStyle(_HelpIcon);

						_HelpIcon2.normal.background = GetCustomTexture("TCP2_HelpIcon2");
						_HelpIcon2.active.background = GetCustomTexture("TCP2_HelpIcon2_Down");
						_HelpIcon2.hover.background = GetCustomTexture("TCP2_HelpIcon2_Hover");
					}

					return UseNewHelpIcon ? _HelpIcon2 : _HelpIcon;
				}
			}

			private static GUIStyle _CogIcon;
			public static GUIStyle CogIcon
			{
				get
				{
					if (_CogIcon == null)
					{
						_CogIcon = new GUIStyle(EditorStyles.label);
						_CogIcon.fixedWidth = 16;
						_CogIcon.fixedHeight = 16;

						_CogIcon.normal.background = GetCustomTexture("TCP2_CogIcon");
						_CogIcon.active.background = GetCustomTexture("TCP2_CogIcon_Down");
					}

					return _CogIcon;
				}
			}

			private static GUIStyle _CogIcon2;
			public static GUIStyle CogIcon2
			{
				get
				{
					if (_CogIcon2 == null)
					{
						_CogIcon2 = new GUIStyle(EditorStyles.label);
						_CogIcon2.fixedWidth = 16;
						_CogIcon2.fixedHeight = 16;

						_CogIcon2.normal.background = GetCustomTexture("TCP2_CogIcon2");
						_CogIcon2.active.background = GetCustomTexture("TCP2_CogIcon2_Down");
					}

					return _CogIcon2;
				}
			}

			private static GUIStyle _HeaderLabel;
			private static GUIStyle HeaderLabel
			{
				get
				{
					if (_HeaderLabel == null)
					{
						_HeaderLabel = new GUIStyle(EditorStyles.label);
						_HeaderLabel.fontStyle = FontStyle.Bold;

						var gray1 = EditorGUIUtility.isProSkin ? 0.7f : 0.35f;
						_HeaderLabel.normal.textColor = new Color(gray1, gray1, gray1);
					}
					return _HeaderLabel;
				}
			}

			private static GUIStyle _HeaderDropDown;
			public static GUIStyle HeaderDropDown
			{
				get
				{
					if (_HeaderDropDown == null)
					{
						_HeaderDropDown = new GUIStyle(EditorStyles.foldout);

						_HeaderDropDown.focused.background = _HeaderDropDown.normal.background;
						_HeaderDropDown.active.background = _HeaderDropDown.normal.background;
						_HeaderDropDown.onFocused.background = _HeaderDropDown.onNormal.background;
						_HeaderDropDown.onActive.background = _HeaderDropDown.onNormal.background;

						var gray1 = EditorGUIUtility.isProSkin ? 0.8f : 0.0f;
						var gray2 = EditorGUIUtility.isProSkin ? 0.65f : 0.3f;

						var textColor = new Color(gray1, gray1, gray1);
						var textColorActive = new Color(gray2, gray2, gray2);
						_HeaderDropDown.normal.textColor = textColor;
						_HeaderDropDown.onNormal.textColor = textColor;
						_HeaderDropDown.focused.textColor = textColor;
						_HeaderDropDown.onFocused.textColor = textColor;
						_HeaderDropDown.active.textColor = textColorActive;
						_HeaderDropDown.onActive.textColor = textColorActive;
					}
					return _HeaderDropDown;
				}
			}

			private static GUIStyle _HeaderDropDownBold;
			public static GUIStyle HeaderDropDownBold
			{
				get
				{
					if (_HeaderDropDownBold == null)
					{
						_HeaderDropDownBold = new GUIStyle(HeaderDropDown);
						_HeaderDropDownBold.fontStyle = FontStyle.Bold;

						var gray1 = EditorGUIUtility.isProSkin ? 0.7f : 0.3f;
						var gray2 = EditorGUIUtility.isProSkin ? 0.6f : 0.45f;

						var textColor = new Color(gray1, gray1, gray1);
						var textColorActive = new Color(gray2, gray2, gray2);
						_HeaderDropDownBold.normal.textColor = textColor;
						_HeaderDropDownBold.onNormal.textColor = textColor;
						_HeaderDropDownBold.focused.textColor = textColor;
						_HeaderDropDownBold.onFocused.textColor = textColor;
						_HeaderDropDownBold.active.textColor = textColorActive;
						_HeaderDropDownBold.onActive.textColor = textColorActive;
					}
					return _HeaderDropDownBold;
				}
			}

			private static GUIStyle _HeaderDropDownBoldGray;
			public static GUIStyle HeaderDropDownBoldGray
			{
				get
				{
					if (_HeaderDropDownBoldGray == null)
					{
						_HeaderDropDownBoldGray = new GUIStyle(HeaderDropDownBold);
						var gray1 = EditorGUIUtility.isProSkin ? 0.5f : 0.35f;
						var gray2 = EditorGUIUtility.isProSkin ? 0.4f : 0.45f;
						var textColor = new Color(gray1, gray1, gray1);
						var textColorActive = new Color(gray2, gray2, gray2);
						_HeaderDropDownBoldGray.normal.textColor = textColor;
						_HeaderDropDownBoldGray.onNormal.textColor = textColor;
						_HeaderDropDownBoldGray.focused.textColor = textColor;
						_HeaderDropDownBoldGray.onFocused.textColor = textColor;
						_HeaderDropDownBoldGray.active.textColor = textColorActive;
						_HeaderDropDownBoldGray.onActive.textColor = textColorActive;
					}
					return _HeaderDropDownBoldGray;
				}
			}

			private static GUIStyle _SubHeaderLabel;
			private static GUIStyle SubHeaderLabel
			{
				get
				{
					if (_SubHeaderLabel == null)
					{
						_SubHeaderLabel = new GUIStyle(EditorStyles.label);
						_SubHeaderLabel.fontStyle = FontStyle.Normal;
						_SubHeaderLabel.normal.textColor = EditorGUIUtility.isProSkin ? new Color(0.5f, 0.5f, 0.5f) : new Color(0.35f, 0.35f, 0.35f);
					}
					return _SubHeaderLabel;
				}
			}

			private static GUIStyle _BigHeaderLabel;
			private static GUIStyle BigHeaderLabel
			{
				get
				{
					if (_BigHeaderLabel == null)
					{
						_BigHeaderLabel = new GUIStyle(EditorStyles.largeLabel);
						_BigHeaderLabel.fontStyle = FontStyle.Bold;
						_BigHeaderLabel.fixedHeight = 30;
					}
					return _BigHeaderLabel;
				}
			}

			private static GUIStyle _FoldoutBold;
			public static GUIStyle FoldoutBold
			{
				get
				{
					if (_FoldoutBold == null)
					{
						_FoldoutBold = new GUIStyle(EditorStyles.foldout);
						_FoldoutBold.fontStyle = FontStyle.Bold;
					}
					return _FoldoutBold;
				}
			}

			public static GUIStyle _LineStyle;
			public static GUIStyle LineStyle
			{
				get
				{
					if (_LineStyle == null)
					{
						_LineStyle = new GUIStyle();
						_LineStyle.normal.background = EditorGUIUtility.whiteTexture;
						_LineStyle.stretchWidth = true;
					}

					return _LineStyle;
				}
			}

			static GUIStyle _HelpBoxRichTextStyle;
			public static GUIStyle HelpBoxRichTextStyle
			{
				get
				{
					if (_HelpBoxRichTextStyle == null)
					{
						_HelpBoxRichTextStyle = new GUIStyle("HelpBox");
						_HelpBoxRichTextStyle.richText = true;
						_HelpBoxRichTextStyle.margin = new RectOffset(4, 4, 0, 0);
						_HelpBoxRichTextStyle.padding = new RectOffset(4, 4, 4, 4);
					}
					return _HelpBoxRichTextStyle;
				}
			}

			public static Texture2D _SmallHelpIconTexture;
			public static Texture2D SmallHelpIconTexture
			{
				get
				{
					if (_SmallHelpIconTexture == null)
					{
						_SmallHelpIconTexture = GetCustomTexture("TCP2_SmallHelpIcon");
					}
					return _SmallHelpIconTexture;
				}
			}

			public static Texture2D GetHelpBoxIcon(MessageType msgType)
			{
				switch (msgType)
				{
					case MessageType.Error:
						return GetCustomTexture("TCP2_ErrorIcon");
					case MessageType.Warning:
						return GetCustomTexture("TCP2_WarningIcon");
					case MessageType.Info:
						return GetCustomTexture("TCP2_InfoIcon");
				}

				return null;
			}

			static Color hoverColor = new Color(0, 0, 0, 0.05f);
			static Color hoverColorDark = new Color(1, 1, 1, 0.05f);
			static Color HoverColor
			{
				get { return EditorGUIUtility.isProSkin ? hoverColorDark : hoverColor; }
			}

			public static void DrawHoverRect(Rect rect,
#if UNITY_2019_3_OR_NEWER
				float inset = 2
#else
				float inset = 0
#endif
				)
			{
				var mouseRect = rect;
				mouseRect.yMax -= inset;
				mouseRect.yMin += inset;
				if (mouseRect.Contains(Event.current.mousePosition))
				{
					EditorGUI.DrawRect(rect, HoverColor);
				}
			}

			//--------------------------------------------------------------------------------------------------
			// HELP

			public static void HelpButton(Rect rect, string helpTopic, string helpAnchor = null)
			{
				if (Button(rect, HelpIcon, "?", "Help about:\n" + helpTopic))
				{
					OpenHelpFor(string.IsNullOrEmpty(helpAnchor) ? helpTopic : helpAnchor);
				}
			}
			public static void HelpButton(string helpTopic, string helpAnchor = null)
			{
				if (Button(HelpIcon, "?", "Help about:\n" + helpTopic))
				{
					OpenHelpFor(string.IsNullOrEmpty(helpAnchor) ? helpTopic : helpAnchor);
				}
			}

			public static void OpenHelpFor(string helpTopic)
			{
				var rootDir = Utils.FindReadmePath();
				if (rootDir == null)
				{
					EditorUtility.DisplayDialog("TCP2 Documentation", "Couldn't find TCP2 root folder! (the readme file is missing)\nYou can still access the documentation manually in the Documentation folder.", "Ok");
				}
				else
				{
					var helpAnchor = helpTopic.Replace("/", "_").Replace(@"\", "_").Replace(" ", "_").ToLowerInvariant() + ".htm";
					var topicFile = rootDir.Replace(@"\", "/") + "/Documentation/Documentation Data/Anchors/" + helpAnchor;

					if (File.Exists(topicFile))
					{
						Application.OpenURL("file:///" + topicFile);
					}
					else
					{
						Debug.LogError("Documentation anchor file doesn't exist: " + topicFile);
					}
				}
			}

			public static void OpenHelp()
			{
				Application.OpenURL("https://jeanmoreno.com/unity/toonycolorspro/doc/");
			}

			//--------------------------------------------------------------------------------------------------
			// HELP SHADER GENERATOR 2

			public static void HelpButtonSG2(Rect rect, string helpTopic, string helpAnchor = null)
			{
				if (Button(rect, HelpIcon, "?"))
				{
					string append = string.IsNullOrEmpty(helpAnchor) ? helpTopic : helpAnchor;
					Application.OpenURL(ShaderGenerator2.DOCUMENTATION_URL + "#" + append);
				}
			}
			public static void HelpButtonSG2(string helpTopic, string helpAnchor = null)
			{
				if (Button(HelpIcon, "?"))
				{
					string append = string.IsNullOrEmpty(helpAnchor) ? helpTopic : helpAnchor;
					Application.OpenURL(ShaderGenerator2.DOCUMENTATION_URL + "#" + append);
				}
			}

			//--------------------------------------------------------------------------------------------------
			//GUI Functions

			public static void SeparatorSimple()
			{
				var color = EditorGUIUtility.isProSkin ? new Color(0.15f, 0.15f, 0.15f) : new Color(0.65f, 0.65f, 0.65f);
				GUILine(color, 1);
				GUILayout.Space(1);
			}

			public static void Separator()
			{
				var colorDark = EditorGUIUtility.isProSkin ? new Color(.1f, .1f, .1f) : new Color(.3f, .3f, .3f);
				var colorBright = EditorGUIUtility.isProSkin ? new Color(.3f, .3f, .3f) : new Color(.9f, .9f, .9f);

				GUILayout.Space(4);
				GUILine(colorDark, 1);
				GUILine(colorBright, 1);
				GUILayout.Space(4);
			}

			public static void Separator(Rect position)
			{
				var colorDark = EditorGUIUtility.isProSkin ? new Color(.1f, .1f, .1f) : new Color(.3f, .3f, .3f);
				var colorBright = EditorGUIUtility.isProSkin ? new Color(.3f, .3f, .3f) : new Color(.9f, .9f, .9f);

				var lineRect = position;
				lineRect.height = 1;
				GUILine(lineRect, colorDark, 1);
				lineRect.y += 1;
				GUILine(lineRect, colorBright, 1);
			}

			public static void SeparatorBig()
			{
				GUILayout.Space(10);
				GUILine(new Color(.3f, .3f, .3f), 2);
				GUILayout.Space(1);
				GUILine(new Color(.3f, .3f, .3f), 2);
				GUILine(new Color(.85f, .85f, .85f), 1);
				GUILayout.Space(2);
			}

			public static void GUILine(float height = 2f)
			{
				GUILine(Color.black, height);
			}
			public static void GUILine(Color color, float height = 2f)
			{
				var position = GUILayoutUtility.GetRect(0f, float.MaxValue, height, height, LineStyle);

				if (Event.current.type == EventType.Repaint)
				{
					var orgColor = GUI.color;
					GUI.color = orgColor * color;
					LineStyle.Draw(position, false, false, false, false);
					GUI.color = orgColor;
				}
			}
			public static void GUILine(Rect position, Color color, float height = 2f)
			{
				if (Event.current.type == EventType.Repaint)
				{
					var orgColor = GUI.color;
					GUI.color = orgColor * color;
					LineStyle.Draw(position, false, false, false, false);
					GUI.color = orgColor;
				}
			}

			//----------------------

			public static void Header(string header, string tooltip = null, bool expandWidth = false)
			{
				if (tooltip != null)
					EditorGUILayout.LabelField(TempContent(header, tooltip), HeaderLabel, GUILayout.ExpandWidth(expandWidth));
				else
					EditorGUILayout.LabelField(header, HeaderLabel, GUILayout.ExpandWidth(expandWidth));
			}

			public static void Header(Rect position, string header, string tooltip = null, bool expandWidth = false)
			{
				if (tooltip != null)
					EditorGUI.LabelField(position, TempContent(header, tooltip), HeaderLabel);
				else
					EditorGUI.LabelField(position, header, HeaderLabel);
			}

			public static bool HeaderFoldout(bool foldout, GUIContent guiContent, bool drawHover = false)
			{
				var position = GUILayoutUtility.GetRect(EditorGUIUtility.fieldWidth, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight, HeaderDropDownBold);
				if (drawHover)
				{
					DrawHoverRect(position);
				}
				return HeaderFoldout(position, foldout, guiContent);
			}

			public static bool HeaderFoldout(Rect position, bool foldout, GUIContent guiContent)
			{
				foldout = EditorGUI.Foldout(position, foldout, guiContent, true, HeaderDropDownBold);
				return foldout;
			}

			public static bool HeaderFoldoutHighlight(bool foldout, GUIContent guiContent, bool highlighted)
			{
				var position = GUILayoutUtility.GetRect(EditorGUIUtility.fieldWidth, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight, HeaderDropDownBold);
				return HeaderFoldoutHighlight(position, foldout, guiContent, highlighted);
			}

			public static bool HeaderFoldoutHighlight(Rect position, bool foldout, GUIContent guiContent, bool highlighted)
			{
				if (highlighted)
				{
					var highlightColor = EditorGUIUtility.isProSkin ? new Color(0.0f, 0.574f, 0.488f, 0.2f) : new Color(0.0f, 0.5f, 0.4f, 0.2f);
					EditorGUI.DrawRect(position, highlightColor);
				}

				foldout = EditorGUI.Foldout(position, foldout, guiContent, true, HeaderDropDownBold);
				return foldout;
			}

			public static bool HeaderFoldoutHighlightErrorGray(bool foldout, GUIContent guiContent, bool error, bool highlighted)
			{
				var position = GUILayoutUtility.GetRect(EditorGUIUtility.fieldWidth, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight, HeaderDropDownBold);
				return HeaderFoldoutHighlightErrorGrayPosition(position, foldout, guiContent, error, highlighted);
			}

			public static bool HeaderFoldoutHighlightErrorGrayPosition(Rect position, bool foldout, GUIContent guiContent, bool error, bool highlighted)
			{
				if (error)
				{
					var highlightColor = EditorGUIUtility.isProSkin ? new Color(0.85f, 0.1f, 0, 0.2f) : new Color(0.8f, 0, 0, 0.2f);
					EditorGUI.DrawRect(position, highlightColor);
				}
				else if (highlighted)
				{
					var highlightColor = EditorGUIUtility.isProSkin ? new Color(0.0f, 0.574f, 0.488f, 0.2f) : new Color(0.0f, 0.5f, 0.4f, 0.2f);
					EditorGUI.DrawRect(position, highlightColor);
				}

				foldout = EditorGUI.Foldout(position, foldout, guiContent, true, HeaderDropDownBold);
				return foldout;
			}


			public static void SubHeaderGray(string header, string tooltip = null, bool expandWidth = false)
			{
				if (tooltip != null)
					EditorGUILayout.LabelField(TempContent(header, tooltip), SubHeaderLabel, GUILayout.ExpandWidth(expandWidth));
				else
					EditorGUILayout.LabelField(header, SubHeaderLabel, GUILayout.ExpandWidth(expandWidth));
			}

			public static void HeaderAndHelp(string header, string helpTopic)
			{
				HeaderAndHelp(header, null, helpTopic);
			}
			public static void HeaderAndHelp(string header, string tooltip, string helpTopic)
			{
				GUILayout.BeginHorizontal();
				var r = GUILayoutUtility.GetRect(TempContent(header, tooltip), EditorStyles.label, GUILayout.ExpandWidth(true));
				var btnRect = r;
				btnRect.width = 16;
				//Button
				if (GUI.Button(btnRect, TempContent("", "Help about:\n" + helpTopic), HelpIcon))
					OpenHelpFor(helpTopic);
				//Label
				r.x += 16;
				r.width -= 16;
				GUI.Label(r, TempContent(header, tooltip), EditorStyles.boldLabel);
				GUILayout.EndHorizontal();
			}
			public static void HeaderAndHelp(Rect position, string header, string tooltip, string helpTopic)
			{
				if (!string.IsNullOrEmpty(helpTopic))
				{
					var btnRect = position;
					btnRect.width = 16;
					//Button
					if (GUI.Button(btnRect, TempContent("", "Help about:\n" + helpTopic), HelpIcon))
						OpenHelpFor(helpTopic);
				}

				//Label
				position.x += 16;
				position.width -= 16;
				GUI.Label(position, TempContent(header, tooltip), EditorStyles.boldLabel);
			}

			public static void HeaderBig(string header, string tooltip = null)
			{
				if (tooltip != null)
					EditorGUILayout.LabelField(TempContent(header, tooltip), BigHeaderLabel);
				else
					EditorGUILayout.LabelField(header, BigHeaderLabel);
			}

			public static void SubHeader(string header, string tooltip = null, float width = 146f)
			{
				SubHeader(header, tooltip, false, width);
			}
			public static void SubHeader(string header, string tooltip, bool highlight, float width)
			{
				if (tooltip != null)
					GUILayout.Label(TempContent(header, tooltip), highlight ? EnabledLabel : EditorStyles.label, GUILayout.Width(width));
				else
					GUILayout.Label(header, highlight ? EnabledLabel : EditorStyles.label, GUILayout.Width(width));
			}

			public static void SubHeader(Rect position, string header, string tooltip, bool highlight)
			{
				SubHeader(position, TempContent(header, tooltip), highlight);
			}
			public static void SubHeader(Rect position, GUIContent content, bool highlight)
			{
				GUI.Label(position, content, highlight ? EnabledLabel : EditorStyles.label);
			}

			public static void HelpBox(Rect position, string message, MessageType msgType)
			{
				EditorGUI.LabelField(position, GUIContent.none, TempContent(message, GetHelpBoxIcon(msgType)), HelpBoxRichTextStyle);
			}

			public static void HelpBoxLayout(string message, MessageType msgType)
			{
				var guiContent = TempContent(message, GetHelpBoxIcon(msgType));
				Rect rect = GUILayoutUtility.GetRect(guiContent, HelpBoxRichTextStyle);
				// compensate the style margin: we can't set the right margin to 0, else the width isn't calculated correctly, so we compensate here
				rect.xMax += HelpBoxRichTextStyle.margin.right;
				GUI.Label(rect, guiContent, HelpBoxRichTextStyle);
			}

			public static void ContextualHelpBoxLayout(string message, bool canHover = false)
			{
				var style = canHover ? ContextualHelpBoxHover : ContextualHelpBox;
				var globalFont = GUI.skin.font;
				GUI.skin.font = null;
				int indentLevel = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;
				EditorGUILayout.LabelField(GUIContent.none, TempContent(message, GetCustomTexture("TCP2_ContextIcon")), style);
				EditorGUI.indentLevel = indentLevel;
				GUI.skin.font = globalFont;
			}

			//----------------------

			public static bool Button(GUIStyle icon, string noIconText, string tooltip = null)
			{
				if (icon == null)
					return GUILayout.Button(TempContent(noIconText, tooltip), EditorStyles.miniButton);
				return GUILayout.Button(TempContent("", tooltip), icon);
			}

			public static bool Button(Rect rect, GUIStyle icon, string noIconText, string tooltip = null)
			{
				if (icon == null)
					return GUI.Button(rect, TempContent(noIconText, tooltip), EditorStyles.miniButton);
				return GUI.Button(rect, TempContent("", tooltip), icon);
			}

			public static int RadioChoice(int choice, bool horizontal, params string[] labels)
			{
				var guiContents = new GUIContent[labels.Length];
				for (var i = 0; i < guiContents.Length; i++)
				{
					guiContents[i] = new GUIContent(labels[i]);
				}
				return RadioChoice(choice, horizontal, guiContents);
			}
			public static int RadioChoice(int choice, bool horizontal, params GUIContent[] labels)
			{
				if (horizontal)
					EditorGUILayout.BeginHorizontal();

				for (var i = 0; i < labels.Length; i++)
				{
					var style = EditorStyles.miniButton;
					if (labels.Length > 1)
					{
						if (i == 0)
							style = EditorStyles.miniButtonLeft;
						else if (i == labels.Length-1)
							style = EditorStyles.miniButtonRight;
						else
							style = EditorStyles.miniButtonMid;
					}

					if (GUILayout.Toggle(i == choice, labels[i], style))
					{
						choice = i;
					}
				}

				if (horizontal)
					EditorGUILayout.EndHorizontal();

				return choice;
			}

			public static int RadioChoiceHorizontal(Rect position, int choice, params GUIContent[] labels)
			{
				for (var i = 0; i < labels.Length; i++)
				{
					var rI = position;
					rI.width /= labels.Length;
					rI.x += i * rI.width;
					if (GUI.Toggle(rI, choice == i, labels[i], (i == 0) ? EditorStyles.miniButtonLeft : (i == labels.Length - 1) ? EditorStyles.miniButtonRight : EditorStyles.miniButtonMid))
					{
						choice = i;
					}
				}

				return choice;
			}
		}

		//===================================================================================================================================================================
		// Material Property Drawers

		//---------------------------------------------------------------------------------------------------------------------

		//---------------------------------------------------------------------------------------------------------------------

		//---------------------------------------------------------------------------------------------------------------------

		//---------------------------------------------------------------------------------------------------------------------

		//---------------------------------------------------------------------------------------------------------------------

		//---------------------------------------------------------------------------------------------------------------------

		//---------------------------------------------------------------------------------------------------------------------

		//---------------------------------------------------------------------------------------------------------------------

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// Same as Toggle drawer, but doesn't set any keyword
		// This will avoid adding unnecessary shader keyword to the project

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// Same as Toggle drawer, with different label style
		// Also acts as a no-keyword toggle if no keyword is specified

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// Keyword Enum no Prefix
		// Same as KeywordEnum drawer, but uses the keyword supplied as is rather than adding a prefix to them

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// Float enum with extended capacity
		// Same as Unity's MaterialEnumDrawer but allowing up to 16 values, and without the built-in enum support

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// Small texture field in material inspector

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------
		// UV Scrolling property
	}
}
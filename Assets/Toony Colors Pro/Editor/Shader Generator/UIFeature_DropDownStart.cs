using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_DropDownStart : UIFeature
    {
        static List<UIFeature_DropDownStart> AllDropDowns = new List<UIFeature_DropDownStart>();
        internal static void ClearDropDownsList()
        {
            AllDropDowns.Clear();
        }

        public bool foldout;
        public GUIContent guiContent = GUIContent.none;

        internal UIFeature_DropDownStart(List<KeyValuePair<string, string>> list) : base(list)
        {
            customGUI = true;
            ignoreVisibility = true;

            if(list != null)
            {
                foreach(var kvp in list)
                {
                    if(kvp.Key == "lbl")
                    {
                        guiContent = new GUIContent(kvp.Value.Trim('"'));
                    }
                }
            }

            foldout = ProjectOptions.data.OpenedFoldouts.Contains(guiContent.text);

            AllDropDowns.Add(this);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            //Check if any feature within that Foldout are enabled, and show different color if so
            var hasToggledFeatures = false;
            var i = Array.IndexOf(Template.CurrentTemplate.uiFeatures, this);
            if(i >= 0)
            {
                for(i++; i < Template.CurrentTemplate.uiFeatures.Length; i++)
                {
                    var uiFeature = Template.CurrentTemplate.uiFeatures[i];
                    if(uiFeature is UIFeature_DropDownEnd)
                        break;

                    hasToggledFeatures |= uiFeature.Highlighted(config) && uiFeature.Enabled(config);
                }
            }

            var color = GUI.color;
            GUI.color *= EditorGUIUtility.isProSkin ? Color.white : new Color(.95f, .95f, .95f, 1f);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.color = color;
            EditorGUI.BeginChangeCheck();
            {
                var rect = GUILayoutUtility.GetRect(EditorGUIUtility.fieldWidth, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight, TCP2_GUI.HeaderDropDownBold);

                // hover
                TCP2_GUI.DrawHoverRect(rect);

                foldout = TCP2_GUI.HeaderFoldoutHighlight(rect, foldout, guiContent, hasToggledFeatures);
                FoldoutStack.Push(foldout);
            }
            if(EditorGUI.EndChangeCheck())
            {
                UpdatePersistentState();

                if(Event.current.alt || Event.current.control)
                {
                    var state = foldout;
                    foreach(var dd in AllDropDowns)
                    {
                        dd.foldout = state;
                        dd.UpdatePersistentState();
                    }
                }
            }
        }

        public void UpdatePersistentState()
        {
            if(foldout && !ProjectOptions.data.OpenedFoldouts.Contains(guiContent.text))
                ProjectOptions.data.OpenedFoldouts.Add(guiContent.text);
            else if(!foldout && ProjectOptions.data.OpenedFoldouts.Contains(guiContent.text))
                ProjectOptions.data.OpenedFoldouts.Remove(guiContent.text);
        }
    }
}
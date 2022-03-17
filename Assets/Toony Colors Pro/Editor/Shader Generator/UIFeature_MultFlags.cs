using System;
using System.Collections.Generic;
using Toony_Colors_Pro.Editor.Shader_Generator.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal class UIFeature_MultFlags : UIFeature
    {
        string keyword;
        string[] labels;
        string[] values;
        string cachedKeywordValue;
        List<string> flagsList = new List<string>();
        int cachedFlagListCount;

        string popupLabel = "None";

        Rect flagsMenuPosition;
        bool reopenFlagsMenu = false;

        internal UIFeature_MultFlags(List<KeyValuePair<string, string>> list) : base(list) { }

        protected override void ProcessProperty(string key, string value)
        {
            if (key == "kw")
            {
                keyword = value;
            }
            else if (key == "default")
            {
                flagsList.Add(value);
            }
            else if(key == "values")
            {
                Debug.Log("process values: " + value);
                var data = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                labels = new string[data.Length];
                this.values = new string[data.Length];

                for(var i = 0; i < data.Length; i++)
                {
                    var lbl_feat = data[i].Split('|');
                    if(lbl_feat.Length != 2)
                    {
                        Debug.LogWarning("[UIFeature_MultFlags] Invalid data:" + data[i]);
                        continue;
                    }

                    labels[i] = lbl_feat[0];
                    this.values[i] = lbl_feat[1];
                }
            }
            else
                base.ProcessProperty(key, value);
        }

        protected override void DrawGUI(Rect position, Config config, bool labelClicked)
        {
            // update from flag lists
            if (cachedFlagListCount != flagsList.Count)
            {
                cachedFlagListCount = flagsList.Count;
                string newKeywordValue = string.Join(" ", flagsList.ToArray());
                config.SetKeyword(keyword, newKeywordValue);
                cachedKeywordValue = newKeywordValue;
                UpdateButtonLabel();
            }

            // update from config
            string configKeywordValue = config.GetKeyword(keyword);
            if (cachedKeywordValue != configKeywordValue)
            {
                cachedKeywordValue = configKeywordValue;
                flagsList.Clear();
                if (configKeywordValue != null)
                {
                    var data = configKeywordValue.Split(' ');
                    flagsList.AddRange(data);
                }
            }

            if (GUI.Button(position, TCP2_GUI.TempContent(popupLabel), EditorStyles.popup) || reopenFlagsMenu)
            {
                GetFlagsMenu(config, reopenFlagsMenu);
                reopenFlagsMenu = false;
            }
        }

        void GetFlagsMenu(Config config, bool reusePosition = false)
        {
            var flagsMenu = new GenericMenu();
            for (int i = 0; i < labels.Length; i++)
            {
                flagsMenu.AddItem(new GUIContent(labels[i]), flagsList.Contains(values[i]), OnSelectFlag, new object[] { config, values[i] });
            }

            if (!reusePosition)
            {
                flagsMenuPosition = new Rect(Event.current.mousePosition, Vector2.zero);
            }
            flagsMenu.DropDown(flagsMenuPosition);
        }

        void UpdateButtonLabel()
        {
            if (flagsList.Count == 0)
            {
                popupLabel = "None";
            }
            else if (flagsList.Count == 1)
            {
                int index = Array.IndexOf(values, flagsList[0]);
                popupLabel = labels[index];
            }
            else
            {
                popupLabel = "Multiple values...";
            }
        }

        void OnSelectFlag(object data)
        {
            int previousCount = flagsList.Count;

            Config config = (Config)((object[])data)[0];
            string value = (string)((object[])data)[1];

            if (flagsList.Contains(value))
            {
                flagsList.Remove(value);
            }
            else
            {
                flagsList.Add(value);
            }

            UpdateButtonLabel();
            config.SetKeyword(keyword, string.Join(" ", flagsList.ToArray()));

            reopenFlagsMenu = true;
            EditorApplication.delayCall += () =>
            {
                // will force the menu to reopen next frame
                ShaderGenerator2.RepaintWindow();
            };
        }

        internal override bool Highlighted(Config config)
        {
            return flagsList.Count > 0;
        }
    }
}
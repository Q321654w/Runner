using System;
using System.Text;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEditor;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2HelpBoxDecorator : MaterialPropertyDrawer
    {
        protected readonly MessageType msgType;
        protected readonly string message;
        protected Texture2D icon;

        private float InspectorWidth;   //Workaround to detect vertical scrollbar in the inspector
        static string ParseMessage(string message)
        {
            //double space = line break
            message = message.Replace("  ", "\n");

            // __word__ = <b>word</b>
            var sb = new StringBuilder();
            var words = message.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                var w = words[i];
                if (w.StartsWith("__") && w.EndsWith("__"))
                {
                    var w2 = w.Replace("__", "");
                    w = w.Replace("__" + w2 + "__", "<b>" + w2 + "</b>");
                }

                sb.Append(w + " ");
            }
            var str = sb.ToString();
            return str.TrimEnd();
        }

        public TCP2HelpBoxDecorator(string messageType, string msg)
        {
            msgType = (MessageType)Enum.Parse(typeof(MessageType), messageType);
            message = ParseMessage(msg);
            icon = TCP2_GUI.GetHelpBoxIcon(msgType);
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            position.height -= 4f;
            EditorGUI.LabelField(position, GUIContent.none, new GUIContent(message, icon), TCP2_GUI.HelpBoxRichTextStyle);
            //EditorGUI.HelpBox(position, this.message, this.msgType);

            if (Event.current != null && Event.current.type == EventType.Repaint)
                InspectorWidth = position.width;
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            //Calculate help box height
            var scrollBar = (Screen.width - InspectorWidth) > 20;
            var height = TCP2_GUI.HelpBoxRichTextStyle.CalcHeight(new GUIContent(message, icon), Screen.width - (scrollBar ? 51 : 34));
            return height + 6f;
        }
    }
}
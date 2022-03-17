using System.IO;
using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_IfProperty : GUICommand
    {
        string _expression;
        public string expression
        {
            get { return _expression; }
            set { _expression = value.Replace("!=", "<>"); }
        }
        public Material[] materials { get; set; }

        public override void OnGUI()
        {
            bool show = ExpressionParser.EvaluateExpression(expression, EvaluatePropertyExpression);
            MaterialInspector_Hybrid.PushShowProperty(show);
        }

        protected bool EvaluatePropertyExpression(string expr)
        {
            //expression is expected to be in the form of: property operator value
            var reader = new StringReader(expr);
            string property = "";
            string op = "";
            float value = 0f;

            int overflow = 0;
            while (true)
            {
                char c = (char)reader.Read();

                //operator
                if (c == '=' || c == '>' || c == '<' || c == '!')
                {
                    op += c;
                    //second operator character, if any
                    char c2 = (char)reader.Peek();
                    if (c2 == '=' || c2 == '>')
                    {
                        reader.Read();
                        op += c2;
                    }

                    //end of string is the value
                    var end = reader.ReadToEnd();
                    if (!float.TryParse(end, out value))
                    {
                        Debug.LogError("Couldn't parse float from property expression:\n" + end);
                        return false;
                    }

                    break;
                }

                //property name
                property += c;

                overflow++;
                if (overflow >= 9999)
                {
                    Debug.LogError("Expression parsing overflow!\n");
                    return false;
                }
            }

            //evaluate property
            bool conditionMet = false;
            foreach (var m in materials)
            {
                float propValue = 0f;
                if (property.Contains(".x") || property.Contains(".y") || property.Contains(".z") || property.Contains(".w"))
                {
                    string[] split = property.Split('.');
                    string component = split[1];
                    switch (component)
                    {
                        case "x": propValue = m.GetVector(split[0]).x; break;
                        case "y": propValue = m.GetVector(split[0]).y; break;
                        case "z": propValue = m.GetVector(split[0]).z; break;
                        case "w": propValue = m.GetVector(split[0]).w; break;
                        default: Debug.LogError("Invalid component for vector property: '" + property + "'"); break;
                    }
                }
                else
                    propValue = m.GetFloat(property);

                switch (op)
                {
                    case ">=": conditionMet = propValue >= value; break;
                    case "<=": conditionMet = propValue <= value; break;
                    case ">": conditionMet = propValue > value; break;
                    case "<": conditionMet = propValue < value; break;
                    case "<>": conditionMet = propValue != value; break;    //not equal, "!=" is replaced by "<>" to prevent bug with leading ! ("not" operator)
                    case "==": conditionMet = propValue == value; break;
                    default:
                        Debug.LogError("Invalid property expression:\n" + expr);
                        break;
                }
                if (conditionMet)
                    return true;
            }

            return false;
        }
    }
}
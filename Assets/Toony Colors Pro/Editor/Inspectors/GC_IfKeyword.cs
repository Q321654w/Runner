using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;
using UnityEngine;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_IfKeyword : GUICommand
    {
        public string expression { get; set; }
        public Material[] materials { get; set; }
        public override void OnGUI()
        {
            bool show = ExpressionParser.EvaluateExpression(expression, (string s) =>
            {
                foreach (var m in materials)
                {
                    if (m.IsKeywordEnabled(s))
                        return true;
                }
                return false;
            });
            MaterialInspector_Hybrid.PushShowProperty(show);
        }
    }
}
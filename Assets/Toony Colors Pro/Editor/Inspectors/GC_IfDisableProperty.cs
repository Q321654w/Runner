using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_IfDisableProperty : GC_IfProperty
    {
        public override void OnGUI()
        {
            bool enable = ExpressionParser.EvaluateExpression(expression, EvaluatePropertyExpression);
            MaterialInspector_Hybrid.PushDisableProperty(!enable);
        }
    }
}
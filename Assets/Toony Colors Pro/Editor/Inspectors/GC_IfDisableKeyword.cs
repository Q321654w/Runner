using Toony_Colors_Pro.Editor.Inspectors.ShaderGenerator;
using Toony_Colors_Pro.Editor.Utils.Utilities;

namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GC_IfDisableKeyword : GC_IfKeyword
    {
        public override void OnGUI()
        {
            bool enable = ExpressionParser.EvaluateExpression(expression, (string s) =>
            {
                foreach (var m in materials)
                {
                    if (m.IsKeywordEnabled(s))
                        return true;
                }
                return false;
            });
            MaterialInspector_Hybrid.PushDisableProperty(!enable);
        }
    }
}
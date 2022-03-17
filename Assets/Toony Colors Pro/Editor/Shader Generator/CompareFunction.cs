namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    public enum CompareFunction
    {
        [Enums.Order(0)]									Never		= UnityEngine.Rendering.CompareFunction.Never,
        [Enums.Order(1)]									Less		= UnityEngine.Rendering.CompareFunction.Less,
        [Enums.Order(2), Enums.Label("Less or Equal")]		LEqual		= UnityEngine.Rendering.CompareFunction.LessEqual,
        [Enums.Order(3)]									Equal		= UnityEngine.Rendering.CompareFunction.Equal,
        [Enums.Order(4), Enums.Label("Greater or Equal")]	GEqual		= UnityEngine.Rendering.CompareFunction.GreaterEqual,
        [Enums.Order(5)]									Greater		= UnityEngine.Rendering.CompareFunction.Greater,
        [Enums.Order(6), Enums.Label("Not Equal")]			NotEqual	= UnityEngine.Rendering.CompareFunction.NotEqual,
        [Enums.Order(7)]									Always		= UnityEngine.Rendering.CompareFunction.Always
    }
}
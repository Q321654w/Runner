namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    public enum BlendOperation
    {
        [Enums.Order(0)]									Add		= UnityEngine.Rendering.BlendOp.Add,
        [Enums.Order(1), Enums.Label("Subtract")]			Sub		= UnityEngine.Rendering.BlendOp.Subtract,
        [Enums.Order(2), Enums.Label("Reverse Subtract")]	RevSub	= UnityEngine.Rendering.BlendOp.ReverseSubtract,
        [Enums.Order(3)]									Min		= UnityEngine.Rendering.BlendOp.Min,
        [Enums.Order(4)]									Max		= UnityEngine.Rendering.BlendOp.Max
    }
}
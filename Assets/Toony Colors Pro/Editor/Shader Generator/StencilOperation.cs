namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    public enum StencilOperation
    {
        [Enums.Order(0)]										Keep = UnityEngine.Rendering.StencilOp.Keep,
        [Enums.Order(1)]										Zero = UnityEngine.Rendering.StencilOp.Zero,
        [Enums.Order(2)]										Replace = UnityEngine.Rendering.StencilOp.Replace,
        [Enums.Order(3)]										Invert = UnityEngine.Rendering.StencilOp.Invert,
        [Enums.Order(4), Enums.Label("Increment Saturate")]		IncrSat = UnityEngine.Rendering.StencilOp.IncrementSaturate,
        [Enums.Order(5), Enums.Label("Decrement Saturate")]		DecrSat = UnityEngine.Rendering.StencilOp.DecrementSaturate,
        [Enums.Order(6), Enums.Label("Increment Wrap")]			IncrWrap = UnityEngine.Rendering.StencilOp.IncrementWrap,
        [Enums.Order(7), Enums.Label("Decrement Wrap")]			DecrWrap = UnityEngine.Rendering.StencilOp.DecrementWrap,
    }
}
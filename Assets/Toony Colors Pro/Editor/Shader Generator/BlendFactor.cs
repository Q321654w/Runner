namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    public enum BlendFactor
    {
        [Enums.Order(0)]										One					= UnityEngine.Rendering.BlendMode.One,
        [Enums.Order(1)]										Zero				= UnityEngine.Rendering.BlendMode.Zero,
        [Enums.Order(2), Enums.Label("Source Color")]			SrcColor			= UnityEngine.Rendering.BlendMode.SrcColor,
        [Enums.Order(3), Enums.Label("1 - Source Color")]		OneMinusSrcColor	= UnityEngine.Rendering.BlendMode.OneMinusSrcColor,
        [Enums.Order(4), Enums.Label("Destination Color")]		DstColor			= UnityEngine.Rendering.BlendMode.DstColor,
        [Enums.Order(5), Enums.Label("1 - Destination Color")]	OneMinusDstColor	= UnityEngine.Rendering.BlendMode.OneMinusDstColor,
        [Enums.Order(6), Enums.Label("Source Alpha")]			SrcAlpha			= UnityEngine.Rendering.BlendMode.SrcAlpha,
        [Enums.Order(7), Enums.Label("1 - Source Alpha")]		OneMinusSrcAlpha	= UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha,
        [Enums.Order(8), Enums.Label("Destination Alpha")]		DstAlpha			= UnityEngine.Rendering.BlendMode.DstAlpha,
        [Enums.Order(9), Enums.Label("1 - Destination Alpha")]	OneMinusDstAlpha	= UnityEngine.Rendering.BlendMode.OneMinusDstAlpha
    }
}
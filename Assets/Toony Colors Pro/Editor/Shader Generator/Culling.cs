namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    public enum Culling
    {
        [Enums.Order(0), Enums.Label("Back faces")]			Back	= UnityEngine.Rendering.CullMode.Back,
        [Enums.Order(1), Enums.Label("Front faces")]		Front	= UnityEngine.Rendering.CullMode.Front,
        [Enums.Order(2), Enums.Label("Off (double-sided)")]	Off		= UnityEngine.Rendering.CullMode.Off
    }
}
using UnityEditor;

namespace Toony_Colors_Pro.Scripts
{
    [CustomEditor(typeof(Runtime.TCP2_PlanarReflection))]
    class TCP2_PlanarReflectionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This script only works with axis-aligned meshes.\nMake sure that the GameObject isn't rotated (e.g. it will work with the \"Plane\" built-in mesh, but not with the \"Quad\" one).", MessageType.Info);
            base.OnInspectorGUI();
        }
    }
}
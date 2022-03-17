namespace Toony_Colors_Pro.Editor.Utils
{
    public class TCP2Vector3FloatsDrawer : TCP2Vector4FloatsDrawer
    {
        public TCP2Vector3FloatsDrawer(string labelX, string labelY, string labelZ) : base(labelX, labelY, labelZ, "")
        {
            channelsCount = 3;
        }

        public TCP2Vector3FloatsDrawer(string labelX, string labelY, string labelZ, float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
            : base(labelX, labelY, labelZ, "", minX, maxX, minY, maxY, minZ, maxZ, 0, 0)
        {
            channelsCount = 3;
        }
    }
}
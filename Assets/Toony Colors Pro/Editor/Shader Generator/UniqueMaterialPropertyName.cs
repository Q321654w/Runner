namespace Toony_Colors_Pro.Editor.Shader_Generator
{
    internal static class UniqueMaterialPropertyName
    {
        internal delegate bool CheckUniqueVariableName(string variableName, IMaterialPropertyName materialPropertyName);
        internal static event CheckUniqueVariableName checkUniqueVariableName;

        internal static string GetUniquePropertyName(string baseName, IMaterialPropertyName materialPropertyName)
        {
            if (checkUniqueVariableName == null)
            {
                return baseName;
            }

            //name doesn't exist: all good
            if (checkUniqueVariableName(baseName, materialPropertyName))
                return baseName;

            //extract the last digits of the name, if any
            for (var i = baseName.Length - 1; i >= 0; i--)
            {
                if (baseName[i] >= '0' && baseName[i] <= '9')
                    continue;
                baseName = baseName.Substring(0, i + 1);
                break;
            }

            //check if name is unique: requires a class that registers to the event and supply its own checks
            var newName = baseName;
            var count = 1;
            while (!checkUniqueVariableName(newName, materialPropertyName))
            {
                newName = string.Format("{0}{1}", baseName, count);
                count++;
            }

            return newName;
        }
    }
}
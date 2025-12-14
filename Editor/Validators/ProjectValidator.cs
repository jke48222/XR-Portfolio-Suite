#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using XrShared.Core.App;

namespace XrShared.Editor.Validators
{
    public static class ProjectValidator
    {
        public static ValidationReport ValidateActiveProject()
        {
            ValidationReport r = new ValidationReport();

            if (EditorBuildSettings.scenes == null || EditorBuildSettings.scenes.Length == 0)
            {
                r.AddWarning("No scenes in Build Settings. Add Bootstrap and Experience scenes.");
            }

            AppConfig[] configs = AssetDatabase.FindAssets("t:AppConfig")
                .Select(guid => AssetDatabase.LoadAssetAtPath<AppConfig>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();

            if (configs.Length == 0)
            {
                r.AddWarning("No AppConfig assets found. Create one under Assets/_App/Config.");
            }

            return r;
        }
    }
}
#endif

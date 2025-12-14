#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using XrShared.Editor.Validators;

namespace XrShared.Editor.Build
{
    public static class BuildMenu
    {
        [MenuItem("XR Suite/Validate Project")]
        public static void ValidateProject()
        {
            ValidationReport r = ProjectValidator.ValidateActiveProject();

            foreach (string w in r.warnings) Debug.LogWarning("[Validation] " + w);
            foreach (string e in r.errors) Debug.LogError("[Validation] " + e);

            if (!r.HasErrors) Debug.Log("Validation complete. No errors.");
        }
    }
}
#endif

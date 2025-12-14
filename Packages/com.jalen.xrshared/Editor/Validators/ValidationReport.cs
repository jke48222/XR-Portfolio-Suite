using System.Collections.Generic;

namespace XrShared.Editor.Validators
{
    public sealed class ValidationReport
    {
        public readonly List<string> errors = new List<string>();
        public readonly List<string> warnings = new List<string>();

        public bool HasErrors => errors.Count > 0;

        public void AddError(string msg) => errors.Add(msg);
        public void AddWarning(string msg) => warnings.Add(msg);
    }
}

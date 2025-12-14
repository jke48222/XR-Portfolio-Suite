using System;

namespace XrShared.Core.Data
{
    [Serializable]
    public sealed class SaveSchema<T> where T : class
    {
        public int schemaVersion;
        public string savedAtIsoUtc;
        public T payload;
    }
}

namespace XrShared.Core.Data
{
    public interface IPersistenceStore
    {
        int SchemaVersion { get; }
        void Save<T>(T data) where T : class;
        bool TryLoad<T>(out T data) where T : class;
        void Delete();
    }
}

namespace XrShared.MR.Compositing
{
    public interface IPassthroughController
    {
        bool IsAvailable { get; }
        void SetEnabled(bool enabled);
    }
}

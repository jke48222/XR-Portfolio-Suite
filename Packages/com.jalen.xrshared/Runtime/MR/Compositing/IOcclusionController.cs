namespace XrShared.MR.Compositing
{
    public interface IOcclusionController
    {
        bool IsAvailable { get; }
        void SetEnabled(bool enabled);
    }
}

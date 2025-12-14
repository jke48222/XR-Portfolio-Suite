namespace XrShared.MR.Compositing
{
    public sealed class NullPassthroughController : IPassthroughController
    {
        public bool IsAvailable => false;
        public void SetEnabled(bool enabled) { }
    }
}

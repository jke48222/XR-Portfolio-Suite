namespace XrShared.MR.Compositing
{
    public sealed class NullOcclusionController : IOcclusionController
    {
        public bool IsAvailable => false;
        public void SetEnabled(bool enabled) { }
    }
}

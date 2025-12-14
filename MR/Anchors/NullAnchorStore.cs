namespace XrShared.MR.Anchors
{
    public sealed class NullAnchorStore : IAnchorStore
    {
        public bool IsAvailable => false;
    }
}

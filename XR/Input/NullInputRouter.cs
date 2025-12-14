namespace XrShared.XR.Input
{
    public sealed class NullInputRouter : IInputRouter
    {
        public bool IsHandsMode => false;
        public bool IsControllersMode => true;
    }
}

namespace XrShared.XR.Input
{
    public interface IInputRouter
    {
        bool IsHandsMode { get; }
        bool IsControllersMode { get; }
    }
}

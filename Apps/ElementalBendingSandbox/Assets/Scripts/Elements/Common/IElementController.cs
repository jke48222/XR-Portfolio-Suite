using ElementalBendingSandbox.Gestures;

namespace ElementalBendingSandbox.Elements.Common
{
    public interface IElementController
    {
        ElementState CurrentState { get; }
        ElementOutputState Output { get; }
        void ProcessIntent(in GestureIntentData intent, float deltaTime);
    }

    public enum ElementState
    {
        Idle = 0,
        Engaging,
        Sustaining,
        Releasing,
        Unstable
    }
}

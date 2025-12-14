namespace XrShared.Core.Tick
{
    public interface ITickable
    {
        void Tick(float dt);
    }

    public interface IFixedTickable
    {
        void FixedTick(float fixedDt);
    }

    public interface ILateTickable
    {
        void LateTick(float dt);
    }
}

using System.Collections.Generic;
using UnityEngine;
using XrShared.Core.Services;

namespace XrShared.Core.Tick
{
    public sealed class TickRunner : MonoBehaviour
    {
        private ServiceLocator _services;

        private readonly List<ITickable> _tickables = new List<ITickable>(64);
        private readonly List<IFixedTickable> _fixedTickables = new List<IFixedTickable>(64);
        private readonly List<ILateTickable> _lateTickables = new List<ILateTickable>(64);

        public void Initialize(ServiceLocator services)
        {
            _services = services;
            RebuildTickLists();
        }

        public void RebuildTickLists()
        {
            _tickables.Clear();
            _fixedTickables.Clear();
            _lateTickables.Clear();

            MonoBehaviour[] behaviours = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (MonoBehaviour b in behaviours)
            {
                if (b is ITickable t) _tickables.Add(t);
                if (b is IFixedTickable f) _fixedTickables.Add(f);
                if (b is ILateTickable l) _lateTickables.Add(l);
            }
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            for (int i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].Tick(dt);
            }
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            for (int i = 0; i < _fixedTickables.Count; i++)
            {
                _fixedTickables[i].FixedTick(dt);
            }
        }

        private void LateUpdate()
        {
            float dt = Time.deltaTime;
            for (int i = 0; i < _lateTickables.Count; i++)
            {
                _lateTickables[i].LateTick(dt);
            }
        }
    }
}

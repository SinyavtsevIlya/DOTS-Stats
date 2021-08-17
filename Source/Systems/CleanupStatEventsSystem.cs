using Unity.Entities;

namespace Nanory.Unity.Entities.Stats
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public class CleanupStatEventsSystem : SystemBase
    {
        private EntityQuery _changedStats;
        private EntityQuery _removedStats;

        protected override void OnCreate()
        {
            _changedStats = GetEntityQuery(ComponentType.ReadOnly<StatsChangedEvent>());
            _removedStats = GetEntityQuery(ComponentType.ReadOnly<StatsRemovedEvent>());
        }
        protected override void OnUpdate()
        {
            EntityManager.RemoveComponent<StatsChangedEvent>(_changedStats);
            EntityManager.RemoveComponent<StatsRemovedEvent>(_removedStats);

            Entities
                .ForEach((DynamicBuffer<StatRecievedElementEvent> statRecievedElementEvents) =>
                {
                    statRecievedElementEvents.Clear();
                })
                .ScheduleParallel();
        }
    }
}


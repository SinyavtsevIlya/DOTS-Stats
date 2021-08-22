using Unity.Entities;
using Unity.Collections;

namespace Nanory.Unity.Entities.Stats
{
    [UpdateInGroup(typeof(StatSystemGroup), OrderFirst = true)]
    public class UpdateStatRecieverLinkSystem : SystemBase
    {
        private EntityQuery _changeStatsRequests;
        private EntityQuery _removeStatsRequests;

        protected override void OnCreate()
        {
            _changeStatsRequests = GetEntityQuery(
                ComponentType.ReadOnly<StatsChangedRequest>());

            _removeStatsRequests = GetEntityQuery(
                ComponentType.ReadOnly<StatsRemovedRequest>());
        }

        protected override void OnUpdate()
        {
            ProcessChangeStatRequests();
            ProcessRemoveStatRequests();
        }

        private void ProcessChangeStatRequests()
        {
            var changeStateRequests = _changeStatsRequests.ToComponentDataArray<StatsChangedRequest>(Allocator.TempJob);

            for (int idx = 0; idx < changeStateRequests.Length; idx++)
            {
                var request = changeStateRequests[idx];
                EntityManager.SetStatsChanged(request.StatContext, request.StatReceiver);
            }

            EntityManager.DestroyEntity(_changeStatsRequests);

            changeStateRequests.Dispose();
        }

        private void ProcessRemoveStatRequests()
        {
            var removeStatsRequests = _removeStatsRequests.ToComponentDataArray<StatsRemovedRequest>(Allocator.TempJob);

            for (int idx = 0; idx < removeStatsRequests.Length; idx++)
            {
                var request = removeStatsRequests[idx];
                EntityManager.SetStatsRemoved(request.StatContext);
            }

            EntityManager.DestroyEntity(_removeStatsRequests);

            removeStatsRequests.Dispose();
        }
    }
}

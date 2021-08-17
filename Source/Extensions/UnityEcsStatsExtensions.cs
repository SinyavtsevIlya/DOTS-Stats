using Unity.Entities;
using Unity.Collections;

namespace Nanory.Unity.Entities.Stats
{
    public static class UnityEcsStatsExtensions
    {
        /// <summary>
        /// Notifies the <see cref="StatRecieverTag">Stat-Reciver</see> entity about applying the Stat of statContextEntity.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="statContextEntity">Entity which holds one or more Stat-Entities (e.g. sword, potion)</param>
        /// <param name="statReceiverEntity">Entity which receives and accumulates all it's Stat-Entities values. <see cref="StatRecieverTag">See more</see></param>
        public static void SetStatsChanged(this EntityManager manager, Entity statContextEntity, Entity statReceiverEntity)
        {
            if (!manager.HasComponent<StatElement>(statContextEntity)) return; 

            var statEntites = manager.GetBuffer<StatElement>(statContextEntity).ToNativeArray(Allocator.Temp);
           
            for (int i = 0; i < statEntites.Length; i++)
            {
                var statEntity = statEntites[i].Value;
                manager.AddComponent<StatsChangedEvent>(statEntity);

                var statReceiverLink = new StatReceiverLink() { Value = statReceiverEntity };

                if (manager.HasComponent<StatReceiverLink>(statContextEntity))
                    manager.SetSharedComponentData(statEntity, statReceiverLink);
                else
                    manager.AddSharedComponentData(statEntity, statReceiverLink);
            }
        }
        /// <summary>
        /// Notifies the <see cref="StatRecieverTag">Stat-Reciver</see> entity about removing the Stat of statContextEntity.
        /// </summary>
        /// <param name="statContextEntity">Entity which holds one or more Stat-Entities (e.g. sword, potion)</param>
        public static void SetStatsRemoved(this EntityManager manager, Entity statContextEntity)
        {
            if (!manager.HasComponent<StatElement>(statContextEntity)) return;

            var statEntites = manager.GetBuffer<StatElement>(statContextEntity).ToNativeArray(Allocator.Temp);

            for (int i = 0; i < statEntites.Length; i++)
            {
                var statEntity = statEntites[i].Value;
                manager.AddComponent<StatsRemovedEvent>(statEntity);
                
            }
        }
    }
}

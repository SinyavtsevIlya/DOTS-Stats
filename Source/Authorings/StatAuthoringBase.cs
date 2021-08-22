using Unity.Entities;
using UnityEngine;
using System;

namespace Nanory.Unity.Entities.Stats
{
    /// <summary>
    /// Base Authoring for all Stat Components. 
    /// </summary>
    /// <typeparam name="TStatComponent"></typeparam>
    public abstract class StatAuthoringBase<TStatComponent> : MonoBehaviour, IStatAuthoring, IConvertGameObjectToEntity where TStatComponent : struct, IComponentData
    {
        [SerializeField] StatOpType _opType;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var statEntity = dstManager.CreateEntity();
            dstManager.AddComponentData(statEntity, GetStat());

            if (_opType == StatOpType.Additive)
                dstManager.AddComponent<AdditiveStatTag>(statEntity);
            if (_opType == StatOpType.Multiply)
                dstManager.AddComponent<MultiplyStatTag>(statEntity);

            if (!dstManager.HasComponent<StatElement>(entity))
                dstManager.AddBuffer<StatElement>(entity);

            var stats = dstManager.GetBuffer<StatElement>(entity);

            stats.Add(new StatElement() { Value = statEntity });

            if (dstManager.HasComponent<StatReceiverTag>(entity))
            {
                dstManager.AddSharedComponentData(statEntity, new StatReceiverLink() { Value = entity });
                dstManager.AddComponentData(entity, GetStat());
            }
        }

        /// <summary>
        /// Expects the new instance of TStatComponent created by the conversion process.
        /// </summary>
        protected abstract TStatComponent GetStat();
    }

    internal interface IStatAuthoring
    {
    }
}

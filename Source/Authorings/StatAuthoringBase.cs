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
        [SerializeField]StatOpType _opType;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var statEntity = dstManager.CreateEntity();
            AddStatComponentToEntity(statEntity, dstManager, conversionSystem);

            if (_opType == StatOpType.Additive)
                dstManager.AddComponent<AdditiveStatTag>(statEntity);
            if (_opType == StatOpType.Multiply)
                dstManager.AddComponent<MultiplyStatTag>(statEntity);

            if (!dstManager.HasComponent<StatElement>(entity))
                dstManager.AddBuffer<StatElement>(entity);

            var stats = dstManager.GetBuffer<StatElement>(entity);

            stats.Add(new StatElement() { Value = statEntity });

            if (dstManager.HasComponent<StatReceiverTag>(entity))
                dstManager.AddSharedComponentData(statEntity, new StatReceiverLink() { Value = entity });
        }

        /// <summary>
        /// Simply add the <see cref="TStatComponent">concreate Stat Component</see> to the entity, using dstManager.
        /// </summary>
        public abstract void AddStatComponentToEntity(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem);
    }

    internal interface IStatAuthoring
    {
        void AddStatComponentToEntity(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem);
    }
}

using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Nanory.Unity.Entities.Stats
{
    /// <summary>
    /// Authoring for <see cref="StatReceiverTag">Stat Reciever Tag</see>
    /// </summary>
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class StatRecieverAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
#if UNITY_EDITOR
        private void OnEnable()
        {
            var comps = GetComponents<MonoBehaviour>();
            var thisIdx = new List<MonoBehaviour>(comps).IndexOf(this);
            for (var idx = 0; idx < comps.Length; idx++)
            {
                if (comps[idx] is IStatAuthoring)
                    if (idx < thisIdx)
                        Debug.LogError($"{nameof(StatRecieverAuthoring)} should be placed before all Stats Component authoring's", this);
            }
        }
#endif
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<StatReceiverTag>(entity);
            dstManager.AddBuffer<StatRecievedElementEvent>(entity);

            foreach (var statAuthoring in GetComponents<IStatAuthoring>())
            {
                statAuthoring.AddStatComponentToEntity(entity, dstManager, conversionSystem);
            }
        }
    }
}

using Unity.Entities;

namespace Nanory.Unity.Entities.Stats
{
    /// <summary>
    /// The Stat-Entity marked with this tag applies the effect 
    /// to the <see cref="StatReceiverTag">Stat-Receiver</see> using the addition/subtraction operation
    /// </summary>
    public struct AdditiveStatTag : IComponentData { }
    /// <summary>
    /// The Stat-Entity marked with this tag applies the effect 
    /// to the <see cref="StatReceiverTag">Stat-Receiver</see> using the multiply/divide operation
    /// </summary>
    public struct MultiplyStatTag : IComponentData { }

    /// <summary>
    /// Request of applying Stat-Entity (to the <see cref="StatReceiverTag">Stat-Receiver</see>) or changing it's value
    /// </summary>
    public struct StatsChangedRequest : IComponentData
    {
        public Entity StatContext;
        public Entity StatReceiver;
    }

    /// <summary>
    /// Request of removing Stat-Entity (from the <see cref="StatReceiverTag">Stat-Receiver</see>)
    /// </summary>
    public struct StatsRemovedRequest : IComponentData
    {
        public Entity StatContext;
    }

    /// <summary>
    /// Event of applying Stat-Entity (to the <see cref="StatReceiverTag">Stat-Receiver</see>) or changing it's value
    /// </summary>
    public struct StatsChangedEvent : IComponentData { }
    /// <summary>
    /// Event of removing Stat-Entity (from the <see cref="StatReceiverTag">Stat-Receiver</see>)
    /// </summary>
    public struct StatsRemovedEvent : IComponentData { }

    /// <summary>
    /// An event that any of the Stat-Entity of <see cref="StatReceiverTag">Stat-Receiver</see> has changed. Always placed on the <see cref="StatReceiverTag">Stat-Receiver</see> entity
    /// </summary>
    public struct StatRecievedElementEvent : IBufferElementData 
    {
        public ComponentType StatType;

        /// <summary>
        /// Defines the type of Stat that has changed
        /// </summary>
        /// <typeparam name="TStatComponent">Type of Stat that has changed</typeparam>
        /// <returns></returns>
        public bool Is<TStatComponent>() where TStatComponent : struct, IComponentData 
            => StatType == ComponentType.ReadOnly<TStatComponent>();
    }

    /// <summary>
    /// Reference to the <see cref="StatReceiverTag">Stat-Receiver</see> entity to which this effect will be applied
    /// </summary>
    internal struct StatReceiverLink : ISharedComponentData
    {
        public Entity Value;
    }

    /// <summary>
    /// This tag determines the entity as a "Stat-Receiver". This means that the entity 
    /// may accumulate stats values from it's children Stat-Entities. 
    /// As soon as the value of the Stat-Entity changes, the value of the Stat-Receiver also changes.
    /// </summary>
    public struct StatReceiverTag : IComponentData { }

    /// <summary>
    /// Stat-Entity - is the entity which holds one or several Stat-Components (e.g. Strength, Max-Health, etc.) 
    /// It persists as a StatElement (dynamic buffer element) of a "Stat-Context" Entity. Stat-Context Entity can be anything, say an item (sword, gun, potion etc.) or a buff, an ability. 
    /// Stat-Entity also stores a reference
    /// to a Stat-Receiver entity to which this effect will be applied.
    /// </summary>
    public struct StatElement : IBufferElementData
    {
        public Entity Value;
    }

    /// <summary>
    /// Used only configuration. For runtime see <see cref="AdditiveStatTag">AdditiveStatTag</see> and <see cref="MultiplyStatTag">MultiplyStatTag</see>
    /// </summary>
    public enum StatOpType
    {
        Additive,
        Multiply
    }
}

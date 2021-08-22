using Unity.Entities;

namespace Nanory.Unity.Entities.Stats
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public class StatSystemGroup : ComponentSystemGroup
    {
    }
}

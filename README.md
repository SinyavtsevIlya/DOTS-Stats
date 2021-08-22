# DOTS-Stats
DOTS-Stats is a high performance and scalable Stats-System.

## Features

- Maximum flexibility. Thanks to the pure component approach, it allows you to implement abilities, items, buffs, potions and make them work together as one single thing.
- High performance. Zero GC allocations.
- 100% ECS-ish. No more complex abstact OOP classes that work here but don't work there.
- Ready-made basic Authoring component for customization in the editor
- Supports *multiplying* and *additive* stats
- The minimum amount of boilerplate code.
- Compatible with burst.
- Compatible with il2cpp.

## HowTo

### Installation
Requirement: Unity >= 2019.4.18 and entities package >= 0.14.0-preview.19

### Usage

#### Create a new Stat component you need

``` c#
public struct Attack : IComponentData
{
    // The only constraint is you must have a float field. 
    public float Value; 
}
```

#### Create a new Authoring for this Stat

``` c#
public class AttackAuthoring : StatAuthoringBase<Attack>
{
    [SerializeField] float _value;
    // Simply return a new instance of "Attack" and set it's value from the serialized field. 
    protected override Attack GetStat() => new Attack() { Value = _value };
}
```

## How it works under the hood

![Self-editing Diagram](Diagram.svg)

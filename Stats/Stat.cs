using Game.Companions;

namespace Game.Stats;

/// <summary>
/// An enum representing the different stats a <see cref="Pokemon"/> can have.
/// See: https://bulbapedia.bulbagarden.net/wiki/Stat#List_of_stats
/// </summary>
public enum Stat
{
    Health,
    Attack,
    Defense,
    SpecialAttack,
    SpecialDefense,
    Speed,
    Accuracy,
    Evasion
}
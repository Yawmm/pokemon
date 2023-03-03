using Game.Companions;

namespace Game.Statuses;

/// <summary>
/// An enum representing the different status conditions a <see cref="Pokemon"/> can have.
/// See: https://bulbapedia.bulbagarden.net/wiki/Status_condition#Non-volatile_status
/// </summary>
public enum PokemonStatus
{
    Burn,
    Freeze,
    Poison,
    Flinch
}
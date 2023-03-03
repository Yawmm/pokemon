using Game.Moves.Checks;
using Game.Moves.Cleanups;
using Game.Moves.Damage;
using Game.Moves.Damage.Modifiers;
using Game.Moves.Effects;
using Newtonsoft.Json;

namespace Game.Moves;

/// <summary>
/// A builder class used to create a finished <see cref="PokemonMoveStage"/>.
/// </summary>
public class PokemonMoveStageBuilder
{
    public PokemonMoveStageBuilder(PokemonMoveBuilder parent)
    {
        _parent = parent;
        _instance = new PokemonMoveStage
        {
            Checks = new ()
            {
                new FlinchCheck(), 
                new FrozenCheck(),
                new EvasionCheck(),
                new AccuracyCheck(), 
                new PPCheck()
            },
            Cleanups = new (){ new PPCleanup() },
            Modifiers = new ()
            {
                new CritModifier(), 
                new StabModifier(),
                new EffectivenessModifier(), 
                new RandomModifier()
            }
        };
    }
    
    /// <summary>
    /// The instance of the <see cref="PokemonMove"/> being constructed.
    /// </summary>
    private readonly PokemonMoveStage _instance;
    
    /// <summary>
    /// The parent builder of the <see cref="PokemonMoveStageBuilder"/>.
    /// </summary>
    private readonly PokemonMoveBuilder _parent;

    /// <summary>
    /// Add a range of <see cref="IMoveCheck"/> to the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="checks">The range of <see cref="IMoveCheck"/> that should be added to the <see cref="PokemonMoveStage"/>.</param>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder WithChecks(IEnumerable<IMoveCheck> checks)
    {
        _instance.Checks = checks.ToList();
        return this;
    } 
    
    /// <summary>
    /// Set a range of <see cref="IMoveEffect"/> on the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="effects">The range of <see cref="IMoveEffect"/> that should be set on the <see cref="PokemonMoveStage"/>.</param>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder WithEffects(IEnumerable<IMoveEffect> effects)
    {
        _instance.Effects = effects.ToList();
        return this;
    }

    /// <summary>
    /// Add an <see cref="IMoveEffect"/> to the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="effect">The <see cref="IMoveEffect"/> that should be added.</param>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder AddEffect(IMoveEffect effect)
        => WithEffects(_instance.Effects.Append(effect));

    /// <summary>
    /// Set a range of <see cref="IMoveEffect"/> suitable to stages that only hit on the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder WithHitEffects()
        => WithEffects(new IMoveEffect[] { new HitEffect() });
    
    /// <summary>
    /// Set a range of <see cref="IMoveCleanup"/> on the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="cleanups">The range of <see cref="IMoveCleanup"/> that should be added to the <see cref="PokemonMoveStage"/>.</param>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder WithCleanups(IEnumerable<IMoveCleanup> cleanups)
    {
        _instance.Cleanups = cleanups.ToList();
        return this;
    }

    /// <summary>
    /// Set a range of <see cref="IMoveCleanup"/> suitable to stages that only hit on the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder WithHitCleanups()
        => WithCleanups(new IMoveCleanup[] { new KillCleanup(), new PPCleanup() });
    
    /// <summary>
    /// Set a range of <see cref="IDamageModifier"/> on the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="modifiers">The range of <see cref="IDamageModifier"/> that should be added set on <see cref="PokemonMoveStage"/>.</param>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder WithDamageModifiers(IEnumerable<IDamageModifier> modifiers)
    {
        _instance.Modifiers = modifiers.ToList();
        return this;
    }

    /// <summary>
    /// Replace a <see cref="IDamageModifier"/> on the <see cref="PokemonMoveStage"/>.
    /// </summary>
    /// <param name="replacement">The replacing <see cref="IDamageModifier"/>.</param>
    /// <typeparam name="T">The type of <see cref="IDamageModifier"/> that should be replaced.</typeparam>
    /// <returns>The same <see cref="PokemonMoveStageBuilder"/> from which the <see cref="PokemonMoveStage"/> can be built.</returns>
    public PokemonMoveStageBuilder ReplaceDamageModifier<T>(T replacement)
        where T : IDamageModifier
        => WithDamageModifiers(_instance.Modifiers
            .Where(m => m.GetType() != typeof(T))
            .Append(replacement)
        );

    /// <summary>
    /// Add the <see cref="PokemonMoveStage"/> to the <see cref="PokemonMove"/>.
    /// </summary>
    /// <returns>The same <see cref="PokemonMoveBuilder"/> from which the <see cref="PokemonMove"/> can be built.</returns>
    public PokemonMoveBuilder Add(int amount = 1)
    {
        if (amount <= 1)
        {
            _parent.AddStage(_instance);
            return _parent;
        }
        
        // Duplicate instance for more stages
        for (var i = 0; i < amount; i++)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            
            var json = JsonConvert.SerializeObject(_instance, settings);
            var duplicate = JsonConvert.DeserializeObject<PokemonMoveStage>(json, settings);

            if (duplicate is not null)
                _parent.AddStage(duplicate);
        }

        return _parent;
    }
}
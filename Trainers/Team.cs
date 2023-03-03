using Game.Battles;
using Game.Companions;

namespace Game.Trainers;

/// <summary>
/// A class representing the team of a <see cref="Trainer"/> in a <see cref="Battle"/>.
/// </summary>
public class Team
{
    public Team(Trainer owner, List<Pokemon> members, Pokemon actor)
    {
        Owner = owner;
        
        foreach (var item in members)
            AddMember(item);
        
        Actor = actor;
    }
    
    /// <summary>
    /// The range of <see cref="Pokemon"/> in the <see cref="Team"/>.
    /// </summary>
    public List<Pokemon> Members { get; } = new();
    
    /// <summary>
    /// The <see cref="Pokemon"/> who is taking all the actions of the <see cref="Team"/>.
    /// </summary>
    public Pokemon Actor { get; set; }
    
    /// <summary>
    /// The owning <see cref="Trainer"/> of the <see cref="Team"/>.
    /// </summary>
    public Trainer Owner { get; }

    /// <summary>
    /// Whether or not the <see cref="Team"/> has been defeated.
    /// </summary>
    public bool IsDefeated => Members.All(t => t.Whiteout);

    /// <summary>
    /// Remove a member from the <see cref="Team"/>.
    /// </summary>
    /// <param name="pokemon">The member who should be removed.</param>
    public void RemoveMember(Pokemon pokemon)
    {
        Members.Remove(pokemon);
        pokemon.Team = null;
    }
    
    /// <summary>
    /// Add a member to the <see cref="Team"/>.
    /// </summary>
    /// <param name="pokemon">The member who should be added.</param>
    public void AddMember(Pokemon pokemon)
    {
        Members.Add(pokemon);
        pokemon.Team = this;
    }

    /// <summary>
    /// Replace the <see cref="Actor"/> of the <see cref="Team"/>.
    /// </summary>
    /// <param name="replacements">The available replacements of the current <see cref="Actor"/>.</param>
    /// <returns>The member who replaced the previous <see cref="Actor"/>.</returns>
    public Pokemon ReplaceActor(IEnumerable<Pokemon>? replacements = null)
    {
        replacements ??= Members.Where(p => !p.Whiteout);
        
        var options = replacements.ToList();
        if (!options.Any())
            return null;
        
        var replacement = Owner.Decider.Single($"Your [{Colors.Pokemon}]actor[/] has fainted! Please choose a replacement.", options);
        Actor = replacement;

        return Actor;
    }

    public override string ToString() => Owner.ToString();
}
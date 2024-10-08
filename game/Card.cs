using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
  // Abstract class Card, representing a basic card
  public abstract class Card
  {
    public string Name { get; set; }
    public string Type { get; set; }
    public int Damage { get; set; }


    public Card(string name, string type, int damage)
    {
      Name = name;
      Type = type;
      Damage = damage;
    }

    public virtual void Display()
    {
      Console.WriteLine($"Card Name: {Name}, Type: {Type},Damage: {Damage}");
    }
  }

  // MagicCard class, derived from Card, representing a card with additional effects
  public class MagicCard : Card
  {
    // Special effect of the magic card
    public string Effect { get; set; }

    // Constructor to initialize the magic card's name, type, damage, and effect
    public MagicCard(string name, string type, int damage, string effect)
        : base(name, type, damage)
    {
      Effect = effect;
    }

    // Override the Display method to show the magic card's base information and effect
    public override void Display()
    {
      base.Display();
      Console.WriteLine($"Effect: {Effect}");
    }
  }

  // MonsterCard class, derived from Card, representing a card with a race attribute
  public class MonsterCard : Card
  {
    public string Race { get; set; }

    // Constructor to initialize the monster card's name, type, damage, and race
    public MonsterCard(string name, string type, int damage, string race)
        : base(name, type, damage)
    {
      Race = race;
    }

    // Override the Display method to show the monster card's base information and race
    public override void Display()
    {
      base.Display();
      Console.WriteLine($"Race: {Race}");
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
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


  public class MagicCard : Card
  {
    public string Effect { get; set; }


    public MagicCard(string name, string type, int damage, string effect)
        : base(name, type, damage)
    {
      Effect = effect;
    }


    public override void Display()
    {
      base.Display();
      Console.WriteLine($"Effect: {Effect}");
    }
  }


  public class MonsterCard : Card
  {

    public string Race { get; set; }


    public MonsterCard(string name, string type, int damage, string race)
        : base(name, type, damage)
    {
      Race = race;
    }


    public override void Display()
    {
      base.Display();
      Console.WriteLine($"Race: {Race}");
    }
  }
}

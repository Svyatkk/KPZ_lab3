namespace Task2_Decorator
{
    public interface IHero
    {
        string GetDescription();
    }

    public class Warrior : IHero { public string GetDescription() => "Warrior"; }
    public class Mage : IHero { public string GetDescription() => "Mage"; }
    public class Palladin : IHero { public string GetDescription() => "Palladin"; }

    public abstract class InventoryDecorator : IHero
    {
        protected IHero _hero;
        public InventoryDecorator(IHero hero) { _hero = hero; }
        public virtual string GetDescription() => _hero.GetDescription();
    }

    public class WeaponDecorator : InventoryDecorator
    {
        private string _weapon;
        public WeaponDecorator(IHero hero, string weapon) : base(hero) { _weapon = weapon; }
        public override string GetDescription() => $"{base.GetDescription()} with {_weapon}";
    }

    public class ArmorDecorator : InventoryDecorator
    {
        public ArmorDecorator(IHero hero) : base(hero) { }
        public override string GetDescription() => $"{base.GetDescription()} wearing Armor";
    }
}
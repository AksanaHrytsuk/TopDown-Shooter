

public class MedicineChest : PickUp
{
    private BaseClass baseClass;
    public float addHealth;
    
    
    public override void Apply()
    {
        if (GetPlayer().health + addHealth <= GetPlayer().maxHealth)
        {
            GetPlayer().health += addHealth;
        }
        else
        {
            GetPlayer().health = GetPlayer().maxHealth;
        }
    }
}

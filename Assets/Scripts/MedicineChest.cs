using UnityEngine.UI;

public class MedicineChest : PickUp
{
    
    public float addHealth;

    public override void Apply()
    {
        if (GetPlayer().health + addHealth <= GetPlayer().maxHealth)
        {
            GetPlayer().health += addHealth;
            GetPlayerUI().UpdateSlider();
        }
        else
        {
            GetPlayer().health = GetPlayer().maxHealth;
            GetPlayerUI().UpdateSlider();

        }
    }
}

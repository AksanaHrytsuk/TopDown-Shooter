using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int hpPrice;
    public int ammoPrice; 
    
    public Text moneyUi;
    public Text ammoUI;
    public GameObject shopPanel;

    private Weapon weapon;
    private Ammo ammo;
    private MedicineChest medicineChest;
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Characters"))
        {
           shopPanel.SetActive(true);
           Time.timeScale = 0;
           player.enabled = false;
           player.GetWeapon().enabled = false;
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        medicineChest = FindObjectOfType<MedicineChest>();
        weapon = FindObjectOfType<Weapon>();
        ammo = FindObjectOfType<Ammo>();
    }

    void Start()
    {
        shopPanel.SetActive(false);
    }

    public void BuyHp()
    {
        if (player.Money1 >= hpPrice)
        {
            medicineChest.Apply();
            player.Money1 -= hpPrice;
            moneyUi.text = "$ " + player.Money1;
        }
    }
    
    public void BuyAmmo()
    {
        if (weapon.maxAmountBullets >= ammoPrice & player.Money1 >= hpPrice)
        {
            ammo.Apply();
            player.Money1 -= ammoPrice;
            moneyUi.text = "$ " + player.Money1;
            ammoUI.text = "Ammo: " + weapon.amountBullets;
        }
    }
    
    public void Resume()
    {
            player.enabled = true;
            player.GetWeapon().enabled = true;
            shopPanel.SetActive(false);
            Time.timeScale = 1;
    }
}

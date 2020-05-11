using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Player _player;
    public Slider healthSlider;
    
   
    void Start()
    {
        _player.onHealthChanged += UpdateSlider;

        healthSlider.maxValue = _player.health;
        healthSlider.value = _player.health;
    }

   public void UpdateSlider()
    {
        healthSlider.value = _player.health;
    }
    
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}

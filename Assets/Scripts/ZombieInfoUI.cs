using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieInfoUI : MonoBehaviour
{
    public Slider _slider;
    
    public Zombie _zombie;
    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = _zombie.health;
        _slider.value = _zombie.health;
        _zombie.onHealthChanged += UpdateSlider;
    }

    void UpdateSlider()
    {
        _slider.value = _zombie.health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}

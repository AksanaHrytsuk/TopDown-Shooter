﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieInfoUI : MonoBehaviour
{
    [Header("UI Eliments")]
    public Slider _slider;
    public Zombie _zombie;

    void Start()
    {
        _slider.maxValue = _zombie.health;
        _slider.value = _zombie.health;
        _zombie.onHealthChanged += UpdateSlider;
    }

    void UpdateSlider()
    {
        Debug.Log("Update slider");
        _slider.value = _zombie.health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
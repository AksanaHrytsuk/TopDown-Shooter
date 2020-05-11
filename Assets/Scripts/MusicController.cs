using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private void Awake()
    {
        {
            MusicController[] pointsList = FindObjectsOfType<MusicController>();
            if (pointsList.Length > 1)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}

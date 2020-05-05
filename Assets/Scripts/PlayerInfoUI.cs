using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    private Player _player;
    public Slider healthslider;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();

        healthslider.maxValue = _player.health;
    }

    // Update is called once per frame
    void Update()
    {
        healthslider.value = _player.health;
    }
}

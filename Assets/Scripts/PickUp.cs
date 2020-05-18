using UnityEngine;

public class PickUp : MonoBehaviour
{
    private PlayerInfoUI _playerInfoUi;
    private Player _player;
    private LoadNextLevel _loadNextLevel;

    public PlayerInfoUI GetPlayerUI()
    {
        return _playerInfoUi;
    }
    
    public LoadNextLevel GetLoadNextLvl()
    {
        return _loadNextLevel;
    }
    public Player GetPlayer()
    {
        return _player;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Characters"))
        {
         Apply();
         Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerInfoUi = FindObjectOfType<PlayerInfoUI>();
        _player = FindObjectOfType<Player>();
        _loadNextLevel = FindObjectOfType<LoadNextLevel>();
    }

   public virtual void Apply()
    {
        
    }
}

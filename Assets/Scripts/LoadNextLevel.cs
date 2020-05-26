using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [HideInInspector]
    public Player player;
    //[HideInInspector]
    public GameObject image;
    //[HideInInspector]
    public GameObject image1;
    [HideInInspector]
    public GameObject playerInfo;
    
    public bool startGame;
    

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        if (!startGame)
        {
            Instructions();
            image.SetActive(false);
            player.ifDeath += RestartMenu;
        }
    }

    public void LoadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1);
    }
    public void LoadNextSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

     public void RestartLevel()
     {
         string name = SceneManager.GetActiveScene().name;
         SceneManager.LoadScene(name);
     }

     private void Instructions()
     {
         if (!startGame)
         {
             image1.SetActive(true);
             Time.timeScale = 0;
             
         }
     }

     public void Resume()
     {
         if (!startGame)
         {
             image1.SetActive(false);
             Time.timeScale = 1;
         }
     }
     public void RestartMenu()
     {
         image.SetActive(true);
         //playerInfo.SetActive(false);
     }
}

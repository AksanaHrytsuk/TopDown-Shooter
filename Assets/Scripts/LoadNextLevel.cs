using UnityEngine.SceneManagement;

public class LoadNextLevel : PickUp
{
    public override void Apply()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex+1);
    }
}

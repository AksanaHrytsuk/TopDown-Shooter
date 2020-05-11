using UnityEngine.SceneManagement;

public class LoadNextLevelPickup : PickUp
{
    public override void Apply()
    {
        GetLoadNextLvl().LoadLevel();
    }
}

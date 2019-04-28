using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.Scripts.World.GridHandler;

public class GameVariables : MonoBehaviour
{
    public Level CurrentLevel;

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayGame(string levelName)
    {
        switch (levelName)
        {
            case "France":
                CurrentLevel = Level.FRANCE;
                break;
            default:
                CurrentLevel = Level.FULL;
                break;
        }

        SceneManager.LoadScene("GameScene");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

}

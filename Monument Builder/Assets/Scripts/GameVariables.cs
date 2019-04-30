using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameVariables : MonoBehaviour
    {
        public GridHandler.Level CurrentLevel;

        public void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void PlayGame(string levelName)
        {
            switch (levelName)
            {
                case "France":
                    CurrentLevel = GridHandler.Level.FRANCE;
                    break;
                case "Netherlands":
                    CurrentLevel = GridHandler.Level.NETHERLANDS;
                    break;
                default:
                    CurrentLevel = GridHandler.Level.FULL;
                    break;
            }

            SceneManager.LoadScene("GameScene");
        }

        public void ExitApplication()
        {
            Application.Quit();
        }

    }
}

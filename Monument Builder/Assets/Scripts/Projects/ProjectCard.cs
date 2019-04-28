using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Projects
{
    public class ProjectCard : MonoBehaviour
    {
        public GameObject[] Stars = new GameObject[3];
        public Text DescriptionText;

        public Project Project;
        public BuildingShape Building;

        void Start()
        {

        }

        public void Initialize()
        {
            for (var i = 0; i < (int)Project.Difficulty + 1; i++)
                Stars[i].SetActive(true);

            DescriptionText.text = Project.Name;
        }
    }
}

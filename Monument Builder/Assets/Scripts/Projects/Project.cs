
using Assets.Scripts.World;

namespace Assets.Scripts.Projects
{
    public class Project
    {
        public enum ProjectDifficulty
        {
            EASY,
            NORMAL,
            HARD
        }
        
        public ProjectDifficulty Difficulty;
        public int Occurance;
        public string Name;
        public BuildingShape Building;

        public Project(ProjectDifficulty difficulty, string name, BuildingShape building)
        {
            Difficulty = difficulty;
            Name = name;
            Building = building;
        }
    }
}

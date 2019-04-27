
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
        public int InitialFundingCost;
        public BuildingShape Building;

        public Project(ProjectDifficulty difficulty, string name, int fundingCost, BuildingShape building)
        {
            Difficulty = difficulty;
            Name = name;
            InitialFundingCost = fundingCost;
            Building = building;
        }
    }
}

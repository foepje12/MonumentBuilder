using System.Collections.Generic;
using Assets.Scripts.World;

namespace Assets.Scripts.Projects
{
    public class ProjectList
    {
        private static readonly List<Project> ProjectsList = new List<Project>
        {
            //Easy Projects
            new Project(Project.ProjectDifficulty.EASY, "Cube", 10000, new BuildingShape(0)),

            //Normal Projects
            new Project(Project.ProjectDifficulty.NORMAL, "Cube", 100000, new BuildingShape(1)),

            //Hard
            new Project(Project.ProjectDifficulty.HARD, "Cube", 1000000, new BuildingShape(3))
        };

        public Project GetRandomProject(Project.ProjectDifficulty difficulty)
        {
            var projects = ProjectsList.FindAll(j => j.Difficulty == difficulty);

            var randomProject = projects[0];
            randomProject.Occurance += 1;

            return randomProject;
        }
    }
}

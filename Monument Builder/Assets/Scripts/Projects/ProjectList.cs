using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Projects
{
    public class ProjectList
    {
        private static readonly List<Project> ProjectsList = new List<Project>
        {
            //Easy Projects
            new Project(Project.ProjectDifficulty.EASY, "Simple House", new BuildingShape(0)),

            //Normal Projects
            new Project(Project.ProjectDifficulty.NORMAL, "Long House",  new BuildingShape(1)),
            new Project(Project.ProjectDifficulty.NORMAL, "Corner Building", new BuildingShape(2)),

            //Hard
            new Project(Project.ProjectDifficulty.HARD, "Market",  new BuildingShape(3)),
            new Project(Project.ProjectDifficulty.HARD, "Big Building",  new BuildingShape(4))
        };

        public Project GetRandomProject(Project.ProjectDifficulty difficulty)
        {
            var projects = ProjectsList.FindAll(j => j.Difficulty == difficulty);

            var randomProject = projects[Random.Range(0, projects.Count)];
            randomProject.Occurance += 1;

            return randomProject;
        }
    }
}

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
            new Project(Project.ProjectDifficulty.EASY, "Le Maison", 10000, new BuildingShape(0)),

            //Normal Projects
            new Project(Project.ProjectDifficulty.NORMAL, "Le Villa", 100000, new BuildingShape(1)),
            new Project(Project.ProjectDifficulty.NORMAL, "Grand Coin", 100000, new BuildingShape(2)),

            //Hard
            new Project(Project.ProjectDifficulty.HARD, "Market", 1000000, new BuildingShape(3)),
            new Project(Project.ProjectDifficulty.HARD, "Square Building", 1000000, new BuildingShape(4))
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

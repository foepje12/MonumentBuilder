using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Projects;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class BuildingShape
    {

        /// <summary>
        /// The shapelist of buildings
        ///
        /// 0: C   |   1: CX      EASY
        ///        |
        /// 1: CX  |   2: CX      MEDIUM
        ///        |      X
        ///        |
        /// 3: XCX |    4: CX     HARD
        ///     X  |       XX
        /// 
        /// </summary>
        public string[] ShapeList =
        {
@"
C",

@"
CX",

@"
CX
X",

@"
XCX
 X",

@"
CX
XX"
        };

        public string Shape;

        public BuildingShape(int shape)
        {
            Shape = ShapeList[shape];
        }

        public BuildingShape(Project.ProjectDifficulty difficulty)
        {
            //TODO Get a random shape based on difficulty (and space available?)
        }

        public List<Vector2> GetPositions(int rotation)
        {
            var vectors = new List<Vector2>();
            var lines = Shape.Split('\n');
            var center = GetCenter(lines);
            
            vectors.Add(center);

            var z = -1;
            foreach (string line in lines)
            {
                var x = 0;
                foreach (char c in line)
                {
                    if (c == 'X')
                    {
                        var vec2 = CalculateRotation(new Vector2(x, z), center, 90);
                        vectors.Add(vec2);
                    }
                    x++;
                }
                z++;
            }
            return vectors;
        }

        private Vector2 CalculateRotation(Vector2 oldPos, Vector2 center, float degrees)
        {
            float angle = 0;
            oldPos -= center;

            float rad = degrees * Mathf.Deg2Rad;
            angle += rad;

            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);

            return center + new Vector2(oldPos.x * cos - oldPos.y * sin, oldPos.y * cos + oldPos.x * sin);
        }

        private Vector2 GetCenter(string[] lines)
        {

            var z = -1;
            foreach (string line in lines)
            {
                var x = 0;
                foreach (char c in line)
                {
                    if (c == 'C')
                        return new Vector2(x, z);
                    x++;
                }
                z++;
            }
            return Vector2.negativeInfinity;
        }
    }
}

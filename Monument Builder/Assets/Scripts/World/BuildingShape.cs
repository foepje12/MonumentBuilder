using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Projects;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.World
{
    public class BuildingShape
    {

        /// <summary>
        /// The shapelist of buildings
        ///
        /// 0: C   |              EASY
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
        public string ShapeName;


        public BuildingShape(int shape)
        {
            Shape = ShapeList[shape];
            ShapeName = GetNameFromShape(shape);
        }

        public BuildingShape(Project.ProjectDifficulty difficulty)
        {
            //TODO Get a random shape based on difficulty (and space available?)
        }

        public bool IsBuild(Vector2 pos)
        {
            var lines = Shape.Split('\n');
            
            if (pos.y + 1 > lines.Length || pos.x >= lines[(int)pos.y].Length)
                return false;
            
            char c = lines[(int)pos.y][(int)pos.x];
            return c != ' ' && char.IsControl(c) == false;
        }

        public List<Vector2> GetPositions(int rotation)
        {
            var vectors = new List<Vector2>();
            var lines = Shape.Split('\n');
            var center = GetCenter(lines);

            //The center
            vectors.Add(new Vector2(0, 0));

            var z = -1;
            foreach (string line in lines)
            {
                var x = 0;
                foreach (char c in line)
                {
                    if (c == 'X')
                    {
                        var vec2 = CalculateRotation(new Vector2(x, z), center, rotation);
                        vectors.Add(vec2 - center);
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

        private static string GetNameFromShape(int shape)
        {
            switch (shape)
            {
                case 0:
                    return "Single";
                case 1:
                    return "Line";
                case 2:
                    return "Corner";
                case 3:
                    return "T";
                case 4:
                    return "Square";
                default:
                    return "";
            }
        }
    }
}

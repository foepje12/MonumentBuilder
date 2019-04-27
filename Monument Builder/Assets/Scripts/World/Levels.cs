using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.World
{
    public class Levels
    {
        //X == Buildable tile
        //- == empty space
        //m == monument space

        public static string GridFull =
            @"
XXXXXX
XXXXXX
XXXXXX
XXXXXX
XXXXXX
XXXXXX";

        public static string GridNetherlands = 
            @"
X--XXX
XX-XX-
XmmmmX
-mmmmX
XX--XX
XXXXX-
        ";


        public static string GridGermany =
            @"
            
        ";


        public static string GridFrance =
            @"
            
        ";
    }
}

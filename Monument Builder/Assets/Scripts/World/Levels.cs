﻿using System;
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

        public static string GridEmpty =
            @"
-----X
------
--mm--
--mm--
------
------";

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
XX-XX-
        ";


        public static string GridGermany =
            @"
            
        ";


      /*   public static string GridFrance =
            @"
------
------
--mm--
--mm--
------
--XX--
              ";*/

             public static string GridFrance =
                   @"
--XXXX
XXXX-X
--mm--
-Xmm--
XX--XX
XXX-XX
                    ";//*/
    }
}

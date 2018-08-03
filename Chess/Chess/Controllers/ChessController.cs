using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Controllers
{
    class ChessController
    {
        private static string filePath;

        public static void Run()
        {

        }

        public static void SetMoveFilePath(string loadPath)
        {
            filePath = loadPath;
        }
    }
}

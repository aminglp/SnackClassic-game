using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackClassic
{
    public class Settings
    {
        public static int Width {  get; set; }
        public static int Height { get; set; }

        public static string Direction;

        public Settings()
        {
            Width = 20;
            Height = 20;
            Direction = "left";

        }

    }
}

using System;
using System.Drawing;
using static PathfindingTest.Constants;

namespace PathfindingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int WORLD_IDX = 1; // Tundra Infame

            var world = new World(WORLD_IDX); 

            using var bitmap = new Bitmap(256, 256);
            for (var x = 0; x < 255; x++)
            {
                for (var y = 0; y < 255; y++)
                {
                    var cellAttr = world.GetCellAttrEx(x, y);
                    var color = cellAttr switch
                    {
                        M_MOVABLE => Color.White,
                        M_UNMOVABLE => Color.DarkGray,
                        T_MOVABLE => Color.LightGreen,
                        T_UNMOVABLE => Color.DarkGray,
                        _ => Color.Pink
                    };

                    bitmap.SetPixel(x, y, color);
                }
            }

            var outputFile = $"data/output/world-{WORLD_IDX}.bmp";

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save(outputFile);
            
            Console.WriteLine($"File saved on {outputFile}");
            Console.WriteLine("Press any key to close the application");
            Console.ReadLine();
        }
    }
}

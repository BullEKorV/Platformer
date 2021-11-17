global using System.Numerics;
global using Raylib_cs;
class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(1000, 800, "Platformer");
        Raylib.SetTargetFPS(120);

        new Animation(@"player\idle");

        new Animation(@"player\run");


        Console.WriteLine(Animation.allAnimations[@"player\idle"].frames.Count);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.EndDrawing();
        }
    }
}
global using System.Numerics;
global using Raylib_cs;
class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(1000, 800, "Platformer");
        Raylib.SetTargetFPS(120);

        Animation.LoadAllAnimations();

        new Player();

        // Console.WriteLine(Animation.allAnimations[@"player\idle"].frames.Count);

        while (!Raylib.WindowShouldClose())
        {
            foreach (GameObject obj in GameObject.gameObjects)
            {
                obj.Update();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            foreach (GameObject obj in GameObject.gameObjects)
            {
                obj.Draw();
            }
            Raylib.EndDrawing();
        }
    }
}
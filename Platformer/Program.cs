global using System.Numerics;
global using Raylib_cs;
class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(1000, 800, "Platformer");
        Raylib.SetTargetFPS(120);

        Animation.LoadAnimationsFromDirectories();
        Tile.LoadTilesFromDirectory();

        new Player();
        // new Opussom();
        new Tile(new Vector2(0, 80));
        new Tile(new Vector2(80, 0));
        new Tile(new Vector2(160, 0));
        new Tile(new Vector2(240, 0));
        new Tile(new Vector2(240, 80));


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
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
        LevelManager.LoadLevel();

        new Player();

        while (!Raylib.WindowShouldClose())
        {
            foreach (GameObject obj in GameObject.gameObjects)
            {
                obj.Update();
            }

            // Remove dead enemies
            GameObject.gameObjects.RemoveAll(x =>
            {
                return (x is Entity) ? !((Entity)x).isAlive : false;
            });

            Camera.Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            foreach (GameObject obj in GameObject.gameObjects)
            {
                if (!(obj is Player)) obj.Draw(); // Draw all except player
            }

            GameObject.gameObjects.Find(x => x is Player).Draw(); // Draw player
            // if (obj is Player)
            // {
            //     Raylib.DrawText(((Player)obj).hp.ToString(), 100, 0, 20, Color.BLUE);
            //     if (((Player)obj).invisibilityTimer == 0)
            //         Raylib.DrawRectangle(100, 100, 100, 100, Color.BLACK);
            // }
            Raylib.EndDrawing();
        }
    }
}
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
        new Opussom();
        new Tile(new Vector2(0, 80));
        new Tile(new Vector2(80, 0));
        new Tile(new Vector2(160, 0));
        new Tile(new Vector2(240, 0));
        new Tile(new Vector2(240, 80));
        // new Tile(new Vector2(160, 320));



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
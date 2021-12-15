global using System.Numerics;
global using Raylib_cs;
class Program
{
    public static bool windowShouldClose = false;
    static void Main(string[] args)
    {
        Raylib.InitWindow(1200, 800, "Platformer");
        Raylib.SetTargetFPS(120);
        Raylib.SetExitKey(0);

        Animation.LoadAnimationsFromDirectories();
        Tile.LoadTilesFromDirectory();
        LevelManager.LoadLevel(1);
        UI.LoadUIFromJSON();
        UI.currentScreen = UI.allScreens.Find(x => x.name == "Main Menu");

        new Player();

        while (!Raylib.WindowShouldClose() && !windowShouldClose)
        {
            Camera.Update();

            foreach (GameObject obj in GameObject.gameObjects)
            {
                obj.Update();
            }

            // UI button presses etc.
            UI.currentScreen.Update();

            // Remove dead enemies
            GameObject.gameObjects.RemoveAll(x =>
            {
                return (x is Entity) ? !((Entity)x).isAlive : false;
            });

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE) && UI.currentScreen.name == "") UI.currentScreen = UI.allScreens.Find(x => x.name == "Main Menu");

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            foreach (GameObject obj in GameObject.gameObjects)
            {
                if (!(obj is Player)) obj.Draw(); // Draw all except player
            }

            GameObject.gameObjects.Find(x => x is Player).Draw(); // Draw player

            UI.currentScreen.Draw();

            Raylib.EndDrawing();
        }
    }
}
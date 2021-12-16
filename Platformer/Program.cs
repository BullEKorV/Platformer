global using System.Numerics;
global using System.Text.Json;
global using Raylib_cs;
class Program
{
    public static bool windowShouldClose = false;
    public static float timeScale;
    static void Main(string[] args)
    {
        Raylib.InitWindow(1920, 1080, "Platformer");
        Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(120);
        Raylib.SetExitKey(0); // Disable escape to close

        // Load misc stuff
        Animation.LoadAnimationsFromDirectories();
        Tile.LoadTexturesFromDirectory();
        UI.LoadScreensFromJSON();

        new Player();

        while (!Raylib.WindowShouldClose() && !windowShouldClose)
        {
            if (timeScale > 0) Camera.Update();

            foreach (GameObject obj in GameObject.gameObjects)
                obj.Update();

            // UI button presses etc.
            UI.currentScreen.Update();

            // Remove dead enemies
            GameObject.gameObjects.RemoveAll(x =>
            {
                return (x is Entity) ? !((Entity)x).IsAlive() : false;
            });

            // open and close pause menu with escapekey
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE) && UI.currentScreen.name == "") UI.currentScreen = UI.allScreens.Find(x => x.name == "Pause");
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE) && UI.currentScreen.name == "Pause") UI.currentScreen = UI.allScreens.Find(x => x.name == "");

            // Freeze time if not in play mode 
            if (UI.currentScreen.name == "")
                timeScale = 1;
            else
                timeScale = 0;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            foreach (GameObject obj in GameObject.gameObjects)
                if (!(obj is Player)) obj.Draw(); // Draw all except player

            if (UI.currentScreen.name == "" || UI.currentScreen.name == "Pause") GameObject.gameObjects.Find(x => x is Player).Draw(); // Draw player

            UI.currentScreen.Draw();

            Raylib.EndDrawing();
        }
    }
}
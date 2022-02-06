global using System.Numerics;
global using System.Text.Json;
global using Raylib_cs;
class Program
{
    public static bool windowShouldClose = false;
    public static float timeScale = 1;
    static void Main(string[] args)
    {
        Raylib.InitWindow(1920, 1080, "Platformer");
        // Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(120);
        Raylib.SetExitKey(0); // Disable escape to close

        // Load misc stuff
        Animation.LoadAnimationsFromDirectories();
        Tile.LoadTexturesFromDirectory();
        Background.Setup();

        new UI();
        new Player();

        while (!Raylib.WindowShouldClose() && !windowShouldClose)
        {
            Background.Update();

            foreach (GameObject obj in GameObject.gameObjects)
                obj.Update();

            // UI button presses etc.
            UI.currentScreen.Update();

            if (Createmode.isActive) Createmode.Update();

            // Remove dead enemies
            GameObject.gameObjects.RemoveAll(x =>
            {
                return (x is Entity) ? !((Entity)x).IsAlive() : false;
            });

            // Freeze time if not in play mode
            if (UI.currentScreen.name == "")
                timeScale = 1;
            else
                timeScale = 0;

            if (timeScale > 0 && !Createmode.isActive) Camera.Update(); // Update camera pos

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            if ((UI.currentScreen.name == "" || UI.currentScreen.name == "Pause") && !Createmode.isActive) Background.Draw();
            Background.Draw();

            Raylib.DrawText(Raylib.GetFPS().ToString(), 5, 5, 20, Color.WHITE);

            foreach (GameObject obj in GameObject.gameObjects)
                if (obj is not Player && (obj.id != "marker" || Createmode.isActive)) obj.Draw(); // Draw all except player and markers

            if ((UI.currentScreen.name == "" || UI.currentScreen.name == "Pause") && !Createmode.isActive) GameObject.gameObjects.Find(x => x is Player).Draw(); // Draw player

            UI.Draw(); // Draw UI elements (player hearts etc)
            UI.currentScreen.Draw(); // Draw current screen

            Raylib.EndDrawing();
        }
    }
}
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
        UI.LoadUIFromJSON();

        new Player();
        // new UI("help", new List<Button>() { new Button("Hej", Button.WriteHello, new Rectangle(100, 10, 100, 50), Color.BLUE) });

        while (!Raylib.WindowShouldClose())
        {
            Camera.Update();

            foreach (GameObject obj in GameObject.gameObjects)
            {
                obj.Update();
            }

            foreach (UI screen in UI.allScreens)
            {
                screen.Update();
            }

            // Remove dead enemies
            GameObject.gameObjects.RemoveAll(x =>
            {
                return (x is Entity) ? !((Entity)x).isAlive : false;
            });

            // UIScreen.buttons.Add(new Button(Button.WriteHello, new Rectangle(10, 10, 100, 100), Color.BLUE));

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            foreach (GameObject obj in GameObject.gameObjects)
            {
                if (!(obj is Player)) obj.Draw(); // Draw all except player
            }

            GameObject.gameObjects.Find(x => x is Player).Draw(); // Draw player

            foreach (UI screen in UI.allScreens)
            {
                if (screen.isActive) screen.Draw();
            }

            Raylib.EndDrawing();
        }
    }
}
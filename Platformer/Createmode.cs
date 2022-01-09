public class Createmode
{
    public static bool isActive = false;
    public static bool allowPlacing;
    public static Vector2 pos;
    public static Tile temp;
    public static string tile;
    public static void Update()
    {
        if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON) || Raylib.IsMouseButtonReleased(MouseButton.MOUSE_RIGHT_BUTTON) || Raylib.IsKeyReleased(KeyboardKey.KEY_E) || Raylib.IsKeyReleased(KeyboardKey.KEY_ESCAPE)) allowPlacing = true; // Make sure you dont automatically place blocks

        Console.WriteLine(GameObject.gameObjects.Count);
        // Update pos
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            pos.X -= 2 * Program.timeScale;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            pos.X += 2 * Program.timeScale;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
            pos.Y -= 2 * Program.timeScale;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
            pos.Y += 2 * Program.timeScale;
        Camera.MoveToCords(pos);

        // Open and close inventory
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_E) && UI.currentScreen.name == "") UI.currentScreen = UI.allScreens.Find(x => x.name == "Object Select");
        else if ((Raylib.IsKeyPressed(KeyboardKey.KEY_E) || Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE)) && UI.currentScreen.name == "Object Select") UI.currentScreen = UI.allScreens.Find(x => x.name == "");

        // Only allow placing in resume mode
        if (UI.currentScreen.name != "") allowPlacing = false;

        Vector2 rectPos = ConvertToGrid(Raylib.GetMousePosition(), pos);
        temp.rect = new Rectangle(rectPos.X, -rectPos.Y, 16 * GameObject.scale, 16 * GameObject.scale);
        // Console.WriteLine(temp.rect.x);
        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON) && allowPlacing)
            PlaceTile();
        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON) && allowPlacing)
            DeleteTileAtMarker();

    }
    private static void PlaceTile()
    {
        DeleteTileAtMarker();
        new Tile(new Vector2(temp.rect.x / (16 * GameObject.scale), temp.rect.y / (16 * GameObject.scale)), tile);
    }
    private static void DeleteTileAtMarker()
    {
        List<GameObject> sameXPos = GameObject.gameObjects.FindAll(x => x.rect.x == temp.rect.x);
        sameXPos.Remove(temp);
        GameObject.gameObjects.Remove(sameXPos.Find(y => y.rect.y == temp.rect.y));
    }
    private static Vector2 ConvertToGrid(Vector2 mousePos, Vector2 cameraPos)
    {
        Vector2 rectPos = new Vector2(mousePos.X - Raylib.GetScreenWidth() / 2 + pos.X, mousePos.Y - Raylib.GetScreenHeight() / 2 - pos.Y);

        rectPos.X = (int)Math.Floor((rectPos.X) / (16 * GameObject.scale));
        rectPos.X *= (16 * GameObject.scale);

        rectPos.Y = (int)Math.Floor((rectPos.Y) / (16 * GameObject.scale));
        rectPos.Y *= (16 * GameObject.scale);

        return rectPos;
    }
    public static void StartCreatemode()
    {
        pos = new Vector2();
        isActive = true;
        // Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
        temp = new Tile(new Vector2(), "selector");
        allowPlacing = false;
        tile = "grass";
    }
    public static void EndCreatemode()
    {
        isActive = false;
        temp = null;
        LevelManager.ClearLevel();
        // Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
    }
}

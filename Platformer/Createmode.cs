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

        int shifting = 1;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT)) shifting = 3;
        // Update pos
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            pos.X -= 3 * Program.timeScale * shifting;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            pos.X += 3 * Program.timeScale * shifting;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
            pos.Y -= 3 * Program.timeScale * shifting;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
            pos.Y += 3 * Program.timeScale * shifting;
        Camera.MoveToCords(pos);

        // Only allow placing in resume mode
        if (UI.currentScreen.name != "") allowPlacing = false;

        Vector2 rectPos = ConvertToGrid(Raylib.GetMousePosition(), pos);
        temp.rect = new Rectangle(rectPos.X, -rectPos.Y, 16 * GameObject.scale, 16 * GameObject.scale);
        // Console.WriteLine(temp.rect.x);
        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON) && allowPlacing)
            PlaceTile();
        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON) && allowPlacing)
            DeleteTileAtMarker();
    }
    private static void PlaceTile()
    {
        if (tile == "player")
            DeleteAlreadyExistingPlayer();
        DeleteTileAtMarker();

        List<GameObject> sameXPos = GameObject.gameObjects.FindAll(x => x.rect.x == temp.rect.x);
        sameXPos.Remove(temp);
        GameObject existingTile = sameXPos.Find(y => y.rect.y == temp.rect.y);
        if ((existingTile != null && existingTile.id != "player") || existingTile == null)
            new Tile(new Vector2(temp.rect.x / (16 * GameObject.scale), temp.rect.y / (16 * GameObject.scale)), tile);

        GameObject.gameObjects.Remove(temp); // Keep marker above
        GameObject.gameObjects.Add(temp);
    }
    private static void DeleteAlreadyExistingPlayer()
    {
        List<GameObject> players = GameObject.gameObjects.FindAll(x => x.id == "player");
        players.Remove((GameObject.gameObjects.Find(x => x is Player)));

        if (players.Count > 0) GameObject.gameObjects.Remove(players[0]);
    }
    private static void DeleteTileAtMarker()
    {
        List<GameObject> sameXPos = GameObject.gameObjects.FindAll(x => x.rect.x == temp.rect.x);
        sameXPos.Remove(temp);
        GameObject existingTile = sameXPos.Find(y => y.rect.y == temp.rect.y);
        if (existingTile != null && existingTile.id != "player") GameObject.gameObjects.Remove(existingTile);
    }
    private static Vector2 ConvertToGrid(Vector2 mousePos, Vector2 cameraPos)
    {
        Vector2 rectPos = new Vector2(mousePos.X - Raylib.GetScreenWidth() / 2 + pos.X, mousePos.Y - Raylib.GetScreenHeight() / 2 - pos.Y + GameObject.scale * 16);

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
        if (GameObject.gameObjects.FindAll(x => x.id == "player").Count == 1) new Tile(new Vector2(0, 0), "player"); // create player automatically
    }
    public static void EndCreatemode()
    {
        isActive = false;
        temp = null;
        LevelManager.ClearLevel();
        // Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
    }
}

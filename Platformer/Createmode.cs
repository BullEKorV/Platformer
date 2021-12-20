public class Createmode
{
    public static bool isActive = false;
    public static Vector2 pos;
    public static GameObject temp;
    public static string tile;
    public static void Update()
    {
        // Update pos
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            pos.X--;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            pos.X++;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
            pos.Y--;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
            pos.Y++;
        Camera.MoveToCords(pos);

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_E) && UI.currentScreen.name == "") UI.currentScreen = UI.allScreens.Find(x => x.name == "Object Select");
        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_E) && UI.currentScreen.name == "Object Select") UI.currentScreen = UI.allScreens.Find(x => x.name == "");


        Vector2 rectPos = ConvertToGrid(Raylib.GetMousePosition(), pos);
        temp.rect = new Rectangle(rectPos.X - Raylib.GetScreenWidth() / 2 + pos.X, -rectPos.Y + Raylib.GetScreenHeight() / 2 + pos.Y, 16 * GameObject.scale, 16 * GameObject.scale);
        temp.Draw();
        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) new Tile(new Vector2(temp.rect.x / (16 * GameObject.scale), temp.rect.y / (16 * GameObject.scale)), "grass");
    }
    private static Vector2 ConvertToGrid(Vector2 mousePos, Vector2 cameraPos)
    {
        Vector2 rectPos = mousePos;

        Vector2 gridOffset = new Vector2(cameraPos.X % (16 * GameObject.scale), cameraPos.Y % (16 * GameObject.scale));

        rectPos.X = (int)((rectPos.X + gridOffset.X) / (16 * GameObject.scale));
        rectPos.X *= (16 * GameObject.scale);
        rectPos.X -= gridOffset.X;

        rectPos.Y = (int)((rectPos.Y - gridOffset.Y) / (16 * GameObject.scale));
        rectPos.Y *= (16 * GameObject.scale);
        rectPos.Y += gridOffset.Y;

        return rectPos;
    }
    public static void StartCreatemode()
    {
        pos = new Vector2();
        isActive = true;
        Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
        temp = new GameObject();
    }
    public static void EndCreatemode()
    {
        isActive = true;
        Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
    }
}

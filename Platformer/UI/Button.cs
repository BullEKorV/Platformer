public class Button // IS CALCULATED FROM TOP LEFT
{
    public string name;
    public Action action;
    public Rectangle rect;
    public Color color = Color.BLUE;
    private bool isMouseOver = false;
    public Texture2D texture;
    public bool drawText = true;
    public Button(string name, Action action, Rectangle rect, Color color, Texture2D texture)
    {
        this.name = name;
        this.action = action;
        this.rect = rect;
        this.color = color;
        this.texture = texture;
    }
    public void Draw()
    {
        // Draw texture and highlight around button
        DrawTexture();
        if (isMouseOver) DrawHighlight(Color.WHITE);
        else DrawHighlight(Color.BLACK);

        // Draw button text
        // Console.WriteLine(action.GetType());
        if (drawText) Raylib.DrawText(name, (int)rect.x + (int)rect.width / 2 - Raylib.MeasureText(name, (int)rect.height / 2) / 2, (int)rect.y + Raylib.MeasureText("◯", (int)rect.height / 2), (int)rect.height / 2, Color.WHITE);
    }
    public void Update()
    {
        if (Raylib.GetMouseX() > rect.x && Raylib.GetMouseX() < rect.x + rect.width && Raylib.GetMouseY() > rect.y && Raylib.GetMouseY() < rect.y + rect.height)
            isMouseOver = true;
        else
            isMouseOver = false;
        if (isMouseOver && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            action.Invoke();
    }
    void DrawTexture() // Later draw texture depending on type of button
    {
        Raylib.DrawRectangleRec(rect, color);

        Vector2 size = new Vector2(texture.width, texture.height);
        Rectangle sourceRec = new Rectangle(0, 0, size.X, size.Y);
        Rectangle destRec = new Rectangle(rect.x, rect.y, rect.width, rect.height);
        Raylib.DrawTexturePro(texture, sourceRec, destRec, new Vector2(0, 0), 0, Color.WHITE);
    }
    void DrawHighlight(Color color)
    {
        int lineThickness = (int)rect.height / 22; // Match linethickness with button size
        Rectangle lineRect = new Rectangle(rect.x - lineThickness, rect.y - lineThickness, rect.width + lineThickness * 2, rect.height + lineThickness * 2); // Get rectangle outside main so it draws around
        Raylib.DrawRectangleLinesEx(lineRect, lineThickness, color);
    }
    public static void NewGame()
    {
        UI.currentScreen = UI.allScreens.Find(x => x.name == "");
    }
    public static void Resume()
    {
        UI.currentScreen = UI.allScreens.Find(x => x.name == "");

    }
    public static void LevelSelect()
    {
        UI.currentScreen = UI.allScreens.Find(x => x.name == "Level Select");
        Screen.ReloadLevelsToButtons();
    }
    public static void CreateLevel()
    {
        UI.currentScreen = UI.allScreens.Find(x => x.name == "");
        Createmode.StartCreatemode();
    }
    public static void Settings()
    {
        UI.currentScreen = UI.allScreens.Find(x => x.name == "Settings");
    }
    public static void MainMenu()
    {
        LevelManager.ClearLevel();
        UI.currentScreen = UI.allScreens.Find(x => x.name == "Main Menu");

        Createmode.EndCreatemode();
    }
    public static void LoadLevel(int level)
    {
        LevelManager.LoadLevel(level);
        UI.currentScreen = UI.allScreens.Find(x => x.name == "");
    }
    public static void SelectTile(string tile)
    {
        Createmode.tile = tile;
        UI.currentScreen = UI.allScreens.Find(x => x.name == "");
    }
    public static void EndApp()
    {
        Program.windowShouldClose = true;
    }
}
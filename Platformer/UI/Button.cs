public class Button // IS CALCULATED FROM TOP LEFT
{
    public string name;
    public Action action;
    public Rectangle rect;
    public Color color = Color.BLUE;
    private bool isMouseOver = false;
    public Button(string name, Action action, Rectangle rect, Color color)
    {
        this.name = name;
        this.action = action;
        this.rect = rect;
        this.color = color;
    }
    public void Draw()
    {
        Raylib.DrawRectangleRec(rect, color);
        if (isMouseOver) Raylib.DrawRectangleRec(rect, new Color(color.r, color.g, color.b, color.a));

        int margin = 10;
        Rectangle tempRect = new Rectangle(rect.x + margin, rect.y + margin, rect.width - margin * 2, rect.height - margin * 2);
        Raylib.DrawTextRec(Raylib.GetFontDefault(), name, tempRect, 30, 1, true, Color.WHITE);
    }
    public void Update()
    {
        if (Raylib.GetMouseX() > rect.x && Raylib.GetMouseX() < rect.x + rect.width && Raylib.GetMouseY() > rect.y && Raylib.GetMouseY() < rect.y + rect.height)
        {
            isMouseOver = true;
        }
        else
        {
            isMouseOver = false;
        }
        if (isMouseOver && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
        {
            // Console.WriteLine(action.Method.);
            action.Invoke();
        }
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
    }
    public static void CreateLevel()
    {
        UI.currentScreen = UI.allScreens.Find(x => x.name == "");
    }
    public static void Settings()
    {
        UI.currentScreen = UI.allScreens.Find(x => x.name == "Settings");
    }
    public static void MainMenu()
    {
        LevelManager.ClearLevel();
        UI.currentScreen = UI.allScreens.Find(x => x.name == "Main Menu");
    }
    public static void LoadLevel(int level)
    {
        LevelManager.LoadLevel(level);
        UI.currentScreen = UI.allScreens.Find(x => x.name == "");
    }
    public static void EndApp()
    {
        Program.windowShouldClose = true;
    }
}
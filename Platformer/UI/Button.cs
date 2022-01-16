public class Button
{
    public string name { get; set; }
    private Action Action;
    public string action { get; set; }
    public string par { get; set; }
    public Rectangle rect;
    private bool isMouseOver = false;
    public Texture2D texture;
    public bool drawText = true;
    public Button(string name, string action, string par)
    {
        this.name = name;
        this.action = action;
        this.Action = StringToAction(action, par);
        // this.texture = texture;
    }
    public void Draw()
    {
        // Draw texture and highlight around button
        DrawTexture();
        // if (isMouseOver) DrawHighlight(Color.WHITE);
        // else DrawHighlight(Color.BLACK);

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
        {
            Action.Invoke();
        }
    }
    void DrawTexture() // Later draw texture depending on type of button
    {
        Raylib.DrawRectangleRec(rect, Color.BLUE);

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
    private Action StringToAction(string stringAction, string par)
    {
        switch (stringAction)
        {
            case "EndApp":
                return Button.EndApp;
            case "OpenScreen":
                return () => Button.OpenScreen(par);
            case "LastScreen":
                return Button.LastScreen;
            case "LoadLevel":
                return () => Button.LoadLevel(par);


            default:
                return Button.Empty;
        }
    }
    public static void NewGame()
    {
        UI.ChangeToScreen("");
    }
    public static void OpenScreen(string screen)
    {
        UI.ChangeToScreen(screen);
    }
    public static void LastScreen()
    {
        UI.ChangeToLastScreen();
    }
    public static void CreateLevel()
    {
        UI.ChangeToScreen("");
        Createmode.StartCreatemode();
    }
    // public static void Back()
    // {
    //     LevelManager.ClearLevel();
    // UI.GoBack();

    //     Createmode.EndCreatemode();
    // }
    public static void LoadLevel(string target)
    {
        LevelManager.LoadLevel(int.Parse(target));
        UI.ChangeToScreen("");
    }
    public static void SelectTile(string tile)
    {
        Createmode.tile = tile;
        UI.ChangeToScreen("");
    }
    public static void EndApp()
    {
        Program.windowShouldClose = true;
    }
    public static void Empty()
    {
    }
}
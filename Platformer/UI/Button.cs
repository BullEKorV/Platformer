public class Button
{
    public string name { get; set; }
    private Action Action;
    public string action { get; set; }
    public string par { get; set; }
    public Rectangle rect;
    private bool isMouseOver = false;
    public Texture2D texture;
    public Button(string name, string action, string par)
    {
        this.name = name;
        this.action = action;
        this.Action = StringToAction(action, par);
        this.texture = Tile.textures["grass"];
    }
    public void Draw()
    {
        // Draw texture and highlight around button
        DrawTexture();
        if (isMouseOver) DrawHighlight(Color.WHITE);
        else DrawHighlight(new Color(0, 0, 0, 200));

        if (UI.currentScreen.selectedButton != null && UI.currentScreen.selectedButton.name == name) Raylib.DrawRectangleRec(UI.currentScreen.selectedButton.rect, new Color(255, 255, 255, 100));

        // Draw button text
        // Console.WriteLine(action.GetType());
        Raylib.DrawText(name, (int)rect.x + (int)rect.width / 2 - Raylib.MeasureText(name, (int)rect.height / 2) / 2, (int)rect.y + Raylib.MeasureText("◯", (int)rect.height / 2), (int)rect.height / 2, Color.WHITE);
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
            case "ResetLevel":
                return Button.ResetLevel;
            case "NewLevel":
                return Button.NewLevel;
            case "SaveLevel":
                return Button.SaveLevel;
            case "SelectTile":
                return () => Button.SelectTile(par);
            case "SelectLevel":
                return () => Button.SelectLevel(par);
            case "PlayLevel":
                return Button.PlayLevel;
            case "EditLevel":
                return Button.EditLevel;
            case "DeleteLevel":
                return Button.DeleteLevel;


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
    public static void NewLevel()
    {
        UI.ChangeToScreen("");
        Createmode.StartCreatemode();
    }
    public static void PlayLevel()
    {
        if (UI.currentScreen.selectedButton != null)
        {
            LevelManager.LoadLevel(int.Parse(UI.currentScreen.selectedButton.name));
            UI.currentScreen.selectedButton = null;
            UI.ChangeToScreen("");
        }
    }
    public static void EditLevel()
    {
        if (UI.currentScreen.selectedButton != null)
        {
            LevelManager.LoadLevelToEdit(int.Parse(UI.currentScreen.selectedButton.name));
            UI.currentScreen.selectedButton = null;
            UI.ChangeToScreen("");
            Createmode.StartCreatemode();
        }
    }
    public static void DeleteLevel()
    {
        if (UI.currentScreen.selectedButton != null)
        {
            string root = @"levels\";
            File.Delete(root + UI.currentScreen.selectedButton.name + ".json");
            UI.currentScreen.selectedButton = null;
        }
    }
    public static void SaveLevel()
    {
        LevelManager.SaveLevel();
        UI.ChangeToScreen("");
    }
    public static void ResetLevel()
    {
        LevelManager.ResetLevel();
        UI.ChangeToScreen("");
    }
    public static void SelectTile(string tile)
    {
        Createmode.tile = tile;
        UI.ChangeToScreen("");
    }
    public static void SelectLevel(string level)
    {
        UI.currentScreen.selectedButton = LookForLevelString(UI.currentScreen.layout, level);
    }

    public static Button LookForLevelString(Layout layout, string s)
    {
        Button b = null;

        if (layout.buttons != null)
        {
            b = layout.buttons.Find(x => x.name == s);
        }

        if (b == null)
        {
            if (layout.layouts != null)
                foreach (Layout l in layout.layouts)
                {
                    b = LookForLevelString(l, s);

                    if (b != null && b.name == s) return b;
                }
        }
        return b;
    }

    public static void EndApp()
    {
        Program.windowShouldClose = true;
    }
    public static void Empty()
    {
    }
}
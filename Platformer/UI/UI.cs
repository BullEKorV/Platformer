public class UI // IS CALCULATED FROM TOP LEFT
{
    public static List<UI> allScreens = new List<UI>();
    public static UI currentScreen;
    public string name;
    public List<Button> buttons;
    public UI(string name, List<Button> buttons)
    {
        this.name = name;
        this.buttons = buttons;

        allScreens.Add(this);
    }
    public void Update()
    {
        foreach (Button butt in buttons)
        {
            butt.Update();
        }
    }
    public void Draw()
    {
        if (currentScreen.name != "") Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 150)); // make darker

        Raylib.DrawText(currentScreen.name, Raylib.GetScreenWidth() / 2 - Raylib.MeasureText(currentScreen.name, 90) / 2, 10, 90, Color.BLACK);
        foreach (Button butt in buttons)
        {
            butt.Draw();
        }
        Raylib.DrawLine(0, Raylib.GetScreenHeight() / 2, Raylib.GetScreenWidth(), Raylib.GetScreenHeight() / 2, Color.BLUE);
    }
    public static void LoadUIFromJSON()
    {
        string root = @"UI\screens.json";
        string response = File.ReadAllText(root);

        List<UIJson> UI = JsonSerializer.Deserialize<List<UIJson>>(response);

        foreach (UIJson ui in UI)
        {
            List<Button> tempButtons = new List<Button>();
            int buttonWidth = (int)(Raylib.GetScreenWidth() / 2.5f);
            int buttonHeight = (int)(Raylib.GetScreenHeight() / 10);
            int buttonSpacing = (int)(Raylib.GetScreenHeight() / 25);
            int x = Raylib.GetScreenWidth() / 2 - buttonWidth / 2;
            int y = (Raylib.GetScreenHeight() - (ui.buttons.Count * buttonHeight + (ui.buttons.Count - 1) * buttonSpacing)) / 2;

            bool lastButtonHalf = false;
            foreach (UIButton button in ui.buttons)
            {
                // Reset values
                buttonWidth = (int)(Raylib.GetScreenWidth() / 2.5f);
                int tempX = x;
                int tempY = y;

                if (button.bottomPlacement) tempY = Raylib.GetScreenHeight() - buttonHeight - buttonSpacing * 2;

                // Make button shorter
                if (button.halfButton || lastButtonHalf) buttonWidth = buttonWidth / 2 - buttonSpacing / 2;
                if (lastButtonHalf) tempX += buttonWidth + buttonSpacing;

                tempButtons.Add(new Button(button.name, button.actions, new Rectangle(tempX, tempY, buttonWidth, buttonHeight), button.colors));
                if (!button.halfButton) y += buttonHeight + buttonSpacing;

                lastButtonHalf = button.halfButton;
            }
            if (ui.name == "Level Select") tempButtons.AddRange(LoadLevelsToButtons(x));

            new UI(ui.name, tempButtons);
        }
    }
    public static List<Button> LoadLevelsToButtons(int startX)
    {
        List<Button> LevelButtons = new List<Button>();

        string root = @"levels\";

        string[] levelsDir = Directory.GetFiles(root);

        levelsDir = levelsDir.OrderBy(i => i.ToString().Substring(4).Remove(0, 7)).ToArray(); // Reorder list because c# thinks 10 comes before 2 >:(

        int width = (int)(Raylib.GetScreenWidth() / 10f);
        int spacing = (int)(Raylib.GetScreenHeight() / 25);
        int levelsPerRow = 5;
        int rowAmounts = (int)Math.Ceiling((double)levelsDir.Length / levelsPerRow);
        int x = startX;
        int y = (Raylib.GetScreenHeight() - (rowAmounts * width + ((rowAmounts - 1) * spacing))) / 2;

        // int uwu = (int)Math.Ceiling(12.44);

        for (int i = 0; i < levelsDir.Length; i++)
        {
            string response = File.ReadAllText(levelsDir[i]);

            string levelName = JsonSerializer.Deserialize<Level>(response).level.ToString();

            LevelButtons.Add(new Button(levelName, () => Button.LoadLevel(levelName), new Rectangle(x, y, width, width), Color.BLUE));

            x += width + spacing;
            if ((i + 1) % levelsPerRow == 0)
            {
                Console.WriteLine(i / levelsPerRow);
                y += width + spacing;
                x = startX;
            }
        }

        return LevelButtons;
    }
}
class UIJson
{
    public string name { get; set; }
    public List<UIButton> buttons { get; set; }
    public bool isActive { get; set; }
}
class UIButton
{
    public string name { get; set; }
    Dictionary<string, Action> allActions = new Dictionary<string, Action> { { "NewGame", Button.NewGame }, { "LevelSelect", Button.LevelSelect },
    { "CreateLevel", Button.CreateLevel }, { "Settings", Button.Settings }, { "MainMenu", Button.MainMenu }, { "EndApp", Button.EndApp } };
    public string action
    {
        get
        {
            return allActions.Where(a => a.Value == actions).First().Key;  // Returnerar keyn som matchar objektets action i dictionaryn… 
        }
        set
        {
            actions = allActions[value];
        }
    }
    public Action actions { get; set; }
    Dictionary<string, Color> allColors = new Dictionary<string, Color> { { "blue", Color.BLUE }, { "red", Color.RED } };
    public string color
    {
        get
        {
            return allColors[color].ToString();
        }
        set
        {
            colors = allColors[value];
        }
    }
    public Color colors { get; set; }
    public bool halfButton { get; set; } = false;
    public bool bottomPlacement { get; set; } = false;
}
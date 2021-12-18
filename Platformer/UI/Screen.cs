public class Screen
{
    public string name;
    public List<Button> buttons;
    public Screen(string name, List<Button> buttons)
    {
        this.name = name;
        this.buttons = buttons;
    }
    public void Update()
    {
        foreach (Button butt in buttons) // Update all the button presses etc
            butt.Update();
    }
    public void Draw()
    {
        // Write screen name
        int fontSize = Raylib.GetScreenHeight() / 7;
        Raylib.DrawText(name, Raylib.GetScreenWidth() / 2 - Raylib.MeasureText(name, fontSize) / 2, Raylib.GetScreenHeight() / 18, fontSize, Color.WHITE);

        // Draw buttons
        foreach (Button butt in buttons)
            butt.Draw();
    }
    public static List<Screen> LoadScreensFromJSON()
    {
        // Fetch screens and deserialize them
        string response = File.ReadAllText(@"UI\screens.json");
        List<ScreenJSON> screenJSON = JsonSerializer.Deserialize<List<ScreenJSON>>(response);
        List<Screen> screens = new List<Screen>();

        foreach (ScreenJSON screen in screenJSON)
        {
            List<Button> tempButtons = new List<Button>();

            // A bunch of variables for button placement
            int buttonWidth = (int)(Raylib.GetScreenWidth() / 2.5f);
            int buttonHeight = (int)(Raylib.GetScreenHeight() / 10);
            int buttonSpacing = (int)(Raylib.GetScreenHeight() / 25);
            int x = Raylib.GetScreenWidth() / 2 - buttonWidth / 2;
            int extraButtonsAmount = screen.buttons.FindAll(x => x.halfButton == true).Count + screen.buttons.FindAll(x => x.bottomPlacement == true).Count; // Keep centered even with halfbuttons and bottomplacement, otherwise it got miscalculated
            int y = (Raylib.GetScreenHeight() - ((screen.buttons.Count - extraButtonsAmount) * buttonHeight + (screen.buttons.Count - 1 - extraButtonsAmount) * buttonSpacing)) / 2;

            bool lastButtonHalf = false;
            foreach (UIButton button in screen.buttons)
            {
                // Reset values
                buttonWidth = (int)(Raylib.GetScreenWidth() / 2.5f);
                int tempX = x;
                int tempY = y;

                if (button.bottomPlacement) tempY = Raylib.GetScreenHeight() - buttonHeight - buttonSpacing * 2; // Place button at bottom if bottomplacement is true

                // Make button shorter if halfbutton or last button was halfbutton
                if (button.halfButton || lastButtonHalf) buttonWidth = buttonWidth / 2 - buttonSpacing / 2;
                if (lastButtonHalf) tempX += buttonWidth + buttonSpacing;

                tempButtons.Add(new Button(button.name, button.actions, new Rectangle(tempX, tempY, buttonWidth, buttonHeight), button.colors));
                if (!button.halfButton) y += buttonHeight + buttonSpacing; // Increase Y if this button is not a halfbutton

                lastButtonHalf = button.halfButton;
            }
            // Fetch levels if level select menu
            if (screen.name == "Level Select") tempButtons.AddRange(LoadLevelsToButtons());

            screens.Add(new Screen(screen.name, tempButtons));
        }
        return screens;
    }
    public static List<Button> LoadLevelsToButtons()
    {
        List<Button> levelButtons = new List<Button>();

        string[] levelsDir = Directory.GetFiles(@"levels\").OrderBy(i => i.Substring(4).Remove(0, 7)).ToArray(); // Get all level filenames and reoders because C# thinks 10 comes before 2

        // Bunch of determening variables for button placement
        int width = (int)(Raylib.GetScreenWidth() / 15f);
        int spacing = (int)(Raylib.GetScreenHeight() / 25);
        const int levelsPerRow = 6;
        int rowAmounts = (int)Math.Ceiling((double)levelsDir.Length / levelsPerRow);
        int x = (Raylib.GetScreenWidth() - (levelsPerRow * width + ((levelsPerRow - 1) * spacing))) / 2;
        int y = (Raylib.GetScreenHeight() - (rowAmounts * width + ((rowAmounts - 1) * spacing))) / 2;

        for (int i = 0; i < levelsDir.Length; i++)
        {
            // Find levelname by reading levelJSON and deserializing it
            int levelName = JsonSerializer.Deserialize<Level>(File.ReadAllText(levelsDir[i])).level;

            // Give a nice offcenter to last row if it isn't full
            if (levelsPerRow * rowAmounts % levelsDir.Length != 0 && i == levelsPerRow * (rowAmounts - 1))
                x = (Raylib.GetScreenWidth() - ((levelsDir.Length - i) * width + ((levelsDir.Length - i - 1) * spacing))) / 2;

            levelButtons.Add(new Button(levelName.ToString(), () => Button.LoadLevel(levelName), new Rectangle(x, y, width, width), Color.BLUE));

            x += width + spacing; // Move x for next button
            if ((i + 1) % levelsPerRow == 0) // Checks for if a row has been filled
            {
                y += width + spacing; // Moves down y
                x = (Raylib.GetScreenWidth() - (levelsPerRow * width + ((levelsPerRow - 1) * spacing))) / 2; // Resets x
            }
        }
        return levelButtons;
    }
}
class ScreenJSON
{
    public string name { get; set; }
    public List<UIButton> buttons { get; set; }
    public bool isActive { get; set; }
}
class UIButton
{
    public string name { get; set; }
    Dictionary<string, Action> allActions = new Dictionary<string, Action> { { "NewGame", Button.NewGame }, {"Resume",Button.Resume}, { "LevelSelect", Button.LevelSelect },
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
    Dictionary<string, Color> allColors = new Dictionary<string, Color> { { "blue", Color.BLUE }, { "red", new Color(250, 75, 75, 255) }, { "green", new Color(44, 201, 100, 255) } };
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
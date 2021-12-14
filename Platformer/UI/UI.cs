public class UI // IS CALCULATED FROM TOP LEFT
{
    public static List<UI> allScreens = new List<UI>();
    protected string name;
    public List<Button> buttons;
    public bool isActive;
    public UI(string name, List<Button> buttons, bool isActive)
    {
        this.name = name;
        this.buttons = buttons;
        this.isActive = isActive;

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
        foreach (Button butt in buttons)
        {
            butt.Draw();
        }
    }
    public static void LoadUIFromJSON()
    {
        string root = @"UI\screens.json";
        string response = File.ReadAllText(root);

        List<UIJson> UI = JsonSerializer.Deserialize<List<UIJson>>(response);

        foreach (UIJson ui in UI)
        {
            List<Button> temp = new List<Button>();
            foreach (UIButton button in ui.buttons)
            {
                temp.Add(new Button(button.name, button.actions, new Rectangle(button.rect.x, button.rect.y, button.rect.width, button.rect.height), button.colors));
            }
            new UI(ui.name, temp, ui.isActive);
        }
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
    Dictionary<string, Action> allActions = new Dictionary<string, Action> { { "WriteHello", Button.WriteHello }, { "WriteNo", Button.WriteNo } };
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
    public rectangle rect { get; set; }
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
}
class rectangle
{
    private int X;
    public int x
    {
        get
        {
            return X;
        }
        set
        {
            X = x;
            if (X.ToString().StartsWith("-")) X = Raylib.GetScreenWidth() - X;
            if (X.ToString().StartsWith("/")) X = Raylib.GetScreenWidth() / 2 - width / 2;
        }
    }
    public int y { get; set; }
    public int width { get; set; }
    public int height { get; set; }

}
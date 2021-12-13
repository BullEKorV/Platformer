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


    }
}
class UIJson
{
    protected string name { get; set; }
    public List<UIButton> buttons { get; set; }
    public bool isActive { get; set; }
}
class UIButton
{
    public string name { get; set; }
    public Action action
    {
        get { return action; }
        set
        {
            action = (Action)(value);
        }
    }
    public Rectangle rect
    {
        get { return rect; }
        set
        {
            rect = new Rectangle(value.x, value.y, value.width, value.height);
        }
    }
}
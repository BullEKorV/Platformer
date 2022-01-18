public class UI // IS CALCULATED FROM TOP LEFT
{
    public static List<Screen> allScreens = new List<Screen>();
    public static Stack<Screen> history = new Stack<Screen>();
    public static Screen currentScreen;
    public UI()
    {
        allScreens = Screen.LoadScreensFromJSON();
        currentScreen = allScreens.Find(x => x.name == "Main Menu");
    }
    public static void ChangeToScreen(string screen)
    {
        history.Push(currentScreen);
        currentScreen = UI.allScreens.Find(x => x.name == screen);
        if (screen == "Main Menu")
        {
            Createmode.EndCreatemode();
            history.Clear();
            LevelManager.ClearLevel();
        }
    }
    public static void ChangeToLastScreen()
    {
        if (history.Any()) currentScreen = history.Pop();
    }
    public static void Update()
    {

    }
    public static void Draw()
    {



        // Make background darker
        if (currentScreen.name == "Pause" || currentScreen.name == "Object Select" || currentScreen.name == "Pause Create") Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 175));
        else if (currentScreen.name != "") Raylib.ClearBackground(Color.SKYBLUE);



        Raylib.DrawLine(0, Raylib.GetScreenHeight() / 2, Raylib.GetScreenWidth(), Raylib.GetScreenHeight() / 2, Color.BLUE); // center line
        Raylib.DrawLine(Raylib.GetScreenWidth() / 2, 0, Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight(), Color.BLUE); // center line

    }
}
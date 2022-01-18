public class LevelManager
{
    public static List<Level> allLevels = new List<Level>();
    public static int currentLevel;
    public static void LoadLevel(int level)
    {
        ClearLevel();
        Level lvl = GetLevelJson(level);
        // Console.WriteLine(lvl.level);
        currentLevel = lvl.level;

        // Reset player position to start position
        Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
        player.ResetPos(new Vector2(lvl.startPos.x, lvl.startPos.y));
        Camera.MoveToPlayer(); // Move camera to player immideately

        // Load tiles and enemies to gameobjects
        foreach (JsonGameobject gameobject in lvl.gameobjects)
        {
            if (gameobject.type == "opossum")
                new Opossum(new Vector2(gameobject.x, gameobject.y));
            else if (gameobject.type == "cherry")
                new Cherry(new Vector2(gameobject.x, gameobject.y));
            else if (gameobject.type == "gem")
                new Gem(new Vector2(gameobject.x, gameobject.y));
            else new Tile(new Vector2(gameobject.x, gameobject.y), gameobject.type);

        }
    }
    public static void ResetLevel()
    {
        LoadLevel(currentLevel);
    }
    public static void ClearLevel()
    {
        GameObject.gameObjects.RemoveAll(x => (x is Tile));
        GameObject.gameObjects.RemoveAll(x => (x is Enemy));
        GameObject.gameObjects.RemoveAll(x => (x is Collectible));
    }
    public static void SaveLevel()
    {
        List<GameObject> allGameobjects = GameObject.gameObjects;

        List<JsonGameobject> jsonObjects = new List<JsonGameobject>();
        foreach (GameObject gameObject in allGameobjects)
        {
            if (gameObject.id == "selector" || gameObject.id == "player") continue;
            jsonObjects.Add(new JsonGameobject(gameObject.id, (int)gameObject.rect.x / 16 / GameObject.scale, (int)gameObject.rect.y / 16 / GameObject.scale));
        }
        int totalLevels = Directory.GetFiles(@"levels\").Length;

        List<GameObject> players = allGameobjects.FindAll(x => x.id == "player");
        GameObject player = players.Find(x => x is not Player);
        StartPos startPos = new StartPos(); // Player startpos
        startPos.x = (int)(player.rect.x / 16 / GameObject.scale);
        startPos.y = (int)(player.rect.y / 16 / GameObject.scale); // MAKE IT CHANGE!!!!

        Level newLevel = new Level();
        newLevel.gameobjects = jsonObjects;
        newLevel.level = (totalLevels + 1); // Set level name
        newLevel.startPos = startPos;

        string jsonString = JsonSerializer.Serialize<Level>(newLevel);

        string filePath = @"levels\" + (totalLevels + 1 + ".json");
        File.Create(filePath).Dispose();

        File.WriteAllText(filePath, jsonString);
    }
    static Level GetLevelJson(int lvl)
    {
        string response = File.ReadAllText(@"levels\" + lvl + ".json"); // Level names start at 1, so therefore -1

        Level level = JsonSerializer.Deserialize<Level>(response);

        return level;
    }
}

public class Level
{
    public int level { get; set; }
    public StartPos startPos { get; set; }
    public List<JsonGameobject> gameobjects { get; set; }
}
public class JsonGameobject
{
    public JsonGameobject(string type, int x, int y)
    {
        this.type = type;
        this.x = x;
        this.y = y;
    }
    public string type { get; set; }
    public int x { get; set; }
    public int y { get; set; }
}
public class StartPos
{
    public int x { get; set; }
    public int y { get; set; }
}

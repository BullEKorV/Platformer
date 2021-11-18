
public class Tile : GameObject
{
    public static Dictionary<string, Texture2D> allTiles = new Dictionary<string, Texture2D>();

    public Tile()
    {
        texture = allTiles["grass"];

        // Define tile hitbox
        Vector2 hitboxSize = new Vector2(128, 128);
        rect = new Rectangle(100, 100, hitboxSize.X, hitboxSize.Y);
        // Match texture cord with hitbox
        textureOffset = new Vector2(0, 0);
    }
    public static void LoadTilesFromDirectory()
    {
        string root = @"tiles\";


        string[] tiles = Directory.GetFiles(root);
        foreach (string tile in tiles)
        {
            allTiles.Add(tile.Replace(@"tiles\", "").Replace(@".png", ""), Raylib.LoadTexture(tile));
        }
    }
}


public class Tile : GameObject
{
    public static Dictionary<string, Texture2D> allTiles = new Dictionary<string, Texture2D>();

    public Tile(Vector2 pos)
    {
        texture = allTiles["grass"];

        // Define tile hitbox
        Vector2 hitboxSize = new Vector2(80, 80);
        rect = new Rectangle(pos.X, pos.Y, hitboxSize.X, hitboxSize.Y);
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

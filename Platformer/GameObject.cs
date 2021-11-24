public class GameObject
{
    public static List<GameObject> gameObjects = new List<GameObject>();
    public Rectangle rect;
    protected Texture2D texture;
    protected Vector2 textureOffset;
    protected bool lookingRight = true;
    public GameObject()
    {
        gameObjects.Add(this);
    }
    public virtual void Update()
    {

    }
    public void Draw()
    {
        int worldScale = 5;
        Rectangle newRect = FlipYAxis(rect);

        Rectangle player = gameObjects.Find(x => (x is Player)).rect;

        // Follow player
        Vector2 cameraPosition = new Vector2(-player.x + Raylib.GetScreenWidth() / 2 - player.width / 2, player.y - Raylib.GetScreenWidth() / 4);

        cameraPosition = new Vector2(0, 0); // Temporarily disable player follow

        // BAD ???
        Raylib.DrawRectangleRec(new Rectangle(newRect.x + (int)cameraPosition.X, newRect.y + (int)cameraPosition.Y, newRect.width, newRect.height), Color.GOLD);

        DrawTexture(texture, new Vector2(newRect.x + textureOffset.X + (int)cameraPosition.X, newRect.y - textureOffset.Y + (int)cameraPosition.Y), worldScale, lookingRight);


        // Raylib.DrawTextureEx(texture, new Vector2(newRect.x + textureOffset.X + cameraPosition.X, newRect.y - textureOffset.Y + cameraPosition.Y), 0, worldScale, Color.WHITE);
        // Console.WriteLine(rect.width / texture.width);
    }
    public void DrawTexture(Texture2D texture, Vector2 position, float scale, bool lookingRight)
    {
        Vector2 size = new Vector2(texture.width, texture.height);
        int flipTextue = lookingRight == true ? 1 : -1;

        Rectangle sourceRec = new Rectangle(0.0f, 0.0f, size.X * flipTextue, size.Y);

        Rectangle destRec = new Rectangle(position.X, position.Y, size.X * scale, size.Y * scale);

        Raylib.DrawTexturePro(texture, sourceRec, destRec, new Vector2(0, 0), 0, Color.WHITE);
    }

    public Rectangle FlipYAxis(Rectangle rect)
    {
        Rectangle oldRectangle = rect;

        Rectangle flippedRectangle = new Rectangle(oldRectangle.x, Raylib.GetScreenHeight() + (oldRectangle.y + oldRectangle.height) * -1, oldRectangle.width, oldRectangle.height);
        return flippedRectangle;
    }
}
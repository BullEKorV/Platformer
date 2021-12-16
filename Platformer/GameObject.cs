public class GameObject
{
    public static List<GameObject> gameObjects = new List<GameObject>();
    public static int scale = 6;
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
        Rectangle flippedRect = FlipYAxis(rect); // Flip rectangle on the y axis

        // Raylib.DrawRectangleRec(new Rectangle(flippedRect.x + Camera.viewPos.X, flippedRect.y + Camera.viewPos.Y, flippedRect.width, flippedRect.height), Color.GOLD); // Draw hitboxes

        DrawTexture(texture, new Vector2(flippedRect.x + textureOffset.X + Camera.viewPos.X, flippedRect.y - textureOffset.Y + Camera.viewPos.Y), scale, lookingRight); // Draw textures
    }
    public void DrawTexture(Texture2D texture, Vector2 position, float scale, bool lookingRight)
    {
        // Draw texture with parameters and stuff (idk 100%, it works)
        Vector2 size = new Vector2(texture.width, texture.height);
        int flipTextue = lookingRight == true ? 1 : -1;

        Rectangle sourceRec = new Rectangle(0, 0, size.X * flipTextue, size.Y);

        Rectangle destRec = new Rectangle(position.X, position.Y, size.X * scale, size.Y * scale);

        Raylib.DrawTexturePro(texture, sourceRec, destRec, new Vector2(0, 0), 0, Color.WHITE);
    }

    public Rectangle FlipYAxis(Rectangle rect)
    {
        Rectangle flippedRectangle = new Rectangle(rect.x, Raylib.GetScreenHeight() + (rect.y + rect.height) * -1, rect.width, rect.height);
        return flippedRectangle;
    }
}
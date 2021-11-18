public class GameObject
{
    public static List<GameObject> gameObjects = new List<GameObject>();
    protected Rectangle rect;
    protected Texture2D texture;
    protected Vector2 textureOffset;
    public GameObject()
    {
        gameObjects.Add(this);
    }
    public virtual void Update()
    {

    }
    private int worldScale = 5;
    public void Draw()
    {
        Rectangle newRect = FlipYAxis(rect);

        // BAD ???
        // Raylib.DrawRectangle((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, Color.BROWN);
        Raylib.DrawRectangleRec(new Rectangle(newRect.x, newRect.y, newRect.width, newRect.height), Color.GOLD);
        Raylib.DrawTextureEx(texture, new Vector2(newRect.x + textureOffset.X, newRect.y - textureOffset.Y), 0, worldScale, Color.WHITE);
        // Console.WriteLine(rect.width / texture.width);
    }
    // https://www.mathsisfun.com/algebra/matrix-transform.html
    public Rectangle FlipYAxis(Rectangle rect)
    {
        Rectangle oldRectangle = rect;

        Rectangle flippedRectangle = new Rectangle(oldRectangle.x, Raylib.GetScreenHeight() + (oldRectangle.y + oldRectangle.height) * -1, oldRectangle.width, oldRectangle.height);
        return flippedRectangle;
    }
}
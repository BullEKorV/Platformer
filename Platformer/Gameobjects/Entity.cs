public class Entity : GameObject
{
    protected bool isAlive = true;
    protected Animation animation;
    protected Vector2 velocity;
    protected int speed;
    protected int mass;
    protected bool touchingGround;
    protected int currentFrame;
    public Entity() : base()
    {
    }
    public override void Update()
    {
        base.Update();

        velocity.Y -= 260 * scale * mass * Raylib.GetFrameTime() * Program.timeScale; // Gravity 
        if (Program.timeScale > 0) velocity.X *= 0.86f; // Constrain xvelocity  FIX SCALE

        // Collide with tiles
        foreach (GameObject gameobject in gameObjects)
            if (gameobject is Tile && mass > 0)
                CheckCollisionTile(gameobject.rect);

        // Update velocity
        rect.x += velocity.X * Raylib.GetFrameTime() * Program.timeScale;
        rect.y += velocity.Y * Raylib.GetFrameTime() * Program.timeScale;

        // Texture to frame in animation
        currentFrame = animation.AdvanceFrame(currentFrame);
        texture = animation.GetCurrentFrame(currentFrame);
    }

    protected void CheckCollisionTile(Rectangle tile) // Kinda iffy code, but still works 100%
    {
        if (rect.x + velocity.X * Raylib.GetFrameTime() <= tile.x + tile.width && rect.x > tile.x && rect.y + rect.height > tile.y && rect.y < tile.y + tile.height)
        {
            // Console.WriteLine("left");
            rect.x = tile.x + tile.width;
            velocity.X = 1;
        }
        if (rect.x + rect.width + velocity.X * Raylib.GetFrameTime() >= tile.x && rect.x + rect.width < tile.x + tile.width && rect.y + rect.height > tile.y && rect.y < tile.y + tile.height)
        {
            // Console.WriteLine("right");
            rect.x = tile.x - rect.width;
            velocity.X = -1;
        }
        if (rect.y + velocity.Y * Raylib.GetFrameTime() <= tile.y + tile.height && rect.y > tile.y && rect.x + rect.width > tile.x && rect.x < tile.x + tile.width)
        {
            // Console.WriteLine("down");
            touchingGround = true;
            rect.y = tile.y + tile.height;
            velocity.Y = 0;
        }
        else if (velocity.Y < 0) touchingGround = false; // Disable jump ability if falling
        if (rect.y + rect.height + velocity.Y * Raylib.GetFrameTime() >= tile.y && rect.y + rect.height < tile.y + tile.height && rect.x + rect.width > tile.x && rect.x < tile.x + tile.width)
        {
            // Console.WriteLine("up");
            rect.y = tile.y - rect.height;
            velocity.Y = 0;
            if (this is (Player)) ((Player)this).DisableHighJump(); // Disable high jump if touching ceiling
        }
    }
    public virtual void OnCollision()
    {
    }
    public bool IsAlive()
    {
        return isAlive;
    }
}

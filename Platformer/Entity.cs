public class Entity : GameObject
{
    protected Animation animation;
    protected int hp;
    protected Vector2 velocity;
    protected int speed;
    protected bool touchingGround;
    protected int currentFrame = 0;
    public Entity() : base()
    {

    }
    public override void Update()
    {
        base.Update();

        velocity.Y -= 5f;

        // Collide with tiles
        foreach (GameObject gameobject in gameObjects)
        {
            if (gameobject is Tile)
                CheckCollision(gameobject.rect);
        }

        // Update velocity
        rect.x += velocity.X * Raylib.GetFrameTime();
        rect.y += velocity.Y * Raylib.GetFrameTime();

        // Animation stuff
        currentFrame = animation.AdvanceFrame(currentFrame);
        texture = animation.GetCurrentFrame(currentFrame);
    }
    protected void CheckCollision(Rectangle tile)
    {
        // Console.WriteLine($"entity: {rect.x} {rect.y} tile: {tile.x} {tile.y}");
        if (rect.x <= tile.x + tile.width && rect.x > tile.x && rect.y + rect.height > tile.y && rect.y < tile.y + tile.height)
        {
            // Console.WriteLine("left");
            rect.x = tile.x + tile.width;
            velocity.X = +1;
        }
        if (rect.x + rect.width >= tile.x && rect.x + rect.width < tile.x + tile.width && rect.y + rect.height > tile.y && rect.y < tile.y + tile.height)
        {
            // Console.WriteLine("right");
            rect.x = tile.x - rect.width;
            velocity.X = -1;
        }
        if (rect.y + velocity.Y * Raylib.GetFrameTime() <= tile.y + tile.height && rect.y > tile.y && rect.x + rect.width > tile.x && rect.x < tile.x + tile.width)
        {
            // Console.WriteLine("down");
            touchingGround = true;
            // jumping = false;
            rect.y = tile.y + tile.height;
            velocity.Y = 0;
        }
        if (rect.y + rect.height + velocity.Y * Raylib.GetFrameTime() >= tile.y && rect.y + rect.height < tile.y + tile.height && rect.x + rect.width > tile.x && rect.x < tile.x + tile.width)
        {
            // Console.WriteLine("up");
            // jumping = false;
            rect.y = tile.y - rect.height;
            velocity.Y = 0;
        }
    }
}

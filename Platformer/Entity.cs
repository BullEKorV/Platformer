public class Entity : GameObject
{
    protected Animation animation;
    protected int hp;
    protected Vector2 velocity;
    protected int speed;
    protected bool touchingGround;
    public Entity() : base()
    {
    }
    public override void Update()
    {
        base.Update();

        velocity.Y--;

        foreach (GameObject tile in GameObject.gameObjects)
        {
            if (tile.GetType() == typeof(Tile))
                CheckCollision(tile.rect);
        }

        // Update velocity
        rect.x += velocity.X * Raylib.GetFrameTime();
        rect.y += velocity.Y * Raylib.GetFrameTime();

        animation.Update();
        texture = animation.GetCurrentFrame();
    }
    void CheckCollision(Rectangle tile)
    {
        // Console.WriteLine($"entity: {rect.x} {rect.y} tile: {tile.x} {tile.y}");
        bool isOverlapping = Raylib.CheckCollisionRecs(rect, tile);
        if (isOverlapping)
        {
            if (rect.x <= tile.x + tile.width && rect.x > tile.x && rect.y + rect.height + velocity.Y > tile.y && rect.y + velocity.Y < tile.y + tile.height)
            {
                Console.WriteLine("1");
                rect.x = tile.x + tile.width;
                velocity.X = 0;
            }
            if (rect.x + rect.width >= tile.x && rect.x + rect.width < tile.x + tile.width && rect.y + rect.height + velocity.Y > tile.y && rect.y + velocity.Y < tile.y + tile.height)
            {
                Console.WriteLine("2");

                rect.x = tile.x - rect.width;
                velocity.X = 0;
            }
            if (rect.y + velocity.Y <= tile.y + tile.height && rect.x + rect.width > tile.x && rect.x < tile.x + tile.width)
            {
                Console.WriteLine("3");

                touchingGround = true;
                // jumping = false;
                rect.y = tile.y + tile.height;
                velocity.Y = 0;
            }
            // if (rect.y + velocity.Y >= tile.y + tile.height && rect.x + rect.width > tile.x && rect.x < tile.x + tile.width)
            // {
            //     Console.WriteLine("4");

            //     // jumping = false;
            //     rect.y = tile.y + tile.height;
            //     velocity.Y = 0;
            // }
            animation = Animation.allAnimations["frog-jump"];
        }
    }
}

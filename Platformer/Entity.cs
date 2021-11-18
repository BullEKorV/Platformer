public class Entity : GameObject
{
    protected Animation animation;
    protected int hp;
    protected Vector2 velocity;
    protected int speed;

    public Entity() : base()
    {
    }
    public override void Update()
    {
        base.Update();

        // Update velocity
        rect.x += velocity.X * Raylib.GetFrameTime();
        rect.y += velocity.Y * Raylib.GetFrameTime();

        animation.Update();
        texture = animation.GetCurrentFrame();
    }
}

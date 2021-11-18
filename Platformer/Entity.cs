public class Entity : GameObject
{
    protected Animation animation;
    protected int hp;
    protected Vector2 velocity;
    int speed;
    public override void Update()
    {
        base.Update();
        texture = animation.GetCurrentFrame();
        animation.Update();
    }
}

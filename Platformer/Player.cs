public class Player : Entity
{

    public Player() : base()
    {
        speed = 30;
        hp = 100;

        animation = Animation.allAnimations["player-idle"];

        rect = new Rectangle(200, 200, 330, 320);
    }
    public override void Update()
    {
        base.Update();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) velocity.X -= speed;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) velocity.X += speed;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) velocity.Y += speed;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) velocity.Y -= speed;

        velocity *= 0.8f;

        if (Math.Abs(velocity.X) > 4)
        {
            animation = Animation.allAnimations["player-run"];
        }
        else
        {
            animation = Animation.allAnimations["player-idle"];
        }
    }
}
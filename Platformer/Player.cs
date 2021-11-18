public class Player : Entity
{

    public Player() : base()
    {
        speed = 30;
        hp = 100;

        animation = Animation.allAnimations["player-idle"];

        // Define player hitbox
        Vector2 hitboxSize = new Vector2(90, 105);
        rect = new Rectangle(50, 50, hitboxSize.X, hitboxSize.Y);
        // Match texture cord with hitbox
        textureOffset = new Vector2(-30, 55);
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
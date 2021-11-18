public class Player : Entity
{

    public Player() : base()
    {
        speed = 40;
        hp = 100;

        animation = Animation.allAnimations["player-idle"];

        // Define player hitbox
        Vector2 hitboxSize = new Vector2(90, 105);
        rect = new Rectangle(120, 100, hitboxSize.X, hitboxSize.Y);
        // Match texture cord with hitbox
        textureOffset = new Vector2(-30, 55);
    }
    public override void Update()
    {
        base.Update();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) velocity.X -= speed;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) velocity.X += speed;
        velocity.X *= 0.8f;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) velocity.Y = 125;


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
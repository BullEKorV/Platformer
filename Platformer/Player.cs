public class Player : Entity
{

    public Player() : base()
    {
        speed = 40;
        hp = 100;

        animation = Animation.allAnimations["player-idle"];

        // Define player hitbox
        Vector2 hitboxSize = new Vector2(75, 105);
        rect = new Rectangle(120, 100, hitboxSize.X, hitboxSize.Y);
        // Match texture cord with hitbox
        textureOffset = new Vector2(-45, 55);
    }
    public override void Update()
    {
        base.Update();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            lookingRight = false;
            velocity.X -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            lookingRight = true;
            velocity.X += speed;
        }

        velocity.X *= 0.8f;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) velocity.Y = 125;

        // Check collision with enemies
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetType() == typeof(Enemy))
                CheckEnemyCollision(gameObject.rect);
        }


        if (Math.Abs(velocity.X) > 4)
        {
            animation = Animation.allAnimations["player-run"];
        }
        else
        {
            animation = Animation.allAnimations["player-idle"];
        }

        if (velocity.Y > 0)
            animation = Animation.allAnimations["player-jump-up"];
        if (velocity.Y < -0)
            animation = Animation.allAnimations["player-jump-down"];


    }
    void CheckEnemyCollision(Rectangle enemy)
    {
        bool isOverlapping = Raylib.CheckCollisionRecs(rect, enemy);


    }
}
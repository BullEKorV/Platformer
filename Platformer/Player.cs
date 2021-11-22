public class Player : Entity
{
    int jumpForce = 480;
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

        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && touchingGround)
        {
            velocity.Y = jumpForce;
            touchingGround = false;
        }

        // Check collision with enemies
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject is Enemy)
                CheckEnemyCollision((Enemy)gameObject);
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
    void CheckEnemyCollision(Enemy enemy)
    {
        int extraDepth = 10;

        bool isOverlapping = Raylib.CheckCollisionRecs(rect, enemy.rect);

        if (isOverlapping)
        {
            if (rect.y + velocity.Y * Raylib.GetFrameTime() >= enemy.rect.y + enemy.rect.height - extraDepth)
            {
                enemy.Die();
            }
            else
            {
                Raylib.DrawRectangle(0, 0, 100, 100, Color.BLUE);
            }
        }
    }
}
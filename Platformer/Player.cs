public class Player : Entity
{
    protected int hp;
    int jumpForce = 420;
    float highJumpTimer;
    bool highJumpActive;
    float invisibilityTimer = 0;
    public Player() : base()
    {
        speed = 40;
        hp = 3;

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

        // Lower character invisibility timer
        invisibilityTimer -= Raylib.GetFrameTime();
        invisibilityTimer = Math.Max(0, invisibilityTimer);

        // Lower high jump timer
        highJumpTimer -= Raylib.GetFrameTime();
        highJumpTimer = Math.Max(0, highJumpTimer);

        float xVelocity = 0;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            lookingRight = false;
            xVelocity -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            lookingRight = true;
            xVelocity += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            xVelocity *= 1.4f;
        }
        // Console.WriteLine(rect.x + " : " + rect.y);

        velocity.X = (velocity.X + xVelocity) * 0.86f;

        // Jump logic
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && touchingGround)
        {
            highJumpTimer = 0.613f;
            highJumpActive = true;
            velocity.Y = jumpForce;
            touchingGround = false;
        }
        // high jump logic
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE))
            highJumpActive = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && highJumpActive && highJumpTimer > 0)
            velocity.Y += 2000 * highJumpTimer * Raylib.GetFrameTime();

        // Check collision with enemies
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject is Enemy)
                CheckEnemyCollision((Enemy)gameObject);
        }

        // Character animations
        if (Math.Abs(velocity.X) > 4)
            animation = Animation.allAnimations["player-run"];
        else
            animation = Animation.allAnimations["player-idle"];
        if (velocity.Y > 0)
            animation = Animation.allAnimations["player-jump-up"];
        if (velocity.Y < -0)
            animation = Animation.allAnimations["player-jump-down"];
    }
    void CheckEnemyCollision(Enemy enemy)
    {
        int extraDepth = 13;

        Rectangle rectVelocity = new Rectangle(rect.x + velocity.X * Raylib.GetFrameTime(), rect.y + velocity.Y * Raylib.GetFrameTime(), rect.width, rect.height); // Maybe better??

        bool isOverlapping = Raylib.CheckCollisionRecs(rectVelocity, enemy.rect);

        if (isOverlapping)
        {
            if (rectVelocity.y >= enemy.rect.y + enemy.rect.height - extraDepth && velocity.Y < 0) // If you come from above enemy dies
            {
                velocity.Y = 300;
                enemy.Die();
            }
            else if (invisibilityTimer == 0) // Player take damage
            {
                DamageTaken();
            }
        }
    }
    public Vector2 GetVelocity()
    {
        return velocity;
    }
    public void DisableHighJump()
    {
        highJumpActive = false;
    }
    void DamageTaken()
    {
        hp--;
        invisibilityTimer = 3;
    }
}
public class Player : Entity
{
    public int hp;
    public int score;
    int jumpForce;
    float highJumpTimer;
    bool highJumpActive;
    float invulnerabilityTimer;
    public bool isCrouching = false;
    public Vector2 crouchStuff;
    public Player() : base()
    {
        speed = 12 * scale;
        jumpForce = 96 * scale;
        hp = 3;
        mass = 1; // Controll gravity
        id = "player";

        animation = Animation.allAnimations["player-idle"];

        Vector2 hitboxSize = new Vector2(15 * scale, 21 * scale); // Define player hitbox
        rect = new Rectangle(24 * scale, 20 * scale, hitboxSize.X, hitboxSize.Y);
        crouchStuff.Y = rect.height;

        textureOffset = new Vector2(-9 * scale, 11 * scale); // Give texture an offset to match with hitbox
        crouchStuff.X = textureOffset.Y;
    }
    public override void Update()
    {
        // Decrease character invisibility timer
        invulnerabilityTimer -= Raylib.GetFrameTime() * Program.timeScale;
        invulnerabilityTimer = Math.Max(0, invulnerabilityTimer);

        // Decrease high jump timer
        highJumpTimer -= Raylib.GetFrameTime() * Program.timeScale;
        highJumpTimer = Math.Max(0, highJumpTimer);

        // Calculate x velocity
        float thisFrameX = 0;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && Program.timeScale > 0)
        {
            lookingRight = false;
            thisFrameX -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && Program.timeScale > 0)
        {
            lookingRight = true;
            thisFrameX += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            thisFrameX *= 1.6f; // FIX SCALE
        }
        velocity.X += thisFrameX;

        if ((Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL)) && velocity.Y == 0)
            isCrouching = true;
        else if (CanStandUp())
            isCrouching = false;

        if (isCrouching)
        {
            velocity.X *= 0.8f;
            int crouchSize = 5 * scale;
            rect.height = crouchStuff.Y - crouchSize;
            textureOffset.Y = crouchStuff.X + crouchSize;
        }
        else
        {
            rect.height = crouchStuff.Y;
            textureOffset.Y = crouchStuff.X;
        }

        // Jump logic
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && touchingGround)
        {
            highJumpTimer = 0.628f; // Perfect for hitting 3 blocks
            highJumpActive = true;
            velocity.Y = jumpForce * Program.timeScale;
            touchingGround = false;
        }
        // High jump logic
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE) || highJumpTimer == 0)
            highJumpActive = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && highJumpActive)
            velocity.Y += 460 * scale * highJumpTimer * Raylib.GetFrameTime() * Program.timeScale; // Perfect for hitting 3 blocks

        // Check collision with entities
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject is Entity)
                CheckEntityCollision((Entity)gameObject);
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
        if (isCrouching)
            animation = Animation.allAnimations["player-crouch"];

        base.Update(); // 
    }
    bool CanStandUp()
    {
        Rectangle headCollider = new Rectangle(rect.x, rect.y + rect.height, rect.width, 5);
        List<Tile> tiles = GameObject.gameObjects.FindAll(x => x is Tile).ConvertAll(x => (Tile)x);

        foreach (Tile tile in tiles)
        {
            if (Raylib.CheckCollisionRecs(headCollider, tile.rect)) return false;
        }
        return true;
    }
    void CheckEntityCollision(Entity entity)
    {
        int extraDepth = 13;

        Rectangle rectVelocity = new Rectangle(rect.x + velocity.X * Raylib.GetFrameTime(), rect.y + velocity.Y * Raylib.GetFrameTime(), rect.width, rect.height); // Maybe better??

        bool isOverlapping = Raylib.CheckCollisionRecs(rectVelocity, entity.rect);

        if (isOverlapping)
        {
            if (entity is (Enemy))
            {
                if (rectVelocity.y >= entity.rect.y + entity.rect.height - extraDepth && velocity.Y < 0) // If you come from above enemy dies
                {
                    velocity.Y = 60 * scale;
                    entity.OnCollision();
                }
                else if (invulnerabilityTimer == 0) // Player take damage
                {
                    DamageTaken();
                }
            }
            else if (entity is Collectible)
            {
                entity.OnCollision();
            }
        }
    }
    public void ResetPos(Vector2 pos)
    {
        rect.x = pos.X * scale * 16;
        rect.y = pos.Y * scale * 16;
        velocity.Y = 0;
        velocity.X = 0;
        lookingRight = true;
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
        invulnerabilityTimer = 1.2f; // Make player invu
    }
}
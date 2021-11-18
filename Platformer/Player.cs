public class Player : Entity
{

    public Player()
    {
        hp = 100;

        animation = Animation.allAnimations["player-idle"];

        rect = new Rectangle(200, 200, 330, 320);
    }
}
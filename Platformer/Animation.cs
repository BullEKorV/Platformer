public class Animation
{
    public static Dictionary<string, Animation> allAnimations = new Dictionary<string, Animation>();
    public List<Texture2D> frames = new List<Texture2D>();
    int currentFrame = 0;
    int totalFrames;
    int framesPerFrame = 4;
    public Animation(string dir)
    {
        string[] fileArray = Directory.GetFiles(dir);

        for (var i = 0; i < fileArray.Length; i++)
        {
            frames.Add(Raylib.LoadTexture(fileArray[i]));
        }
        totalFrames = frames.Count * framesPerFrame;


        string name = dir.Replace(@"animations\", "");
        // Console.WriteLine(name);

        allAnimations.Add(name, this);
    }
    public static void LoadAllAnimations()
    {
        string root = @"animations\";

        string[] animations = Directory.GetDirectories(root);
        foreach (string animation in animations)
        {
            new Animation(animation);
        }
    }
    public Texture2D GetCurrentFrame()
    {
        return frames[currentFrame / framesPerFrame];
    }
    public void Update()
    {
        currentFrame++;
        if (currentFrame == totalFrames * framesPerFrame)
        {
            currentFrame = 0;
        }
    }
}
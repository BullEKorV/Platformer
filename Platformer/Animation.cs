public class Animation
{
    public static Dictionary<string, Animation> allAnimations = new Dictionary<string, Animation>();
    public List<Texture2D> frames = new List<Texture2D>();
    int totalFrames;
    int framesPerFrame = 16;
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
    public static void LoadAnimationsFromDirectories()
    {
        string root = @"animations\";

        string[] animations = Directory.GetDirectories(root);
        foreach (string animation in animations)
        {
            new Animation(animation);
        }
    }
    public Texture2D GetCurrentFrame(int currentFrame)
    {
        // Console.WriteLine("frame: " + currentFrame / framesPerFrame);
        return frames[currentFrame / framesPerFrame];
    }

    public int AdvanceFrame(int currentFrame)
    {
        currentFrame++;
        // Console.WriteLine("total: " + totalFrames);
        // Console.WriteLine("current:" + currentFrame);
        // Console.WriteLine("total time:" + totalFrames * framesPerFrame);
        if (currentFrame * framesPerFrame >= totalFrames * framesPerFrame)
        {
            currentFrame = 0;
        }
        return currentFrame;
    }

    public void Update()
    {
    }
}
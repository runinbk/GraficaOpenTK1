namespace paint1
{
    public class program
    {
        
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "LearnOpenTK"))
            {
                game.Run(60.0);
            }
        }
    }

    
}

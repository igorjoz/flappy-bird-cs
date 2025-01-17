using Raylib_cs;
using System.Numerics;

namespace FlappyBird
{
    public class Game
    {
        private Bird bird;
        private PipeManager pipeManager;
        private GameStatus gameStatus;
        private const int screenWidth = 800;
        private const int screenHeight = 600;
        private float gravity = 0.5f;
        private int countdown = 3; // Countdown time in seconds
        private float countdownTimer = 0;
        private int score = 0;

        public void Initialize()
        {
            Raylib.InitWindow(screenWidth, screenHeight, "Flappy Bird Game");
            Raylib.SetTargetFPS(60);

            bird = new Bird(new Vector2(screenWidth / 4, screenHeight / 2), gravity);
            pipeManager = new PipeManager(screenWidth, screenHeight);
            gameStatus = GameStatus.Ready;
            countdown = 3; 
            countdownTimer = 0;
            score = 0;
        }

        public void Update()
        {
            if (gameStatus == GameStatus.Ready)
            {
                countdownTimer += Raylib.GetFrameTime();
                if (countdownTimer >= 1)
                {
                    countdown--;
                    countdownTimer = 0;
                }

                if (countdown <= 0)
                {
                    gameStatus = GameStatus.Playing;
                }
            }
            else if (gameStatus == GameStatus.Playing)
            {
                bird.Update();

                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    bird.Flap();
                }
                score = pipeManager.addPoint(score);

                pipeManager.Update();

                if (bird.CheckCollisionWithGround(screenHeight)
                    || bird.CheckCollisionWithPipes(pipeManager.Pipes)
                    )
                {
                    gameStatus = GameStatus.GameOver;
                }
            }
            else
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    Initialize();
                }
            }
        }

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.SkyBlue);

            if (gameStatus == GameStatus.Ready)
            {
                Raylib.DrawText(countdown.ToString(), screenWidth / 2 - 20, screenHeight / 2 - 20, 60, Raylib_cs.Color.Black);
            }
            else
            {
                bird.Draw();
                pipeManager.Draw();

                if (gameStatus == GameStatus.GameOver)
                {
                    Raylib.DrawText("Game Over", screenWidth / 2 - 160, screenHeight / 2 - 50, 70, Raylib_cs.Color.Red);
                    Raylib.DrawText("Press Enter to Restart", screenWidth / 2 - 170, screenHeight / 2 + 30, 30, Raylib_cs.Color.White);
                }
                Raylib.DrawText("Score: " + score, 10, 10, 30, Raylib_cs.Color.Black);
            }

            Raylib.EndDrawing();
        }

        public void Run()
        {
            Initialize();

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
            }

            Raylib.CloseWindow();
        }
    }
}

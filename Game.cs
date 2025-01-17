using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    public class Game
    {
        private GameStatus gameStatus;
        private const int screenWidth = 800;
        private const int screenHeight = 600;
        private float gravity = 0.5f;
        private int countdown = 3;
        private float countdownTimer = 0f;
        private int score = 0;

        public void Initialize()
        {
            Raylib.InitWindow(screenWidth, screenHeight, "Flappy pigeon");
            Raylib.SetTargetFPS(144);

            gameStatus = GameStatus.Ready;
            countdown = 3;
            countdownTimer = 0f;
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
                if (gameStatus == GameStatus.GameOver)
                {
                    Raylib.DrawText("Game Over", screenWidth / 2 - 160, screenHeight / 2 - 50, 70, Raylib_cs.Color.Red);

                    Raylib.DrawText("Press Enter to Restart", screenWidth / 2 - 170, screenHeight / 2 + 30, 30, Raylib_cs.Color.Red);
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

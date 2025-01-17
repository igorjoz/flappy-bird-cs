using Raylib_cs;
using System.Numerics;

namespace FlappyBird
{
    public class Pipe
    {
        private Vector2 position;
        private readonly int width;
        private readonly int height;
        private readonly int gap;
        private readonly Color color;

        private const int ScreenHeight = 600;
        private const int BirdSize = 10;
        private const int MoveSpeed = 2;

        public Vector2 Position => position;
        public int Width => width;
        public bool Passed { get; set; }

        public Pipe(Vector2 position, int width, int height, int gap, Color color)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.gap = gap;
            this.color = color;
            this.Passed = false;
        }

        public void Update()
        {
            // Move pipe to the left
            position.X -= MoveSpeed;
        }

        public void Draw()
        {
            // Draw top pipe
            Raylib.DrawRectangle((int)position.X, (int)position.Y, width, height, color);
            // Draw bottom pipe
            Raylib.DrawRectangle((int)position.X, (int)position.Y + height + gap, width, ScreenHeight - height - gap, color);
        }

        public bool IsOffScreen()
        {
            return position.X + width < 0;
        }

        public bool CheckCollision(Vector2 birdPosition)
        {
            // Check collision with top pipe
            bool collisionWithTopPipe = birdPosition.X + BirdSize > position.X && birdPosition.X - BirdSize < position.X + width &&
                                        birdPosition.Y - BirdSize < position.Y + height;

            // Check collision with bottom pipe
            bool collisionWithBottomPipe = birdPosition.X + BirdSize > position.X && birdPosition.X - BirdSize < position.X + width &&
                                           birdPosition.Y + BirdSize > position.Y + height + gap;

            return collisionWithTopPipe || collisionWithBottomPipe;
        }
    }
}

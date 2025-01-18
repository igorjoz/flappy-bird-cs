using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace FlappyBird
{
    public class Pipe
    {
        private Vector2 position;
        private int width;
        private int height;
        private int gap;
        private Raylib_cs.Color color;

        private const int screenWidth = 800;
        private const int screenHeight = 600;
        private const int birdRadius = 10;
        private const int moveSpeed = 3;

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
            position.X -= moveSpeed;
            // position.X = position.X - moveSpeed;
        }

        public void Draw()
        {
            Raylib.DrawRectangle(
                (int)position.X,
                (int)position.Y,
                width,
                height,
                color);

            Raylib.DrawRectangle(
                (int)position.X,
                (int)position.Y + height + gap,
                width,
                screenHeight - height - gap,
                color);
        }

        public bool IsOffScreen()
        {
            return position.X + width < 0;
        }

        public bool CheckCollision(Vector2 birdPosition)
        {
            // Check collision with top pipe
            bool collisionWithTopPipe = birdPosition.X + birdRadius > position.X && birdPosition.X - birdRadius < position.X + width &&
                                        birdPosition.Y - birdRadius < position.Y + height;

            // Check collision with bottom pipe
            bool collisionWithBottomPipe = birdPosition.X + birdRadius > position.X && birdPosition.X - birdRadius < position.X + width &&
                                           birdPosition.Y + birdRadius > position.Y + height + gap;

            return collisionWithTopPipe || collisionWithBottomPipe;
        }
    }
}

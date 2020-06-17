using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Polyastro
{
    public class Player
    {
        public static Texture2D BaseTexture;
        public static Texture2D PointerTexture;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public float Speed;
        public float Dampening;
        public float Size;

        public float PointerAngle;

        public Player(float speed, float dampening, float size)
        {
            // Set speed parameters.
            Speed = speed;
            Dampening = dampening;

            Size = size;
        }

        public void Update(GameTime time)
        {
            // Get seconds of time elapsed since last update.
            float timeElapsed = (float)time.ElapsedGameTime.TotalSeconds;

            // Update acceleration from keyboard state.
            KeyboardState keyState = Keyboard.GetState();
            Acceleration = Vector2.Zero;
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                Acceleration.X = -1f;
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
                Acceleration.X = +1f;
            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
                Acceleration.Y = -1f;
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
                Acceleration.Y = +1f;
            // Acceleration.Normalize();
            Acceleration = Vector2.Multiply(Acceleration, Speed);

            // Update position and velocity using exact integration.
            Position +=
                Vector2.Multiply(Velocity, timeElapsed) +
                Vector2.Multiply(Acceleration, timeElapsed * timeElapsed);
            Velocity +=
                Vector2.Multiply(Acceleration, timeElapsed);
            Velocity = Vector2.Multiply(Velocity, 1 - Dampening * timeElapsed);

            // Set the pointer angle from the mouse position.
            MouseState mouseState = Mouse.GetState();
            PointerAngle = (float)Math.Atan2(
                mouseState.Y - Position.Y,
                mouseState.X - Position.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the base.
            spriteBatch.Draw(
                BaseTexture,
                Position,
                null,
                Color.BlueViolet,
                0f,
                new Vector2(64f, 64f),
                Size,
                SpriteEffects.None,
                0f
            );

            // Draw the cursor.
            spriteBatch.Draw(
                PointerTexture,
                Position,
                null,
                Color.BlueViolet,
                PointerAngle,
                new Vector2(-80f, 32f),
                Size,
                SpriteEffects.None,
                0f
            );
        }
    }
}
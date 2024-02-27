using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Project2
{
    public class Hole : DrawableGameComponent
    {
        public  Texture2D _texture;
        public  Vector2 _position;
        public  bool ballInHole;
        public  SoundEffect _holeSound; // Add this field to store the sound effect
        public Texture2D texture2D;
        public Vector2 vector2;

        public Hole(Texture2D texture, Vector2 position, SoundEffect holeSound) : base(game: Game1.Instance)
        {
            _texture = texture;
            _position = position;
            _holeSound = holeSound;
            ballInHole = false;
        }

        public Hole(Texture2D texture2D, Vector2 vector2, Game game = null) : base(game)
        {
            this.texture2D = texture2D;
            this.vector2 = vector2;
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, _position, Color.White);
            spriteBatch.End();
        }


        public void Update(Ball ball)
        {
            if (!ballInHole)
            {
                // Calculate the distance between the ball and the center of the hole
                float distance = Vector2.Distance(ball.Position, new Vector2(_position.X + _texture.Width / 2, _position.Y + _texture.Height / 2));

                // Adjust the threshold based on your needs
                float holeRadius = Math.Max(_texture.Width / 2, _texture.Height / 2);
                float threshold = holeRadius / 2;

                // Check if the ball is in the hole
                if (distance < threshold)
                {
                    ballInHole = true;

                    // Play sound effect when the ball enters the hole
                    _holeSound.Play();
                }
            }
        }
    }

}

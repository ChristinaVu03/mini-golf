using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project2
{
    public class Obstacle : DrawableGameComponent, Ball.ICollidable
    {
        public Texture2D _texture;
        public Vector2 _position;

        public Vector2 Position => throw new NotImplementedException();

        public Texture2D Texture => throw new NotImplementedException();

        public Obstacle(Texture2D texture, Vector2 position) : base(Game1.Instance)
        {
            _texture = texture;
            _position = position;
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
    }

}


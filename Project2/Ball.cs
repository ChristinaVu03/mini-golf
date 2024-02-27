using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project2;
using System;
namespace Project2
{
    public class Ball : DrawableGameComponent
{
    public Texture2D _texture;
    public Vector2 _position;
    public Vector2 _velocity;
    public bool _canHit;
        public readonly SpriteBatch spriteBatch;

        public interface ICollidable
        {
            Vector2 Position { get; }
            Texture2D Texture { get; }
        }

        public Ball(Texture2D texture, Vector2 position) : base(Game1.Instance)
    {
        _texture = texture;
        _position = position;
        _velocity = Vector2.Zero;
        _canHit = true;
    }

    public override void Update(GameTime gameTime)
    {
        // Apply friction to slow down the ball
        _velocity *= 0.98f;

        // Update position based on velocity
        _position += _velocity;

        // Handle input to hit the ball
        HandleInput();

        base.Update(gameTime);
    }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, _position, Color.White);
            spriteBatch.End();
        }

        public void HandleInput()
    {
        if (_canHit && Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            // Calculate angle and power based on mouse position
            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 direction = mousePosition - _position;
            float angle = (float)Math.Atan2(direction.Y, direction.X);
            float distance = Math.Min(direction.Length(), 200); // Limit the maximum distance for power
            float power = distance / 10f;

            // Set velocity based on angle and power
            _velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * power;

            // Disable hitting until the ball stops moving
            _canHit = false;
        }

        // Enable hitting when the ball is not moving
        if (_velocity.Length() < 0.1f)
        {
            _canHit = true;
        }
    }

    // Define Position and Texture properties
    public Vector2 Position { get { return _position; } }
    public Texture2D Texture { get { return _texture; } }

        public bool CollidesWith(DrawableGameComponent other)
        {
            if (other is Ball || other is Hole || other is Obstacle)
            {
                // Ensure the other component is ICollidable
                if (other is ICollidable collidable)
                {
                    // Calculate the center and radius of the ball's bounding circle
                    Vector2 ballCenter = new Vector2(_position.X + _texture.Width / 2, _position.Y + _texture.Height / 2);
                    float ballRadius = _texture.Width / 2;

                    // Calculate the center and radius of the other component's bounding circle
                    Vector2 otherCenter = new Vector2(collidable.Position.X + collidable.Texture.Width / 2, collidable.Position.Y + collidable.Texture.Height / 2);
                    float otherRadius = Math.Max(collidable.Texture.Width / 2, collidable.Texture.Height / 2);

                    // Check if the two bounding circles intersect
                    float distance = Vector2.Distance(ballCenter, otherCenter);
                    return distance < (ballRadius + otherRadius);
                }
                // Handle the case when 'other' is of type Ball, Hole, or Obstacle but does not implement ICollidable
                // You might want to throw an exception or handle this case differently based on your design
            }

            // Handle the case when 'other' is not of type Ball, Hole, or Obstacle
            return false;
        }


        public void ReverseDirection()
        {
            _velocity = -_velocity;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
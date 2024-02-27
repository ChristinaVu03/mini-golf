using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Ball _ball;
        public Hole _hole;
        public Obstacle _obstacle;
        public int strokes;
        public bool ballInHole;
        private Vector2 _velocity;
        internal static Game Instance;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _ball = new Ball(Content.Load<Texture2D>("ballTexture.png"), new Vector2(100, 100));
            _hole = new Hole(Content.Load<Texture2D>("holeTexture"), new Vector2(500, 500));
            _obstacle = new Obstacle(Content.Load<Texture2D>("obstacleTexture"), new Vector2(300, 300));


            Components.Add(_ball);
            Components.Add(_hole);
            Components.Add(_obstacle);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void ReverseDirection()
        {
            _velocity = -_velocity;

            // Handle the case when _velocity is not a Vector2 (optional)
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!ballInHole)
            {
                // Update game logic
                _ball.Update(gameTime);
                _hole.Update(_ball);

                // Check for collisions and update strokes
                if (_ball.CollidesWith(_obstacle))
                {
                    // Play sound when the ball hits the obstacle
                    // Assuming you have a sound effect named "collisionSound" associated with collisions
                    SoundEffect collisionSound = Content.Load<SoundEffect>("collisionSound");
                    collisionSound.Play();

                    // Handle collision response (you might want to adjust the ball's velocity or position)
                    // For example, reverse the ball's direction when it hits the obstacle
                    _ball.ReverseDirection();

                    // Update the number of strokes (you might want to increase it when there's a collision)
                    strokes++;
                }


                if (_ball.CollidesWith(_hole))
                {
                    // Play sound and handle ball in hole
                    ballInHole = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw game elements
            _ball.Draw(spriteBatch: _spriteBatch);
            _hole.Draw(_spriteBatch);
            _obstacle.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

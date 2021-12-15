using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Seminarium2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D car;
        Texture2D ball;
        Vector2 ballStartPos, ballPos, ballSpeed;
        float angleSpeed;
        Vector2 circlePos, carPos;
        double circleAngle;
        double ballAngle;
        bool pause;

        Rectangle carRect;
        Rectangle ballRect;
        int ballRadius;
        float circleRadius;
        private float rotation;
        double time;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballRadius = 40;
            circleRadius = 100.0f;
            time = 0;
            ball = Content.Load<Texture2D>("ball");
            car = Content.Load<Texture2D>("Ground");
            pause = false;
            ballStartPos = new Vector2(0, Window.ClientBounds.Height - ball.Height);
            circlePos = new Vector2(3 * Window.ClientBounds.Width / 4, 3 * Window.ClientBounds.Height / 4);
            ballPos = ballStartPos;
            carPos = circlePos + (new Vector2((float)Math.Cos(circleAngle), (float)Math.Sin(circleAngle)) * circleRadius);


            carRect = new Rectangle((int)carPos.X, (int)carPos.Y, 40, 40);
            ballRect = new Rectangle((int)ballPos.X, (int)ballPos.Y, ballRadius, ballRadius);
            // TODO: use this.Content to load your game content here
            rotation = (float)(MathF.PI - circleAngle);
            ballAngle = -Math.PI/15;
            circleAngle = 0;
            angleSpeed = MathF.PI*1.5f;
            ballSpeed = new Vector2((float)Math.Cos(ballAngle), (float)Math.Sin(ballAngle))*3;


            
            


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            circleAngle +=angleSpeed*gameTime.ElapsedGameTime.TotalSeconds;

            ballPos += ballSpeed;
            // TODO: Add your update logic here

            carPos = circlePos + (new Vector2((float)Math.Cos(circleAngle), (float)Math.Sin(circleAngle)) * circleRadius);
            
            carRect.X = (int)carPos.X;
            carRect.Y = (int)carPos.Y;
            ballRect.X = (int)ballPos.X;
            ballRect.Y = (int)ballPos.Y;
            rotation = (float)(-3*Math.PI/2+ circleAngle);
            time = gameTime.ElapsedGameTime.TotalSeconds;
            if (Collision())
            {
                HandleCollison();
            }
            if (pause && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Restart();
            }
            else if (ballPos.X > Window.ClientBounds.Width || ballPos.Y < 0)
            {
                Restart();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(car, carRect, new Rectangle(0,0,car.Width,car.Height), Color.White, rotation, new Vector2(car.Width / 2, car.Height / 2), SpriteEffects.None, 0);
            _spriteBatch.Draw(ball, ballRect, Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        bool Collision()
        {
            return carRect.Intersects(ballRect);
        }
        void HandleCollison()
        {
            System.Diagnostics.Debug.WriteLine("ball pos: " + (int)ballPos.X+" "+(int)ballPos.Y + "time: " + (int)time);
            ballSpeed = Vector2.Zero;
            angleSpeed = 0;
            pause = true;
        }
        void Restart()
        {
            ballSpeed= new Vector2((float)Math.Cos(ballAngle), (float)Math.Sin(ballAngle)) * 3;
            angleSpeed = MathF.PI * 1.5f;
            SetBallPosition(ballStartPos);
            time = 0;

        }
        void SetBallPosition(Vector2 ball)
        {
            ballPos = ball;
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Myra;
using Myra.Graphics2D.UI;
using System;

namespace Maze_Crawler
{
    public class Core : Game
    {
        private GraphicsDeviceManager _graphicsDevice;
        private SpriteBatch _spriteBatch;

        private enum ActiveInputSource
        {
            Keyboard,
            Controller
        }
        private ActiveInputSource currentInputSource;

        // Player
        Texture2D playerTexture;
        string playerTextureName;
        private Vector2 playerPosition;
        private int playerRectangleSize;
        private int playerSpriteSheetCellX;
        private int playerSpriteSheetCellY;
        private float  playerRotation;
        private float playerSpeed;
        private bool playerMouseAimMode;
        private bool playerGamepadAimMode;

        public Core()
        {   Content.RootDirectory = "Content";

            _graphicsDevice = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Input Source
            currentInputSource = ActiveInputSource.Keyboard;

            // Player Init
            playerTextureName = "7Soul1_prisoner_player";
            playerPosition = new Vector2(200, 200);
            playerRectangleSize =  48;
            playerSpriteSheetCellX = 0;
            playerSpriteSheetCellY = 0;
            playerRotation = 0f;
            playerSpeed = 3f;
            playerMouseAimMode = false;
            playerGamepadAimMode = false;

            // Player Load texture
            playerTexture = Content.Load<Texture2D>(playerTextureName);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState currKeyboarState = Keyboard.GetState();
            MouseState currMouseState = Mouse.GetState();
            GamePadState currGamePadState = GamePad.GetState(PlayerIndex.One);

            // Gamepad Control section
            // Exit Game
            if (currGamePadState.Buttons.Back == ButtonState.Pressed)
                Exit();

            // Right Thumb Stick Looking
            if (Math.Abs(currGamePadState.ThumbSticks.Right.X) > 0.01f || Math.Abs(currGamePadState.ThumbSticks.Right.Y) > 0.01f)
            {
                playerRotation = (float)Math.Atan2(
                       -((float)(currGamePadState.ThumbSticks.Right.X)),
                       -((float)(currGamePadState.ThumbSticks.Right.Y))
                   );
                playerGamepadAimMode = true;
            }
            else 
            { 
                playerGamepadAimMode = false; 
            }


            // Left Thumb Stick Movement
            if (Math.Abs(currGamePadState.ThumbSticks.Left.X) > 0.01f || Math.Abs(currGamePadState.ThumbSticks.Left.Y) > 0.01f)
            {   
                if (playerGamepadAimMode == false) 
                { 
                    float targetRotation = (float)Math.Atan2(-currGamePadState.ThumbSticks.Left.X, -currGamePadState.ThumbSticks.Left.Y);
                    playerRotation = targetRotation;

                    playerPosition.X += (float)(currGamePadState.ThumbSticks.Left.X) * playerSpeed;
                    playerPosition.Y -= (float)(currGamePadState.ThumbSticks.Left.Y) * playerSpeed;
                }
                else
                {
                    // Modify speed based on movement and look directions

                    double angleLTS = (Math.Atan2(currGamePadState.ThumbSticks.Left.X, currGamePadState.ThumbSticks.Left.Y));
                    double angleRTS = (Math.Atan2(currGamePadState.ThumbSticks.Right.X, currGamePadState.ThumbSticks.Right.Y));
                    double difference = angleLTS - angleRTS;


                    if (Math.Abs(difference) < Math.PI * .5)
                    {
                        playerPosition.X += (float)(currGamePadState.ThumbSticks.Left.X) * playerSpeed;
                        playerPosition.Y -= (float)(currGamePadState.ThumbSticks.Left.Y) * playerSpeed;
                    }

                    else if (Math.Abs(difference) >= Math.PI * .5)
                    {
                        playerPosition.X += (float)(currGamePadState.ThumbSticks.Left.X) * playerSpeed / 2;
                        playerPosition.Y -= (float)(currGamePadState.ThumbSticks.Left.Y) * playerSpeed/2;
                    }

                }

                currentInputSource = ActiveInputSource.Controller;
            }



            // Keyboard Control
            // Exit Game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Aim mode (keyboard)
            if (currMouseState.RightButton == ButtonState.Pressed)
            {
                playerMouseAimMode = true;
            }
            else if (currMouseState.RightButton == ButtonState.Released)
            {
                playerMouseAimMode = false;
            }


            // Mouse Look and PC rotation
            if (currentInputSource == ActiveInputSource.Keyboard)
            {             
                if (playerMouseAimMode == true) 
                { 
                    playerRotation = (float)Math.Atan2(
                        -(currMouseState.X - playerPosition.X), 
                        (currMouseState.Y - playerPosition.Y)
                    );
                }
                currentInputSource = ActiveInputSource.Keyboard;
            }
            

            // Movement
            if (currKeyboarState.IsKeyDown(Keys.A))
            {
                if (playerMouseAimMode == true)
                {
                    playerPosition.X += (float)(Math.Cos(playerRotation)) * playerSpeed / 2;
                    playerPosition.Y += (float)(Math.Sin(playerRotation)) * playerSpeed / 2;
                }
                else
                {
                    playerRotation -= (float)Math.PI / 60;
                }
    
                currentInputSource = ActiveInputSource.Keyboard;

            }
            else if (currKeyboarState.IsKeyDown(Keys.D))
            {
                if (playerMouseAimMode == true)
                {
                    playerPosition.X -= (float)(Math.Cos(playerRotation)) * playerSpeed / 2;
                    playerPosition.Y -= (float)(Math.Sin(playerRotation)) * playerSpeed / 2;
                }
                else
                {
                    playerRotation += (float)Math.PI / 60;
                }

                currentInputSource = ActiveInputSource.Keyboard;
            }

            if (currKeyboarState.IsKeyDown(Keys.W))
            {
                playerPosition.X -= (float)(Math.Sin(playerRotation)) * playerSpeed;
                playerPosition.Y += (float)(Math.Cos(playerRotation)) * playerSpeed;
                currentInputSource = ActiveInputSource.Keyboard;
            } 
            else if (currKeyboarState.IsKeyDown(Keys.S))
            {
                playerPosition.X += (float)(Math.Sin(playerRotation)) * (playerSpeed/2);
                playerPosition.Y -= (float)(Math.Cos(playerRotation)) * (playerSpeed/2);
                currentInputSource = ActiveInputSource.Keyboard;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();

            _spriteBatch.Draw(
                playerTexture, 
                playerPosition, 
                new Rectangle(playerSpriteSheetCellX * playerRectangleSize, playerSpriteSheetCellY * playerRectangleSize, playerRectangleSize, playerRectangleSize), 
                Color.White,
                playerRotation,
                new Vector2(playerRectangleSize/2, playerRectangleSize/2),
                1.1f,
                SpriteEffects.None,
                1);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
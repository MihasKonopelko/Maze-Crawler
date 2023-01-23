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



        // Player
        Texture2D playerTexture;
        string playerTextureName;
        private Vector2 playerPosition;
        private int playerRectangleSize;
        private int playerSpriteSheetCellX;
        private int playerSpriteSheetCellY;
        private float  playerRotation;
        private float playerSpeed;

        public Core()
        {   Content.RootDirectory = "Content";

            _graphicsDevice = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Player Init
            playerTextureName = "7Soul1_prisoner_player";
            playerPosition = new Vector2(200, 200);
            playerRectangleSize =  48;
            playerSpriteSheetCellX = 0;
            playerSpriteSheetCellY = 0;
            playerRotation = 0f;
            playerSpeed = 1f;

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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); 

            if (currKeyboarState.IsKeyDown(Keys.A))
            {
                playerRotation -= ((float)Math.PI)/60;
            }
            if (currKeyboarState.IsKeyDown(Keys.D))
            {
                playerRotation += ((float)Math.PI) / 60;
            }

            if (currKeyboarState.IsKeyDown(Keys.W))
            {
                playerPosition.X -= (float)(Math.Sin(playerRotation)) * playerSpeed;
                playerPosition.Y += (float)(Math.Cos(playerRotation)) * playerSpeed;
            }

            if (currKeyboarState.IsKeyDown(Keys.S))
            {
                playerPosition.X += (float)(Math.Sin(playerRotation)) * (playerSpeed/2);
                playerPosition.Y -= (float)(Math.Cos(playerRotation)) * (playerSpeed/2);
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
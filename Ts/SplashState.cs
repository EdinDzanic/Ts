using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ts
{
    class SplashState : IGameState
    {
        GameStateManager gameStateManager;
        float deltaTime = 0f;
        private Game1 game;

        public SplashState(Game1 game, GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
            this.game = game;
        }
        
        public void Update(GameTime gameTime)
        {
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (deltaTime > 2f)
            {
                gameStateManager.ChangeState("menu");
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Aqua);
        }
    }
}

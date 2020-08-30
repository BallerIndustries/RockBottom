using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GRODG2
{
    public class StartPromptState : BaseState, IState
    {
        private SpriteBatch spriteBatch;
        private string text = "PRESS START";
        private Vector2 position = new Vector2();

        public StartPromptState(SpriteBatch spriteBatch)
        {
            position.X = (1280 - Fonts.MenuFont.MeasureString(text).X) / 2;
            position.Y = (720 - Fonts.MenuFont.MeasureString(text).Y) / 2;

            this.spriteBatch = spriteBatch;
        }

        public void Enter()
        {
            Game1.current_state.Exit();
            Game1.current_state = this;
        }

        public void Exit()
        {
        }

        public void Update(GameTime gameTime)
        {
#if XBOX
            Controls.controlling_player = PlayerIndex.One;

            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                if (GamePad.GetState(index).Buttons.Start == ButtonState.Pressed)
                {
                    Controls.controlling_player = index;
                    Game1.current_state = Game1.menu_state;
                    //game_state.state = GameState.State.LanguageWarning;
                    break;
                }
            }
#else

            //if (Controls.pressed_once(Keys.Enter))
                Game1.current_state = Game1.menu_state;
#endif
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(Fonts.MenuFont, text, position, Color.White);
        }
    }
}

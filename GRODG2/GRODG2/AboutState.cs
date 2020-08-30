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
    public class AboutState : BaseState, IState
    {
        private Texture2D about;
        private ContentManager Content;
        private SpriteBatch spriteBatch;

        public AboutState(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.Content = new ContentManager(Content.ServiceProvider);
            this.Content.RootDirectory = "Content";
            //this.Content = new ContentManager();
            //this.Content = Content;
            this.spriteBatch = spriteBatch;
        }

        public void Enter()
        {
            Game1.current_state.Exit();
            Game1.current_state = this;
            about = Content.Load<Texture2D>("about");
        }

        public void Exit()
        {
            Content.Unload();
            //Content.Unload();
            //about = null;
        }

        public void Update(GameTime gameTime)
        {
            if (Controls.pressed_once(Keys.Escape) || Controls.pressed_once(Buttons.B))
            {
                Exit();
                Game1.current_state = Game1.menu_state;
                //G
                //Game1.current_state = 
                //game_state.state = GameState.State.Menu;
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(about, Vector2.Zero, Color.White);
        }
    }
}

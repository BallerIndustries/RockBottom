using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GRODG2
{
    public class MenuState : BaseState, IState
    {
        private MenuData main_menu_data;
        private SpriteBatch spriteBatch;
        private Game1 game;

        private string build_date;
        private Vector2 build_date_pos;

        public MenuState(SpriteBatch spriteBatch, Game1 game)
        {
            main_menu_data = new MenuData();
            main_menu_data.menu_text.Add("New Game");
            main_menu_data.menu_text.Add("About");
            main_menu_data.menu_text.Add("Exit");

            this.spriteBatch = spriteBatch;
            this.game = game;

            build_date = "26th August 2011 Build";
            build_date_pos.Y = 641 - Fonts.MenuFont.MeasureString(build_date).Y;
            build_date_pos.X = 1151 - Fonts.MenuFont.MeasureString(build_date).X;
        }

        public void Update(GameTime gameTime)
        {
            if (Controls.down_once())
                main_menu_data.move_down();
            else if (Controls.up_once())
                main_menu_data.move_up();

            if (Controls.pressed_once(Keys.Enter) || Controls.pressed_once(Buttons.A))
            {
                switch (main_menu_data.selected_index)
                {
                    case 0:
                        Game1.cutscene_state.Enter(Game1.opening_scene);
                        break;

                    case 1:
                        Game1.about_state.Enter();
                        break;

                    case 2:
                        game.Exit();
                        break;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            Vector2 pos = new Vector2();

            pos.X = 100;

            for (int i = 0; i < main_menu_data.menu_text.Count; i++)
            {
                pos.Y += 100; 

                if (i == main_menu_data.selected_index)
                    spriteBatch.DrawString(Fonts.MenuFont, main_menu_data.menu_text[i], pos, Color.Yellow);
                else
                    spriteBatch.DrawString(Fonts.MenuFont, main_menu_data.menu_text[i], pos, Color.White);
            }

            spriteBatch.DrawString(Fonts.MenuFont, build_date, build_date_pos, Color.White);
        }

        public void Enter()
        {
            main_menu_data.selected_index = 0;
        }

        public void Exit()
        {
            
        }
    }
}

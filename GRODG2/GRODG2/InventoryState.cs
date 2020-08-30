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
    public class InventoryState : BaseState, IState
    {
        //private ContentManager Content;
        private SpriteBatch spriteBatch;
        private Inventory inventory;
        private Rectangle title_safe_rect;
        private Texture2D white_dot;
        

        public InventoryState(ContentManager Content, SpriteBatch spriteBatch, Rectangle title_safe_rect)
        {
            this.spriteBatch = spriteBatch;
            inventory = new Inventory();

            white_dot = Content.Load<Texture2D>("white_dot");
            this.title_safe_rect = title_safe_rect;

            inventory.add_item(new Item(Globals.tm, Item.Type.glitter));
            inventory.add_item(new Item(Globals.tm, Item.Type.paraskeetamol));
            inventory.add_item(new Item(Globals.tm, Item.Type.glitter));
        }

        public void Enter()
        {
            Game1.current_state.Exit();
            Game1.current_state = Game1.inventory_state;
        }

        public void Exit()
        {
        }

        public void Update(GameTime gameTime)
        {
            //cursor.position.X = mouseState.X;
            //cursor.position.Y = mouseState.Y;
            Controls.set_cursor();

            if (Controls.pressed_once(Keys.I) || Controls.pressed_once(Buttons.Y))
            {
                Game1.gameplay_state.Enter();
                //game_state.state = GameState.State.GamePlay;
            }

            inventory.Update(Controls.cursor.position.X, Controls.cursor.position.Y);

            if (Controls.pressed_once(Keys.Enter) || Controls.clicked_once() || Controls.pressed_once(Buttons.A))
            {
                Game1.gameplay_state.Enter();
                //game_state.state = GameState.State.GamePlay;

                if (inventory.selected_item != null)
                {
                    Controls.cursor.item = inventory.selected_item;
                    Controls.cursor.type = Cursor.Type.item;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Globals.current_room.texture, new Rectangle(0, 0, 1280, 720), Globals.current_room.bound, Color.White);

            foreach (Character c in Globals.current_room.characters)
            {
                spriteBatch.Draw(c.texture, c.position, Color.White);
            }

            spriteBatch.Draw(white_dot, title_safe_rect, Color.White);
            draw_rect(title_safe_rect, Color.Black);
            spriteBatch.DrawString(Fonts.DialogueItemFont, "Items", new Vector2(150, 100), Color.Black);

            inventory.Draw(spriteBatch);
            if (inventory.selected_item != null)
                draw_rect(new Rectangle((int)inventory.selected_item.position.X, (int)inventory.selected_item.position.Y, 100, 100), Color.Red);

            Controls.cursor.Draw(spriteBatch);
        }

        private void draw_rect(Rectangle rect, Color col)
        {
            int x, y, w, h;
            x = rect.X;
            y = rect.Y;
            w = rect.Width;
            h = rect.Height;

            spriteBatch.Draw(white_dot, new Rectangle(x, y, 2, h), col);
            spriteBatch.Draw(white_dot, new Rectangle(x, y, w, 2), col);
            spriteBatch.Draw(white_dot, new Rectangle(x + w, y, 2, h), col);
            spriteBatch.Draw(white_dot, new Rectangle(x, y + h, w, 2), col);
        }
    }
}

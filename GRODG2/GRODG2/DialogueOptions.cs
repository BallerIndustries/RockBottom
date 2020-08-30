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
    public class DialogueOptions
    {
        private List<DialogueItem> options = new List<DialogueItem>(5);
        private const int x = 900;
        private const int y = 81;

        private Vector2 next_item_pos;
        private Vector2 tail_pos;
        private Texture2D bubble_top, bubble_mid, bubble_bottom, selector;
        private SpriteFont font;

        public DialogueOptions()
        {
            this.font = Fonts.DialogueItemFont;
            TextureManager tm = Globals.tm;

            bubble_top = tm.find_texture("bubble_top");
            bubble_mid = tm.find_texture("bubble_item");
            bubble_bottom = tm.find_texture("bubble_bottom");
            selector = tm.find_texture("arrow");

            next_item_pos = new Vector2();
            tail_pos = new Vector2();

            next_item_pos.X = x;
            next_item_pos.Y = y + bubble_top.Height;

            tail_pos.X = x;
            tail_pos.Y = next_item_pos.Y;
        }

        public void add_dialogue_item(DialogueItem di)
        {
            options.Add(di);

            di.position.X = x;

            if (di.visible)
            {
                di.position.Y = next_item_pos.Y;
                next_item_pos.Y += bubble_mid.Height;
                tail_pos.Y += bubble_mid.Height;
            }

            di.center_text(Fonts.DialogueItemFont, bubble_mid.Width, bubble_mid.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bubble_top, new Vector2(x, y), Color.White);

            foreach (DialogueItem di in options)
            {
                if (di.visible == false)
                    continue;

                spriteBatch.Draw(bubble_mid, di.position, Color.White);
                
                if (di.clicked)
                    spriteBatch.DrawString(font, di.text, di.text_position, Color.Gray);
                else
                    spriteBatch.DrawString(font, di.text, di.text_position, Color.Black);

                if (di.highlighted)
                    spriteBatch.Draw(selector, new Vector2(x + 30, di.text_position.Y + 10), Color.White);
            }

            spriteBatch.Draw(bubble_bottom, tail_pos, Color.White);
        }

        public void Update(Rectangle position)
        {
            Rectangle r = new Rectangle();
            r.X = x;
            r.Width = bubble_mid.Width;
            r.Height = bubble_mid.Height;

            foreach (DialogueItem di in options)
            {   
                r.Y = (int)di.position.Y;

                if (r.Contains((int)position.X, (int)position.Y))
                    di.highlighted = true;
                else
                    di.highlighted = false;
            }
        }

        public CutScene get_highlighted_scene()
        {
            //foreach (DialogueItem di in options)
            for (int i = 0; i < options.Count; i++)
            {
                DialogueItem di = options[i];

                if (di.highlighted)
                {
                    next_item_visible(i);
                    di.clicked = true;
                    return di.scene_to_play;
                }
            }

            return null;
        }
        
        //This makes the next 
        void next_item_visible(int i)
        {
            if (i < options.Count - 1)
            {
                options[i + 1].visible = true;
                rearrange_elements_and_tail();
            }
        }

        void rearrange_elements_and_tail()
        {
            int current_y = y + bubble_top.Height;

            foreach (DialogueItem di in options)
            {
                if (di.visible)
                {
                    di.position.Y = current_y;
                    di.center_text(font, bubble_mid.Width, bubble_mid.Height);
                    current_y += bubble_mid.Height;
                }
            }

            tail_pos.Y = current_y;
        }
    }
}

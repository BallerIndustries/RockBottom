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
    public class Inventory
    {
        private List<Item> items = new List<Item>(10);
        private Vector2 start_pos = new Vector2(150, 150);
        //private Texture2D white_dot;
        public Item selected_item;

        public Inventory()
        {
        }

        public void add_item(Item item)
        {
            int count = items.Count;

            if (count == 0)
                item.position = start_pos;
            else
            {
                item.position.Y = start_pos.Y + ((count / 9) * 100);
                item.position.X = start_pos.X + ((count % 9) * 100);
            }

            items.Add(item);
        }

        public void Update(int x, int y)
        {
            Rectangle rect = new Rectangle();
            rect.Width = 100;
            rect.Height = 100;

            foreach (Item item in items)
            {
                rect.X = (int)item.position.X;
                rect.Y = (int)item.position.Y;

                if (rect.Contains(x, y))
                {
                    selected_item = item;
                    return;
                }
            }

            selected_item = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Item item in items)
            {
                spriteBatch.Draw(item.texture, item.position, Color.White);
            }
        }

    }

    public class Item
    {
        public Texture2D texture;
        public Vector2 position;

        public enum Type
        {
            glitter,
            scroll,
            paraskeetamol,
            guard_custume
        };


        public Item(TextureManager tm, Type type)
        {
            string name = get_texture_name(type);
            texture = tm.find_texture(name);
        }

        private string get_texture_name(Type type)
        {
            switch (type)
            {
                case Type.glitter:
                    return "Items//tangerine_glitter";

                case Type.paraskeetamol:
                    return "Items//paraskeetamol";



                default:
                    return "";
            }
        }
    }
}

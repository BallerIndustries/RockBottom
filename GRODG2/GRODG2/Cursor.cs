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
    public class Cursor
    {
        public enum Type
        {
            grab,
            look,
            talk,
            go,
            exit,
            item
        }

        public Item item;
        public Rectangle position;
        private SpriteEffects se;
        private Type _type;
        public Type type
        {
            get { return _type; }
            set
            {
                _type = value;

                if (value == Type.go)
                {
                    position.Width = go.Width;
                    position.Height = go.Height;
                    se = SpriteEffects.None;    
                }
                else if (value == Type.talk)
                {
                    position.Width = talk.Width;
                    position.Height = talk.Width;
                    se = SpriteEffects.None;
                }
                else if (value == Type.look)
                {
                    position.Width = look.Width;
                    position.Height = look.Width;
                    se = SpriteEffects.None;
                }
                else if (value == Type.grab)
                {
                    position.Width = grab.Width;
                    position.Height = grab.Width;
                    se = SpriteEffects.None;
                }
                else if (value == Type.exit)
                {
                    position.Width = go.Width;
                    position.Height = go.Height;
                    se = SpriteEffects.FlipVertically;
                }
            }
        }
        public Texture2D go, look, grab, talk;

        public Cursor()
        {
            TextureManager tm = Globals.tm;

            go = tm.find_texture("go");
            look = tm.find_texture("look");
            grab = tm.find_texture("grab");
            talk = tm.find_texture("talk");

            position.X = 640;
            position.Y = 360;

            position.Width = go.Width;
            position.Height = go.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (type)
            {
                case Type.go:
                    spriteBatch.Draw(go, position, Color.White);
                    break;

                case Type.look:
                    spriteBatch.Draw(look, position, Color.White);
                    break;

                case Type.grab:
                    spriteBatch.Draw(grab, position, Color.White);
                    break;

                case Type.talk:
                    spriteBatch.Draw(talk, position, Color.White);
                    break;

                case Type.exit:
                    spriteBatch.Draw(go, position, go.Bounds, Color.White, 0, Vector2.Zero, se, 0);
                    break;

                case Type.item:
                    spriteBatch.Draw(item.texture, position, Color.White);
                    break;
            }
          
        }
    }
}

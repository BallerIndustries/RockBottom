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
    public class Character
    {
        public enum Type
        {
            sweepy,
            guard,
            professor_x,
            black_betty,
            collar_popper,
            mc_hummus,
            ben_seib,
            red_square
        }

        public Type type;
        public Texture2D texture;
        public Rectangle position;
        public bool visible = true;
        public List<Animation> animations;

        private int blink_hold = 480;
        private int frame_holds = 5;

        public Character(Type type, Point position)
        {
            TextureManager tm = Globals.tm;

            string texture_name = find_texture_name(type);
            texture = tm.find_texture(texture_name);

            this.position = new Rectangle();

            this.position.X = position.X;
            this.position.Y = position.Y;
            this.position.Width = texture.Width;
            this.position.Height = texture.Height;

            set_animations(tm);
        }

        private void set_animations(TextureManager tm)
        {
            if (type == Type.mc_hummus)
            {
                Animation blink = new Animation();
                blink.add_frame("Animations//MC Hummus//Blink//mc_blink0001", tm);
                blink.add_frame("Animations//MC Hummus//Blink//mc_blink0002", tm);
                blink.add_frame("Animations//MC Hummus//Blink//mc_blink0003", tm);
                blink.add_frame("Animations//MC Hummus//Blink//mc_blink0004", tm);
                blink.add_frame("Animations//MC Hummus//Blink//mc_blink0005", tm);
                blink.add_frame("Animations//MC Hummus//Blink//mc_blink0006", tm);
                blink.add_frame("Animations//MC Hummus//Blink//mc_blink0007", tm);

                animations.Add(blink);
            }
        }

        public void Update()
        {
            blink_hold--;

            if (blink_hold <= 0)
            {
                if (frame_holds < 0)
                {
                    //Increment frames

                }
            }
        }

        private string find_texture_name(Type type)
        {
            switch (type)
            {
                case Type.ben_seib:
                    return "Characters//ben_seib";

                case Type.black_betty:
                    return "Characters//black_betty";

                case Type.collar_popper:
                    return "Characters//collar_poppa";

                case Type.guard:
                    return "Characters//guard";

                case Type.mc_hummus:
                    return "Characters//mc_hummus";

                case Type.professor_x:
                    return "Characters//professor_x";

                case Type.sweepy:
                    return "Characters//sweepy";

                case Type.red_square:
                    return "Characters//red_square";

                default:
                    return "";
            }
        }
    }
}

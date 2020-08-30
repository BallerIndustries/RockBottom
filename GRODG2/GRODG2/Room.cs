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
    public class Room
    {
        public enum ID
        {
            cell,
            hallway,
            yard,
            toilet,
            laundry,
            library,
            ballroom,
            collar_poppa
        }

        public ID id;
        public Texture2D texture;
        public List<HotSpot> hotspots = new List<HotSpot>(5);
        public List<Character> characters = new List<Character>(3);
        public Rectangle bound;

        public Room(ID id)
        {
            this.id = id;
            string texture_name = find_texture_name();

            this.texture = Globals.tm.find_texture(texture_name);
        }

        private string find_texture_name()
        {
            string return_string = "";
            
            switch (id)
            {
                case ID.collar_poppa:
                    return_string = "black_dot";
                    bound = new Rectangle(0, 0, 1, 1);
                    break;

                case ID.cell:
                    return_string = "Backgrounds//cell";
                    bound = new Rectangle(0, 0, 1280, 720);
                    break;

                case ID.hallway:
                    return_string = "Backgrounds//lobby";
                    bound = new Rectangle(0, 0, 1280, 720);
                    break;

                case ID.yard:
                    return_string = "Backgrounds//yard";
                    bound = new Rectangle(0, 0, 1280, 720);
                    break;

                case ID.toilet:
                    return_string = "Backgrounds//bathroom";
                    bound = new Rectangle(0, 0, 1280, 720);
                    break;

                case ID.laundry:
                    return_string = "Backgrounds//laundry";
                    bound = new Rectangle(0, 0, 1280, 720);
                    break;

                case ID.library:
                    return_string = "Backgrounds//library";
                    bound = new Rectangle(0, 0, 1280, 720);
                    break;

                case ID.ballroom:
                    return_string = "Backgrounds//ballroom";
                    bound = new Rectangle(0, 0, 1280, 720);
                    break;
            }

            return return_string;
        }
    }
}

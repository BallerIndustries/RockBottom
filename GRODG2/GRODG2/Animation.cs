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
    public class Animation
    {
        private int frame_num;
        private List<Texture2D> frames = new List<Texture2D>(10);
        public bool repeats;

        public Texture2D current_frame
        {
            get { return frames[frame_num]; }
        }

        public Animation()
        {
        }

        public void add_frame(string name, TextureManager tm)
        {
            frames.Add(tm.find_texture(name));
        }
    }
}

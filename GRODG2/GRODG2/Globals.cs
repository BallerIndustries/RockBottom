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
    public class Globals
    {
        public static SoundBank soundBank;
        public static AudioEngine audioEngine;
        public static Room current_room;
        public static TextureManager tm;
        public static Rectangle title_safe_rect;

        public Globals()
        {
        }

        public static void StopEffects()
        {
            AudioCategory c = audioEngine.GetCategory("Default");
            c.Stop(AudioStopOptions.AsAuthored);
        }
    }
}

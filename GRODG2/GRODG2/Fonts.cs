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
    public class Fonts
    {
        public Fonts(ContentManager Content)
        {
            Fonts.MenuFont =  Content.Load<SpriteFont>("MenuFont");
            Fonts.SubtitleFont = Content.Load<SpriteFont>("SubtitleFont");
            Fonts.DialogueItemFont = Content.Load<SpriteFont>("DialogueItemFont");
        }

        public static SpriteFont MenuFont;
        public static SpriteFont SubtitleFont;
        public static SpriteFont DialogueItemFont;
    }
}

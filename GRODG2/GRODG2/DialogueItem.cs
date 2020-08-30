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
    public class DialogueItem
    {
        public string text;
        public  CutScene scene_to_play;
        public bool highlighted = false;
        public bool visible;
        public bool clicked = false; //Defines whether it has been clicked on before.
        public Vector2 position;
        public Vector2 text_position;

        public DialogueItem(string text, CutScene scene_to_play, bool visible)
        {
            this.text = text;
            this.scene_to_play = scene_to_play;
            this.visible = visible; 
        }

        public void center_text(SpriteFont font, int width, int height)
        {
            text_position = new Vector2();
            text_position.X = (position.X + (position.X + width - font.MeasureString(text).X)) / 2;
            text_position.Y = (position.Y + (position.Y + height - font.MeasureString(text).Y)) / 2;
        }
    }
}

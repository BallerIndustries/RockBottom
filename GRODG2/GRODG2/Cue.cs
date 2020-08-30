﻿using System;
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
    public enum CueType
    {
        Sound,
        Transition,
        ViewPanel,
        End,
        Comic,
        Graphic,
        Text,
        Fade
    }

    public class Cue : IComparable
    {
        public ulong fire_at;
        public ulong remove_at;
        public CueType type;
        public int draw_order;

        public Cue(ulong fire_at, ulong remove_at, CueType type, int draw_order)
        {
            this.fire_at = fire_at;
            this.remove_at = remove_at;
            this.type = type;
            this.draw_order = draw_order;
        }

        public int CompareTo(Object obj)
        {
            Cue c = (Cue)obj;

            if (c.draw_order < draw_order)
                return 1;
            else if (c.draw_order == draw_order)
                return 0;
            else
                return -1;
        }
    }

    //The defintion for a sound cue.
    public class Sound : Cue
    {
        public string name;

        public Sound(ulong fire_at, string name) : base(fire_at, 0, CueType.Sound, 0)
        {
            this.name = name;
        }
    }

    //The defines how long we look at a region of the comic strip.
    public class ViewPanel : Cue
    {
        public Rectangle panel_region;

        public ViewPanel(ulong fire_at, ulong remove_at, Rectangle panel_region)
            : base(fire_at, remove_at, CueType.ViewPanel, 0)
        {
            this.panel_region = panel_region;
        }
    }

    //This defines when a cut scene ends
    public class EndCue : Cue
    {
        public EndCue(ulong fire_at)
            : base(fire_at, 0, CueType.End, 0)
        {
        }
    }

    public class Comic : Cue
    {
        public Texture2D texture;
        public int x; //This is where the comic starts.

        public Comic(ulong fire_at, ulong remove_at, string name)
            : base(fire_at, remove_at, CueType.Comic, 0)
        {
            texture = Globals.tm.find_texture(name);
        }
    }

    public class Graphic : Cue
    {
        public Texture2D texture;
        public Rectangle position;
        public Rectangle bound;

        public Graphic(ulong fire_at, ulong remove_at, string texture_name, int draw_order, Vector2 position) :
            base(fire_at, remove_at, CueType.Graphic, draw_order)
        {
            this.texture = Globals.tm.find_texture(texture_name);
            this.position.X = (int)position.X;
            this.position.Y = (int)position.Y;
            this.position.Width = texture.Bounds.Width;
            this.position.Height = texture.Bounds.Height;

            this.bound = new Rectangle(0, 0, this.position.Width, this.position.Height);
        }

        public Graphic(ulong fire_at, ulong remove_at, Room room) :
            base(fire_at, remove_at, CueType.Graphic, 0)
        {
            this.texture = room.texture;
            this.position = new Rectangle(0, 0, 1280, 720);
            this.bound = room.bound;
        }
    }

    public class Text : Cue
    {
        public string text;
        public List<string> lines;
        public List<Vector2> pos;

        public Text(ulong fire_at, ulong remove_at, string text) :
            base(fire_at, remove_at, CueType.Text, 1000)
        {
            this.text = text;
            lines = WrapText(Fonts.SubtitleFont);
            set_positions(lines, Fonts.SubtitleFont);
        }

        private void set_positions(List<string> lines, SpriteFont font)
        {
            pos = new List<Vector2>(lines.Count);

            for (int i = 0; i < lines.Count; i++)
            {
                Vector2 vec2 = new Vector2();

                vec2.X = (1280 - font.MeasureString(lines[i]).X) / 2;
                vec2.Y = 648 - ((font.MeasureString(lines[i]).Y) * (lines.Count - i));

                pos.Add(vec2);
            }
        }

        private List<string> WrapText(SpriteFont font)
        {
            List<String> lines = new List<String>();
            String[] words = text.Split(' ');
            String line = "";
            int width = 1064;

            foreach (String word in words)
            {
                // If the current line, plus the current word is greater than the 
                // width argument, then add the current line to the list of lines.
                // Set the current line to the word
                if (font.MeasureString(line + " " + word).X > width)
                {
                    lines.Add(line);
                    line = word;
                }
                // If not, add a [SPACE] plus the word to the line
                else
                    line += " " + word;
            }

            if (!string.IsNullOrEmpty(line))
                lines.Add(line);

            return lines;
        }
    }

    public class Fade : Cue
    {
        public float alpha_val = 0;
        private ulong halfway_point;
      
        public Fade(ulong fire_at, ulong remove_at)
            : base(fire_at, remove_at, CueType.Fade, 2000)
        {
            halfway_point = (fire_at + remove_at) / 2;
        }

        public void Update(double time)
        {
            if (time < halfway_point)
            {
                alpha_val = MathHelper.Lerp(0, 1.0f, ((float)time - fire_at) / (float)(halfway_point - fire_at));
            }
            else
            {
                alpha_val = MathHelper.Lerp(1.0f, 0, ((float)time - halfway_point) / (float)(remove_at - halfway_point));
            }
        }
    }

    public class Transition : Cue
    {
        public Rectangle from_rect;
        public Rectangle to_rect;
        public Rectangle trans_rect;

        public Transition(ulong fire_at, ulong remove_at, Rectangle from_rect, Rectangle to_rect)
            : base(fire_at, remove_at, CueType.Transition, 0)
        {
            this.from_rect = from_rect;
            this.to_rect = to_rect;
        }

        public void calc_trans_rect(float percentage)
        {
            //Returns a rectangle that represents the transition from one rectangle to another
            //at the point in a certain percentage.

            trans_rect.X = (int)MathHelper.SmoothStep(from_rect.X, to_rect.X, percentage);
            trans_rect.Y = (int)MathHelper.SmoothStep(from_rect.Y, to_rect.Y, percentage);
            trans_rect.Width = (int)MathHelper.SmoothStep(from_rect.Width, to_rect.Width, percentage);
            trans_rect.Height = (int)MathHelper.SmoothStep(from_rect.Height, to_rect.Height, percentage);
        }
    }
}

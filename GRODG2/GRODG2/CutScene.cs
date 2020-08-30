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
    public class CutScene
    {
        public readonly List<Cue> currently_displayed = new List<Cue>(5);
        public readonly List<Cue> all_scene_objects = new List<Cue>(15);
        private List<Comic> comics = new List<Comic>(5);
        public int comic_num;
        //public GameState.State return_state;
        public IState return_state;
        
        private Texture2D white_dot;
        private TextureManager tm;
        public DialogueOptions dialogue_options;
        public bool skippable = true;

        public delegate void LoadAction();
        public LoadAction load_action;

        public Comic current_comic
        {
            get { return comics[comic_num]; }
        }

        public CutScene(IState return_state, LoadAction load_action)
        {
            TextureManager tm = Globals.tm;

            this.return_state = return_state;
            this.load_action = load_action;
            this.tm = tm;
            white_dot = tm.find_texture("white_dot");
        }

        public CutScene(IState return_state, LoadAction load_action, DialogueOptions dialogue_options)
        {
            TextureManager tm = Globals.tm;

            this.return_state = return_state;
            this.load_action = load_action;
            this.tm = tm;
            this.dialogue_options = dialogue_options;
            white_dot = tm.find_texture("white_dot");
        }

        //I want some sort of control over adding stuff to the two lists.
        //This is because I am a CONTROL freak.
        public void add_cue(Cue cue)
        {
            ViewPanel vp = cue as ViewPanel;
            Comic c = cue as Comic;
            //EndCue ec = cue as EndCue;

            if (vp != null)
            {
                //Increment the region's X value by the previous comic widths
                vp.panel_region.X += sum_prev_comic_width();
            }
            else if (c != null)
            {
                comics.Add(c);
                c.x = sum_prev_comic_width();
                comic_num++;
                return;
            }
            //else if (ec != null)
            //{
            //    //truncate_cues(ec.fire_at);
            //}

            if (cue.fire_at == 0)
                currently_displayed.Add(cue);
            else
                all_scene_objects.Add(cue);
        }

        private void truncate_cues(ulong remove_at)
        {
            foreach (Cue cue in all_scene_objects)
            {
                //Truncate cues that are past the EndCue
                if (cue.remove_at > remove_at)
                {
                    cue.remove_at = remove_at;
                }
            }

            foreach (Cue cue in currently_displayed)
            {
                if (cue.remove_at > remove_at)
                {
                    cue.remove_at = remove_at;
                }
            }
        }

        private int sum_prev_comic_width()
        {
            int sum = 0;

            for (int i = 0; i < comic_num; i++)
            {
                sum += comics[i].texture.Width;
            }

            return sum;
        }

        public void add_transitions()
        {
            //This lazy fucked up code written by Angus Cheng makes a lot of
            //lazy fucked up assumptions.
            //1. The ViewPanel's are in order
            //2. We want a transition between all ViewPanels

            //Count up the number of ViewPanels.
            List<ViewPanel> vp_list = new List<ViewPanel>(10);

            foreach (Cue cue in currently_displayed)
            {
                if (cue.type == CueType.ViewPanel)
                {
                    vp_list.Add(cue as ViewPanel);
                }
            }

            foreach (Cue cue in all_scene_objects)
            {
                if (cue.type == CueType.ViewPanel)
                {
                    vp_list.Add(cue as ViewPanel);
                }
            }

            for (int i = 0; i < vp_list.Count - 1; i++)
            {
                all_scene_objects.Add(new Transition(vp_list[i].remove_at - 500, vp_list[i].remove_at, vp_list[i].panel_region, vp_list[i + 1].panel_region));
                vp_list[i].remove_at -= 500;
            }
        }

        public void add_fades()
        {
            List<ViewPanel> vp_list = new List<ViewPanel>(10);

            foreach (Cue cue in currently_displayed)
            {
                if (cue.type == CueType.ViewPanel)
                {
                    vp_list.Add(cue as ViewPanel);
                }
            }

            foreach (Cue cue in all_scene_objects)
            {
                if (cue.type == CueType.ViewPanel)
                {
                    vp_list.Add(cue as ViewPanel);
                }
            }

            for (int i = 0; i < vp_list.Count - 1; i++)
            {
                all_scene_objects.Add(new Fade(vp_list[i].remove_at - 500, vp_list[i].remove_at));
                vp_list[i].remove_at -= 250;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D SubtitleLayover)
        {
            foreach (Cue cue in currently_displayed)
            {
                if (cue.type == CueType.ViewPanel)
                {
                    ViewPanel vp = cue as ViewPanel;
                    Comic begins_in = rect_begins_in(vp.panel_region);
                    Rectangle source = vp.panel_region;
                    source.X -= begins_in.x;

                    spriteBatch.Draw(begins_in.texture, scale_rect(source), source, Color.White);
                }

                else if (cue.type == CueType.Transition)
                {
                    Transition t = cue as Transition;
                    Comic begins_in = rect_begins_in(t.trans_rect);
                    Rectangle source = t.trans_rect;
                    source.X -= begins_in.x;

                    spriteBatch.Draw(begins_in.texture, scale_rect(source), source, Color.White);
                }

                else if (cue.type == CueType.Graphic)
                {
                    Graphic graphic = (Graphic)cue;
                    spriteBatch.Draw(graphic.texture, graphic.position, graphic.bound, Color.White);
                }

                else if (cue.type == CueType.Text)
                {
                    Text text = (Text)cue;

                    int y = (int)text.pos[0].Y;

                    spriteBatch.Draw(SubtitleLayover, new Rectangle(0, y, 1280, 720 - y), Color.Black);

                    for (int i = 0; i < text.lines.Count; i++)
                    {
                        string s = text.lines[i];
                        Vector2 pos = text.pos[i];

                        
                        spriteBatch.DrawString(Fonts.SubtitleFont, s, pos, Color.Orange);
                    }
                }

                if (cue.type == CueType.Fade)
                {
                    Fade f = cue as Fade;
                    spriteBatch.Draw(white_dot, new Rectangle(0, 0, 1280, 720), Color.Black * f.alpha_val);

                    //spriteBatch.Draw(white_dot, new Rectangle(0, 0, 400, 400), Color.Red);
                    //Console.WriteLine(f.alpha_val.ToString());
                }
            }
        }

        private bool not_between_pages(Rectangle rect)
        {
            //Checks if rect is between Comic objects. 
            foreach (Comic c in comics)
            {
                if (c.texture.Bounds.Contains(rect))
                    return true;

                rect.X -= c.texture.Width;
            }

            return false;
        }

        private Comic rect_begins_in(Rectangle rect)
        {
            //Figures out where the rect starts.
            foreach (Comic c in comics)
            {
                if (c.texture.Bounds.Contains(rect.X, rect.Y))
                    return c;

                rect.X -= c.texture.Width;
            }

            return null;
        }

        private Comic rect_begins_in(int x, int y)
        {
            //Figures out where the rect starts.
            foreach (Comic c in comics)
            {
                if (c.texture.Bounds.Contains(x, y))
                    return c;

                x -= c.texture.Width;
            }

            return null;
        }

        private Rectangle scale_rect(Rectangle draw_region)
        {
            //We want to scale the rectangle to fit into a 960x540
            //viewport
            float width_scale = 1280 / (float)draw_region.Width;
            float height_scale = 720 / (float)draw_region.Height;

            if (width_scale > height_scale)
            {
                //This means we need to increase both dimensions by height scale
                draw_region.Width = (int)((float)draw_region.Width * height_scale);
                draw_region.Height = (int)((float)draw_region.Height * height_scale);
            }
            else
            {
                //This means we need to increase both dimensions by width scale
                draw_region.Width = (int)((float)draw_region.Width * width_scale);
                draw_region.Height = (int)((float)draw_region.Height * width_scale);
            }

            //Alright that's great. Now we need to center the rectangle. Should be easy I think.

            draw_region.X = (1280 - draw_region.Width) / 2;
            draw_region.Y = (720 - draw_region.Height) / 2;

            return draw_region;
        }
    }
}

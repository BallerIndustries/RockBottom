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
    public class CutSceneState : BaseState, IState
    {
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private CutScene current_cutscene;
        private double time, prev_time;
        private Stopwatch stop_watch;
        private Texture2D subtitle_overlay;

        public CutSceneState(SpriteBatch spriteBatch, ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider);
            this.Content.RootDirectory = "Content";
            this.spriteBatch = spriteBatch;
            stop_watch = new Stopwatch();
        }

        public void Enter(CutScene current_cutscene)
        {
            Game1.current_state.Exit();
            Game1.current_state = this;

            //Initialise stuff
            this.current_cutscene = current_cutscene;
            this.current_cutscene.load_action();
            time = 0;
            prev_time = 0;
            stop_watch.Start();
            subtitle_overlay = Content.Load<Texture2D>("subtitle_overlay");
        }

        public void Exit()
        {
            stop_watch.Stop();
            Content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            //CutScene cs = game_state.current_cs;
            CutScene cs = current_cutscene;
            
            List<Cue> aso = cs.all_scene_objects;
            List<Cue> cdo = cs.currently_displayed;

            bool ending = false;

            time += stop_watch.ElapsedMilliseconds - prev_time;
            prev_time = stop_watch.ElapsedMilliseconds;

            //1. Look for Cues that can be added to currently displayed objects
            for (int i = 0; i < aso.Count; i++)
            {
                Cue cue = aso[i];
                if (time > cue.fire_at)
                {
                    cdo.Add(cue);
                    aso.Remove(cue);

                    cdo.Sort();

                    if (cue.type == CueType.End)
                        ending = true;
                }
            }

            //2. Remove expired Cues from currently displayed objects
            for (int i = 0; i < cdo.Count; i++)
            {
                Cue cue = cdo[i];
                if (time > cue.remove_at && cue.type != CueType.Sound)
                {
                    cdo.Remove(cue);
                }
            }

            //3. Look for the current comic and update any transitions
            foreach (Cue cue in cdo)
            {
                if (cue.type == CueType.Fade)
                {
                    Fade f = (Fade)cue;
                    f.Update(time);
                }

                if (cue.type == CueType.Transition)
                {
                    Transition t = cue as Transition;
                    float percentage = ((float)time - (float)t.fire_at) / ((float)t.remove_at - (float)t.fire_at);
                    t.calc_trans_rect(percentage);
                }
            }

            //4. Play sounds and then remove them
            for (int i = 0; i < cdo.Count; i++)
            {
                Cue cue = cdo[i];

                if (cue.type == CueType.Sound)
                {
                    Sound s = cue as Sound;
                    Globals.soundBank.PlayCue(s.name);
                    cdo.Remove(s);
                }
            }

            //5. Check if we have reached the end of the CutScene
            if (Controls.pressed_once(Keys.Enter) || Controls.pressed_once(Buttons.B) || ending)
            {
                Globals.StopEffects();

                Game1.current_state = cs.return_state;
                Game1.chat_state.dialogue_options = cs.dialogue_options;
                //game_state.state = cs.return_state;
                //game_state.dialogue_options = cs.dialogue_options;
                cdo.Clear();
                aso.Clear();
                stop_watch.Reset();
                prev_time = 0;
            }

            //6. Skip to the next panel or the next audio cue
            if ((Controls.pressed_once(Keys.Space) || Controls.pressed_once(Buttons.A) || Controls.clicked_once()) && cs.skippable)
            {
                ViewPanel vp = next_vp();

                if (vp != null)
                {
                    Globals.StopEffects();
                    time = vp.fire_at;
                }
                else
                {
                    Sound s = next_sound();
                    if (s != null)
                    {
                        Globals.StopEffects();
                        time = s.fire_at;
                    }
                    else
                    {
                        stop_watch.Reset();
                        prev_time = 0;
                        Globals.StopEffects();

                        Game1.current_state = cs.return_state;
                        Game1.chat_state.dialogue_options = cs.dialogue_options;

                        //game_state.state = cs.return_state;
                        //game_state.dialogue_options = cs.dialogue_options;
                        cdo.Clear();
                        aso.Clear();
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            current_cutscene.Draw(spriteBatch, Fonts.SubtitleFont, subtitle_overlay);
        }

        private Sound next_sound()
        {
            foreach (Cue c in current_cutscene.all_scene_objects)
            {
                Sound s = c as Sound;
                if (s != null)
                    return s;
            }

            return null;
        }

        private ViewPanel next_vp()
        {
            foreach (Cue c in current_cutscene.all_scene_objects)
            {
                ViewPanel vp = c as ViewPanel;
                if (vp != null)
                    return vp;
            }

            return null;
        }

        public void Enter()
        {
            //DON'T WRITE ANY CODE HERE YOU MOTHERFUCKER!!!!
        }
    }
}

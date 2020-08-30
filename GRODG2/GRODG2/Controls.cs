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
    public class Controls
    {
        private static GamePadState gpstate, prev_gpstate;
        private static KeyboardState kbstate, prev_kbstate;
        private static MouseState mousestate, prev_mousestate;
        public static PlayerIndex controlling_player;

        public static Cursor cursor;

        public Controls()
        {
        }

        public static void set_pc()
        {
#if WINDOWS
            prev_mousestate = mousestate;
            mousestate = Mouse.GetState();

            prev_kbstate = kbstate;
            kbstate = Keyboard.GetState();
#endif
        }

        public static void set_xbox()
        {
#if XBOX
            prev_gpstate = gpstate;
            gpstate = GamePad.GetState(controlling_player);
#endif
        }

        //These are convenience methods. They are a pain in the ass to write. Hopefully they will be
        //really useful. If they are not really useful I am going to throw a bottle of beer at Nick
        //Seedo's Honda Accord.

        public static bool pressed_once(Keys key)
        {
#if XBOX
            return false;
#else
            return kbstate.IsKeyDown(key) && prev_kbstate.IsKeyUp(key);
#endif 
        }

        public static bool pressed_once(Buttons button)
        {
#if XBOX
            return gpstate.IsButtonDown(button) && prev_gpstate.IsButtonUp(button);
#else
            return false;
#endif
        }

        public static bool pressed(Keys key)
        {
#if WINDOWS
            return kbstate.IsKeyDown(key);
#else
            return false;
#endif
        }

        public static bool pressed(Buttons button)
        {
#if XBOX
            return gpstate.IsButtonDown(button);
#else
            return false;
#endif
        }

        public static bool down_once()
        {
#if WINDOWS
            return pressed_once(Keys.Down);
#else
            return pressed_once(Buttons.DPadDown) || (gpstate.ThumbSticks.Left.Y < -0.5f && prev_gpstate.ThumbSticks.Left.Y >= -0.5f);
#endif
        }

        public static bool up_once()
        {
#if WINDOWS
            return pressed_once(Keys.Up);
#else
            return pressed_once(Buttons.DPadUp) || (gpstate.ThumbSticks.Left.Y > 0.5f && prev_gpstate.ThumbSticks.Left.Y <= 0.5f);
#endif
        }

        public static bool left_once()
        {
#if WINDOWS
            return pressed_once(Keys.Left);
#else
            return pressed_once(Buttons.DPadDown) || (gpstate.ThumbSticks.Left.X > 0.5f && prev_gpstate.ThumbSticks.Left.X <= 0.5f);
#endif            
        }

        public static bool right_once()
        {
#if WINDOWS
            return pressed_once(Keys.Right);
#else
            return pressed_once(Buttons.DPadDown) || (gpstate.ThumbSticks.Left.X < -0.5f && prev_gpstate.ThumbSticks.Left.X >= -0.5f);
#endif
        }

        public static bool up()
        {
#if WINDOWS
            return pressed(Keys.Up);
#else
            return pressed(Buttons.DPadUp) || gpstate.ThumbSticks.Left.Y > 0.5f;
#endif
        }

        public static bool down()
        {
#if WINDOWS
            return pressed(Keys.Down);
#else
            return pressed(Buttons.DPadDown) || gpstate.ThumbSticks.Left.Y < -0.5f;
#endif
        }

        public static bool left()
        {
#if WINDOWS
            return pressed(Keys.Left);
#else
            return pressed(Buttons.DPadLeft) || gpstate.ThumbSticks.Left.X < -0.5f;
#endif
        }

        public static bool right()
        {
#if WINDOWS
            return pressed(Keys.Right);
#else
            return pressed(Buttons.DPadRight) || gpstate.ThumbSticks.Left.X > 0.5f;
#endif
        }

        public static bool clicked_once()
        {
#if WINDOWS
            return mousestate.LeftButton == ButtonState.Pressed && prev_mousestate.LeftButton == ButtonState.Released; 
#else
            return false;
#endif
        }

        public static void set_cursor()
        {
#if WINDOWS
            cursor.position.X = mousestate.X;
            cursor.position.Y = mousestate.Y;
#else
            cursor.position.X += (int)(gpstate.ThumbSticks.Left.X * 10.0f);
            cursor.position.Y -= (int)(gpstate.ThumbSticks.Left.Y * 10.0f);
#endif
        }
    }
}

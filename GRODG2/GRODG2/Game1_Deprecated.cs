using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRODG2
{
    class Game1_Deprecated
    {

        //void update_menu(GameTime gameTime)
        //{
        //    if (pressed_once(Keys.Down))
        //        main_menu_data.move_down();
        //    else if (pressed_once(Keys.Up))
        //        main_menu_data.move_up();

        //    if (pressed_once(Keys.Enter))
        //    {
        //        switch (main_menu_data.selected_index)
        //        {
        //            case 0:
        //                game_state.current_cs = opening_scene;
        //                stop_watch.Start();
        //                time = 0;
        //                change_state(GameState.State.CutScene);
        //                break;

        //            case 1:
        //                game_state.state = GameState.State.About;
        //                break;

        //            case 2:
        //                this.Exit();
        //                break;
        //        }
        //    }
        //}

        //void draw_menu()
        //{
        //    Vector2 pos = new Vector2();

        //    pos.X = 100;

        //    for (int i = 0; i < main_menu_data.menu_text.Count; i++)
        //    {
        //        pos.Y += 100; 

        //        if (i == main_menu_data.selected_index)
        //            spriteBatch.DrawString(Fonts.MenuFont, main_menu_data.menu_text[i], pos, Color.Yellow);
        //        else
        //            spriteBatch.DrawString(Fonts.MenuFont, main_menu_data.menu_text[i], pos, Color.White);
        //    }
        //}

        //public void update_cutscene(GameTime gameTime)
        //{
        //    CutScene cs = game_state.current_cs;
        //    List<Cue> aso = cs.all_scene_objects;
        //    List<Cue> cdo = cs.currently_displayed;

        //    bool ending = false;

        //    time += stop_watch.ElapsedMilliseconds - prev_time;
        //    prev_time = stop_watch.ElapsedMilliseconds;

        //    //1. Look for Cues that can be added to currently displayed objects
        //    for (int i = 0; i < aso.Count; i++)
        //    {
        //        Cue cue = aso[i];
        //        if (time > cue.fire_at)
        //        {
        //            cdo.Add(cue);
        //            aso.Remove(cue);

        //            cdo.Sort();

        //            if (cue.type == CueType.End)
        //                ending = true;
        //        }
        //    }

        //    //2. Remove expired Cues from currently displayed objects
        //    for (int i = 0; i < cdo.Count; i++)
        //    {
        //        Cue cue = cdo[i];
        //        if (time > cue.remove_at && cue.type != CueType.Sound)
        //        {
        //            cdo.Remove(cue);
        //        }
        //    }

        //    //3. Look for the current comic and update any transitions
        //    foreach (Cue cue in cdo)
        //    {
        //        if (cue.type == CueType.Fade)
        //        {
        //            Fade f = (Fade)cue;
        //            f.Update(time);
        //        }

        //        if (cue.type == CueType.Transition)
        //        {
        //            Transition t = cue as Transition;
        //            float percentage = ((float)time - (float)t.fire_at) / ((float)t.remove_at - (float)t.fire_at);
        //            t.calc_trans_rect(percentage);
        //        }
        //    }

        //    //4. Play sounds and then remove them
        //    for (int i = 0; i < cdo.Count; i++)
        //    {
        //        Cue cue = cdo[i];

        //        if (cue.type == CueType.Sound)
        //        {
        //            Sound s = cue as Sound;
        //            soundBank.PlayCue(s.name);
        //            cdo.Remove(s);
        //        }


        //    }

        //    //5. Check if we have reached the end of the CutScene
        //    if (pressed_once(Keys.Enter) || pressed_once(Buttons.B) || ending)
        //    {
        //        StopEffects();
        //        //game_state.state = cs.return_state;
        //        //game_state.dialogue_options = cs.dialogue_options;
        //        cdo.Clear();
        //        aso.Clear();
        //        stop_watch.Reset();
        //        prev_time = 0;
        //    }

        //    //6. Skip to the next panel or the next audio cue
        //    if ((pressed_once(Keys.Space) || pressed_once(Buttons.A) || clicked_once()) && cs.skippable)
        //    {
        //        ViewPanel vp = next_vp();

        //        if (vp != null)
        //        {
        //            StopEffects();
        //            time = vp.fire_at;
        //        }
        //        else
        //        {
        //            Sound s = next_sound();
        //            if (s != null)
        //            {
        //                StopEffects();
        //                time = s.fire_at;
        //            }
        //            else
        //            {
        //                stop_watch.Reset();
        //                prev_time = 0;
        //                StopEffects();
        //                //game_state.state = cs.return_state;
        //                game_state.dialogue_options = cs.dialogue_options;
        //                cdo.Clear();
        //                aso.Clear();
        //            }
        //        }
        //    }
        //}

        //void draw_cutscene()
        //{
        //    game_state.current_cs.Draw(spriteBatch, SubtitleFont, subtitle_overlay);
        //    //spriteBatch.DrawString(SubtitleFont, time.ToString(), new Vector2(100, 100), Color.Orange);
        //}

        //void update_about(GameTime gameTime)
        //{
        //    if (pressed_once(Keys.Escape))
        //    {
        //        game_state.state = GameState.State.Menu;
        //    }
        //}

        //void draw_about()
        //{
        //    //spriteBatch.Draw(about, Vector2.Zero, Color.White);
        //}

        //void update_chat(GameTime gameTime)
        //{
        //    cursor.position.X = mouseState.X;
        //    cursor.position.Y = mouseState.Y;

        //    game_state.dialogue_options.Update(cursor.position);

        //    if (clicked_once())
        //    {
        //        CutScene next_scene = game_state.dialogue_options.get_highlighted_scene();
        //        if (next_scene != null)
        //        {
        //            game_state.current_cs = next_scene;
        //            stop_watch.Start();
        //            time = 0;
        //            change_state(GameState.State.CutScene);
        //        }
        //    }
        //}

        //void draw_chat()
        //{
        //    spriteBatch.Draw(game_state.current_room.texture, new Rectangle(0, 0, 1280, 720), new Rectangle(0, 0, 1280, 720), Color.White);

        //    foreach (Character c in game_state.current_room.characters)
        //    {
        //        if (c.visible)
        //        spriteBatch.Draw(c.texture, c.position, Color.White);
        //    }

        //    game_state.dialogue_options.Draw(spriteBatch);

        //    cursor.Draw(spriteBatch);
        //}

        //void update_gameplay(GameTime gameTime)
        //{
        //    cursor.position.X = mouseState.X;
        //    cursor.position.Y = mouseState.Y;

        //    //This is totally cray cray. We change the icon, if we hover over a hotspot.
        //    foreach (HotSpot hs in game_state.current_room.hotspots)
        //    {
        //        if (hs.region.Contains(cursor.position.X, cursor.position.Y) && hs.active)
        //        {
        //            if (hs.type == HotSpot.Type.room_change)
        //            {
        //                cursor.type = Cursor.Type.go;
        //                break;
        //            }
        //            else if (hs.type == HotSpot.Type.dialogue)
        //            {
        //                cursor.type = Cursor.Type.talk;
        //                break;
        //            }
        //            else if (hs.type == HotSpot.Type.exit)
        //            {
        //                cursor.type = Cursor.Type.exit;
        //                break;
        //            }
        //        }

        //        if (cursor.type != Cursor.Type.item)    
        //            cursor.type = Cursor.Type.grab;
        //    }

        //    if (pressed_once(Keys.I))
        //    {
        //        game_state.state = GameState.State.Inventory;
        //        cursor.type = Cursor.Type.grab;
        //    }

        //    //Click happened
        //    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
        //    {
        //        //Look through all hot spots in the current room
        //        foreach (HotSpot hs in game_state.current_room.hotspots)
        //        {
        //            if (cursor.position.Intersects(hs.region) && hs.active)
        //            {
        //                if (hs.type == HotSpot.Type.room_change && cursor.type == Cursor.Type.go)
        //                {
        //                    //Clicked on a room change hotspot. Better do something SIC
        //                    RoomChangeHS rcHS = (RoomChangeHS)hs;
        //                    game_state.current_room = rcHS.leads_to;
        //                    break;
        //                }
        //                else if (hs.type == HotSpot.Type.dialogue && cursor.type == Cursor.Type.talk)
        //                {
        //                    //Clicked on a dialogue hot spot. Better play a CutScene
        //                    DialogueHS dialogueHS = (DialogueHS)hs;

        //                    game_state.current_cs = dialogueHS.scene;
        //                    time = 0;
        //                    stop_watch.Start();
        //                    change_state(GameState.State.CutScene);
        //                }
        //                else if (hs.type == HotSpot.Type.exit && cursor.type == Cursor.Type.exit)
        //                {
        //                    //Clicked on a room change hotspot. Better do something SIC
        //                    ExitHS exitHS = (ExitHS)hs;
        //                    game_state.current_room = exitHS.leads_to;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //}

        //void draw_gameplay()
        //{
        //    spriteBatch.Draw(game_state.current_room.texture, new Rectangle(0, 0, 1280, 720), game_state.current_room.bound, Color.White);

        //    foreach (Character c in game_state.current_room.characters)
        //    {
        //        spriteBatch.Draw(c.texture, c.position, Color.White);
        //    }

        //    cursor.Draw(spriteBatch);
        //}

        //void update_inventory()
        //{
        //    cursor.position.X = mouseState.X;
        //    cursor.position.Y = mouseState.Y;

        //    if (pressed_once(Keys.I))
        //        game_state.state = GameState.State.GamePlay;

        //    inventory.Update(cursor.position.X, cursor.position.Y);

        //    if (pressed_once(Keys.Enter) || clicked_once())
        //    {
        //        game_state.state = GameState.State.GamePlay;

        //        if (inventory.selected_item != null)
        //        {
        //            cursor.item = inventory.selected_item;
        //            cursor.type = Cursor.Type.item;
        //        }
        //    }
        //}

        //void draw_inventory()
        //{
        //    spriteBatch.Draw(game_state.current_room.texture, new Rectangle(0, 0, 1280, 720), game_state.current_room.bound, Color.White);

        //    foreach (Character c in game_state.current_room.characters)
        //    {
        //        spriteBatch.Draw(c.texture, c.position, Color.White);
        //    }

        //    spriteBatch.Draw(white_dot, title_safe_rect, Color.White);
        //    drawRect(title_safe_rect, Color.Black);
        //    spriteBatch.DrawString(DialogueItemFont, "Items", new Vector2(150, 100), Color.Black);

        //    inventory.Draw(spriteBatch);
        //    if (inventory.selected_item != null)
        //        drawRect(new Rectangle((int)inventory.selected_item.position.X, (int)inventory.selected_item.position.Y, 100, 100), Color.Red);

        //    cursor.Draw(spriteBatch);
        //}


        //ViewPanel next_vp()
        //{
        //    foreach (Cue c in game_state.current_cs.all_scene_objects)
        //    {
        //        ViewPanel vp = c as ViewPanel;
        //        if (vp != null)
        //            return vp;
        //    }

        //    return null;
        //}

        //Sound next_sound()
        //{
        //    foreach (Cue c in game_state.current_cs.all_scene_objects)
        //    {
        //        Sound s = c as Sound;
        //        if (s != null)
        //            return s;
        //    }

        //    return null;
        //}


        //void change_state(GameState.State state)
        //{
        //    game_state.state = state;
        //}

        //public void StopEffects()
        //{
        //    AudioCategory c = audioEngine.GetCategory("Default");
        //    c.Stop(AudioStopOptions.AsAuthored);
        //}

        //public void drawRect(Rectangle rect, Color col)
        //{
        //    int x, y, w, h;
        //    x = rect.X;
        //    y = rect.Y;
        //    w = rect.Width;
        //    h = rect.Height;

        //    spriteBatch.Draw(white_dot, new Rectangle(x, y, 2, h), col);
        //    spriteBatch.Draw(white_dot, new Rectangle(x, y, w, 2), col);
        //    spriteBatch.Draw(white_dot, new Rectangle(x + w, y, 2, h), col);
        //    spriteBatch.Draw(white_dot, new Rectangle(x, y + h, w, 2), col);
        //}

    }
}

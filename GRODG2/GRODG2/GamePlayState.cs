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
    public class GamePlayState : BaseState, IState
    {
        private SpriteBatch spriteBatch;

        public GamePlayState(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Enter()
        {
            Game1.current_state.Exit();
            Game1.current_state = Game1.gameplay_state;
        }

        public void Exit()
        {
        }

        public void Update(GameTime gameTime)
        {
            //cursor.position.X = mouseState.X;
            //cursor.position.Y = mouseState.Y;
            Controls.set_cursor();

            //This is totally cray cray. We change the icon, if we hover over a hotspot.
            foreach (HotSpot hs in Globals.current_room.hotspots)
            {
                if (hs.region.Contains(Controls.cursor.position.X, Controls.cursor.position.Y) && hs.active)
                {
                    if (hs.type == HotSpot.Type.room_change)
                    {
                        Controls.cursor.type = Cursor.Type.go;
                        break;
                    }
                    else if (hs.type == HotSpot.Type.dialogue)
                    {
                        Controls.cursor.type = Cursor.Type.talk;
                        break;
                    }
                    else if (hs.type == HotSpot.Type.exit)
                    {
                        Controls.cursor.type = Cursor.Type.exit;
                        break;
                    }
                }

                if (Controls.cursor.type != Cursor.Type.item)
                    Controls.cursor.type = Cursor.Type.grab;
            }

            if (Controls.pressed_once(Keys.I) || Controls.pressed_once(Buttons.Y))
            {
                Game1.inventory_state.Enter();
                //game_state.state = GameState.State.Inventory;
                
                //cursor.type = Cursor.Type.grab; //Should be in InventoryState.Enter()
            }

            //Click happened
            if (Controls.clicked_once() || Controls.pressed_once(Buttons.A))
            {
                //Look through all hot spots in the current room
                foreach (HotSpot hs in Globals.current_room.hotspots)
                {
                    if (Controls.cursor.position.Intersects(hs.region) && hs.active)
                    {
                        if (hs.type == HotSpot.Type.room_change && Controls.cursor.type == Cursor.Type.go)
                        {
                            //Clicked on a room change hotspot. Better do something SIC
                            RoomChangeHS rcHS = (RoomChangeHS)hs;
                            Globals.current_room = rcHS.leads_to;
                            break;
                        }
                        else if (hs.type == HotSpot.Type.dialogue && Controls.cursor.type == Cursor.Type.talk)
                        {
                            //Clicked on a dialogue hot spot. Better play a CutScene
                            DialogueHS dialogueHS = (DialogueHS)hs;

                            Game1.cutscene_state.Enter(dialogueHS.scene);
                            //game_state.current_cs = dialogueHS.scene;
                            //time = 0;
                            //stop_watch.Start();
                            //change_state(GameState.State.CutScene);
                        }
                        else if (hs.type == HotSpot.Type.exit && Controls.cursor.type == Cursor.Type.exit)
                        {
                            //Clicked on a room change hotspot. Better do something SIC
                            ExitHS exitHS = (ExitHS)hs;
                            Globals.current_room = exitHS.leads_to;
                            break;
                        }
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Globals.current_room.texture, new Rectangle(0, 0, 1280, 720), Globals.current_room.bound, Color.White);

            foreach (Character c in Globals.current_room.characters)
            {
                spriteBatch.Draw(c.texture, c.position, Color.White);
            }

            Controls.cursor.Draw(spriteBatch);
        }
    }
}

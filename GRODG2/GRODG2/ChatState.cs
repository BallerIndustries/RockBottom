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
    public class ChatState : BaseState, IState
    {
        public DialogueOptions dialogue_options;
        public SpriteBatch spriteBatch;

        public ChatState(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Update(GameTime gameTime)
        {
            Controls.set_cursor();

            dialogue_options.Update(Controls.cursor.position);

            if (Controls.clicked_once() || Controls.pressed_once(Buttons.A))
            {
                CutScene next_scene = dialogue_options.get_highlighted_scene();
                if (next_scene != null)
                {
                    Game1.cutscene_state.Enter(next_scene);
                    //game_state.current_cs = next_scene;
                    //stop_watch.Start();
                    //time = 0;
                    //change_state(GameState.State.CutScene);
                }
            }

        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Globals.current_room.texture, new Rectangle(0, 0, 1280, 720), new Rectangle(0, 0, 1280, 720), Color.White);

            foreach (Character c in Globals.current_room.characters)
            {
                if (c.visible)
                    spriteBatch.Draw(c.texture, c.position, Color.White);
            }

            dialogue_options.Draw(spriteBatch);

            Controls.cursor.Draw(spriteBatch);
        }

    }
}

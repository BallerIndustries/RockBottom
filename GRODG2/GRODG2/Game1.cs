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
    /// <summary>
    /// SHUT UP YOU ASSHOLE!!!!
    /// </summary>
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Room cell, hallway, yard, laundry, toilet, library, ballroom, collar_poppa_room;
        Character sweepy, ben_seib, professor_x, collar_poppa, black_betty, guard, mc_hummus, red_square;
        DialogueHS mc_hummus_hs, professor_hs;
        WaveBank waveBank;
        string webaddress;
        Vector2 webaddress_pos;
        Fonts fonts;

        /////////////////////////////////////////////
        // ALL THE STATES BABY
        /////////////////////////////////////////////
        public static IState current_state;
        public static MenuState menu_state;
        public static AboutState about_state;
        public static CutSceneState cutscene_state;
        public static GamePlayState gameplay_state;
        public static ChatState chat_state;
        public static InventoryState inventory_state;
        public static StartPromptState start_prompt_state;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            Content.RootDirectory = "Content";
            Components.Add(new FrameRateCounter(this));
        }

        protected override void Initialize()
        {
            webaddress = "ballerindustries.blogspot.com";
            webaddress_pos = new Vector2();

            //game_state = new GameState(GameState.State.Menu);

            Globals.audioEngine = new AudioEngine("Content\\audio.xgs");
            waveBank = new WaveBank(Globals.audioEngine, "Content\\Wave Bank.xwb");
            Globals.soundBank = new SoundBank(Globals.audioEngine, "Content\\Sound Bank.xsb");

            Globals.tm = new TextureManager(Content);
            Controls.cursor = new Cursor();

            //Initialise rooms
            cell = new Room(Room.ID.cell);
            hallway = new Room(Room.ID.hallway);
            yard = new Room(Room.ID.yard);
            laundry = new Room(Room.ID.laundry);
            toilet = new Room(Room.ID.toilet);
            library = new Room(Room.ID.library);
            ballroom = new Room(Room.ID.ballroom);
            collar_poppa_room = new Room(Room.ID.collar_poppa);

            //Initialise characters
            sweepy = new Character(Character.Type.sweepy, new Point(340, 26));
            professor_x = new Character(Character.Type.professor_x, new Point(342, 22));
            ben_seib = new Character(Character.Type.ben_seib, new Point(366, 10));
            collar_poppa = new Character(Character.Type.collar_popper, new Point(400, 29));
            black_betty = new Character(Character.Type.black_betty, new Point(300, 40));
            guard = new Character(Character.Type.guard, new Point(325, 10));
            mc_hummus = new Character(Character.Type.mc_hummus, new Point(274, 6));
            red_square = new Character(Character.Type.red_square, new Point(507, 407));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            fonts = new Fonts(Content);
            Globals.title_safe_rect = GetTitleSafeArea(0.8f);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //THE INDEPENDANT STATES OF GRODG2
            menu_state = new MenuState(spriteBatch, this);
            about_state = new AboutState(Content, spriteBatch);
            cutscene_state = new CutSceneState(spriteBatch, Content);
            gameplay_state = new GamePlayState(spriteBatch);
            chat_state = new ChatState(spriteBatch);
            inventory_state = new InventoryState(Content, spriteBatch, Globals.title_safe_rect);
            start_prompt_state = new StartPromptState(spriteBatch);

            current_state = start_prompt_state;

            //Initialise cutscenes
            InitialiseDialogueOptions();
            InitialiseCutScenes();
            AddDialogueItems();

            webaddress_pos.X = Globals.title_safe_rect.Right - Fonts.SubtitleFont.MeasureString(webaddress).X;
            webaddress_pos.Y = Globals.title_safe_rect.Top;

            Globals.current_room = cell;

            collar_poppa_room.characters.Add(collar_poppa);
            collar_poppa_room.characters.Add(black_betty);

            cell.hotspots.Add(new RoomChangeHS(new Rectangle(527, 220, 205, 347), hallway));

            hallway.hotspots.Add(new RoomChangeHS(new Rectangle(958, 246, 232, 447), yard));
            hallway.hotspots.Add(new RoomChangeHS(new Rectangle(132, 310, 144, 318), cell));

            hallway.characters.Add(sweepy);
            hallway.hotspots.Add(new DialogueHS(new Rectangle(492, 22, 191, 220), sweepy_hello));

            yard.hotspots.Add(new RoomChangeHS(new Rectangle(213, 271, 57, 215), toilet));
            yard.hotspots.Add(new RoomChangeHS(new Rectangle(879, 293, 71, 215), laundry));
            yard.hotspots.Add(new RoomChangeHS(new Rectangle(990, 274, 119, 394), library));
            yard.characters.Add(red_square);
            yard.hotspots.Add(new DialogueHS(new Rectangle(507, 407, 208, 244), collar_hello));
            yard.hotspots.Add(new ExitHS(hallway));

            professor_hs = new DialogueHS(new Rectangle(513, 32, 272, 355), prof_hello);
            professor_hs.active = false;
            laundry.hotspots.Add(professor_hs);
            laundry.hotspots.Add(new ExitHS(yard));
            laundry.hotspots.Add(new RoomChangeHS(new Rectangle(0, 0, 404, 240), ballroom));

            mc_hummus_hs = new DialogueHS(new Rectangle(389, 17, 276, 335), mc_bad_romance);
            toilet.hotspots.Add(new ExitHS(yard));
            toilet.characters.Add(mc_hummus);
            toilet.hotspots.Add(mc_hummus_hs);

            library.hotspots.Add(new ExitHS(yard));
            library.hotspots.Add(new DialogueHS(new Rectangle(473, 14, 242, 276), bs_hello));
            library.characters.Add(ben_seib);

            ballroom.hotspots.Add(new ExitHS(laundry));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Controls.set_pc();
            Controls.set_xbox();

            current_state.Update(gameTime);
            base.Update(gameTime);
            
            Globals.audioEngine.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            current_state.Draw(gameTime);
            spriteBatch.DrawString(Fonts.SubtitleFont, webaddress, webaddress_pos, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected Rectangle GetTitleSafeArea(float percent)
        {
            Rectangle retval = new Rectangle(
                graphics.GraphicsDevice.Viewport.X,
                graphics.GraphicsDevice.Viewport.Y,
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height);

            float border = (1 - percent) / 2;
            retval.X = (int)(border * retval.Width);
            retval.Y = (int)(border * retval.Height);
            retval.Width = (int)(percent * retval.Width);
            retval.Height = (int)(percent * retval.Height);
            return retval;
        }
    }
}

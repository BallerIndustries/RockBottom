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

namespace GRODG2
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        public static CutScene sweepy_hello, sweepy_benseib, sweepy_tree, sweepy_yes, sweepy_no, sweepy_exit, sweepy_rumour,
            sweepy_rumour_a, sweepy_rumour_b, sweepy_rumour_c, sweepy_rumour_d, sweepy_rumour_e,
            bs_hello, bs_naked, bs_items, bs_exit, bs_quiet,
            prof_hello, prof_longstory, prof_no_story, prof_bitch, prof_cosplay, prof_halloween,
            prof_godzilla, prof_wizard, prof_guard, prof_murder, prof_revolutionary, prof_truth,
            prof_guilty, prof_innocent, prof_refuse,
            mc_bad_romance, mc_umbrella,
            collar_hello, collar_interrupt, collar_observe, collar_exit,

            opening_scene;
        DialogueOptions collar_chat1, sweepy_chat1, sweepy_yes_no, sweepy_chat2, bs_chat1, prof_bool, prof_reason, prof_costume, prof_crime, prof_guilty_question;

        void InitialiseCutScenes()
        {
            collar_hello = new CutScene(chat_state, Collar_Hello, collar_chat1);
            collar_interrupt = new CutScene(chat_state, Collar_Interrupt, collar_chat1);
            collar_observe = new CutScene(chat_state, Collar_Observe, collar_chat1);
            collar_exit = new CutScene(gameplay_state, Collar_Exit, collar_chat1);

            opening_scene = new CutScene(gameplay_state, Load_OpeningScene);
            sweepy_hello = new CutScene(chat_state, Sweppy_Hello, sweepy_chat1);
            sweepy_benseib = new CutScene(chat_state, Sweepy_BenSeib, sweepy_yes_no);
            sweepy_exit = new CutScene(gameplay_state, Sweepy_Exit, null);
            sweepy_tree = new CutScene(chat_state, Sweepy_Tree, sweepy_chat1);
            sweepy_yes = new CutScene(chat_state, Sweepy_Yes, sweepy_chat1);
            sweepy_no = new CutScene(chat_state, Sweepy_No, sweepy_chat1);
            sweepy_rumour = new CutScene(chat_state, Sweepy_RumourStart, sweepy_chat2);

            sweepy_rumour_a = new CutScene(chat_state, Sweepy_Rumour_A, sweepy_chat2);
            sweepy_rumour_b = new CutScene(chat_state, Sweepy_Rumour_B, sweepy_chat2);
            sweepy_rumour_c = new CutScene(chat_state, Sweepy_Rumour_C, sweepy_chat2);
            sweepy_rumour_d = new CutScene(chat_state, Sweepy_Rumour_D, sweepy_chat2);
            sweepy_rumour_e = new CutScene(chat_state, Sweepy_Rumour_E, sweepy_chat2);

            bs_hello = new CutScene(chat_state, BS_Hello, bs_chat1);
            bs_naked = new CutScene(chat_state, BS_Naked, bs_chat1);
            bs_items = new CutScene(chat_state, BS_Items, bs_chat1);
            bs_exit = new CutScene(gameplay_state, BS_Exit, bs_chat1);
            bs_quiet = new CutScene(chat_state, BS_Quiet, bs_chat1);

            mc_bad_romance = new CutScene(gameplay_state, MC_BadRomance, null);
            mc_umbrella = new CutScene(gameplay_state, MC_Umbrella, null);

            //These aren't linked correctly at all.
            prof_hello         = new CutScene(chat_state    , Prof_Hello, prof_bool);
            prof_longstory = new CutScene(gameplay_state, Prof_LongStory, null);
            prof_longstory.skippable = false;

            prof_no_story      = new CutScene(chat_state    , Prof_No_Story, prof_reason);
            prof_bitch = new CutScene(gameplay_state, Prof_Bitch, null);
            prof_cosplay = new CutScene(gameplay_state, Prof_Cosplay, null);
            prof_halloween     = new CutScene(chat_state, Prof_Halloween, prof_costume);
            prof_godzilla = new CutScene(gameplay_state, Prof_Godzilla, null);
            prof_wizard = new CutScene(gameplay_state, Prof_Wizard, null); 
            prof_guard         = new CutScene(chat_state    , Prof_Guard, prof_crime);
            prof_murder        = new CutScene(chat_state    , Prof_Murder, prof_guilty_question);
            prof_revolutionary = new CutScene(chat_state, Prof_Revolutionary, prof_guilty_question);
            prof_truth = new CutScene(chat_state, Prof_Truth, prof_guilty_question);
            prof_guilty        = new CutScene(gameplay_state, Prof_Guilty, null);
            prof_innocent      = new CutScene(gameplay_state, Prof_Innocent, null);
            prof_refuse        = new CutScene(gameplay_state, Prof_Refuse, null);
        }

        void InitialiseDialogueOptions()
        {
            sweepy_chat1 = new DialogueOptions();
            sweepy_chat2 = new DialogueOptions();
            sweepy_yes_no = new DialogueOptions();
            bs_chat1 = new DialogueOptions();

            prof_bool = new DialogueOptions();
            prof_reason = new DialogueOptions();
            prof_costume = new DialogueOptions();
            prof_crime = new DialogueOptions();
            prof_guilty_question = new DialogueOptions();

            collar_chat1 = new DialogueOptions();
        }

        void AddDialogueItems()
        {
            collar_chat1.add_dialogue_item(new DialogueItem("Interrupt", collar_interrupt, true));
            collar_chat1.add_dialogue_item(new DialogueItem("Observe", collar_observe, true));
            collar_chat1.add_dialogue_item(new DialogueItem("Exit", collar_exit, true));

            sweepy_chat1.add_dialogue_item(new DialogueItem("Ben Seib", sweepy_benseib, true));
            sweepy_chat1.add_dialogue_item(new DialogueItem("Rumours", sweepy_rumour, false));
            sweepy_chat1.add_dialogue_item(new DialogueItem("Tree", sweepy_tree, false));
            sweepy_chat1.add_dialogue_item(new DialogueItem("Exit", sweepy_exit, true));

            sweepy_yes_no.add_dialogue_item(new DialogueItem("Yes", sweepy_yes, true));
            sweepy_yes_no.add_dialogue_item(new DialogueItem("No", sweepy_no, true));

            sweepy_chat2.add_dialogue_item(new DialogueItem("Rumour 1", sweepy_rumour_a, true));
            sweepy_chat2.add_dialogue_item(new DialogueItem("Rumour 2", sweepy_rumour_b, false));
            sweepy_chat2.add_dialogue_item(new DialogueItem("Rumour 3", sweepy_rumour_c, false));
            sweepy_chat2.add_dialogue_item(new DialogueItem("Rumour 4", sweepy_rumour_d, false));
            sweepy_chat2.add_dialogue_item(new DialogueItem("Rumour 5", sweepy_rumour_e, false));
            sweepy_chat2.add_dialogue_item(new DialogueItem("Exit", sweepy_exit, true));

            bs_chat1.add_dialogue_item(new DialogueItem("Naked", bs_naked, true));
            bs_chat1.add_dialogue_item(new DialogueItem("Quiet", bs_quiet, false));
            bs_chat1.add_dialogue_item(new DialogueItem("Scroll", bs_items, false));
            bs_chat1.add_dialogue_item(new DialogueItem("Exit", bs_exit, true));

            prof_bool.add_dialogue_item(new DialogueItem("Yes", prof_longstory, true));
            prof_bool.add_dialogue_item(new DialogueItem("No", prof_no_story, true));

            prof_reason.add_dialogue_item(new DialogueItem("Bitch", prof_bitch, true));
            prof_reason.add_dialogue_item(new DialogueItem("Cosplay", prof_cosplay, true));
            prof_reason.add_dialogue_item(new DialogueItem("Halloween", prof_halloween, true));

            prof_costume.add_dialogue_item(new DialogueItem("Godzilla", prof_godzilla, true));
            prof_costume.add_dialogue_item(new DialogueItem("Guard", prof_guard, true));
            prof_costume.add_dialogue_item(new DialogueItem("Wizard", prof_wizard, true));

            prof_crime.add_dialogue_item(new DialogueItem("Murder", prof_murder, true));
            prof_crime.add_dialogue_item(new DialogueItem("Activist", prof_revolutionary, true));
            prof_crime.add_dialogue_item(new DialogueItem("Truth", prof_truth, true));

            prof_guilty_question.add_dialogue_item(new DialogueItem("Guilty", prof_guilty, true));
            prof_guilty_question.add_dialogue_item(new DialogueItem("Innocent", prof_innocent, true));
            prof_guilty_question.add_dialogue_item(new DialogueItem("Refuse", prof_refuse, true));
        }

        void Collar_Hello()
        {
            CutScene cs = collar_hello;

            //cs.add_cue(new Graphic(0, 100000, "derelict", 0, Vector2.Zero));
            cs.add_cue(new Graphic(0, 100000, "Characters//collar_poppa", 1, new Vector2(400, 29)));

            cs.add_cue(new Sound(0, "collar_hello_a"));
            cs.add_cue(new Sound(1200, "collar_hello_b"));

            cs.add_cue(new Text(0, 1200, "Hey guys." ));
            cs.add_cue(new Text(1200, 3000, "Silence that face hole brother!" ));
            cs.add_cue(new Text(3000, 5000, "Black Betty and Collar Poppa' be playing some serious" ));
            cs.add_cue(new Text(5000, 6000, "I spy." ));

            cs.add_cue(new EndCue(6000));

            //Code that fires
            Globals.current_room = collar_poppa_room;

            black_betty.visible = false;
            collar_poppa.visible = true;
        }


        void Collar_Interrupt()
        {
            CutScene cs = collar_interrupt;

            cs.add_cue(new Sound(0, "collar_interrupt_a"));
            cs.add_cue(new Sound(7600, "collar_interrupt_b"));
            cs.add_cue(new Sound(10100, "collar_interrupt_c"));
            cs.add_cue(new Sound(14300, "collar_interrupt_d"));

            cs.add_cue(new Sound(23500, "collar_interrupt_e"));
            cs.add_cue(new Sound(28200, "collar_interrupt_f"));
            cs.add_cue(new Sound(30300, "collar_interrupt_g"));
            cs.add_cue(new Sound(32900, "collar_interrupt_h"));
            cs.add_cue(new Sound(35200, "collar_interrupt_i"));
            cs.add_cue(new Sound(38200, "collar_interrupt_j"));
            cs.add_cue(new Sound(50800, "collar_interrupt_k"));
            cs.add_cue(new Sound(54000, "collar_interrupt_l"));
            cs.add_cue(new Sound(61100, "collar_interrupt_m"));
            cs.add_cue(new Sound(63700, "collar_interrupt_n"));
            cs.add_cue(new Sound(66500, "collar_interrupt_o"));
            cs.add_cue(new Sound(69900, "collar_interrupt_p"));
            cs.add_cue(new Sound(75400, "collar_interrupt_q"));
            cs.add_cue(new Sound(80200, "collar_interrupt_r"));
            cs.add_cue(new Sound(83100, "collar_interrupt_s"));

            cs.add_cue(new Graphic(0, 7600, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(7600, 10100, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(10100, 23500, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(23500, 28200, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(28200, 30300, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(30300, 32900, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(32900, 35200, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(35200, 38200, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(38200, 50800, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(50800, 54000, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(54000, 61100, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(61100, 63700, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(63700, 66500, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(66500, 69900, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(69900, 75400, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(75400, 80200, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(80200, 83100, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(83100, 100000, "Characters//black_betty", 1, new Vector2(300, 40)));

            //Code that fires
            black_betty.visible = true;
            collar_poppa.visible = false;


            //CP
            cs.add_cue(new Text(0, 1500, "I spy!" ));
            cs.add_cue(new Text(1500, 3000, "What's wrong with you two?" ));
            cs.add_cue(new Text(3000, 7600, "You're acting like a bunch of baby butterflies with rainbow colored wangs." ));

            //BB
            cs.add_cue(new Text(7600, 10100, "Do you hate life or something little man?" ));
            
            //CP
            cs.add_cue(new Text(10100, 14300, "Look man, My wang ain't rainbow colored almost ever." ));
            
            cs.add_cue(new Text(14300, 18100, "All I'm saying is you guys are the two biggest prisoners here." ));
            cs.add_cue(new Text(18100, 20200, "Why the hell don't you act more tough?" ));
            cs.add_cue(new Text(20200, 23500, "Like you know, talk about shanking and women and shanking." ));

            //BB
            cs.add_cue(new Text(23500, 25100, "Wilson, you've got a point there." ));
            cs.add_cue(new Text(25100, 28200, "Colla Poppa, from now on we ain't playin' I Spy." ));

            //CP
            cs.add_cue(new Text(28200, 30300, "Hey, but I love spying" ));

            //BB
            cs.add_cue(new Text(30300, 32900, "From now on, we play 20 Questions." ));

            //CP
            cs.add_cue(new Text(32900, 35200, "C'mon Betty! I hate asking questions." ));

            //BB
            cs.add_cue(new Text(35200, 38200, "Alright Poppa, I've got one, ask your question." ));

            //CP
            cs.add_cue(new Text(38200, 47500, "[Thinking noises]" ));
            cs.add_cue(new Text(47500, 50800, "Ok I got it now. Is it a person?" ));

            //BB
            cs.add_cue(new Text(50800, 54000, "Yep! That's right CP, damn you is on fire." ));

            //CP
            cs.add_cue(new Text(54000, 56900, "[Thinking noises]" ));
            cs.add_cue(new Text(56900, 61100, "This is hard! Ok, I got one. Is it a man-person?"));

            //BB
            cs.add_cue(new Text(61100, 63700, "Uhuh CP, Uhuh."));

            //CP
            cs.add_cue(new Text(63700, 66500, "Alright alright. Does he like ice-cream?"));

            //BB
            cs.add_cue(new Text(66500, 69900, "Um what? I guess. Yes?"));

            //CP
            cs.add_cue(new Text(69900, 75400, "Right right right, ok. Does he like strawberry ice cream?"));

            //BB
            cs.add_cue(new Text(75400, 80200, "What kinda stupid fucking question is that? I don't know, I guess probably."));

            //CP
            cs.add_cue(new Text(80200, 83100, "And does he like listening to jazz?"));

            //BB
            cs.add_cue(new Text(83100, 84400, "Oh fuck this."));


            cs.add_cue(new EndCue(84400));
        }

        void Collar_Observe()
        {
            CutScene cs = collar_observe;

            cs.add_cue(new Sound(0, "collar_observe_a"));
            cs.add_cue(new Sound(3400, "collar_observe_b"));
            cs.add_cue(new Sound(6800, "collar_observe_c"));
            cs.add_cue(new Sound(7600, "collar_observe_d"));
            cs.add_cue(new Sound(11800, "collar_observe_e"));
            cs.add_cue(new Sound(14200, "collar_observe_f"));
            cs.add_cue(new Sound(16100, "collar_observe_g"));
            cs.add_cue(new Sound(18400, "collar_observe_h"));
            cs.add_cue(new Sound(20800, "collar_observe_i"));
            cs.add_cue(new Sound(23700, "collar_observe_j"));
            cs.add_cue(new Sound(29000, "collar_observe_k"));
            cs.add_cue(new Sound(35300, "collar_observe_l"));
            cs.add_cue(new Sound(38500, "collar_observe_m"));
            cs.add_cue(new Sound(41300, "collar_observe_n"));
            cs.add_cue(new Sound(53200, "collar_observe_o"));
            cs.add_cue(new Sound(57100, "collar_observe_p"));
            cs.add_cue(new Sound(67100, "collar_observe_q"));
            cs.add_cue(new Sound(77900, "collar_observe_r"));
            cs.add_cue(new Sound(79300, "collar_observe_s"));
            cs.add_cue(new Sound(84200, "collar_observe_t"));

            cs.add_cue(new Graphic(0, 3400, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(3400, 6800, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(6800, 7600, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(7600, 11800, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(11800, 14200, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(14200, 16100, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(16100, 18400, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(18400, 20800, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(20800, 23700, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(23700, 29000, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(29000, 35300, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(35300, 38500, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(38500, 41300, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(41300, 53200, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(53200, 57100, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(57100, 67100, "Characters//collar_poppa", 1, new Vector2(400, 29)));
            cs.add_cue(new Graphic(67100, 77900, "Characters//guard", 1, new Vector2(356, 10)));
            cs.add_cue(new Graphic(77900, 79300, "Characters//black_betty", 1, new Vector2(300, 40)));
            cs.add_cue(new Graphic(79300, 84200, "Characters//guard", 1, new Vector2(356, 10)));
            cs.add_cue(new Graphic(84200, 100000, "Characters//black_betty", 1, new Vector2(300, 40)));

            black_betty.visible = true;
            collar_poppa.visible = false;
            

            //BB
            cs.add_cue(new Text(0, 1800, "I spy with my little eye"));
            cs.add_cue(new Text(1800, 3400, "something beginning with C"));

            //CP
            cs.add_cue(new Text(3400, 5600, "Oh shit yeah!"));
            cs.add_cue(new Text(5600, 6800, "We talking ceiling."));

            //BB
            cs.add_cue(new Text(6800, 7600, "Nope we ain't"));

            //CP
            cs.add_cue(new Text(7600, 9800, "Mother fucker I can't hack this shit."));
            cs.add_cue(new Text(9800, 11800, "I be totally conceding defeat."));

            //BB
            cs.add_cue(new Text(11800, 14200, "Oh come on Poppa you just guessed once"));

            //CP
            cs.add_cue(new Text(14200, 16100, "Man this shit be too hard."));

            //BB
            cs.add_cue(new Text(16100, 18400, "Come on. You just hate guessing and like spying"));

            //CP
            cs.add_cue(new Text(18400, 20800, "Alright but I be needing a clue fool."));

            //BB
            cs.add_cue(new Text(20800, 23700, "Ok. Here's a clue. You get to see it everyday."));

            //CP
            cs.add_cue(new Text(23700, 25900, "Damn son, that don't mean nothing to me."));
            cs.add_cue(new Text(25900, 29000, "It's like you're being all deliberately cryptic and shit."));

            //BB
            cs.add_cue(new Text(29000, 30600, "Ok. Another clue."));
            cs.add_cue(new Text(30600, 35300, "Ain't no woman in here that has one. And it's what you think about all the time. No matter what."));

            //CP
            cs.add_cue(new Text(35300, 38500, "Damn son, I still don't know."));

            //BB
            cs.add_cue(new Text(38500, 41300, "Ok. It rhymes with something you've got on right now."));

            //CP
            cs.add_cue(new Text(41300, 43800, "Oh snap, but I ain't wearing shit."));
            cs.add_cue(new Text(43800, 47200, "Just these fly foot glooves, and my leg pants"));
            cs.add_cue(new Text(47200, 50300, "and on my feet I got on,"));
            cs.add_cue(new Text(50300, 53200, "oh snap, does the answer rhyme with sock?"));

            //BB
            cs.add_cue(new Text(53200, 55500, "Man you are so fucking stupid."));
            cs.add_cue(new Text(55500, 57100, "That doesn't even begin with the letter C."));
            
            //CP
            cs.add_cue(new Text(57100, 61600, "Woah man no I mean like a clue son."));
            cs.add_cue(new Text(61600, 64600, "The one you said it be rhyming with."));
            cs.add_cue(new Text(64600, 67100, "Rhyming holmes. With sock."));
            
            //GUARD
            cs.add_cue(new Text(67100, 68600, "What the hell is going on here?"));
            cs.add_cue(new Text(68600, 71200, "Are you two having fun? 'cause you sure look happy."));
            cs.add_cue(new Text(71200, 73700, "What do we fucking tell you about having fun in here?"));
            cs.add_cue(new Text(73700, 75100, "Look at the fucking sign."));
            cs.add_cue(new Text(75100, 77900, "No fun allowed ever."));
            
            //BB
            cs.add_cue(new Text(77900, 79300, "Yes sir, sorry sir."));
            
            //GUARD
            cs.add_cue(new Text(79300, 81500, "Don't let me catch you guys having a good time again."));
            cs.add_cue(new Text(81500, 84200, "Or I will go Guantanamo on your asses."));
            
            //BB
            cs.add_cue(new Text(84200, 85700, "Yes sir."));
            cs.add_cue(new Text(85700, 88000, "The fucking clue was gel man,"));
            cs.add_cue(new Text(88000, 89500, "and the answer was cell."));

            cs.add_cue(new EndCue(89500));
        }

        void Collar_Exit()
        {
            CutScene cs = collar_exit;

            //Change rooms
            Globals.current_room = yard;

            cs.add_cue(new EndCue(100));
        }

        void Prof_Hello()
        {
            CutScene cs = prof_hello;

            cs.add_cue(new Sound(0, "profx_hello_a"));
            cs.add_cue(new Sound(1800, "profx_hello_b"));
            cs.add_cue(new Sound(8400, "profx_hello_c"));
            cs.add_cue(new Sound(14000, "profx_hello_d"));
            cs.add_cue(new Sound(22800, "profx_hello_e"));
            cs.add_cue(new Sound(26100, "profx_hello_f"));
            cs.add_cue(new Sound(28900, "profx_hello_g"));
            cs.add_cue(new Sound(32400, "profx_hello_h"));
            cs.add_cue(new Sound(45700, "profx_hello_i"));
            cs.add_cue(new Sound(49200, "profx_hello_j"));

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            //cs.add_cue(new Graphic(0, 100000, "laundry", 0, Vector2.Zero));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Text(0   , 1800, "Hi there."));
            cs.add_cue(new Text(1800, 4800, "I am Professor X"));
            cs.add_cue(new Text(4800, 8400, "nice to meet you."));
            cs.add_cue(new Text(8400, 9900, "So, I heard that"));
            cs.add_cue(new Text(9900, 12300, "if a man's looking to get some textile work done"));
            cs.add_cue(new Text(12300, 14000, "then you're the guy to talk to."));
            cs.add_cue(new Text(14000, 17100, "Maybe I am the man with the needles"));
            cs.add_cue(new Text(17100, 20700, "who can produce the clothing"));
            cs.add_cue(new Text(20700, 22800, "if you know what I mean."));
            cs.add_cue(new Text(22800, 26100, "I'd like to think I know exactly what you are saying."));
            cs.add_cue(new Text(26100, 28900, "Now there lies a tale."));
            cs.add_cue(new Text(28900, 30300, "Wait, what?"));
            cs.add_cue(new Text(30300, 32400, "What do you mean?"));
            cs.add_cue(new Text(32400, 34100, "Aren't you curious"));
            cs.add_cue(new Text(34100, 38000, "how a hard man like myself"));
            cs.add_cue(new Text(38000, 41500, "developed such a, how shall I put it."));
            cs.add_cue(new Text(41500, 45700, "Skill at tailoring?"));
            cs.add_cue(new Text(45700, 49200, "Not even a little."));
            cs.add_cue(new Text(49200, 53100, "Let me tell you how I came to be little child."));
            cs.add_cue(new Text(53100, 56300, "Let me tell you about the story"));
            cs.add_cue(new Text(56300, 57300, "of"));	
	        cs.add_cue(new Text(57300, 59100, "The Sewing Man."));

            cs.add_cue(new EndCue(59100));
        }

        void Prof_LongStory()
        {
            CutScene cs = prof_longstory;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            //cs.add_cue(new Graphic(0, 1000000, "laundry", 0, Vector2.Zero));
            cs.add_cue(new Graphic(0, 1000000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_rant_a"));
            cs.add_cue(new Sound(60000, "profx_rant_b"));
            cs.add_cue(new Sound(120000, "profx_rant_c"));
            cs.add_cue(new Sound(180000, "profx_rant_d"));
            cs.add_cue(new Sound(240000, "profx_rant_e"));

            cs.add_cue(new Text(1000, 4300, "Alright."));
            cs.add_cue(new Text(4300, 12600, "Once upon a time in a place far far away there was a war."));
            cs.add_cue(new Text(12600, 16100, "I wasn't always known as the sewing man."));
            cs.add_cue(new Text(16100, 19100, "They used to just call me Chucky."));
            cs.add_cue(new Text(19100, 24600, "Well Chucky was just a boy, a boy with hair,"));
            cs.add_cue(new Text(24600, 34100, "a boy with hair and a gun and a God damn need to kill him some Russians."));
            cs.add_cue(new Text(34100, 40000, "See during the Cold War there was a little bit of a battle you see."));
            cs.add_cue(new Text(40000, 44400, "Don't bother looking it up in Wikipedia"));
            cs.add_cue(new Text(44400, 49200, "or in the interwebs or any of your cheating damn history books."));
            cs.add_cue(new Text(49200, 54600, "Because the Alaskans rubbed that shit right out."));
            cs.add_cue(new Text(54600, 62100, "'Cause history as you know, or you should is written by the victors."));
            cs.add_cue(new Text(62100, 66200, "So there I was, knee deep in snow."));
            cs.add_cue(new Text(66200, 69400, "Polar bear carcasses all around me,"));
            cs.add_cue(new Text(69400, 76300, "the hot steaming blood of penguins dripping from my finger tips."));
            cs.add_cue(new Text(76300, 79400, "I was out of ammo and water,"));
            cs.add_cue(new Text(79400, 84000, "and if you're out of ammo and water in the Arctic waste,"));

            cs.add_cue(new Text(84000, 87400, "you may as well be dead."));

            cs.add_cue(new Text(87400, 90600, "But I didn't give up, because I noticed something."));
            cs.add_cue(new Text(90600, 93800, "I noticed summer was right around the corner."));
            cs.add_cue(new Text(93800, 99400, "And if I could just hold out for four more days,"));
            cs.add_cue(new Text(99400, 103600, "then the heat would melt the whole damn country"));
            cs.add_cue(new Text(103600, 108400, "and I could live of slushies 'til the end of my days."));
            cs.add_cue(new Text(108400, 110800, "So you know what I did?"));


            cs.add_cue(new Text(110800, 117500, "I took the needle dick needle of the nearest enemy combatant."));
            cs.add_cue(new Text(117500, 123300, "And I took the polar bear carcases that laid strewn around me"));
            cs.add_cue(new Text(123300, 128400, "and I put two and two together, and I made myself a tent."));
            cs.add_cue(new Text(128400, 133900, "A tent made of nothing but polar bear and guns."));
            cs.add_cue(new Text(133900, 138900, "And I waited for four days for summer to arrive."));
            cs.add_cue(new Text(138900, 143400, "With nothing to eat except my rations."));
            cs.add_cue(new Text(143400, 145900, "And then, one day,"));
            cs.add_cue(new Text(145900, 149800, "four days later the sun arrived."));

            cs.add_cue(new Text(149800, 153800, "It was the most beautiful thing I ever saw."));
            cs.add_cue(new Text(153800, 159800, "But unfortunately because I looked at it, I now have to wear glasses."));
            cs.add_cue(new Text(159800, 164100, "Damn you Russian scum for taking my eyes!"));
            cs.add_cue(new Text(164100, 171100, "Anyway, I drank more wet snow than you'd ever imagine."));
            cs.add_cue(new Text(171100, 175200, "And as I did I got strong and stronger"));
            cs.add_cue(new Text(175200, 182400, "until I saw the biggest hairiest whitest polar bear of them all."));

            cs.add_cue(new Text(182400, 188100, "The Ruskies had been training him up for seventy five years."));
            cs.add_cue(new Text(188100, 191900, "He walked right up to me. Claws out stretched."));
            cs.add_cue(new Text(191900, 196100, "And he said I don't want to fight you."));

            cs.add_cue(new Text(196100, 199400, "Your destiny is not to die today."));
            cs.add_cue(new Text(199400, 200400, "No."));
            
            cs.add_cue(new Text(200400, 208900, "Your destiny is to become the greatest prison tailor there ever was."));
            cs.add_cue(new Text(208900, 210900, "And then he told me something weird."));
            cs.add_cue(new Text(210900, 214300, "He said one day sewing man."));
            cs.add_cue(new Text(214300, 217000, "And that's when I learned that would be my name."));
            
            
            cs.add_cue(new Text(217000, 222500, "He said One day sewing man, you will meet a child."));

            cs.add_cue(new Text(222500, 228700, "A child born of a father who did not like butterfly games."));
            cs.add_cue(new Text(228700, 233100, "A child who will one day save the world."));
            cs.add_cue(new Text(233100, 238700, "Or instead, become filthy fucking rich."));
            cs.add_cue(new Text(238700, 242300, "Or instead, will die."));
            cs.add_cue(new Text(242300, 246300, "And then he took off his suit and said"));

            cs.add_cue(new Text(246300, 255100, "This is for you, let you never forget that a polar bear told you to become a tailor."));
            cs.add_cue(new Text(255100, 260400, "And as they say young boy. The rest is history."));

            cs.add_cue(new EndCue(260400));
        }

        void Prof_No_Story()
        {
            CutScene cs = prof_no_story;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            //cs.add_cue(new Graphic(0, 100000, "laundry", 0, Vector2.Zero));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

	        cs.add_cue(new Sound(0, "profx_no_story_a"));
	        cs.add_cue(new Sound(5500, "profx_no_story_b"));
	        cs.add_cue(new Sound(10600, "profx_no_story_c"));
	        cs.add_cue(new Sound(13100, "profx_no_story_d"));
	        cs.add_cue(new Sound(20100, "profx_no_story_e"));
	        cs.add_cue(new Sound(21800, "profx_no_story_f"));
	        cs.add_cue(new Sound(32100, "profx_no_story_g"));
	        cs.add_cue(new Sound(38800, "profx_no_story_h"));

            cs.add_cue(new Text(0, 1800, "Godammit no"));
            cs.add_cue(new Text(1800, 5500, "I haven't got time to listen to another one of you lunatics rant."));
            cs.add_cue(new Text(5500, 7500, "Alright copper top"));
            cs.add_cue(new Text(7500, 10600, "why are you bothering me then?"));
            cs.add_cue(new Text(10600, 13100, "I'm looking for a costume to get made."));
            cs.add_cue(new Text(13100, 16800, "Do I look like a fucking wage slave, bitch?"));
            cs.add_cue(new Text(16800, 20100, "I don't make costumes."));
            cs.add_cue(new Text(20100, 21800, "But I heard"));
            cs.add_cue(new Text(21800, 24500, "I produce custumes!"));
            cs.add_cue(new Text(24500, 28600, "Custom made for discerning customers"));
            cs.add_cue(new Text(28600, 32100, "custard included."));
            cs.add_cue(new Text(32100, 34700, "Ok, I'd like a"));
            cs.add_cue(new Text(34700, 37800, "Custume. But could you leave the"));
            cs.add_cue(new Text(37800, 38800, "custard seperate?"));
            cs.add_cue(new Text(38800, 41000, "CUSTOLUTELY!"));
            cs.add_cue(new Text(41000, 46800, "Let me just get your measurements and then we'll"));
            cs.add_cue(new Text(46800, 48400, "Hey!"));
            cs.add_cue(new Text(48400, 51600, "Wait a minute."));
            cs.add_cue(new Text(51600, 55400, "Why would a prisoner such as yourself"));
	        cs.add_cue(new Text(55400, 59400, "require his own custume?"));

            cs.add_cue(new EndCue(59400));
        }

        void Prof_Bitch()
        {
            CutScene cs = prof_bitch;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            cs.add_cue(new Sound(0, "profx_bitch_a"));
	        cs.add_cue(new Sound(5791, "profx_bitch_b"));
		
	        cs.add_cue(new Text(0, 2603, "I got a job interview down in G block"));
	        cs.add_cue(new Text(2603, 5791, "a position has opened up in the prison bitch department."));
	        cs.add_cue(new Text(6139, 8751, "Very clever little child."));
	        cs.add_cue(new Text(8751, 11931, "But the professor was not born yesterday."));
	        cs.add_cue(new Text(11931, 13666, "BURR!"));
            cs.add_cue(new Text(13666, 16681, "I see through your plan."));
			
	        cs.add_cue(new EndCue(16681));

            //cs.add_cue(new Text(0, 1000, " I got a job interview down in G block,"));
            //cs.add_cue(new Text(1000, 2000, "a position has opened up in the prison bitch department."));
            //cs.add_cue(new Text(2000, 3000, "You know they don't call me professor X for nothing."));
            //cs.add_cue(new Text(3000, 4000, "I will not be hoodwinked by the likes of you."));
            //cs.add_cue(new Text(4000, 5000, "Begone."));

            //cs.add_cue(new EndCue(5000));
        }

        void Prof_Cosplay()
        {
            CutScene cs = prof_cosplay;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));


            cs.add_cue(new Sound(0, "profx_cosplay_a"));
            cs.add_cue(new Sound(7328, "profx_cosplay_b"));
		
	        cs.add_cue(new Text(0, 1446, "Didn't you hear Professor,"));
	        cs.add_cue(new Text(1446, 3432, "but there is a cosplay party going down, and I need"));
            cs.add_cue(new Text(3432, 7328, "a sick good costume to impress the role playing game chicks."));
	        cs.add_cue(new Text(7328, 10292, "Very clever little child."));
	        cs.add_cue(new Text(10292, 13677, "But the professor was not born yesterday."));
	        cs.add_cue(new Text(13677, 15313, "BURR!"));
	        cs.add_cue(new Text(15313, 18468, "I see through your plan."));
			
	        cs.add_cue(new EndCue(18468));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            //cs.add_cue(new Text(0, 1000, "Didn't you hear Professor,"));
            //cs.add_cue(new Text(1000, 2000, "but there is a cosplay party going down,"));
            //cs.add_cue(new Text(2000, 3000, "and I need a sick goot costume to impress the RPG chicks."));
            //cs.add_cue(new Text(3000, 4000, "You know they don't call me professor X for nothing."));
            //cs.add_cue(new Text(4000, 5000, "I will not be hoodwinked by the likes of you."));
            //cs.add_cue(new Text(5000, 6000, "Begone."));

            //cs.add_cue(new EndCue(6000));
        }

        void Prof_Halloween()
        {
            CutScene cs = prof_halloween;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_halloween_a"));
            cs.add_cue(new Sound(3800, "profx_halloween_b"));

            cs.add_cue(new Text(0, 1619, "It's for haloween, professor"));
            cs.add_cue(new Text(1619, 3800, "obviously."));
            cs.add_cue(new Text(3800, 6700, "Halloween?"));
            cs.add_cue(new Text(6700, 10100, "Alright, makes sense."));
            cs.add_cue(new Text(10100, 14000, "What kind of costume are you looking for?"));
	        cs.add_cue(new Text(14000, 19200, "Obviously I can't make anything too suspicious."));

            cs.add_cue(new EndCue(19200));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            //cs.add_cue(new Text(0, 1000, "It's for Halloween professor,"));
            //cs.add_cue(new Text(1000, 2000, "obviously."));
            //cs.add_cue(new Text(2000, 3000, "HALLOWEEN?"));
            //cs.add_cue(new Text(3000, 4000, "Alright makes sense."));
            //cs.add_cue(new Text(4000, 5000, "What kind of custume are you looking for?"));
            //cs.add_cue(new Text(5000, 6000, "Obviously I can't make anything too suspicious."));

            //cs.add_cue(new EndCue(6000));
        }

        void Prof_Godzilla()
        {
            CutScene cs = prof_godzilla;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_godzilla_a"));
            cs.add_cue(new Sound(3568, "profx_godzilla_b"));
		
	        cs.add_cue(new Text(0, 3000, "I'd like you to make me a Godzilla custume."));
	        cs.add_cue(new Text(3568, 5372, "I can't make that!"));
	        cs.add_cue(new Text(5372, 9197, "You ridiculous, diminutive man."));
	        cs.add_cue(new Text(9197, 11570, "What kind of operation you think"));
	        cs.add_cue(new Text(11570, 13346, "I'm running here?"));
	        cs.add_cue(new Text(13346, 16223,"This ain't no red light district."));
	        cs.add_cue(new Text(16223, 18967, "I operate a freakin' business!"));
				
	        cs.add_cue(new EndCue(18967));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            //cs.add_cue(new Text(0, 1000, "I'd like you to make me a Godzilla custume."));
            //cs.add_cue(new Text(1000, 2000, "I can't make that!"));
            //cs.add_cue(new Text(2000, 3000, "You ridiculous, diminutive man."));
            //cs.add_cue(new Text(3000, 4000, "What kinda operation you think I'm running here?"));
            //cs.add_cue(new Text(4000, 5000, "This ain't no red light district."));
            //cs.add_cue(new Text(5000, 6000, "I operate a freakin' bidness."));

            //cs.add_cue(new EndCue(6000));
        }

        void Prof_Wizard()
        {
            CutScene cs = prof_wizard;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            cs.add_cue(new Sound(0, "profx_wizard_a"));
            cs.add_cue(new Sound(3568, "profx_godzilla_b"));

            cs.add_cue(new Text(0, 3000, "All I need is a robe and a wizard's hat."));
            cs.add_cue(new Text(3568, 5372, "I can't make that!"));
            cs.add_cue(new Text(5372, 9197, "You ridiculous, diminutive man."));
            cs.add_cue(new Text(9197, 11570, "What kind of operation you think"));
            cs.add_cue(new Text(11570, 13346, "I'm running here?"));
            cs.add_cue(new Text(13346, 16223, "This ain't no red light district."));
            cs.add_cue(new Text(16223, 18967, "I operate a freakin' business!"));

            cs.add_cue(new EndCue(18967));
        }

        void Prof_Guard()
        {
            CutScene cs = prof_guard;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_guard_a"));
            cs.add_cue(new Sound(7066, "profx_guard_b"));

            cs.add_cue(new Text(0, 2600, "How about a custume that is exactly,"));
            cs.add_cue(new Text(2600, 6234, "100% identical to a guard's uniform?"));
            cs.add_cue(new Text(7066, 11373, "Well now, let me see."));
	        cs.add_cue(new Text(11376, 16039, "I can make you a guard's uniform so accurate,"));
	        cs.add_cue(new Text(16039, 22294, "you could beat me with it afterwards and I would just cry."));
	        cs.add_cue(new Text(22294, 24555, "But hold on now."));
	        cs.add_cue(new Text(24555, 27862, "I got one more question."));
	        cs.add_cue(new Text(27862, 30265, "Why you in prison anyways?"));
				
	        cs.add_cue(new EndCue(30265));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            //cs.add_cue(new Text(0, 1000, "How about a custume that is exactly,"));
            //cs.add_cue(new Text(1000, 2000, "100% identical to a guard's uniform."));
            //cs.add_cue(new Text(2000, 3000, "Yeah I can make you a guards uniform so accurate, "));
            //cs.add_cue(new Text(3000, 4000, "you could beat me with it afterwards and I would just cry."));
            //cs.add_cue(new Text(4000, 5000, "But hold on now, I got one more question."));
            //cs.add_cue(new Text(5000, 6000, "Why you in prison anyway?"));

            //cs.add_cue(new EndCue(6000));
        }

        void Prof_Murder()
        {
            CutScene cs = prof_murder;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_murder_a"));
	        cs.add_cue(new Sound(15333, "profx_murder_b"));
			
	        cs.add_cue(new Text(0, 2650, "I axe murdered my through half of Europe"));
	        cs.add_cue(new Text(2650, 5422, "stopping only for coffee and grindin'."));
	        cs.add_cue(new Text(5422, 9398, "My axe got siezed in Amsterdam, right after I had an epiphany of about murdering."));
	        cs.add_cue(new Text(11116, 14000, "Like how it was mean and stuff."));
	        cs.add_cue(new Text(15514, 21478, "You sick, unpatriotic fool."));
	        cs.add_cue(new Text(21478, 26328, "I don't think I can make custumes for the likes of you."));
	        cs.add_cue(new Text(26328, 30004, "Maybe later on when I've calmed down"));
	        cs.add_cue(new Text(30004, 33513, "But for now, I got to mope."));
	
	        cs.add_cue(new EndCue(33513));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            //cs.add_cue(new Text(0, 1000, "I axe murdered my through half of Europe,"));
            //cs.add_cue(new Text(1000, 2000, "stopping only for coffee and grindin' my axe."));
            //cs.add_cue(new Text(2000, 3000, "Got siezed in Amsterdam, right after I had an epiphany about murdering."));
            //cs.add_cue(new Text(3000, 4000, "Like how it was mean and stuff."));
            //cs.add_cue(new Text(4000, 5000, "I know exactly how you feel."));
            //cs.add_cue(new Text(5000, 6000, "Alright man, I'll knit you up something fierce."));
            //cs.add_cue(new Text(6000, 7000, "Just out of curiosity though,"));
            //cs.add_cue(new Text(7000, 8000, "are you guilty, or innocent?"));

            //cs.add_cue(new EndCue(8000));
        }

        void Prof_Revolutionary()
        {
            CutScene cs = prof_revolutionary;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_revolutionary_a"));
	        cs.add_cue(new Sound(15355, "profx_revolutionary_b"));
			
	        cs.add_cue(new Text(0, 1657, "I spoke out against the status quo."));
	        cs.add_cue(new Text(2560, 4609, "I spit hot truth all over the radio."));
	        cs.add_cue(new Text(4609, 7441, "So they sent the men in black to stop the signal."));
	        cs.add_cue(new Text(7441, 9852, "But I am more than that. I am an idea."));
	        cs.add_cue(new Text(9852, 12774,"And ideas are really, really, hard"));
	        cs.add_cue(new Text(12774, 13889, "to shoot with a gun."));
	        cs.add_cue(new Text(15355, 21722, "You sick, unpatriotic fool."));
	        cs.add_cue(new Text(27122, 26513, "I don't think I can make custumes for the likes of you."));
	        cs.add_cue(new Text(26513, 30249, "Maybe later on when i've calmed down."));
	        cs.add_cue(new Text(30249, 33292, "But for now, I got to mope."));
	
	        cs.add_cue(new EndCue(33292));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            //cs.add_cue(new Text(0, 1000, "I spoke out against the status quo."));
            //cs.add_cue(new Text(1000, 2000, "I spit hot truth all over the radio"));
            //cs.add_cue(new Text(2000, 3000, "so they sent the men in black to stop the signal."));
            //cs.add_cue(new Text(3000, 4000, "But i am more than that."));
            //cs.add_cue(new Text(4000, 5000, "I am an idea."));
            //cs.add_cue(new Text(5000, 6000, "And ideas are really, really, hard to shoot with a gun."));
            //cs.add_cue(new Text(6000, 7000, "I know exactly how you feel."));
            //cs.add_cue(new Text(7000, 8000, "Alright man, I'll knit you up something fierce."));
            //cs.add_cue(new Text(8000, 9000, "Just out of curiosity though,"));
            //cs.add_cue(new Text(9000, 10000, "are you guilty, or innocent?"));

            //cs.add_cue(new EndCue(10000));
        }

        void Prof_Truth()
        {
            CutScene cs = prof_truth;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_truth_a"));
	        cs.add_cue(new Sound(15569, "profx_truth_b"));
			
	        cs.add_cue(new Text(0, 2217,  "My Dad wanted me to code him a butterfly game."));
	        cs.add_cue(new Text(2879, 5513,  "I failed. So he kicked me out of the house"));
	        cs.add_cue(new Text(5513, 6914,  "on the mean streets"));
	        cs.add_cue(new Text(6914, 10625,  "where I tried to become a baller by selling drugs and soliciting hookers."));
	        cs.add_cue(new Text(10625, 14000,  "I got busted by this hot cop who totally teased me about getting naked"));
	        cs.add_cue(new Text(15569, 16524,  "I"));
	        cs.add_cue(new Text(16955, 17925,  "Know"));
	        cs.add_cue(new Text(18587, 21000,  "Exactly how you feel."));
	        cs.add_cue(new Text(21914, 25856,  "Alright man, I'll knit you up something fierce."));
	        cs.add_cue(new Text(26980, 29768,  "Just out of curiosity though"));
	        cs.add_cue(new Text(29768, 31000,  "are you guilty?"));
	        cs.add_cue(new Text(31723, 32649,  "or innocent?"));
						
	        cs.add_cue(new EndCue(34649));

            //cs.add_cue(new Sound(0, "sweepy_rumour1"));

            //cs.add_cue(new Text(0, 1000, "My Dad wanted me to code him a butterfly game."));
            //cs.add_cue(new Text(1000, 2000, "I failed."));
            //cs.add_cue(new Text(2000, 3000, "So he kicked me out on the mean streets,"));
            //cs.add_cue(new Text(3000, 4000, "Where i tried to become a baller by selling drugs and soliciting hookers."));
            //cs.add_cue(new Text(4000, 5000, "I got busted by this hot cop who totally teased me about getting naked."));
            //cs.add_cue(new Text(5000, 6000, "You sick unpatriotic fool. "));
            //cs.add_cue(new Text(6000, 7000, "I don't think i can make custumes for the likes of you."));
            //cs.add_cue(new Text(7000, 8000, "Maybe later on when I've calmed down."));
            //cs.add_cue(new Text(8000, 9000, "But right now, I got to mope."));

            //cs.add_cue(new EndCue(9000));
        }

        void Prof_Guilty()
        {
            CutScene cs = prof_guilty;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_guilty_a"));
            cs.add_cue(new Sound(1300, "yeah"));

            cs.add_cue(new Text(0, 1300, "Guilty as charged."));
            cs.add_cue(new Text(1300, 2300, "*** Wilson got The Guard's Uniform ***"));

            cs.add_cue(new EndCue(2300));
        }
        
        void Prof_Innocent()
        {
            CutScene cs = prof_innocent;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_innocent_a"));
            cs.add_cue(new Sound(1600, "yeah"));

            cs.add_cue(new Text(0, 1600, "Innocent like a fish."));
            cs.add_cue(new Text(1600, 2600, "*** Wilson got The Guard's Uniform ***"));

            cs.add_cue(new EndCue(2600));
        }

        void Prof_Refuse()
        {
            CutScene cs = prof_refuse;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//professor_x", 1, new Vector2(342, 22)));

            cs.add_cue(new Sound(0, "profx_refuse_a"));

            cs.add_cue(new Text(0, 1000, "It's a secret."));
            cs.add_cue(new Text(1000, 2800, "And we aren't BFFs"));
            cs.add_cue(new Text(2800, 5300, "Do I go around interrogating you about your life?"));

            cs.add_cue(new EndCue(5300));
        }

        void MC_BadRomance()
        {
            CutScene cs = mc_bad_romance;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 30600, "anger_04", 1, Vector2.Zero));
            cs.add_cue(new Graphic(30600, 30700, "anger_01", 1, Vector2.Zero));
            cs.add_cue(new Graphic(30700, 30800, "anger_02", 1, Vector2.Zero));
            cs.add_cue(new Graphic(30800, 65300, "anger_03", 1, Vector2.Zero));

            cs.add_cue(new Graphic(65300, 65400, "anger_02", 1, Vector2.Zero));
            cs.add_cue(new Graphic(65400, 65500, "anger_01", 1, Vector2.Zero));
            cs.add_cue(new Graphic(65500, 100000, "anger_04", 1, Vector2.Zero));




            cs.add_cue(new Sound(0, "mc_bad_romance_a"));
            cs.add_cue(new Sound(5900, "mc_bad_romance_b"));
            cs.add_cue(new Sound(6800, "mc_bad_romance_c"));
            cs.add_cue(new Sound(10000, "mc_bad_romance_d"));
            cs.add_cue(new Sound(10900, "mc_bad_romance_e"));
            cs.add_cue(new Sound(14000, "mc_bad_romance_f"));
            cs.add_cue(new Sound(14800, "mc_bad_romance_g"));
            cs.add_cue(new Sound(19700, "mc_bad_romance_h"));
            cs.add_cue(new Sound(20700, "mc_bad_romance_i"));
            cs.add_cue(new Sound(25700, "mc_bad_romance_j"));
            cs.add_cue(new Sound(30600, "mc_bad_romance_k"));
            cs.add_cue(new Sound(33100, "mc_bad_romance_l"));
            cs.add_cue(new Sound(38200, "mc_bad_romance_m"));
            cs.add_cue(new Sound(41900, "mc_bad_romance_n"));
            cs.add_cue(new Sound(49300, "mc_bad_romance_o"));
            cs.add_cue(new Sound(54900, "mc_bad_romance_p"));
            cs.add_cue(new Sound(65300, "mc_bad_romance_q"));            

            cs.add_cue(new Text(0, 1000, "RA RA RA RA RA."));
            cs.add_cue(new Text(1000, 3000, "RA RA OOH OOH LAA"));
            cs.add_cue(new Text(3000, 5900, "RA RA RA ROMANCE."));
            cs.add_cue(new Text(5900, 6800, "Hi James."));
            cs.add_cue(new Text(6800, 10000, "I WANT YO LUNCHBOX, YO PURPLE DISEASE."));
            cs.add_cue(new Text(10000, 10900, "James?"));
            cs.add_cue(new Text(10900, 14000, "I WANT YO' GLITTER ON YO' PERFECT ME MEE"));
            cs.add_cue(new Text(14000, 14800, "James?"));
            cs.add_cue(new Text(14800, 19700, "I WANT YO GLOVE. GLOVE GLOVE GLOVE. I WANT YO GLOVES."));
            cs.add_cue(new Text(19700, 20700, "..."));
            cs.add_cue(new Text(20700, 25700, "I WANT YO DRAMA RAMA RAMA RAMA, I WANT YO GLOVES."));
            cs.add_cue(new Text(25700, 28100, "I WANT YOUR GLOVIN AND I WANT YOUR REVENGE,"));
            cs.add_cue(new Text(28100, 30600, "YOU AND ME COULD HAVE A RAD BROMANCE."));
            cs.add_cue(new Text(30600, 33100, "What the fuck are you singing?"));
            cs.add_cue(new Text(33100, 38200, "Woah oh oh Oh OH OH OH CAUGHT IN A"));
            cs.add_cue(new Text(38200, 39900, "Holy fuck nuts Wilson!"));



            cs.add_cue(new Text(39900, 41900, "That fucking shit is shit!"));
            cs.add_cue(new Text(41900, 42800, "..."));
            cs.add_cue(new Text(42800, 45900, "Yo singing ain't fly at all, fact it be swimmin."));
            cs.add_cue(new Text(45900, 49300, "My ears want me to tell your mouth, to fuck itself."));
            cs.add_cue(new Text(49300, 50200, "Makes sense."));
            cs.add_cue(new Text(50200, 52700, "Unlike yo' fucking song."));
            cs.add_cue(new Text(52700, 53200, "James"));
            cs.add_cue(new Text(53200, 53900, "Fuck."));
            cs.add_cue(new Text(53900, 54900, "I need to ask you something"));



            cs.add_cue(new Text(54900, 56000, "Who be James?"));
            cs.add_cue(new Text(56000, 57200, "I DON'T SEE NO JAMES?  "));
            cs.add_cue(new Text(57200, 60400, "You be talkin to MC HUUUMMUUUUSSSSS."));
            cs.add_cue(new Text(60400, 62900, "I be the taste on yo bland ass pita bread."));
            cs.add_cue(new Text(62900, 64100, "Or should I say,"));
            cs.add_cue(new Text(64100, 65300, "Your pity bread."));
            cs.add_cue(new Text(65300, 68400, "I'll come back later when I have something more specific to ask for."));

            cs.add_cue(new EndCue(68400));
        }

        void MC_Umbrella()
        {
            CutScene cs = mc_umbrella;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//mc_hummus", 1, new Vector2(274, 6)));

            cs.add_cue(new Sound(0, "mc_umbrella_a"));
            cs.add_cue(new Sound(5200, "mc_umbrella_b"));
            cs.add_cue(new Sound(6100, "mc_umbrella_c"));
            cs.add_cue(new Sound(16500, "mc_umbrella_d"));
            cs.add_cue(new Sound(17500, "mc_umbrella_e"));
            cs.add_cue(new Sound(27900, "mc_umbrella_f"));
            cs.add_cue(new Sound(29800, "mc_umbrella_g"));
            cs.add_cue(new Sound(31400, "mc_umbrella_h"));
            cs.add_cue(new Sound(34900, "mc_umbrella_i"));
            cs.add_cue(new Sound(43100, "mc_umbrella_j"));
            cs.add_cue(new Sound(44000, "mc_umbrella_k"));
            cs.add_cue(new Sound(51100, "mc_umbrella_l"));
            cs.add_cue(new Sound(55800, "mc_umbrella_n"));
            cs.add_cue(new Sound(64200, "mc_umbrella_o"));
            cs.add_cue(new Sound(66100, "mc_umbrella_p"));

            cs.add_cue(new Text(0, 5200, "Uh uh Bana NANA, UH UH bana nana."));
            cs.add_cue(new Text(5200, 6100, "Hello James."));
            cs.add_cue(new Text(6100, 8400, "Yeah you might steal my heart,"));
            cs.add_cue(new Text(8400, 10600, "but we'll be worlds apart,"));
            cs.add_cue(new Text(10600, 16500, "downloading magazines, I got a huge 'epeen"));
            cs.add_cue(new Text(16500, 17500, "James?"));
            cs.add_cue(new Text(17500, 21000, "Because, though we may shine shine shine together,"));
            cs.add_cue(new Text(21000, 23700, "you can still borrow umbrella,"));
            cs.add_cue(new Text(23700, 26500, "you can still borrow my umbrella"));
            cs.add_cue(new Text(26500, 27900, "ELLA ELLA."));
            cs.add_cue(new Text(27900, 29800, "EH EH EH."));
            cs.add_cue(new Text(29800, 31400, "ELLA ELLA."));
            cs.add_cue(new Text(31400, 34900, "EH EH EH EH EH."));
            cs.add_cue(new Text(34900, 36200, "YO THIN MAN,"));
            cs.add_cue(new Text(36200, 38000, "that beat be poppin like Mary,"));
            cs.add_cue(new Text(38000, 40200, "I'm totally pirating that shit,"));
            cs.add_cue(new Text(40200, 43100, "I'll put yo' name on the CD sleeve."));
            cs.add_cue(new Text(43100, 44000, "That's nice James."));
            cs.add_cue(new Text(44000, 44800, "Who be James?"));
            cs.add_cue(new Text(44800, 46900, "I see no James thin man."));
            cs.add_cue(new Text(46900, 48600, "Only pig in the sty is I,"));
            cs.add_cue(new Text(48600, 51100, "MC HUMMUS."));
            cs.add_cue(new Text(51100, 53600, "Radical totally MC, my bad."));
            cs.add_cue(new Text(53600, 55800, "I need you to get me some Herowine."));
            cs.add_cue(new Text(55800, 58400, "You put a lid on that baby bottle free willy,"));
            cs.add_cue(new Text(58400, 60400, "I don't sell no Herowine,"));
            cs.add_cue(new Text(60400, 63300, "all I got is, Paraskeetamol."));
            cs.add_cue(new Text(63300, 64200, "Know what I mean?"));
            cs.add_cue(new Text(64200, 66100, "Like a fat man knows dough."));
            cs.add_cue(new Text(66100, 67600, "Yeah doggy doolittle,"));
            cs.add_cue(new Text(67600, 69300, "I'll hook you up with some skeet,"));
            cs.add_cue(new Text(69300, 71100, "for only one tube of tangerine glitter."));


            cs.add_cue(new Sound(71100, "yeah"));
            cs.add_cue(new Text(71100, 73000, "*** Wilson got Paraskeetamol ***"));

            cs.add_cue(new EndCue(73000));
          
            //Game State Changes
            Globals.current_room = yard;
            //toilet.hotspots.Remove(mc_hummus_hs);
            mc_hummus_hs.active = false;
            toilet.characters.Remove(mc_hummus);
        }

        void BS_Hello()
        {
            CutScene cs = bs_hello;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//ben_seib", 1, new Vector2(366, 10)));

            cs.add_cue(new Sound(0, "bens_hello_a"));
            cs.add_cue(new Sound(3300, "bens_hello_b"));
            cs.add_cue(new Sound(5700, "bens_hello_c"));
            cs.add_cue(new Sound(11900, "bens_hello_d"));

            cs.add_cue(new Text(0, 1500, "Finally found you,"));
            cs.add_cue(new Text(1500, 3300, "you theiving bastard."));
            cs.add_cue(new Text(3300, 4500, "Hi there Wilson,"));
            cs.add_cue(new Text(4500, 5700, "How's it cracking mate?"));
            cs.add_cue(new Text(5700, 7400, "It's cracking all twiggy!"));
            cs.add_cue(new Text(7400, 9600, "But Ben you owe me an explanation"));
            cs.add_cue(new Text(9600, 11900, "and a tube of my finest glitter."));
            cs.add_cue(new Text(11900, 12800, "Explanation for what?"));

            cs.add_cue(new EndCue(12800));
        }

        void BS_Naked()
        {
            CutScene cs = bs_naked;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//ben_seib", 1, new Vector2(366, 10)));

            cs.add_cue(new Sound(0, "bens_naked_a"));
            cs.add_cue(new Sound(3200, "bens_naked_b"));
            cs.add_cue(new Sound(9200, "bens_naked_c"));
            cs.add_cue(new Sound(19500, "bens_naked_d"));
            cs.add_cue(new Sound(21000, "bens_naked_e"));
            cs.add_cue(new Sound(24500, "bens_naked_f"));
            cs.add_cue(new Sound(28800, "bens_naked_g"));
            cs.add_cue(new Sound(33600, "bens_naked_h"));
            cs.add_cue(new Sound(41000, "bens_naked_i"));
            cs.add_cue(new Sound(44900, "bens_naked_j"));

            cs.add_cue(new Text(0, 3200, "Why the hell are you taking off your clothes?"));

            cs.add_cue(new Text(3200, 5000, "It's the only way man."));
            cs.add_cue(new Text(5000, 6600, "I need more glitter."));
            cs.add_cue(new Text(6600, 7500, "I owe."));
            cs.add_cue(new Text(7500, 9200, "I owe glitter to scary people."));

            cs.add_cue(new Text(9200, 10600, "Ben!"));
            cs.add_cue(new Text(10600, 13400, "We've known eachother for a long time."));
            cs.add_cue(new Text(13400, 17100, "Remember when you exposed yourself to Ms. Foxcroft in English?"));
            cs.add_cue(new Text(17100, 19500, "That didn't go over well now did it?"));

            cs.add_cue(new Text(19500, 21000, "Not really no."));

            cs.add_cue(new Text(21000, 24500, "And did she appreciate the fact that you were well groomed?"));

            cs.add_cue(new Text(24500, 26200, "She started crying."));
            cs.add_cue(new Text(26200, 28800, "And I had spent all night with my Mom's hair straightener."));

            cs.add_cue(new Text(28800, 30600, "And you died it all purple,"));
            cs.add_cue(new Text(30600, 32500, "we remember Ben."));
            cs.add_cue(new Text(32500, 33600, "Did she care?"));

            cs.add_cue(new Text(33600, 35700, "No she called the police."));

            cs.add_cue(new Text(35700, 37300, "The police Ben!"));
            cs.add_cue(new Text(37300, 39400, "Put your dick away please!"));
            cs.add_cue(new Text(39400, 41000, "This is not the time!"));

            cs.add_cue(new Text(41000, 43000, "The world is a cruel and unforgiving place"));

            cs.add_cue(new Text(43000, 44900, "that just wont accept me for who I am."));
            cs.add_cue(new Text(44900, 48200, "Give me back my scroll you sick man."));

            cs.add_cue(new EndCue(48200));
        }

        void BS_Items()
        {
            CutScene cs = bs_items;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//ben_seib", 1, new Vector2(366, 10)));

            cs.add_cue(new Sound(0, "ben_items_a"));
            cs.add_cue(new Sound(3900, "ben_items_b"));
            cs.add_cue(new Sound(8100, "ben_items_c"));
            cs.add_cue(new Sound(17500, "ben_items_d"));
            cs.add_cue(new Sound(22800, "ben_items_e"));
            cs.add_cue(new Sound(24000, "ben_items_f"));

            cs.add_cue(new Text(0, 3900, "Why the hell did you take my glitter and my scroll?"));
            cs.add_cue(new Text(3900, 5700, "I needed more toilet paper,"));
            cs.add_cue(new Text(5700, 8100, "I was going to trade your scroll with the Loose Cannon for more glitter"));
            cs.add_cue(new Text(8100, 9800, "The Loose Cannon!"));
            cs.add_cue(new Text(9800, 11800, "Do you know why they call him that?"));
            cs.add_cue(new Text(11800, 14700, "He has a bowel as loose as your mother!"));
            cs.add_cue(new Text(14700, 17500, "Oh God, is there any paper left?"));
            cs.add_cue(new Text(17500, 18800, "It's all still here."));
            cs.add_cue(new Text(18800, 22800, "The loose cannon hasn't had any fibre since the great prison famine of last Tuesday."));
            cs.add_cue(new Text(22800, 24000, "Give it back Ben."));
            cs.add_cue(new Text(24000, 24900, "Ok Wilson."));

            cs.add_cue(new Sound(24900, "yeah"));
            cs.add_cue(new Text(24900, 25900, "*** Wilson got Glitter and The Scroll ***"));

            cs.add_cue(new EndCue(25900));

            //Game State Changes :0
            mc_hummus_hs.scene = mc_umbrella;

            professor_hs.active = true;
            if (laundry.characters.Contains(professor_x) == false)
                laundry.characters.Add(professor_x);

            
        }

        void BS_Exit()
        {
            CutScene cs = bs_exit;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//ben_seib", 1, new Vector2(366, 10)));

            cs.add_cue(new EndCue(100));
        }

        void BS_Quiet()
        {
            CutScene cs = bs_quiet;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//ben_seib", 1, new Vector2(366, 10)));

            cs.add_cue(new Sound(0, "bens_quiet_a"));
            cs.add_cue(new Sound(2400, "bens_quiet_b"));
            cs.add_cue(new Sound(4100, "bens_quiet_c"));
            cs.add_cue(new Sound(6100, "bens_quiet_d"));
            cs.add_cue(new Sound(12700, "bens_quiet_e"));
            cs.add_cue(new Sound(14100, "bens_quiet_f"));
            cs.add_cue(new Sound(16900, "bens_quiet_g"));

            cs.add_cue(new Text(0, 2400, "What's up with everyone being so damn quiet here?"));
            cs.add_cue(new Text(2400, 4100, "It's because you aren't pure Wilson."));
            cs.add_cue(new Text(4100, 6100, "But I am pure, can't you see?"));


            cs.add_cue(new Text(6100, 8400, "No Wilson, you're wearing clothes,"));
            cs.add_cue(new Text(8400, 11200, "the indecent exposures only admit the existence"));
            cs.add_cue(new Text(11200, 12700, "of other purely naked people."));
            
            cs.add_cue(new Text(12700, 14100, "But what about the guards?"));
            cs.add_cue(new Text(14100, 16900, "They use their imaginations if you know what I'm saying."));
            cs.add_cue(new Text(16900, 19100, "I wish I did not."));

            cs.add_cue(new EndCue(19100));
        }

        void Sweepy_Rumour_A()
        {
            CutScene cs = sweepy_rumour_a;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_rumour1"));

            cs.add_cue(new Text(0, 2200, "Heard any rumours Sweepy?"));
            cs.add_cue(new Text(2200, 6900, "Well Wilson, I did hear something new."));
            cs.add_cue(new Text(6900, 13800, "Our prison has become the stage for a mega"));
            cs.add_cue(new Text(13800, 17000, "super uber star."));
            cs.add_cue(new Text(17000, 24900, "They call him MC Hummus and I've heard he spits hot fire Wilson."));
            cs.add_cue(new Text(24900, 28800, "Like some kind of fire eater."));
            cs.add_cue(new Text(28800, 32200, "He got arrested for dealing heroine Wilson."));
            cs.add_cue(new Text(32200, 35100, "You know that new street drug."));
            cs.add_cue(new Text(35100, 40300, "That stuff will knock you on your ass and make you dream of"));
            cs.add_cue(new Text(40300, 43400, "nothing but rainbows."));

            cs.add_cue(new EndCue(43400));
        }

        void Sweepy_Rumour_B()
        {
            CutScene cs = sweepy_rumour_b;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_rumour2"));

            cs.add_cue(new Text(0, 2100, "Heard any other rumours?"));
            cs.add_cue(new Text(2100, 6600, "I heard the guards have decided to stop harassing"));
            cs.add_cue(new Text(6600, 11500, "the indecent exposurers that hang around in the library."));
            cs.add_cue(new Text(11500, 18600, "Apparently the IE's just loved the attention."));
            cs.add_cue(new Text(18600, 23900, "So many people like to get naked in this country Wilson."));
            cs.add_cue(new Text(23900, 29000, "In my day we wore our clothes with pride."));

            cs.add_cue(new EndCue(29000));
        }

        void Sweepy_Rumour_C()
        {
            CutScene cs = sweepy_rumour_c;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_rumour3"));

            cs.add_cue(new Text(0, 1700, "Got anything else?"));
            cs.add_cue(new Text(1700, 6600, "Did you hear that the loose cannon is out of toilet paper?"));
            cs.add_cue(new Text(6600, 11100, "Apparently he's willing to trade for one roll."));
            cs.add_cue(new Text(11100, 17100, "If you trade him a roll of toilet paper he'll shank anyone you like."));
            cs.add_cue(new Text(17100, 23000, "Or, instead he's offering to trade a whole damn tube of glitter for it."));

            cs.add_cue(new EndCue(23000));
        }

        void Sweepy_Rumour_D()
        {
            CutScene cs = sweepy_rumour_d;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_rumour4"));

            cs.add_cue(new Text(0, 2100, "Is there anything going on with you?"));
            cs.add_cue(new Text(2100, 3700, "You know Wilson,"));
            cs.add_cue(new Text(3700, 8900, "I'm still waiting for my new suit from the professor."));
            cs.add_cue(new Text(8900, 13000, "I swear that man is a perfectionist."));
            cs.add_cue(new Text(13000, 17800, "I've heard he designed all the guard's uniforms."));
            cs.add_cue(new Text(17800, 21300, "They say his sentence ended years ago."));
            cs.add_cue(new Text(21300, 23200, "But the guards wont let him go,"));
            cs.add_cue(new Text(23200, 27200, "because the man can knit up something fierce."));

            cs.add_cue(new EndCue(27200));
        }

        void Sweepy_Rumour_E()
        {
            CutScene cs = sweepy_rumour_e;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_rumour5"));

            cs.add_cue(new Text(0, 1800, "What's happening Sweepster?"));
            cs.add_cue(new Text(1800, 5400, "The annual prison guard masked ball,"));
            cs.add_cue(new Text(5400, 8500, "is taking place tonight Wilson."));
            cs.add_cue(new Text(8500, 11600, "It's apparently one of the biggest"));
            cs.add_cue(new Text(11600, 15200, "and best prison guard masked balls"));
            cs.add_cue(new Text(15200, 17900, "in the whole world."));
            cs.add_cue(new Text(17900, 21200, "Too bad there is no way to get to the"));
            cs.add_cue(new Text(21200, 22800, "ballroom from here."));
            cs.add_cue(new Text(22800, 24400, "No sir!"));
            cs.add_cue(new Text(24400, 28200, "It is absolutey unpossible."));

            cs.add_cue(new EndCue(28200));
        }

        void Sweppy_Hello()
        {
            CutScene cs = sweepy_hello;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            //cs.add_cue(new Graphic(0, 100000, "lobby", 0, Vector2.Zero));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            //cs.add_cue(new Sound(0, "beep"));

            cs.add_cue(new Sound(0, "sweepy_hello_a"));
            cs.add_cue(new Sound(1300, "sweepy_hello_b"));
            cs.add_cue(new Sound(3100, "sweepy_hello_c"));
            cs.add_cue(new Sound(4600, "sweepy_hello_d"));
            cs.add_cue(new Sound(10400, "sweepy_hello_e"));
            cs.add_cue(new Sound(15800, "sweepy_hello_f"));
            cs.add_cue(new Sound(21600, "sweepy_hello_g"));
            cs.add_cue(new Sound(25200, "sweepy_hello_h"));
            cs.add_cue(new Sound(30300, "sweepy_hello_i"));
            cs.add_cue(new Sound(32700, "sweepy_hello_j"));
            cs.add_cue(new Sound(47100, "sweepy_hello_k"));

            cs.add_cue(new Text(0, 1300, "What's up Sweepy?"));
            cs.add_cue(new Text(1300, 3100, "Ahoy there, Wilson."));
            cs.add_cue(new Text(3100, 4600, "How's the floor today?"));
            cs.add_cue(new Text(4600, 6600, "Same old, same old."));
            cs.add_cue(new Text(6600, 9100, "I heard you rummaging inside your cell,"));
            cs.add_cue(new Text(9100, 10400, "you lose something?"));
            cs.add_cue(new Text(10400, 12100, "Aww Sweepy, nothing much."));
            cs.add_cue(new Text(12100, 14500, "Just this you know, old picture of my girlfriend."));
            cs.add_cue(new Text(14500, 15800, "The one with the huge"));
            cs.add_cue(new Text(15800, 18100, "Was it your escape plan Wilson?"));
            cs.add_cue(new Text(18100, 21600, "'Cause I saw Ben grab it and run before morning showers."));
            cs.add_cue(new Text(21600, 25200, "God damn it! How'd you know about my escape plan?"));
            cs.add_cue(new Text(25200, 30300, "The floors have ears Wilson. Also Ben told me."));
            cs.add_cue(new Text(30300, 32700, "God damn Ben Seib!"));
            cs.add_cue(new Text(32700, 37600, "Don't worry Wilson your secret is safe with me."));
            cs.add_cue(new Text(37600, 41000, "I've been a confidante to many inmates."));
            cs.add_cue(new Text(41000, 47100, "If dead men could tell tales the whole prison would be knee deep in drama."));
            cs.add_cue(new Text(47100, 48000, "Alright Sweepy."));

            cs.add_cue(new EndCue(48000));
        }

        void Sweepy_BenSeib()
        {
            CutScene cs = sweepy_benseib;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_benseib_a"));
            cs.add_cue(new Sound(5100, "sweepy_benseib_b"));

            cs.add_cue(new Text(0, 2400, "Did you say that fat bastard,"));
            cs.add_cue(new Text(2400, 3400, "Ben Seib,"));
            cs.add_cue(new Text(3400, 5100, "took my scroll?"));
            cs.add_cue(new Text(5100, 6900, "Scroll?"));
            cs.add_cue(new Text(6900, 10600, "You mean that roll of toilet paper with the pink writing?"));

            cs.add_cue(new EndCue(10600));
        }

        void Sweepy_Yes()
        {
            CutScene cs = sweepy_yes;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_yes_a"));
            cs.add_cue(new Sound(600, "sweepy_yes_b"));
            cs.add_cue(new Sound(4900, "sweepy_yes_c"));
            cs.add_cue(new Sound(8900, "sweepy_yes_d"));
            cs.add_cue(new Sound(17200, "sweepy_yes_e"));
            cs.add_cue(new Sound(25700, "sweepy_yes_f"));
            cs.add_cue(new Sound(32600, "sweepy_yes_g"));
            cs.add_cue(new Sound(39400, "sweepy_yes_h"));

            cs.add_cue(new Text(0, 600, "Yeah."));
            cs.add_cue(new Text(600, 4900, "Yeah, Ben dashed out of here when they called morning showers, "));
            cs.add_cue(new Text(4900, 8900, "And I never seen him so excited about that before."));
            cs.add_cue(new Text(8900, 17200, "So I kept my eyes peeled and noticed that he had this freaking toilet roll wrapped around his body,"));
            cs.add_cue(new Text(17200, 25700, "Covered in flouresent pink writing, describing in great detail an insane plan to escape the prison."));
            cs.add_cue(new Text(25700, 32600, "He kept muttering something under his breath, sounded a bit like 'glitter'."));
            cs.add_cue(new Text(32600, 35200, "He seemed desperate Wilson."));
            cs.add_cue(new Text(35200, 39400, "Desperate like some kind of desperate panda."));
            cs.add_cue(new Text(39400, 43600, "Except he wasn't black or white or a bear."));

            cs.add_cue(new EndCue(43600));
        }

        void Sweepy_No()
        {
            CutScene cs = sweepy_no;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));

            cs.add_cue(new Sound(0, "sweepy_no_a"));
            cs.add_cue(new Sound(1300, "sweepy_no_b"));

            cs.add_cue(new Text(0, 1300, "No."));
            cs.add_cue(new Text(1300, 4300, "The last scroll I ever seen,"));
            cs.add_cue(new Text(4300, 9000, "was buried in a sea that never lived to tell the tale."));
            cs.add_cue(new Text(9000, 12100, "that was one brave scroll."));

            cs.add_cue(new EndCue(12100));
        }

        void Sweepy_Tree()
        {
	        CutScene cs = sweepy_tree;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));
	
	        cs.add_cue(new Sound(0, "sweepy_tree_a"));
	        cs.add_cue(new Sound(3846, "sweepy_tree_b"));
	        cs.add_cue(new Sound(15325, "sweepy_tree_c"));
	        cs.add_cue(new Sound(18780, "sweepy_tree_d"));
	
	        cs.add_cue(new Text(0, 3846, "Sweepy, if you were a tree, what tree would it be?"));
	        cs.add_cue(new Text(3846, 6530, "Hmm. I'd be a Willow Tree."));
	        cs.add_cue(new Text(6530, 11296, "Always bending over, legs spread"));
	        cs.add_cue(new Text(11296, 14894, "To sweep the dirt off the ground."));
	        cs.add_cue(new Text(16403, 18891, "You are a congruent man Sweepy."));
	        cs.add_cue(new Text(18891, 21868, "That's why they used to call me"));
	        cs.add_cue(new Text(21868, 23207, "Sweepy The Congruent."));
	
	        cs.add_cue(new EndCue(23207));
        }

        void Sweepy_Exit()
        {
            CutScene cs = sweepy_exit;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));
            cs.add_cue(new EndCue(100));
        }

        void Sweepy_RumourStart()
        {
            CutScene cs = sweepy_rumour;

            cs.add_cue(new Graphic(0, 1000000, Globals.current_room));
            cs.add_cue(new Graphic(0, 100000, "Characters//sweepy", 1, new Vector2(340, 26)));
            cs.add_cue(new EndCue(100));
        }

        void Load_OpeningScene()
        {
            CutScene cs = opening_scene;

            cs.add_cue(new Sound(0, "opening_a"));
            cs.add_cue(new Sound(3300, "opening_b"));
            cs.add_cue(new Sound(4900, "opening_c"));
            cs.add_cue(new Sound(10300, "opening_d"));

            cs.add_cue(new ViewPanel(0, 3300,    new Rectangle(45, 70, 1302, 442)));
            cs.add_cue(new ViewPanel(3300, 4900, new Rectangle(48, 539, 1306, 435)));
            cs.add_cue(new ViewPanel(4900, 10300, new Rectangle(43, 1001, 1308, 440)));
            cs.add_cue(new ViewPanel(10300, 13800, new Rectangle(47, 1467, 1302, 442)));

            cs.add_cue(new Comic(0, 0, "comic"));

            cs.add_cue(new EndCue(13800));

            //cs.add_transitions();
            cs.add_fades();

            
            cs.comic_num = 0;
        }
    }
}

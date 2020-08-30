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
    public abstract class HotSpot
    {
        public enum Type
        {
            room_change,
            dialogue,
            view,
            custom,
            exit
        }

        public Type type;
        public Rectangle region;
        public bool active = true;

        public HotSpot(Rectangle region, Type type)
        {
            this.region = region;
            this.type = type;
        }
    }

    public class RoomChangeHS : HotSpot
    {
        public Room leads_to;

        public RoomChangeHS(Rectangle region, Room leads_to)
            : base(region, Type.room_change)
        {
            this.leads_to = leads_to;
        }
    }

    public class DialogueHS : HotSpot
    {
        public CutScene scene;

        public DialogueHS(Rectangle region, CutScene scene)
            : base(region, Type.dialogue)
        {
            this.scene = scene;
        }
    }

    public class ExitHS : HotSpot
    {
        public Room leads_to;

        public ExitHS(Room leads_to) 
            : base(new Rectangle(0, 529, 1280, 191), Type.exit)
        {
            this.leads_to = leads_to;
        }
    }
}

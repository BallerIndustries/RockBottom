using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRODG2
{
    class MenuData
    {
        public int num_options;
        public List<string> menu_text;
        public int selected_index = 0;

        public MenuData()
        {
            //this.num_options = num_options;
            menu_text = new List<string>(num_options);
        }

        public void move_up()
        {
            if (selected_index > 0)
                selected_index--;
        }

        public void move_down()
        {
            if (selected_index < menu_text.Count - 1)
                selected_index++;
        }
    }
}

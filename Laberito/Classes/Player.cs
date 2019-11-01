using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laberito.Classes
{

    class Player
    {
        public PictureBox PlayerSprite;
        public Player(PictureBox pPlayerSprite)
        {
            this.PlayerSprite = pPlayerSprite;
        }
    }
}

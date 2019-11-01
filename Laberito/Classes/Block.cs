using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laberito.Classes
{
    public enum BlockType
    {
        Pared,
        Camino,
        Meta
    }
    class Block
    {
        public Control BlockObject { get; set; }
        public BlockType BlockType { get; set; }

        public Block(Control pBlockObjetct, BlockType pBlockType)
        {
            this.BlockObject = pBlockObjetct;
            this.BlockType = pBlockType;

        }
    }

    
}

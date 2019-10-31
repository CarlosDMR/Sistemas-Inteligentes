using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laberito
{
    public partial class From1 : Form
    {
        public From1()
        {
            InitializeComponent();
            InicializarLaberinto();
        }

        private void InicializarLaberinto()
        {
            string MapaLab = Properties.Resources.Laberinto;
            using (System.IO.StringReader strReader = new System.IO.StringReader(MapaLab))
            {
                int blockHeight = 35;
                int blockWidth = 35;
                int currentPosX = 0;
                int currentPosY = 0;
                int InicialX = 0;
                string strLine = string.Empty;
                while((strLine = strReader.ReadLine()) != null)
                {
                    string[] strLineArray = strLine.Split(' ');
                    foreach(string strBlockChar in strLineArray)
                    {
                        Button btnBlock = new Button();
                        btnBlock.Size = new Size(blockWidth, blockHeight);
                        switch (strBlockChar)
                        {
                            case "1"://Pared
                                btnBlock.BackColor = Color.Black;
                                break;
                            case "0"://Camino
                                btnBlock.BackColor = Color.GreenYellow;
                                break;
                            case "F"://Salida
                                btnBlock.BackColor = Color.Yellow;
                                break;
                        }
                        btnBlock.Location = new Point(currentPosX, currentPosY);
                        this.Controls.Add(btnBlock);
                        currentPosX += (blockWidth+1);
                    }
                    currentPosX = InicialX;
                    currentPosY += blockHeight;
                }
                strReader.Close();
            }
        }

        private void From1_Load(object sender, EventArgs e)
        {

        }
    }
}

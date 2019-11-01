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
        private Classes.Block[,] Mapa = new Classes.Block[17,33];
        private Classes.Player objPlayer;
        public From1()
        {
            InitializeComponent();
            InicializarLaberinto();
        }
        public enum Direccion
        {
            Right,
            Left,
            Up,
            Down
        }

        private Classes.Block BuscarElementoenMatriz(Control elemento)
        {
            Classes.Block result = null;
            for(int f = 0; f < Mapa.GetLength(0); f++)
            {
                for(int c = 0; c < Mapa.GetLength(0); c++)
                {
                    if(elemento == Mapa[f, c].BlockObject)
                    {
                        result = Mapa[f, c];
                    }
                }
            }
            return result;
        }
        private void InicializarLaberinto()
        {
            string MapaLab = Properties.Resources.Laberinto;
            int blockHeight = 35;
            int blockWidth = 35;
            using (System.IO.StringReader strReader = new System.IO.StringReader(MapaLab))
            {
                int currentPosX = 0;
                int currentPosY = 0;
                int InicialX = 0;
                int iRow = 0;
                int iCol = 0;
                string strLine = string.Empty;
                while((strLine = strReader.ReadLine()) != null)
                {
                    string[] strLineArray = strLine.Split(' ');
                    foreach(string strBlockChar in strLineArray)
                    {
                        Button btnBlock = new Button();
                        btnBlock.Size = new Size(blockWidth, blockHeight);
                        Nullable<Classes.BlockType> blockType = null;
                        switch (strBlockChar)
                        {
                            case "1"://Pared
                                btnBlock.BackColor = Color.Black;
                                blockType = Classes.BlockType.Pared;
                                break;
                            case "0"://Camino
                                btnBlock.BackColor = Color.GreenYellow;
                                blockType = Classes.BlockType.Camino;
                                break;
                            case "F"://Salida
                                btnBlock.BackColor = Color.Yellow;
                                blockType = Classes.BlockType.Meta;
                                break;
                        }
                        btnBlock.Location = new Point(currentPosX, currentPosY);
                        this.Controls.Add(btnBlock);
                        this.Mapa[iRow, iCol] = new Classes.Block(btnBlock, blockType.Value);
                        iCol++;
                        currentPosX += (blockWidth+1);
                    }
                    iRow++;
                    iCol = 0;
                    currentPosX = InicialX;
                    currentPosY += blockHeight;
                }
                strReader.Close();
            }
            PictureBox imgPersonaje = new PictureBox();
            imgPersonaje.Image = Properties.Resources.Hat_man1;
            imgPersonaje.Size = new Size(blockWidth, blockHeight);
            imgPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(imgPersonaje);
            imgPersonaje.Location = Mapa[1,1].BlockObject.Location;
            imgPersonaje.BringToFront();
            objPlayer = new Classes.Player(imgPersonaje);
        }

        private void From1_Load(object sender, EventArgs e)
        {

        }
        private void From1_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.Write(e.KeyCode);
        }

        bool movePlayer = false;
        Direccion playerDireccion = Direccion.Down;
        private int currentPlayerRow;
        private int currentPlayerCol;

        private void GetPlayerBlock(ref int fila, ref int columna)
        {
            for(int f = 0; f < Mapa.GetLength(0); f++)
            {
                for(int c = 0; c < Mapa.GetLength(1); c++)
                {
                    if (objPlayer.PlayerSprite.Location == Mapa[f, c].BlockObject.Location)
                    {
                        fila = f;
                        columna = c;
                    }
                }
            }
        }

        int f = 0;
        int c = 0;
        public void Derecha()
        {
            GetPlayerBlock(ref f, ref c);
            if (c + 1 < Mapa.GetLength(1) && Mapa[f, c + 1].BlockType == Classes.BlockType.Camino)
            {
                movePlayer = true;
                this.tmrPlayerMovement.Enabled = true;
                playerDireccion = Direccion.Right;
                this.currentPlayerRow = f;
                this.currentPlayerCol = c;
            }
        }

        public void Abajo()
        {
            GetPlayerBlock(ref f, ref c);
            if (f + 1 < Mapa.GetLength(0) && Mapa[f + 1, c].BlockType == Classes.BlockType.Camino)
            {
                movePlayer = true;
                this.tmrPlayerMovement.Enabled = true;
                playerDireccion = Direccion.Down;
                this.currentPlayerRow = f;
                this.currentPlayerCol = c;
            }
        }

        public void Izquierda()
        {
            GetPlayerBlock(ref f, ref c);
            if (f + 1 < Mapa.GetLength(1) && Mapa[f + 1, c].BlockType == Classes.BlockType.Camino)
            {
                movePlayer = true;
                this.tmrPlayerMovement.Enabled = true;
                playerDireccion = Direccion.Left;
                this.currentPlayerRow = f;
                this.currentPlayerCol = c;
            }
        }

        public void Arriba()
        {
            GetPlayerBlock(ref f, ref c);
            if (f - 1 < Mapa.GetLength(1) && Mapa[f - 1, c].BlockType == Classes.BlockType.Camino)
            {
                movePlayer = true;
                this.tmrPlayerMovement.Enabled = true;
                playerDireccion = Direccion.Up;
                this.currentPlayerRow = f;
                this.currentPlayerCol = c;
            }
        }
        private void From1_KeyUp(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.Right:
                    Derecha();
                    break;
                case Keys.Down:
                    Abajo();       
                    break;

                case Keys.Left:
                    Izquierda();
                    break;

                case Keys.Up:
                    Arriba();
                    break;
            }
        }

        private void tmrPlayerMovement_Tick(object sender, EventArgs e)
        {
            if (movePlayer)
            {
                int f = 0;
                int c = 0;
                switch (playerDireccion)
                {
                    case Direccion.Right:
                        objPlayer.PlayerSprite.Location = new Point(objPlayer.PlayerSprite.Location.X + 1, objPlayer.PlayerSprite.Location.Y);
                        GetPlayerBlock(ref f, ref c);
                        if(Mapa[currentPlayerRow,currentPlayerCol+1].BlockObject.Location == objPlayer.PlayerSprite.Location)
                        {
                            movePlayer = false;
                            this.tmrPlayerMovement.Enabled = false;
                            currentPlayerRow = f;
                            currentPlayerCol = c;
                        }
                        break;

                    case Direccion.Down:
                        objPlayer.PlayerSprite.Location = new Point(objPlayer.PlayerSprite.Location.X, objPlayer.PlayerSprite.Location.Y + 1);
                        GetPlayerBlock(ref f, ref c);
                        if (Mapa[currentPlayerRow + 1, currentPlayerCol].BlockObject.Location == objPlayer.PlayerSprite.Location)
                        {
                            movePlayer = false;
                            this.tmrPlayerMovement.Enabled = false;
                            currentPlayerRow = f;
                            currentPlayerCol = c;
                        }
                        break;

                    case Direccion.Left:
                        objPlayer.PlayerSprite.Location = new Point(objPlayer.PlayerSprite.Location.X - 1, objPlayer.PlayerSprite.Location.Y);
                        GetPlayerBlock(ref f, ref c);
                        if (Mapa[currentPlayerRow, currentPlayerCol - 1].BlockObject.Location == objPlayer.PlayerSprite.Location)
                        {
                            movePlayer = false;
                            this.tmrPlayerMovement.Enabled = false;
                            currentPlayerRow = f;
                            currentPlayerCol = c;
                        }
                        break;

                    case Direccion.Up:
                        objPlayer.PlayerSprite.Location = new Point(objPlayer.PlayerSprite.Location.X, objPlayer.PlayerSprite.Location.Y - 1);
                        GetPlayerBlock(ref f, ref c);
                        if (Mapa[currentPlayerRow - 1, currentPlayerCol].BlockObject.Location == objPlayer.PlayerSprite.Location)
                        {
                            movePlayer = false;
                            this.tmrPlayerMovement.Enabled = false;
                            currentPlayerRow = f;
                            currentPlayerCol = c;
                        }
                        break;

                }
            }
        }

        
    }
}

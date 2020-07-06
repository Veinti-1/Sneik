using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sneik3
{
    public partial class Form1 : Form
    {
        int n = 24;
        Random rnd = new Random();
        LinkedList<Segmento> Serpiente = new LinkedList<Segmento>();
        int direccion = 1;
        Jugador nuevoJugador = new Jugador();
        public static int finalScore = 0;
        DataGridViewCellStyle styleWh = new DataGridViewCellStyle();
        DataGridViewCellStyle styleRe = new DataGridViewCellStyle();
        DataGridViewCellStyle styleGr = new DataGridViewCellStyle();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tablero.RowCount = n;
            Tablero.ColumnCount = n;
            for (int i = 0; i < Tablero.ColumnCount; i++)
            {
                DataGridViewColumn columna = Tablero.Columns[i];
                columna.Width = (int)(Tablero.Width / Tablero.ColumnCount);
            }
            for (int i = 0; i < Tablero.RowCount; i++)
            {
                DataGridViewRow fila = Tablero.Rows[i];
                fila.Height = (int)(Tablero.Height / Tablero.RowCount);
            }

            Segmento cabeza = new Segmento
            {
                x = rnd.Next(0, n),
                y = rnd.Next(0, n)
            };
            styleGr.BackColor = Color.Green;
            styleRe.BackColor = Color.Red;
            styleWh.BackColor = Color.White;
            Serpiente.AddLast(cabeza);
            Tablero[Serpiente.First.Value.x, Serpiente.First.Value.y].Style = styleGr;
            Comida();
            timer1.Enabled = true;
            button1.Visible = false;
            button1.Enabled = false;
        }
        public void Arriba()
        {
            if (direccion != 3)
            {
                direccion = 1;
                Tablero[Serpiente.Last.Value.x, Serpiente.Last.Value.y].Style = styleWh;

                if (Serpiente.First.Value.y - 1 < 0)
                {
                    Serpiente.First.Value.y = 23;
                    Serpiente.First.Value.yA = 0;
                }
                else
                {
                    Serpiente.First.Value.y--;
                    Serpiente.First.Value.yA = Serpiente.First.Value.y + 1;
                }

                Serpiente.First.Value.xA = Serpiente.First.Value.x;
                SerpienteUpdate();
            }
        }
        public void Abajo()
        {
            if (direccion != 1)
            {
                direccion = 3;
                Tablero[Serpiente.Last.Value.x, Serpiente.Last.Value.y].Style = styleWh;

                if (Serpiente.First.Value.y + 1 == n)
                {
                    Serpiente.First.Value.y = 0;
                    Serpiente.First.Value.yA = 23;
                }
                else
                {
                    Serpiente.First.Value.y++;
                    Serpiente.First.Value.yA = Serpiente.First.Value.y - 1;
                }

                Serpiente.First.Value.xA = Serpiente.First.Value.x;
                SerpienteUpdate();
            }
        }
        public void Der()
        {
            if (direccion != 4)
            {
                direccion = 2;
                Tablero[Serpiente.Last.Value.x, Serpiente.Last.Value.y].Style = styleWh;

                if (Serpiente.First.Value.x + 1 == n)
                {
                    Serpiente.First.Value.x = 0;
                    Serpiente.First.Value.xA = 23;
                }
                else
                {
                    Serpiente.First.Value.x++;
                    Serpiente.First.Value.xA = Serpiente.First.Value.x - 1;
                }

                Serpiente.First.Value.yA = Serpiente.First.Value.y;
                SerpienteUpdate();
            }
        }
        public void Izq()
        {
            if (direccion != 2)
            {
                direccion = 4;
                Tablero[Serpiente.Last.Value.x, Serpiente.Last.Value.y].Style = styleWh;

                if (Serpiente.First.Value.x - 1 < 0)
                {
                    Serpiente.First.Value.x = 23;
                    Serpiente.First.Value.xA = 0;
                }
                else
                {
                    Serpiente.First.Value.x--;
                    Serpiente.First.Value.xA = Serpiente.First.Value.x + 1;
                }
                Serpiente.First.Value.yA = Serpiente.First.Value.y;
                SerpienteUpdate();
            }
        }
        public void actualizar()
        {
            var recorre = Serpiente.First;
            while (recorre.Next != null)
            {
                recorre.Next.Value.xA = recorre.Next.Value.x;
                recorre.Next.Value.x = recorre.Value.xA;
                recorre.Next.Value.yA = recorre.Next.Value.y;
                recorre.Next.Value.y = recorre.Value.yA;
                recorre = recorre.Next;
            }
        }
        public void Comida()
        {
            bool placed = false;
            int x = rnd.Next(0, n);
            int y = rnd.Next(0, n);
            while (!placed)
            {
                if (Tablero[x, y].Style.BackColor != Color.Green)
                {
                    Tablero[x, y].Style = styleRe;
                    placed = true;
                }
            }
        }
        public bool CheckF()
        {
            bool x = Tablero[Serpiente.First.Value.x, Serpiente.First.Value.y].Style.BackColor == Color.Red;
            if (x)
            {
                Segmento nuevo = new Segmento();
                Serpiente.AddLast(nuevo);
                actualizar();
                Comida();
                nuevoJugador.score += 100;
                if ((timer1.Interval - nuevoJugador.score / 100) > 0)
                {
                    timer1.Interval -= nuevoJugador.score / 100;
                }
            }
            return x;
        }
        public bool CheckS()
        {
            int i = 0;
            foreach (var item in Serpiente)
            {
                if (i == 2)
                {
                    timer1.Stop();
                    label1.Visible = true;
                    button2.Visible = true;
                    return true;
                }
                else
                {
                    if (item.Equals(Serpiente.First.Value))
                    {
                        i++;
                    }
                }
            }
            return false;
        }
        private void SerpienteUpdate()
        {
            if (!CheckF())
            {
                actualizar();
            }
            CheckS();

            foreach (var item in Serpiente)
            {
                Tablero[item.x, item.y].Style = styleGr;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (direccion)
            {
                case 1:
                    Arriba();
                    break;
                case 2:
                    Der();
                    break;
                case 3:
                    Abajo();
                    break;
                case 4:
                    Izq();
                    break;
            }
            nuevoJugador.score += 10;
            Score.Text = nuevoJugador.score.ToString();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Izq();
                    break;
                case Keys.Up:
                    Arriba();
                    break;
                case Keys.Right:
                    Der();
                    break;
                case Keys.Down:
                    Abajo();
                    break;
                case Keys.A:
                    Izq();
                    break;
                case Keys.D:
                    Der();
                    break;
                case Keys.S:
                    Abajo();
                    break;
                case Keys.W:
                    Arriba();
                    break;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            finalScore = Convert.ToInt32(Score.Text);
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}

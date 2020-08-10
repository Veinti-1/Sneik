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
            for (int i = 0; i < n; i++)
            {
                DataGridViewColumn columna = Tablero.Columns[i];
                columna.Width = (int)(Tablero.Width / Tablero.ColumnCount);
                DataGridViewRow fila = Tablero.Rows[i];
                fila.Height = (int)(Tablero.Height / Tablero.RowCount);
            }
            Tablero.ClearSelection();
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
                    Serpiente.First.Value.y = n - 1;
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
                    Serpiente.First.Value.yA = n - 1;
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
                    Serpiente.First.Value.xA = n - 1;
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
                    Serpiente.First.Value.x = n - 1;
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
                    timer1.Interval -= nuevoJugador.score / 100;
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
                else if (item.Equals(Serpiente.First.Value))
                    i++;
            }
            return false;
        }
        private void SerpienteUpdate()
        {
            if (!CheckF())
                actualizar();
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
                    if (direccion != 4)
                        Izq();
                    break;
                case Keys.Up:
                    if (direccion != 1)
                        Arriba();
                    break;
                case Keys.Right:
                    if (direccion != 2)
                        Der();
                    break;
                case Keys.Down:
                    if (direccion != 3)
                        Abajo();
                    break;
                case Keys.A:
                    if (direccion != 4)
                        Izq();
                    break;
                case Keys.D:
                    if (direccion != 2)
                        Der();
                    break;
                case Keys.S:
                    if (direccion != 3)
                        Abajo();
                    break;
                case Keys.W:
                    if (direccion != 1)
                        Arriba();
                    break;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            finalScore = Convert.ToInt32(Score.Text);
            Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => Close();
            form2.Show();
        }
    }
}

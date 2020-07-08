using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sneik3
{
    public partial class Form2 : Form
    {
        JsonLW jsonWL = new JsonLW();
        public Form2()
        {
            InitializeComponent();
            label1.Text = Form1.finalScore.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Height = 615;
            var lista = jsonWL.LoadJson();
            Jugador nuevoJ = new Jugador
            {
                name = textBox1.Text,
                score = Form1.finalScore
            };
            if (lista.Count >= 10)
            {
                if (nuevoJ.score > lista[9].score)
                {
                    lista[9] = nuevoJ;
                }
            }
            else
            {
                lista.Add(nuevoJ);
            }
            lista.Sort();
            jsonWL.WriteJson(lista);
            label4.Text = "HIGH SCORES: \n \n";
            label4.Visible = true;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            int i = 1;
            foreach (var item in lista)
            {
                label4.Text += i + ") " + item.name + " : " + item.score + "\n";
                i++;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => Close();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Jugador> lista = new List<Jugador>();
            jsonWL.WriteJson(lista);
            label4.Text = "HIGH SCORES: \n \n";
        }
    }
}

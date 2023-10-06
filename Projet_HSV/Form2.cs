using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_HSV
{
    public partial class Form2 : Form
    {
        private void ListViewInit(string filter)
        {
            listView1.Items.Clear();
            foreach (string fileName in Directory.GetFiles("appointment/"))
            {
                Appointment appointment = JsonSerializer.Deserialize<Appointment>(File.ReadAllText(fileName));
                if (appointment.service == filter || filter == "")
                    listView1.Items.Add(new ListViewItem(new string[] {
                    appointment.name,
                    appointment.surname,
                    appointment.service,
                    appointment.date,
                    appointment.status,
                    fileName.Split("/")[1].Split(".")[0],
                    appointment.doctor }));
            }
        }
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            ListViewInit("");
        }



        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string id = listView1.SelectedItems[0].SubItems[5].Text;
            listView1.SelectedItems[0].SubItems[4].Text = "Acceptée";
            listView1.SelectedItems[0].SubItems[6].Text = textBox1.Text;
            string contenu = File.ReadAllText($"appointment/{id}.json");
            Appointment appointment = JsonSerializer.Deserialize<Appointment>(contenu);
            appointment.status = "Acceptée";
            appointment.doctor = textBox1.Text == "" ? null : textBox1.Text;
            contenu = JsonSerializer.Serialize(appointment);
            File.WriteAllText($"appointment/{id}.json", contenu);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = listView1.SelectedItems[0].SubItems[5].Text;
            listView1.SelectedItems[0].SubItems[4].Text = "Refusée";
            string contenu = File.ReadAllText($"appointment/{id}.json");
            Appointment appointment = JsonSerializer.Deserialize<Appointment>(contenu);
            appointment.status = "Refusée";
            contenu = JsonSerializer.Serialize(appointment);
            File.WriteAllText($"appointment/{id}.json", contenu);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            ListViewInit(comboBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
    }
}

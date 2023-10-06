using System.Text.Json;

namespace Projet_HSV
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm";
            dateTimePicker2.ShowUpDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && comboBox1.Text != string.Empty)
            {
                Appointment appointment = new Appointment()
                {
                    name = textBox1.Text,
                    surname = textBox2.Text,
                    service = comboBox1.Text,
                    date = dateTimePicker1.Value.Date.AddHours(dateTimePicker2.Value.Hour).AddMinutes(dateTimePicker2.Value.Minute).ToString(),
                    status = "En attente"
                };
                if (!File.Exists("id.txt")) { File.WriteAllText("id.txt", "10000000"); }
                string id = File.ReadAllText("id.txt");
                File.WriteAllText("id.txt", (int.Parse(id) + 1).ToString());
                string filename = "appointment/" + id + ".json";
                string jsonString = JsonSerializer.Serialize(appointment);
                File.WriteAllText(filename, jsonString);
                label5.Text = $"Prise de rendez-vous réussis. L'ID est {id}.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != string.Empty)
            {
                if (File.Exists("appointment/" + textBox3.Text + ".json"))
                {
                    Appointment appointment = JsonSerializer
                        .Deserialize<Appointment>(File
                        .ReadAllText("appointment/" + textBox3.Text + ".json"))
                        ;
                    if (appointment.doctor != null && appointment.status == "Acceptée")
                    {
                        label7.Text = $"Votre rendez-vous à été accepter avec Docteur {appointment.doctor}";
                    }
                    else
                    {
                        label7.Text = appointment.status;
                    }
                }
                else { label7.Text = "Ce rendez-vous n'existe pas"; }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
    }
}
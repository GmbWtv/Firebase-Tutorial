﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Firebase_Youtube
{ 
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "4nbPNvdASa82MlQddfymR1KH5mhxBrHPja4MQcDI",
            BasePath = "https://fir-b1ecc.firebaseio.com/"
        };

        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                MessageBox.Show("Connection is Established");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                Id = textBox1.Text,
                Name = textBox2.Text,
                Address = textBox3.Text,
                Age = textBox4.Text
            };

            SetResponse response = await client.SetTaskAsync("Information/"+textBox1.Text,data);
            Data result = response.ResultAs<Data>();

            MessageBox.Show("Data Inserted"+result.Id);


        }

        private async void button2_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetTaskAsync("Information/"+textBox1.Text);
            Data obj = response.ResultAs<Data>();
            textBox1.Text = obj.Id;
            textBox2.Text = obj.Name;
            textBox3.Text = obj.Address;
            textBox4.Text = obj.Age;

            MessageBox.Show("Data Retrieved Successfully");
            //something
            //something else
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                Id=textBox1.Text,
                Name=textBox2.Text,
                Address=textBox3.Text,
                Age=textBox4.Text
            };

            FirebaseResponse response = await client.UpdateTaskAsync("Information/"+textBox1.Text,data);
            Data result = response.ResultAs<Data>();
            MessageBox.Show("Data Updated at ID: "+ result.Id);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteTaskAsync("Information/"+textBox1.Text);
            MessageBox.Show("Deleted Record of ID: "+textBox1.Text);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteTaskAsync("Information");
            MessageBox.Show("All information inside table 'Information' has been deleted");
        }
    }
}

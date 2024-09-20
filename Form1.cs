using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace PositivePrediction
{
    public partial class Form1 : Form
    {
        Random Random = new Random();
        private const string APP_NAME = "NextPrediction";
        private readonly string PREDICTION_CONFIG_PATH = $"{Environment.CurrentDirectory}\\predictConfig.json";   //path to config file
        private string[] _predicions;
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved_1(object sender, SplitterEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;
        try 
	        {	        
		    var data =File.ReadAllText(PREDICTION_CONFIG_PATH);  // save in var data

            _predicions = JsonConvert.DeserializeObject<string[]>(data); // convert fron JSON to string[] => data
	        }
	    catch (Exception ex)
	        {

                MessageBox.Show(ex.Message);
	        }
            finally  // if file is not found, and if file is empty
            {
                if (_predicions == null)
                {
                    Close();
                }
                else if (_predicions.Length == 0)
                {
                    MessageBox.Show("This is the end! Wake UP!");
                    Close();
                }
            }
        }


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private async void bPredict_Click(object sender, EventArgs e)   //async make this method asynchronous
        {
            bPredict.Enabled = false;

            await Task.Run(() =>                        //await order to wait untill the end of execution Tack.Run
            {
                for (int i = 1; i < 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBarr(i);
                        this.Text = $"{i}%";
                    }));
                    Thread.Sleep(1);
                }
            });
            var index = Random.Next(0, _predicions.Length);
            var prediction = _predicions[index];
            MessageBox.Show($"{prediction}!");

            progressBar1.Value = 0;
            this.Text = APP_NAME;
            bPredict.Enabled = true;
        }
        private void UpdateProgressBarr(int i)
        {
            if (i == progressBar1.Maximum)
            {
                progressBar1.Maximum = i + 1;
                progressBar1.Value= i + 1;
                progressBar1.Maximum = i ;
            }
            else
            {
                progressBar1.Value = i +1;
            }
            progressBar1.Value = i;
        }
    }
}

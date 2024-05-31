using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool isTimerRunning = false;
        const double mu = 0.00001; // drift  
        const double sigma = 0.005; //  volatility
        Random rnd = new Random();
        double euro, usa;
        double dt = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();

                chart1.Series[0].Points.AddXY(0, euro);
                chart1.Series[1].Points.AddXY(0, usa);

                dt = 0.1; // steps
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt += 0.1; // 
            euro = (double)InputEuro.Value;
            usa = (double)InputUSA.Value;

            euro = euro * Math.Exp((mu - 0.5 * sigma * sigma) * dt + sigma * Math.Sqrt(dt) * SampleStandardNormal());
            chart1.Series[0].Points.AddXY(dt, euro);

            usa = usa * Math.Exp((mu - 0.5 * sigma * sigma) * dt + sigma * Math.Sqrt(dt) * SampleStandardNormal());
            chart1.Series[1].Points.AddXY(dt, usa);

            if (dt > 10)
                timer1.Stop();
        }

        private double SampleStandardNormal()
        {
            // Use the Box-Muller transformation to generate a standard normally distributed random number
            double u1 = 1.0 - rnd.NextDouble(); // uniform(0,1] random
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); // standard normally distributed random number
            return randStdNormal;
        }
    }
}

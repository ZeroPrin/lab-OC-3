using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace WindowsFormsApp112321
{
    public partial class Form1 : Form
    {
        public delegate void del();
        public Form1()
        {
            InitializeComponent();
            Passanger.form = this;

        }


        class Passanger
        {
            public static Form1 form;
            static Semaphore sem = new Semaphore(4, 4);
            Thread myThread;
            int count = 3;

            public Passanger(int i)
            {
                myThread = new Thread(Read);
                myThread.Name = $"Пассажир {i}";
                myThread.Start();
            }

            public void Read()
            {
                while (count > 0)
                {
                    sem.WaitOne();

                    string str = myThread.Name;
                    form.richTextBox1.Invoke(new del(() => form.richTextBox1.Text += $"{str} садится в автобус\n"));


                    form.richTextBox1.Invoke(new del(() => form.richTextBox1.Text += $"{str} едет\n"));
                    Thread.Sleep(1000);

                    form.richTextBox1.Invoke(new del(() => form.richTextBox1.Text += $"{str} выходит из автобуса\n"));

                    sem.Release();

                    count--;
                    Thread.Sleep(1000);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 8; i++)
            {
                Passanger Passanger = new Passanger(i);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        //метод для слияния массивов
            static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
            {
                int left = lowIndex;
                int right = middleIndex + 1;
                int [] tempArray = new int[highIndex - lowIndex + 1];
                int index = 0;

                while ((left <= middleIndex) && (right <= highIndex))
                {
                    if (array[left] < array[right])
                    {
                        tempArray[index] = array[left];
                        left++;
                    }
                    else
                    {
                        tempArray[index] = array[right];
                        right++;
                    }

                    index++;
                }

                for (int i = left; i <= middleIndex; i++)
                {
                    tempArray[index] = array[i];
                    index++;
                }

                for (int i = right; i <= highIndex; i++)
                {
                    tempArray[index] = array[i];
                    index++;
                }

                for (int i = 0; i < tempArray.Length; i++)
                {
                    array[lowIndex + i] = tempArray[i];
                }
            }
        //сортировка слиянием
            static int[] MergeSort(int[] array, int lowIndex, int highIndex)
            {
                if (lowIndex < highIndex)
                {
                    int middleIndex = (lowIndex + highIndex) / 2;
                    MergeSort(array, lowIndex, middleIndex);
                    MergeSort(array, middleIndex + 1, highIndex);
                    Merge(array, lowIndex, middleIndex, highIndex);
                }

                return array;
            } 
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string[] s = textBox2.Text.ToString().Split(new[] { " ", ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
                int[] array = new int[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    array[i] = Convert.ToInt32(s[i]);
                }
                textBox3.Text = (string.Join(", ", MergeSort(array, 0, array.Length-1)));
                stopwatch.Stop();
                textBox4.Text = Convert.ToString(stopwatch.Elapsed);
            }
            catch
            {
                MessageBox.Show ("Ошибка!");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int[] a = new int[10];
            for (int i = 0; i<a.Length; i++)
            {
                a[i] = random.Next(0,100);
                textBox2.Text = (textBox2.Text+Convert.ToString(a[i])+", ");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.txt";
            try
            {
                if (openFileDialog.ShowDialog()==DialogResult.OK)
                {
                    FileInfo file = new FileInfo(openFileDialog.FileName);
                    using (StreamReader sr = new StreamReader(file.OpenRead()))
                    {
                        textBox2.Text = sr.ReadToEnd();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка!");
                }

            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}

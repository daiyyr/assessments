using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AucklandZoo
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbgroupname.Text = textBox1.Text;
            lbgroupsize.Text = textBox2.Text;
            
            if (radioButton1.Checked) //school
            {
                decimal size;
                decimal.TryParse(textBox2.Text, out size);
                School s = new School();
                lbtotalprice.Text = "$" + s.getTotalPrice(size).ToString("f");
                lbtotalamount.Text = "$" + s.getTotalDiscount(size).ToString("f");
            }
            else if (radioButton2.Checked) //tour
            {
                decimal size;
                decimal.TryParse(textBox2.Text, out size);
                Tour s = new Tour();
                lbtotalprice.Text = "$" + s.getTotalPrice(size).ToString("f");
                lbtotalamount.Text = "$" + s.getTotalDiscount(size).ToString("f");

            }
        }

        //only allow int input
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                //&& (e.KeyChar != '.')
                )
            {
                e.Handled = true;
            }
            
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

    }
}

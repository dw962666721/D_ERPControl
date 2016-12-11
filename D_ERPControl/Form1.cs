using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D_ERPControl.Property;

namespace D_ERPControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PropertyFrm propertyFrm;
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("公司类型");
            propertyFrm = new PropertyFrm();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                switch(listBox1.SelectedIndex)
                {
                    case 0:
                        propertyFrm.Show(dockPanel1);
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

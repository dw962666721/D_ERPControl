using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D_ERPControl.Property;
using D_ERPControl.CompanyType;

namespace D_ERPControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PropertyFrm propertyFrm;
        CompanyTypeFrm companyTypeFrm;
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("属性集合");
            listBox1.Items.Add("行业类别");
            propertyFrm = new PropertyFrm();
            companyTypeFrm = new CompanyTypeFrm();
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
                        companyTypeFrm.Show(dockPanel1);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

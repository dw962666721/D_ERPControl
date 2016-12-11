using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace D_ERPControl.Property
{
    public partial class PropertyFrm : Form
    {
        public PropertyFrm()
        {
            InitializeComponent();
        }

        private void PropertyFrm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using UI.Res;
using D_Lib;
using D_Lib.ErpInfo;

namespace D_ERPControl.Property
{
    public partial class AddPropertyFrm : DockContent
    {
        public AddPropertyFrm()
        {
            InitializeComponent();
        }
        Guid id=Guid.Empty;
        public AddPropertyFrm(PropertyInfo info)
        {
            InitializeComponent();
            id = info.aaId;
            textBox1.Text = info.aaPropertyname;
            comboBox1.SelectedIndex = info.aaPropertytype == "1" ? 1 : 0;
            button1.Text = "更新";
        }

        private void AddPropertyFrm_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == Guid.Empty)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("propertyname", textBox1.Text);
                param.Add("propertytype", comboBox1.SelectedIndex);
                Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.AddProductPropertyUrl, param);
                if (dict["res"].ToString() == "1")
                {
                    MessageBox.Show("插入成功!");
                }
                else
                {
                    MessageBox.Show(dict["msg"].ToString());
                }
            }
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("id", id);
                param.Add("propertyname", textBox1.Text);
                param.Add("propertytype", comboBox1.SelectedIndex);
                Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.UpdateProductPropertyUrl, param);
                if (dict["res"].ToString() == "1")
                {
                    if (MessageBox.Show("更新成功!")== System.Windows.Forms.DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(dict["msg"].ToString());
                }
            }
        }
    }
}

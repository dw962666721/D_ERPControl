using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D_Lib.ErpInfo;
using UI.Res;
using D_Lib;

namespace D_ERPControl.CompanyType
{
    public partial class AddCompanyType : Form
    {
        Guid id = Guid.Empty;
        public AddCompanyType()
        {
            InitializeComponent();
        }
        public AddCompanyType(CompanyTypeInfo info)
        {
            InitializeComponent();
            id = info.aaId;
            textBox1.Text = info.aaTypename;
            button1.Text = "更新";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == Guid.Empty)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("typename", textBox1.Text);
                Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.AddCompanyTypeUrl, param);
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
                param.Add("typename", textBox1.Text);
                Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.UpdateCompanyTypeUrl, param);
                if (dict["res"].ToString() == "1")
                {
                    if (MessageBox.Show("更新成功!") == System.Windows.Forms.DialogResult.OK)
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

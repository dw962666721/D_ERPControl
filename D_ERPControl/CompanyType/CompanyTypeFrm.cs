using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using D_Lib;
using D_Lib.ErpInfo;
using UI.Res;
using System.Collections;

namespace D_ERPControl.CompanyType
{
    public partial class CompanyTypeFrm : DockContent
    {
        public CompanyTypeFrm()
        {
            InitializeComponent();
        }

        private void CompanyTypeFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("id","ID");
            dataGridView1.Columns.Add("typename", "行业名称");
            dataGridView1.Columns.Add("createtime", "创建时间");
            dataGridView1.Columns.Add("enable", "是否有效");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dictionary<string, object> param = new Dictionary<string, object>();
            search(param);
        }
        private void search(Dictionary<string, object> param)
        {
            dataGridView1.Rows.Clear();
            Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.GetCompanyTypeUrl, param);
            if (dict["res"].ToString() == "1")
            {
                ArrayList infoList = (ArrayList)dict["companyTypeList"];
                if (infoList != null)
                    for (int i = 0; i < infoList.Count; i++)
                    {
                        CompanyTypeInfo info = HttpTool.DictToObject<CompanyTypeInfo>(infoList[i]);
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.Tag = info;
                        dr.CreateCells(dataGridView1);
                        int index = dataGridView1.Rows.Add(dr);
                        dataGridView1.Rows[index].Cells[0].Value = info.aaId;
                        dataGridView1.Rows[index].Cells[1].Value = info.aaTypename;
                        dataGridView1.Rows[index].Cells[2].Value = info.aaCreatetime;
                        dataGridView1.Rows[index].Cells[3].Value = info.aaEnable == 1 ? "有效" : "无效";
                    }
            }
            else
            {
                MessageBox.Show(dict["msg"].ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("typename", textBox1.Text);
            int enable = int.MinValue;
            if (comboBox1.SelectedIndex == 1)
            {
                enable = 1;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                enable = 0;
            }
            param.Add("enable", enable);
            search(param);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AddCompanyType addFrm = new AddCompanyType();
            addFrm.Show();
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                AddCompanyType addFrom = new AddCompanyType(dataGridView1.SelectedRows[0].Tag as CompanyTypeInfo);
                addFrom.ShowDialog();
            }
        }
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                AddCompanyType addFrom = new AddCompanyType(dataGridView1.SelectedRows[0].Tag as CompanyTypeInfo);
                addFrom.ShowDialog();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("id", (dataGridView1.SelectedRows[0].Tag as CompanyTypeInfo).aaId);
                Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.DelCompanyTypeUrl, param);
                if (dict["res"].ToString() == "1")
                {
                    dataGridView1.SelectedRows[0].Cells[3].Value = "无效";
                }
                else
                {
                    MessageBox.Show(dict["msg"].ToString());
                }
            }
        }

        private void 查看行业属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}

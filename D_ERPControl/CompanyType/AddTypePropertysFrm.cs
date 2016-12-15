using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D_Lib;
using UI.Res;
using System.Collections;
using D_Lib.ErpInfo;

namespace D_ERPControl.CompanyType
{
    public partial class AddTypePropertysFrm : Form
    {
        CompanyTypeInfo TypeInfo;
        int maxSno;
        public AddTypePropertysFrm(CompanyTypeInfo TypeInfo,int maxSno)
        {
            InitializeComponent();
            this.TypeInfo = TypeInfo;
            this.maxSno = maxSno;
            this.Text = "添加属性到【" + TypeInfo.aaTypename + "】" ;
            this.button2.Text = "添加属性到【" + TypeInfo.aaTypename + "】";
            添加ToolStripMenuItem.Text = "添加属性到【" + TypeInfo.aaTypename + "】";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("propertyname", textBox1.Text);
            string type = comboBox1.SelectedIndex == 0 ? "" : (comboBox1.SelectedIndex - 1).ToString();
            param.Add("propertytype", type);
            param.Add("enable", 1);
            search(param);
        }

        private void AddTypePropertysFrm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Add("propertyname", "属性名");
            this.dataGridView1.Columns.Add("propertytype", "属性类型");
            this.dataGridView1.Columns.Add("createtime", "录入时间");
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("enable", "1");
            search(param);
        }
        private void search(Dictionary<string, object> param)
        {
            dataGridView1.Rows.Clear();
            Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.GetProductPropertyUrl, param);
            if (dict["res"].ToString() == "1")
            {
                ArrayList infoList = (ArrayList)dict["propertyInfoList"];
                if (infoList != null)
                    for (int i = 0; i < infoList.Count; i++)
                    {
                        PropertyInfo info = HttpTool.DictToObject<PropertyInfo>(infoList[i]);
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.Tag = info;
                        dr.CreateCells(dataGridView1);
                        int index = dataGridView1.Rows.Add(dr);
                        dataGridView1.Rows[index].Cells[0].Value = info.aaPropertyname;
                        dataGridView1.Rows[index].Cells[1].Value = info.aaPropertytype == "0" ? "文字" : "列表";
                        dataGridView1.Rows[index].Cells[2].Value = info.aaCreatetime;
                    }
            }
            else
            {
                MessageBox.Show(dict["msg"].ToString());
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addProperty();
        }
        void addProperty()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                PropertyInfo propertyInfo = dataGridView1.SelectedRows[0].Tag as PropertyInfo;
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("companytypeid", TypeInfo.aaId);
                param.Add("propertyid", propertyInfo.aaId);
                param.Add("sno", ++maxSno);
                Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.AddCompanyTypeDetailsInfoUrl, param);
                if (dict["res"].ToString() == "1")
                {
                    MessageBox.Show("插入成功!");
                }
                else
                {
                    maxSno--;
                    MessageBox.Show(dict["msg"].ToString());
                }
            } 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            addProperty();
        }
    }
}

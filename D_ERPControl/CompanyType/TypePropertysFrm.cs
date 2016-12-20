using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D_Lib.ErpInfo;
using D_Lib;
using UI.Res;
using System.Collections;

namespace D_ERPControl.CompanyType
{
    public partial class TypePropertysFrm : Form
    {
        CompanyTypeInfo TypeInfo;
        public TypePropertysFrm()
        {
            InitializeComponent();
        }

        public TypePropertysFrm(CompanyTypeInfo TypeInfo)
        {
            InitializeComponent();
            this.TypeInfo = TypeInfo;
            this.Text = "【"+TypeInfo.aaTypename+"】属性集合";
        }

        private void TypePropertysFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("sno", "序号");
            dataGridView1.Columns.Add("propertyname", "属性名称");
            dataGridView1.Columns.Add("placename", "替代名称");
            Dictionary<string, object> param = new Dictionary<string, object>();
            search(param);
        }
        private void search(Dictionary<string, object> param)
        {
            dataGridView1.Rows.Clear();
            Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.GetCompanyTypeDetailsInfoUrl, param);
            if (dict["res"].ToString() == "1")
            {
                ArrayList infoList = (ArrayList)dict["companyTypeDetailsInfoList"];
                if (infoList != null)
                    for (int i = 0; i < infoList.Count; i++)
                    {
                        CompanyTypeDetailsInfo info = HttpTool.DictToObject<CompanyTypeDetailsInfo>(infoList[i]);
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.Tag = info;
                        dr.CreateCells(dataGridView1);
                        int index = dataGridView1.Rows.Add(dr);
                        dataGridView1.Rows[index].Cells[0].Value = info.aaSno;
                        dataGridView1.Rows[index].Cells[1].Value = info.aaPropertyname;
                        dataGridView1.Rows[index].Cells[2].Value = info.aaPlacename;
                    }
            }
            else
            {
                MessageBox.Show(dict["msg"].ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("确定删除？", "提示", MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
                {
                    CompanyTypeDetailsInfo info = dataGridView1.SelectedRows[0].Tag as CompanyTypeDetailsInfo;
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("id", info.aaId);
                    Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.DelCompanyTypeDetailsInfoUrl, param);
                    if (dict["res"].ToString() == "1")
                    {
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    }
                    else
                    {
                        MessageBox.Show(dict["msg"].ToString());
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int maxSno = 0;
            for (int i = 0; i < dataGridView1.Rows.Count;i++ )
            {
                CompanyTypeDetailsInfo info = dataGridView1.SelectedRows[0].Tag as CompanyTypeDetailsInfo;
                if (info.aaSno > maxSno)
                    maxSno = info.aaSno;
            }
            AddTypePropertysFrm addFrm = new AddTypePropertysFrm(TypeInfo,maxSno);
            if (addFrm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                search(param);
            }
        }

        private void 修改序号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                CompanyTypeDetailsInfo info = dataGridView1.SelectedRows[0].Tag as CompanyTypeDetailsInfo;
                UpdateSnoFrm updateFrm = new UpdateSnoFrm(info);
                if (updateFrm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    search(param);
                }
            }
        }

        private void 修改替代名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                CompanyTypeDetailsInfo info = dataGridView1.SelectedRows[0].Tag as CompanyTypeDetailsInfo;
                UpdatePlaceNameFrm updateFrm = new UpdatePlaceNameFrm(info);
                if (updateFrm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    search(param);
                }
            }
        }

    }
}


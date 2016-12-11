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
using UI.Res;
using D_Lib.ErpInfo;

namespace D_ERPControl.Property
{
    public partial class PropertyFrm : DockContent
    {
        public PropertyFrm()
        {
            InitializeComponent();
        }

        private void PropertyFrm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            this.dataGridView1.Columns.Add("propertyname","属性名");
            this.dataGridView1.Columns.Add("propertytype", "属性类型");
            this.dataGridView1.Columns.Add("createtime", "录入时间");
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dictionary<string, object> param = new Dictionary<string, object>();
            Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.GetProductPropertyUrl, param);
            if (dict["res"].ToString() == "1")
            {
                List<PropertyInfo> infoList = (List<PropertyInfo>)dict["propertyInfoList"];
                if (infoList!=null)
                for (int i = 0; i < infoList.Count;i++ )
                {
                    PropertyInfo info = infoList[i];
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(dataGridView1);
                    dr.Cells[0].Value = info.aaPropertyname;
                    dr.Cells[1].Value = info.aaPropertytype=="0"?"文字":"列表";
                    dr.Cells[2].Value = info.aaCreatetime;
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
            AddPropertyFrm addFrm = new AddPropertyFrm();
            addFrm.Show();
        }
    }
}

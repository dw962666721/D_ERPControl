﻿using System;
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
using System.Collections;
using System.Web.Script.Serialization;

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
            search(param);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("propertyname", textBox1.Text);
            param.Add("propertytype", comboBox2.SelectedIndex);
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
        private void button2_Click(object sender, EventArgs e)
        {
            AddPropertyFrm addFrm = new AddPropertyFrm();
            addFrm.Show();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                AddPropertyFrm addFrom = new AddPropertyFrm(dataGridView1.SelectedRows[0].Tag as PropertyInfo);
                addFrom.ShowDialog();
            }
        }
    }
}

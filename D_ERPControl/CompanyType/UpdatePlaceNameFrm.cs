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

namespace D_ERPControl.CompanyType
{
    public partial class UpdatePlaceNameFrm : Form
    {
        CompanyTypeDetailsInfo companyTypeDetailsInfo;
        public UpdatePlaceNameFrm()
        {
            InitializeComponent();
        }

        public UpdatePlaceNameFrm(CompanyTypeDetailsInfo info)
        {
            InitializeComponent();
            this.companyTypeDetailsInfo = info;
            textBox1.Text = info.aaPlacename.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", companyTypeDetailsInfo.aaId);
            param.Add("placename", textBox1.Text);
            Dictionary<string, object> dict = HttpTool.Post(UrlList.ServerUrl + UrlList.UpdateCompanyTypeDetailsInfoUrl, param);
            if (dict["res"].ToString() == "1")
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(dict["msg"].ToString());
            }
        }
    }
}

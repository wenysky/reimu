using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NConvert
{
    public partial class MoreSetting : Form
    {
        public MoreSetting()
        {
            InitializeComponent();
        }

        private void btnSetMoreInfo_Click(object sender, EventArgs e)
        {
            MainForm.PageSize = Convert.ToInt32(tbxPageSize.Text.Trim());
            MainForm.PostsTableSize = Convert.ToInt32(tbxPostsTableSize.Text.Trim());
            this.Close();
        }

        private void MoreSetting_Load(object sender, EventArgs e)
        {
            tbxPageSize.Text = MainForm.PageSize.ToString();
            tbxPostsTableSize.Text = MainForm.PostsTableSize.ToString();
        }
    }
}

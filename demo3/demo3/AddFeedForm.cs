using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo3
{
    public partial class AddFeedForm : Form
    {
        private readonly NewsFeedManager _newsManager;
        public bool Haschanges { get; set; }
        public AddFeedForm(NewsFeedManager newsManager)
        {
            InitializeComponent();
            _newsManager = newsManager;
        }

        private void AddFeedForm_Load(object sender, EventArgs e)
        {
            var publishers = _newsManager.GetNewFeed();
            foreach (var item in publishers)
            {
                cbbToaSoan.Items.Add(item.Name);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var pulishName = cbbToaSoan.Text;
            var categoryName = txtChuyenMuc.Text;
            var rsslink = txtLink.Text;

            if (string.IsNullOrWhiteSpace(pulishName) || string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(rsslink))
            {
                MessageBox.Show("không được bỏ trống ô nào!", "Error");
                return;
            }

            Haschanges = true;

            var success = _newsManager.AddCategory(pulishName, categoryName, rsslink, false);
            if (success)
            {
                _newsManager.SaveChange();
                Haschanges = true;
                ClearForm();
                return;
            }

            if (MessageBox.Show("Chuyên mục này đã tồn tại , bạn có muốn cấp nhật RSS link không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Haschanges = true;
                _newsManager.AddCategory(pulishName, categoryName, rsslink, true);
                _newsManager.SaveChange();
            }

            ClearForm();
        }
        private void ClearForm()
        {
            cbbToaSoan.Text = "";
            txtChuyenMuc.Text = "";
            txtLink.Text = "";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

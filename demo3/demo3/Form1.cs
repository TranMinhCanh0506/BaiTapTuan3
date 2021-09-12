using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using demo3.Models;
using demo3.ID;
using demo3.conponents;

namespace demo3
{
    public partial class MainForm : Form
    {
        private readonly NewsFeedManager _newsManager;


        public MainForm(NewsFeedManager newsManager)
        {
            InitializeComponent();
            _newsManager = newsManager;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowFeedOnTreeView(_newsManager.GetNewFeed());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dialog = new AddFeedForm(_newsManager);
            dialog.ShowDialog(this);

            if (dialog.Haschanges)
            {
                _newsManager.SaveChange();
                ShowFeedOnTreeView(_newsManager.GetNewFeed());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (tvwPublisher.SelectedNode == null) return;
            if (tvwPublisher.SelectedNode.Level == 0)
            {
                _newsManager.RemovePublish(tvwPublisher.SelectedNode.Text);
            }
            else
            {
                var publishNode = tvwPublisher.SelectedNode.Parent;
                _newsManager.RemoveCate(publishNode.Text, tvwPublisher.SelectedNode.Text);
            }
            tvwPublisher.SelectedNode.Remove();
        }

        private void tvwPublisher_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pnlNews.Controls.Clear();
            if(e.Node.Level == 1)
            {
                var acticles = _newsManager.GetNews(e.Node.Parent.Text, e.Node.Text);
                foreach (var acticle in acticles)
                {
                    var item = new NewsControl();
                    item.Size = new Size(519, 101);
                    item.Dock = DockStyle.Top;
                    item.SetArticle(acticle);

                    pnlNews.Controls.Add(item);
                }
            }
           
        }
        private void ShowFeedOnTreeView(List<Publisher> publisher)
        {
            tvwPublisher.Nodes.Clear();
            pnlNews.Controls.Clear();

            foreach (var pub in publisher)
            {
                var publisherNode = tvwPublisher.Nodes.Add(pub.Name);
                foreach (var cate in pub.Categories)
                {
                    publisherNode.Nodes.Add(cate.Name);
                }
            }
            tvwPublisher.ExpandAll();
        }
    }
}

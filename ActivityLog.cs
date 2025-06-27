using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace St10443034ChatbotP3
{
    public partial class ActivityLog : Form
    {
        public ActivityLog()
        {
            InitializeComponent();
        }
        private MainForm parent;
        private List<string> activityLog = new List<string>();

        public ActivityLog(MainForm parent)
        {
            this.parent = parent;
        }

        public void AddAction(string action)
        {
            activityLog.Add($"[{DateTime.Now:MM/dd/yyyy HH:mm}] {action}");
        }

        public void ShowLog()
        {
            var logForm = new Form
            {
                Text = "Activity Log",
                Size = new Size(400, 300),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterParent
            };

            var logListBox = new ListBox
            {
                Location = new Point(20, 20),
                Size = new Size(340, 220),
                Font = new Font("Arial", 10)
            };
            var recentLogs = activityLog.TakeLast(10).ToList();
            foreach (var log in recentLogs)
            {
                logListBox.Items.Add(log);
            }

            logForm.Controls.Add(logListBox);
            logForm.ShowDialog();
            AddAction("Viewed activity log");
        }
    }
}

  

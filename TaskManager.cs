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
    public partial class TaskManager : Form
    {
        public TaskManager()
        {
            InitializeComponent();
        }
        private MainForm parent;
        private List<TaskItem> tasks = new List<TaskItem>();

        public TaskManager(MainForm parent)
        {
            this.parent = parent;
        }

        public void ParseTaskCommand(string input)
        {
            string title = "New Task";
            string description = "";
            DateTime? reminder = null;

            if (input.Contains("remind"))
            {
                if (input.Contains("tomorrow"))
                    reminder = DateTime.Now.AddDays(1);
                else if (input.Contains("in 3 days"))
                    reminder = DateTime.Now.AddDays(3);
                else if (input.Contains("in 7 days"))
                    reminder = DateTime.Now.AddDays(7);
            }

            if (input.Contains("password"))
                description = "Update password to ensure account security.";
            else if (input.Contains("2fa") || input.Contains("two-factor"))
                description = "Enable two-factor authentication for enhanced security.";
            else if (input.Contains("privacy"))
                description = "Review account privacy settings to protect data.";
            else
                description = input.Replace("add task", "").Trim();

            var task = new TaskItem { Title = title, Description = description, Reminder = reminder };
            tasks.Add(task);
            parent.AddToLog($"Task added: {title} - {description}" + (reminder.HasValue ? $" (Reminder: {reminder.Value:MM/dd/yyyy})" : ""));
            parent.AppendToChat($"Task added: '{description}'" + (reminder.HasValue ? $" Reminder set for {reminder.Value:MM/dd/yyyy}." : ""), Color.Green);
            parent.Speak($"Task added: {description}.");
        }

        public void ShowTaskForm()
        {
            var taskForm = new Form
            {
                Text = "Add New Task",
                Size = new Size(400, 300),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterParent
            };

            var titleLabel = new Label { Text = "Task Title:", Location = new Point(20, 20), Size = new Size(100, 20) };
            var titleBox = new TextBox { Location = new Point(120, 20), Size = new Size(250, 20) };

            var descLabel = new Label { Text = "Description:", Location = new Point(20, 50), Size = new Size(100, 20) };
            var descBox = new TextBox { Location = new Point(120, 50), Size = new Size(250, 20) };

            var reminderLabel = new Label { Text = "Reminder Date:", Location = new Point(20, 80), Size = new Size(100, 20) };
            var reminderPicker = new DateTimePicker { Location = new Point(120, 80), Size = new Size(250, 20), Format = DateTimePickerFormat.Short };

            var saveButton = new Button
            {
                Text = "Save Task",
                Location = new Point(120, 120),
                Size = new Size(100, 30),
                BackColor = Color.DarkGreen,
                ForeColor = Color.White
            };
            saveButton.Click += (s, e) =>
            {
                var task = new TaskItem
                {
                    Title = string.IsNullOrWhiteSpace(titleBox.Text) ? "New Task" : titleBox.Text,
                    Description = descBox.Text,
                    Reminder = reminderPicker.Value > DateTime.Now ? reminderPicker.Value : (DateTime?)null
                };
                tasks.Add(task);
                parent.AddToLog($"Task added: {task.Title} - {task.Description}" + (task.Reminder.HasValue ? $" (Reminder: {task.Reminder.Value:MM/dd/yyyy})" : ""));
                parent.AppendToChat($"Task added: '{task.Description}'" + (task.Reminder.HasValue ? $" Reminder set for {task.Reminder.Value:MM/dd/yyyy}." : ""), Color.Green);
                parent.Speak($"Task added: {task.Description}.");
                taskForm.Close();
            };

            taskForm.Controls.AddRange(new Control[] { titleLabel, titleBox, descLabel, descBox, reminderLabel, reminderPicker, saveButton });
            taskForm.ShowDialog();
        }

        public void ShowTasks()
        {
            var taskForm = new Form
            {
                Text = "Task List",
                Size = new Size(600, 400),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterParent
            };

            var taskListBox = new ListBox
            {
                Location = new Point(20, 20),
                Size = new Size(540, 300),
                Font = new Font("Arial", 10)
            };
            foreach (var task in tasks)
            {
                taskListBox.Items.Add($"{task.Title}: {task.Description}" + (task.Reminder.HasValue ? $" (Reminder: {task.Reminder.Value:MM/dd/yyyy})" : ""));
            }

            var completeButton = new Button
            {
                Text = "Mark Complete",
                Location = new Point(20, 330),
                Size = new Size(120, 30),
                BackColor = Color.DarkGreen,
                ForeColor = Color.White
            };
            completeButton.Click += (s, e) =>
            {
                if (taskListBox.SelectedIndex >= 0)
                {
                    var task = tasks[taskListBox.SelectedIndex];
                    tasks.RemoveAt(taskListBox.SelectedIndex);
                    taskListBox.Items.RemoveAt(taskListBox.SelectedIndex);
                    parent.AddToLog($"Task completed: {task.Title}");
                    parent.AppendToChat($"Task '{task.Title}' marked as completed.", Color.Green);
                    parent.Speak($"Task {task.Title} marked as completed.");
                }
            };

            var deleteButton = new Button
            {
                Text = "Delete Task",
                Location = new Point(150, 330),
                Size = new Size(120, 30),
                BackColor = Color.DarkRed,
                ForeColor = Color.White
            };
            deleteButton.Click += (s, e) =>
            {
                if (taskListBox.SelectedIndex >= 0)
                {
                    var task = tasks[taskListBox.SelectedIndex];
                    tasks.RemoveAt(taskListBox.SelectedIndex);
                    taskListBox.Items.RemoveAt(taskListBox.SelectedIndex);
                    parent.AddToLog($"Task deleted: {task.Title}");
                    parent.AppendToChat($"Task '{task.Title}' deleted.", Color.Green);
                    parent.Speak($"Task {task.Title} deleted.");
                }
            };

            taskForm.Controls.AddRange(new Control[] { taskListBox, completeButton, deleteButton });
            taskForm.ShowDialog();
        }
    }
}
    

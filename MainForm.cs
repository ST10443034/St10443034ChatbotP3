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
    public partial class MainForm : Form
    {
        private System.Speech.Synthesis.SpeechSynthesizer synth = new System.Speech.Synthesis.SpeechSynthesizer();
        private string userName = "";
        private string favouriteTopic = "";
        private bool useVoice = true;
        private bool useSoundEffects = true;
        private CyberSecurityChatbot chatbot;
        private TaskManager taskManager;
        private QuizGame quizGame;
        private ActivityLog activityLog;
        private RichTextBox chatDisplay;

        public MainForm()
        {
            chatbot = new CyberSecurityChatbot();
            taskManager = new TaskManager(this);
            quizGame = new QuizGame(this);
            activityLog = new ActivityLog(this);
            InitializeComponent();
        }

        public void AppendToChat(string text, Color? color = null)
        {
            chatDisplay.SelectionColor = color ?? Color.Black;
            chatDisplay.AppendText(text + "\n");
            chatDisplay.ScrollToCaret();
        }

        public void AddToLog(string action)
        {
            activityLog.AddAction(action);
        }

        public void Speak(string text)
        {
            if (useVoice) synth.SpeakAsync(text);
        }

        private void InitializeComponent()
        {
            Text = "Cybersecurity Awareness Chatbot";
            Size = new Size(800, 600);
            BackColor = Color.FromArgb(240, 240, 240);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            var headerLabel = new Label
            {
                Text = "CYBERSECURITY AWARENESS CHATBOT",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkCyan,
                Location = new Point(20, 20),
                Size = new Size(760, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            chatDisplay = new RichTextBox
            {
                Name = "chatDisplay",
                Location = new Point(20, 60),
                Size = new Size(500, 400),
                ReadOnly = true,
                Font = new Font("Arial", 10),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle
            };

            var inputBox = new TextBox
            {
                Name = "inputBox",
                Location = new Point(20, 470),
                Size = new Size(500, 30),
                Font = new Font("Arial", 10)
            };
            inputBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ProcessInput(inputBox.Text);
                    inputBox.Clear();
                    e.SuppressKeyPress = true;
                }
            };

            var sendButton = new Button
            {
                Text = "Send",
                Location = new Point(530, 470),
                Size = new Size(100, 30),
                BackColor = Color.DarkCyan,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            sendButton.Click += (s, e) => { ProcessInput(inputBox.Text); inputBox.Clear(); };

            var addTaskButton = new Button
            {
                Text = "Add Task",
                Location = new Point(530, 60),
                Size = new Size(100, 30),
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            addTaskButton.Click += (s, e) => taskManager.ShowTaskForm();

            var viewTasksButton = new Button
            {
                Text = "View Tasks",
                Location = new Point(530, 100),
                Size = new Size(100, 30),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            viewTasksButton.Click += (s, e) => taskManager.ShowTasks();

            var startQuizButton = new Button
            {
                Text = "Start Quiz",
                Location = new Point(530, 140),
                Size = new Size(100, 30),
                BackColor = Color.DarkMagenta,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            startQuizButton.Click += (s, e) => quizGame.StartQuiz();

            var viewLogButton = new Button
            {
                Text = "View Log",
                Location = new Point(530, 180),
                Size = new Size(100, 30),
                BackColor = Color.DarkOrange,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            viewLogButton.Click += (s, e) => activityLog.ShowLog();

            var toggleVoiceButton = new Button
            {
                Text = "Toggle Voice",
                Location = new Point(530, 220),
                Size = new Size(100, 30),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            toggleVoiceButton.Click += (s, e) =>
            {
                useVoice = !useVoice;
                AppendToChat($"Voice {(useVoice ? "enabled" : "disabled")}.", Color.Cyan);
                AddToLog($"Voice {(useVoice ? "enabled" : "disabled")}");
            };

            Controls.AddRange(new Control[] { headerLabel, chatDisplay, inputBox, sendButton, addTaskButton, viewTasksButton, startQuizButton, viewLogButton, toggleVoiceButton });

            Load += (s, e) =>
            {
                AppendToChat("Welcome to the Cybersecurity Awareness Chatbot!\nWhat's your name?");
                if (useSoundEffects) System.Media.SystemSounds.Beep.Play();
                Speak("Hello! Welcome to the Cybersecurity Awareness Bot.");
            };
        }

        private void ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                AppendToChat("Please enter something.", Color.Red);
                return;
            }

            input = input.ToLower().Trim();
            AddToLog($"User input: {input}");

            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                if (string.IsNullOrWhiteSpace(userName))
                {
                    AppendToChat("Please enter a valid name.", Color.Red);
                    return;
                }
                AppendToChat($"Hello {userName}! Type 'help' for options or ask about cybersecurity.", Color.Green);
                Speak($"Hello {userName}, let's talk about cybersecurity.");
                return;
            }

            if (input == "exit")
            {
                AppendToChat("Thank you for using the Cybersecurity Awareness Chatbot. Stay safe online!", Color.Green);
                Speak("Thank you for using the Cybersecurity Awareness Chatbot. Stay safe online!");
                Application.Exit();
                return;
            }

            if (input == "help")
            {
                AppendToChat("I can help with:\n- Password safety\n- Phishing scams\n- Privacy and data protection\n- Safe browsing\n- Social media security\n- SIM swap fraud\n- Add tasks\n- Start quiz\n- View activity log\nType 'exit' to quit.", Color.Cyan);
                return;
            }

            if (input.Contains("add task") || input.Contains("remind"))
            {
                taskManager.ParseTaskCommand(input);
                return;
            }

            if (input.Contains("start quiz") || input.Contains("quiz"))
            {
                quizGame.StartQuiz();
                return;
            }

            if (input.Contains("show activity log") || input.Contains("what have you done"))
            {
                activityLog.ShowLog();
                return;
            }

            string response = chatbot.GetResponse(input, out string topicDetected, out string sentiment);
            if (!string.IsNullOrEmpty(topicDetected) && string.IsNullOrEmpty(favouriteTopic))
            {
                favouriteTopic = topicDetected;
                AppendToChat($"[Memory] Noted that you're interested in {favouriteTopic}.", Color.Cyan);
                AddToLog($"Detected favorite topic: {favouriteTopic}");
            }

            if (!string.IsNullOrEmpty(sentiment))
            {
                AppendToChat($"[Mood Detected: {sentiment}]", Color.Magenta);
            }

            if (!string.IsNullOrEmpty(favouriteTopic) && !input.Contains(favouriteTopic))
            {
                response += $"\nAs someone interested in {favouriteTopic}, always keep learning about that topic.";
            }

            AppendToChat($"Bot: {response}", Color.Green);
            Speak(response);
        }
    }
}


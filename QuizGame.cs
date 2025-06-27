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
    public partial class QuizGame : Form
    {
        public QuizGame()
        {
            InitializeComponent();
        }
        private MainForm parent;
        private List<QuizQuestion> questions;
        private int currentQuestionIndex;
        private int score;

        public QuizGame(MainForm parent)
        {
            this.parent = parent;
            questions = new List<QuizQuestion>
            {
                new QuizQuestion("What should you do if you receive an email asking for your password?", new[] {"Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it"}, 2, "Reporting phishing emails helps prevent scams."),
                new QuizQuestion("Is it safe to use the same password for multiple accounts?", new[] {"True", "False"}, 1, "Using the same password increases risk if one account is compromised."),
                new QuizQuestion("What does MFA stand for?", new[] {"Multi-Factor Authentication", "Main Firewall Access", "Mobile File Analysis", "Multiple File Archives"}, 0, "MFA adds an extra layer of security."),
                new QuizQuestion("Should you click links in unsolicited emails?", new[] {"Yes", "No"}, 1, "Unsolicited links may lead to phishing or malware."),
                new QuizQuestion("What is a strong password?", new[] {"Your birthdate", "A mix of letters, numbers, and symbols", "Your pet’s name", "A single word"}, 1, "Strong passwords are complex and hard to guess."),
                new QuizQuestion("What is phishing?", new[] {"A type of fish", "A scam to steal personal info", "A secure email service", "A firewall technique"}, 1, "Phishing tricks users into sharing sensitive data."),
                new QuizQuestion("Is public Wi-Fi always safe?", new[] {"True", "False"}, 1, "Public Wi-Fi can be vulnerable to attacks; use a VPN."),
                new QuizQuestion("What should you do to secure your social media?", new[] {"Share everything publicly", "Use weak passwords", "Enable privacy settings", "Post your address"}, 2, "Privacy settings limit who can see your data."),
                new QuizQuestion("What is a SIM swap fraud?", new[] {"Swapping phones", "Stealing phone numbers for access", "Upgrading SIM cards", "A type of email scam"}, 1, "SIM swap fraud targets your phone number to bypass security."),
                new QuizQuestion("Should you update software regularly?", new[] {"Yes", "No"}, 0, "Updates patch security vulnerabilities.")
            };
        }

        public void StartQuiz()
        {
            currentQuestionIndex = 0;
            score = 0;
            parent.AddToLog("Quiz started");
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                string feedback = score >= 8 ? "Great job! You’re a cybersecurity pro!" : score >= 5 ? "Good effort! Keep learning to stay safe." : "Keep studying to boost your cybersecurity skills!";
                parent.AppendToChat($"Quiz completed! Your score: {score}/{questions.Count}\n{feedback}", Color.Green);
                parent.AddToLog($"Quiz completed with score {score}/{questions.Count}");
                parent.Speak($"Quiz completed! Your score: {score} out of {questions.Count}. {feedback}");
                return;
            }

            var question = questions[currentQuestionIndex];
            var quizForm = new Form
            {
                Text = "Cybersecurity Quiz",
                Size = new Size(400, 300),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterParent
            };

            var questionLabel = new Label
            {
                Text = question.QuestionText,
                Location = new Point(20, 20),
                Size = new Size(340, 40),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            var answerGroup = new GroupBox
            {
                Location = new Point(20, 70),
                Size = new Size(340, 150)
            };

            var radioButtons = new RadioButton[question.Options.Length];
            for (int i = 0; i < question.Options.Length; i++)
            {
                radioButtons[i] = new RadioButton
                {
                    Text = question.Options[i],
                    Location = new Point(10, 20 + i * 30),
                    Size = new Size(300, 20),
                    Font = new Font("Arial", 10)
                };
            }
            answerGroup.Controls.AddRange(radioButtons);

            var submitButton = new Button
            {
                Text = "Submit",
                Location = new Point(20, 230),
                Size = new Size(100, 30),
                BackColor = Color.DarkMagenta,
                ForeColor = Color.White
            };
            submitButton.Click += (s, e) =>
            {
                int selectedIndex = -1;
                for (int i = 0; i < radioButtons.Length; i++)
                {
                    if (radioButtons[i].Checked)
                    {
                        selectedIndex = i;
                        break;
                    }
                }

                if (selectedIndex == -1)
                {
                    MessageBox.Show("Please select an answer.");
                    return;
                }

                if (selectedIndex == question.CorrectAnswerIndex)
                {
                    score++;
                    parent.AppendToChat("Correct! " + question.Feedback, Color.Green);
                    parent.Speak("Correct! " + question.Feedback);
                }
                else
                {
                    parent.AppendToChat("Incorrect. " + question.Feedback, Color.Red);
                    parent.Speak("Incorrect. " + question.Feedback);
                }

                parent.AddToLog($"Answered quiz question {currentQuestionIndex + 1}: {(selectedIndex == question.CorrectAnswerIndex ? "Correct" : "Incorrect")}");
                currentQuestionIndex++;
                quizForm.Close();
                ShowNextQuestion();
            };

            quizForm.Controls.AddRange(new Control[] { questionLabel, answerGroup, submitButton });
            quizForm.ShowDialog();
        }
    }
}
   
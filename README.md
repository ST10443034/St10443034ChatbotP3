Cybersecurity Awareness Chatbot (St10443034ChatbotP3)
Overview
The Cybersecurity Awareness Chatbot is a Windows Forms application designed to educate users on cybersecurity best practices through an interactive chatbot, task management, a quiz mini-game, and activity logging. Built for .NET Framework 4.7.2 or 4.8, the project provides a user-friendly interface with enhanced visual styling, including modern fonts, colors, and chat bubble effects, to improve user experience.
This project was developed as part of an academic assignment (Student ID: St10443034) and supports both text-based interaction and voice output using text-to-speech functionality. It includes features like task reminders, a cybersecurity quiz, and activity logging, all styled with a consistent, modern aesthetic.
Features

Interactive Chatbot:
Responds to user queries on cybersecurity topics (e.g., password safety, phishing, privacy, safe browsing, social media security, SIM swap fraud).
Detects user sentiment (e.g., worried, frustrated, curious) and provides tailored responses.
Remembers the user’s favorite topic for personalized responses.
Displays messages with timestamps, distinct user (bold, blue) and bot (green) styling, and chat bubble-like backgrounds.


Task Manager:
Allows users to add tasks with titles, descriptions, and optional reminders (e.g., “add task enable 2fa”).
Supports viewing and managing tasks (mark complete, delete) in a styled form.


Quiz Mini-Game:
Presents 10 multiple-choice questions on cybersecurity.
Features a modern interface with progress indicators, highlighted answers (green for correct, red for incorrect), and detailed feedback.


Activity Log:
Tracks user actions (e.g., input, task additions, quiz answers) with timestamps.
Displays the last 10 actions in a clean, styled list.


Voice Output:
Uses System.Speech.Synthesis to read bot responses aloud.
Toggleable via a “Toggle Voice” button.


Visual Enhancements:
Modern Segoe UI font and color scheme (blue, green, white backgrounds).
Flat-style buttons and chat bubbles for a polished look.
Progress bar in the quiz interface for better UX.
Clear chat button to reset the conversation.



Project Structure
The project consists of the following C# files:

MainForm.cs: Main GUI with chat interface, buttons, and input handling.
CyberSecurityChatbot.cs: Logic for chatbot responses and sentiment detection.
TaskManager.cs: Manages task creation, viewing, and deletion.
QuizGame.cs: Handles the quiz mini-game with styled question forms.
ActivityLog.cs: Logs and displays user actions.
TaskItem.cs: Data model for tasks.
QuizQuestion.cs: Data model for quiz questions.
Program.cs: Application entry point.

Prerequisites

Visual Studio: Version 2019 or 2022 (Community, Professional, or Enterprise).
.NET Framework: 4.7.2 or 4.8.
Operating System: Windows (due to Windows Forms and System.Speech dependencies).
References:
System.Windows.Forms
System.Drawing
System.Speech
System.Linq



Setup Instructions

Clone or Download the Project:

If using a version control system (e.g., Git), clone the repository:git clone <repository-url>


Alternatively, download and extract the project files.


Open in Visual Studio:

Open Visual Studio and select File > Open > Project/Solution.
Navigate to the project directory and open St10443034ChatbotP3.sln.


Verify Project Settings:

In Solution Explorer, right-click the project (St10443034ChatbotP3) > Properties.
Ensure:
Output type: Windows Application
Target framework: .NET Framework 4.7.2 or 4.8




Add References:

In Solution Explorer, right-click References > Add Reference.
Under Assemblies > Framework, check:
System.Windows.Forms
System.Drawing
System.Speech
System.Linq


Click OK.


Verify Project Files:

Ensure Solution Explorer contains only:
MainForm.cs
CyberSecurityChatbot.cs
TaskManager.cs
QuizGame.cs
ActivityLog.cs
TaskItem.cs
QuizQuestion.cs
Program.cs


Delete any extra files (e.g., MainForm.Designer.cs, MainForm.resx).


Build the Project:

Go to Build > Clean Solution, then Build > Rebuild Solution (or press Ctrl+Shift+B).
Check Error List (View > Error List) for any issues.


Run the Application:

Press F5 or Debug > Start Debugging to launch the chatbot.



Usage

Launch the Application:

The application opens with a welcome message: “Welcome to the Cybersecurity Awareness Chatbot! What’s your name?”
Enter your name in the input box and press Enter or click Send.


Interact with the Chatbot:

Type queries related to cybersecurity (e.g., “password”, “phishing”, “privacy”).
Use commands like:
help: Lists available topics and commands.
add task <description>: Adds a task (e.g., “add task enable 2fa”).
start quiz: Begins the quiz.
show activity log: Displays recent actions.
exit: Closes the application.


Messages appear with timestamps and styled as user (blue, bold) or bot (green) with chat bubble backgrounds.


Use Buttons:

Add Task: Opens a form to create a task with title, description, and optional reminder.
View Tasks: Shows all tasks with options to mark complete or delete.
Start Quiz: Launches a 10-question quiz with modern styling and answer feedback.
View Log: Displays the last 10 logged actions.
Toggle Voice: Enables/disables text-to-speech.
Clear Chat: Resets the chat display.


Quiz Mini-Game:

Answer questions by selecting a radio button and clicking Submit.
Correct answers highlight in green, incorrect in red, with feedback displayed in the chat.
A progress bar shows your position (e.g., Question 1/10).



Visual Enhancements
The application features a modern interface:

Fonts: Uses Segoe UI for a clean, professional look.
Colors: Blue (#0078D7) for bot messages, green (#00C853) for tasks, and white/light backgrounds for readability.
Chat Bubbles: User messages have a light blue background, bot messages have light green.
Quiz Interface: Includes a progress bar, highlighted answers, and consistent styling.

Notes

Assignment Context: The project fulfills an academic requirement allowing “XAML / Windows Forms.” This implementation uses Windows Forms for compatibility and simplicity. A WPF version is available upon request for more advanced styling (e.g., animations, smoother layouts).
Text-to-Speech: Requires a Windows environment with System.Speech support. Ensure speakers or headphones are connected for voice output.
Extensibility: To add more features (e.g., additional quiz questions, regex-based input parsing), modify CyberSecurityChatbot.cs or QuizGame.cs.

Troubleshooting

Build Errors:
Ensure all required references are added.
Verify the target framework is .NET Framework 4.7.2 or 4.8.
Delete any designer files (MainForm.Designer.cs, MainForm.resx).
Clean and rebuild the solution (Build > Clean Solution, then Build > Rebuild Solution).


No Voice Output:
Check that System.Speech is referenced.
Ensure the system’s audio is not muted.


Visual Issues:
Confirm the Segoe UI font is available (default on Windows).
Adjust form sizes in MainForm.cs, QuizGame.cs, etc., if controls are misaligned.



Credits

Developed by: Student ID St10443034
Built with: Visual Studio, .NET Framework 4.7.2/4.8, C#, Windows Forms
Purpose: Academic project for cybersecurity awareness education

For questions or contributions, contact the developer or submit a pull request/issue in the repository.

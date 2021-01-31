using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Collections.ObjectModel;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine(new CultureInfo("en-us"));
        SpeechSynthesizer Jarvis = new SpeechSynthesizer();
        SpeechRecognitionEngine startListening = new SpeechRecognitionEngine();
        SpeechRecognitionEngine startProgram = new SpeechRecognitionEngine();
        SpeechRecognitionEngine reminder = new SpeechRecognitionEngine();
        SpeechRecognitionEngine reminderTask = new SpeechRecognitionEngine();
        SpeechRecognitionEngine url = new SpeechRecognitionEngine();
        Random rnd = new Random();
        int RecTimeOut = 0;
        string commandsTxt = @"C:\-4thGrade-\AI\Projekat2\Vestacka Inteligencija\WindowsFormsApp1\WindowsFormsApp1\DefaultCommands.txt";
        string programsTxt = @"C:\-4thGrade-\AI\Projekat2\Vestacka Inteligencija\WindowsFormsApp1\WindowsFormsApp1\Programs.txt";
        List<Reminder> reminders = new List<Reminder>();
        Reminder currentReminder;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            _recognizer.SetInputToDefaultAudioDevice();
            
            Jarvis.SelectVoice("Microsoft David Desktop");
            
            _recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(commandsTxt))))); //File.ReadAllLines(commandsTxt)
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(DefaultSpeechRecognized);
            _recognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

            startListening.SetInputToDefaultAudioDevice();
            startListening.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(commandsTxt)))));
            startListening.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startListening_SpeechRecognized);

            startProgram.SetInputToDefaultAudioDevice();
            startProgram.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(programsTxt)))));
            startProgram.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startProgram_SpeechRecognized);

            reminder.SetInputToDefaultAudioDevice();
            Choices days = new Choices(new string[] { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth",
                "Tenth", "Eleventh", "Twelfth", "Thirteenth", "Fourteenth", "Fifteenth", "Sixteenth", "Seventeenth", "Eighteenth", "Nineteenth",
                "Twentieth", "Twenty-first", "Twenty-second", "Twenty-third", "Twenty-fourth", "Twenty-fifth", "Twenty-sixth", "Twenty-seventh",
                "Twenty-eighth", "Twenty-ninth", "Thirtieth", "Thirty-first" });
            Choices months = new Choices(new string[] { "January", "February", "March", "April", "May", "Jun", "July", "August", "September", "October", "November", "December" });
            GrammarBuilder date = new GrammarBuilder();
            date.Append(days);
            date.Append("of");
            date.Append(months);
            reminder.LoadGrammar(new Grammar(date));
            reminder.BabbleTimeout = TimeSpan.FromSeconds(2);
            reminder.InitialSilenceTimeout = TimeSpan.FromSeconds(2);
            reminder.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(reminder_SpeechRecognized);

            reminderTask.SetInputToDefaultAudioDevice();
            Choices options = new Choices(new string[] {"Birthday", "Groceries", "Grocery", "Anniversary", "Premiere"});
            GrammarBuilder tasks = new GrammarBuilder();
            tasks.Append(options);

            reminderTask.LoadGrammar(new Grammar(tasks));
            reminderTask.BabbleTimeout = TimeSpan.FromSeconds(2);
            reminderTask.InitialSilenceTimeout = TimeSpan.FromSeconds(2);
            reminderTask.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(reminderTask_SpeechHypothesized);

            url.SetInputToDefaultAudioDevice();
            url.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "facebook", "youtube", "no" }))));
            url.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(url_SpeechHypothesized);

            Jarvis.SpeakAsync("Jarvis activated");
        }

        private void DefaultSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            int ranNum;
            string speech = e.Result.Text;

            switch (speech)
            {
                case "Hi":
                    Jarvis.SpeakAsync("Hello, I'm glad to hear your voice.");
                    break;
                case "How are you":
                    Jarvis.SpeakAsync("I'm working at full capacity.");
                    break;
                case "What time is it":
                    Jarvis.SpeakAsync(DateTime.Now.ToString("h mm tt"));
                    break;
                case "Stop talking":
                    Jarvis.SpeakAsyncCancelAll();
                    ranNum = rnd.Next(1, 100);
                    if (ranNum < 50)
                        Jarvis.SpeakAsync("Yes sir");
                    if (ranNum >= 50)
                        Jarvis.SpeakAsync("Sir yes sir");
                    break;
                case "Stop Listening":
                    Jarvis.SpeakAsync("If you need me just ask");
                    _recognizer.RecognizeAsyncCancel();
                    startListening.RecognizeAsync(RecognizeMode.Multiple);
                    break;
                case "Show commands":
                    string[] commands = (File.ReadAllLines(commandsTxt));

                    lstbxCommands.Items.Clear();
                    lstbxCommands.SelectionMode = SelectionMode.None;
                    lstbxCommands.Visible = true;
                    foreach (string cmd in commands)
                    {
                        lstbxCommands.Items.Add(cmd);
                    }
                    break;
                case "Hide commands":
                    lstbxCommands.Items.Clear();
                    break;
                case "Shutdown":
                    this.Dispose();
                    break;
                case "Open program":
                    Jarvis.SpeakAsync("Which program");
                    startProgram.RecognizeAsync(RecognizeMode.Single);
                    break;
                case "Hide yourself":
                    Jarvis.SpeakAsync("I'll be in the background.");
                    this.Opacity = 0;
                    break;
                case "Show yourself":
                    Jarvis.SpeakAsync("How may I assist you");
                    this.Opacity = 100;
                    break;
                case "SetReminder":
                    Jarvis.SpeakAsync("Which date");
                    _recognizer.RecognizeAsyncCancel();
                    reminder.RecognizeAsync(RecognizeMode.Multiple);
                    break;
                case "Reminders":
                    foreach (Reminder r in reminders)
                        Jarvis.SpeakAsync(r.What + " on the " + r.When);
                    break;
            }
        }

        private void _recognizer_SpeechRecognized(object sender, SpeechDetectedEventArgs e)
        {
            RecTimeOut = 0;
        }

        private void startListening_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if( speech == "Wake up" || speech == "Jarvis")
            {
                startListening.RecognizeAsyncCancel();
                Jarvis.SpeakAsync("Yes, I am here");
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void startProgram_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            switch (speech)
            {
                case "notepad":
                    Process.Start("notepad");
                    break;
                case "google chrome":
                    Jarvis.SpeakAsync("Do you want to open a concrete page?");
                    url.RecognizeAsync(RecognizeMode.Multiple);
                    _recognizer.RecognizeAsyncCancel();
                    break;
                case "command prompt":
                    Process.Start("cmd");
                    break;
                case "control panel":
                    Process.Start("control");
                    break;
            }
        }

        private void tmrSpeaking_Tick(object sender, EventArgs e)
        {
            if(RecTimeOut == 10)
            {
                _recognizer.RecognizeAsyncCancel();
            }
            else if(RecTimeOut == 11)
            {
                tmrSpeaking.Stop();
                startListening.RecognizeAsync(RecognizeMode.Multiple);
                RecTimeOut = 0;
            }
        }

        private void reminder_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if(e.Result.Text == null)
            {
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
                reminder.RecognizeAsyncCancel();
                return;
            }
            string speech = e.Result.Text;
            currentReminder = new Reminder();
            currentReminder.When = speech;
            reminder.RecognizeAsyncCancel();
            reminderTask.RecognizeAsync(RecognizeMode.Multiple);
            Jarvis.SpeakAsync("Reminder for");
        }

        private void reminderTask_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            string speech = e.Result.Text;
            if(speech == null)
            {
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
                reminderTask.RecognizeAsyncCancel();
                return;
            }
            currentReminder.What = "You have a " + speech;
            reminders.Add(currentReminder);
            reminderTask.RecognizeAsyncCancel();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            Jarvis.SpeakAsync("Reminder set for" + currentReminder.When);
        }

        private void url_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == null)
            {
                url.RecognizeAsyncCancel();
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
                return;
            }
            switch (speech)
            {
                case "facebook":
                    Process.Start("chrome", "https://www.facebook.com/");
                    break;
                case "youtube":
                    Process.Start("chrome", "https://www.youtube.com");
                    break;
                case "no":
                    Process.Start("chrome");
                    break;
            }
            url.RecognizeAsyncCancel();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

    }
}

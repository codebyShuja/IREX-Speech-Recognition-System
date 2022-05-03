using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO.Ports;

namespace IREX
{
    public partial class voice_system : Form 
    {
        SerialPort port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);
        string Town,Temperature, Condition, WindSpeed, TFCond, TFHigh, TFLow;
        //=================================================================================================================================
        //========================== Function to get weather information from yahoo and store in string====================================
        //=================================================================================================================================
        public void GetWeather()
        {
            string query = String.Format(@"http://weather.yahooapis.com/forecastrss?w=12743763");
            XmlDocument wData = new XmlDocument();
            wData.Load(query);
            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");
            XmlNode channel = wData.SelectSingleNode("rss").SelectSingleNode("channel");
            XmlNodeList nodes = wData.SelectNodes("/rss/channel/item/yweather:forecast", manager);

            //I handle NullReferenceException error using if(vs != Null) and vs is object of this form (voice_system)
            voice_system vs = new voice_system();
            if (vs != null)
            {
                Temperature = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
                Condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;
                WindSpeed = channel.SelectSingleNode("yweather:wind", manager).Attributes["speed"].Value;
                Town = channel.SelectSingleNode("yweather:location", manager).Attributes["city"].Value;
                TFCond = channel.SelectSingleNode("item", manager).SelectSingleNode("yweather:forecast", manager).Attributes["text"].Value;
                TFHigh = channel.SelectSingleNode("item", manager).SelectSingleNode("yweather:forecast", manager).Attributes["high"].Value;
                TFLow = channel.SelectSingleNode("item", manager).SelectSingleNode("yweather:forecast", manager).Attributes["low"].Value;
            }
            
        }
        //=================================================================================================================================
        //I declare two classes one for understand my voice and convert it into computer binary or ASCII code , so processor can understand 
        // ==============================my input. And second will output in the form of Human voice======================================= 
        //=================================================================================================================================
        SpeechRecognitionEngine voiceInput = new SpeechRecognitionEngine();
        SpeechSynthesizer IREX_Speaking = new SpeechSynthesizer();

        browser brow;
        public voice_system()
        {
            voiceInput.SetInputToDefaultAudioDevice();
            voiceInput.LoadGrammar(new DictationGrammar());
            voiceInput.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(System.IO.File.ReadAllLines(@"C:\IREX\commands.txt")))));
            voiceInput.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(voiceInput_SpeechRecognized);
            IREX_Speaking.Rate = -3;
            IREX_Speaking.Volume = 100;
            IREX_Speaking.SelectVoiceByHints(VoiceGender.Male);

            InitializeComponent();
            brow = new browser(this);
            
        }

        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000001,
            SHERB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath,
        RecycleFlags dwFlags);


        private void voice_system_Load(object sender, EventArgs e)
        {
          /*  Choices commands = new Choices();
            //=================================================================================================================================
            //=================================Create a grammar that IREX will understand to answer me=========================================
            //=================================================================================================================================
            commands.Add(new string[] {"hello how are you","give your intro","fine thank you","shutdown","start again", "what day it is",
                "tell me time","my data", "open music folder", "download folder", "document file","open idm","open i d m","weather condition",
                "tell me room condition","internet","find it","load again","go to home","back","forward","open face book","out look mail",
                "holy book","holly book","login face book","enter email and password","enter it","open ucp portal","detect my face","check gmail","operating system",
                "window run time" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);
            voiceInput.LoadGrammarAsync(grammar);   
            voiceInput.SetInputToDefaultAudioDevice();
            voiceInput.SpeechRecognized += voiceInput_SpeechRecognized;
            //=================================================================================================================================
            //========================================== Set the speaking rate and volume======================================================
            //=================================================================================================================================
            IREX_Speaking.Rate = -3;
            IREX_Speaking.Volume = 100;
            IREX_Speaking.SelectVoiceByHints(VoiceGender.Male);*/

        }
        //=================================================================================================================================
        //========================= Function to give voice input to IREX and get voice + perfomance output from it ========================
        //=================================================================================================================================
        public void voiceInput_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "hello how are you":
                    IREX_Speaking.Speak("Hello sir how are you how can i help you today");
                    break;
                case "hello":
                   /* port.Open();
                    port.Write("0");
                    port.Close();*/
                    break;

                case "give your intro":
                    IREX_Speaking.Speak("I am ireex, developed by Ali Shuja Sardar, i am a artificial intelligence robot, i can detect human voice and convert it into binary , hexa or ASCII characters, to perfom tasks according to your voice order, ");
                    break;
                case "fine thank you":
                    IREX_Speaking.SpeakAsync("that's great sir don't waste time sir let start work");
                    break;
               case "shutdown":
                    IREX_Speaking.Speak("as you want sir process starting , good bye sir , Sir you disable shutdown property due to your presentation");
                    // Process.Start("shutdown", "/s /t 0");
                    break;
                case "start again":
                    IREX_Speaking.SpeakAsync("as you want sir process starting , good bye sir, sir you disable restart property due to your presentation");
                    // Process.Start("shutdown", "/r /t 0");
                    break;
                case "what day it is":
                    // IREX_Speaking.SpeakAsync(DateTime.Now.ToString(""));
                    IREX_Speaking.SpeakAsync(System.DateTime.Today.ToShortDateString());
                    break;
                case "tell me time":
                    //IREX_Speaking.SpeakAsync(DateTime.Now.ToString(""));
                    IREX_Speaking.SpeakAsync(DateTime.Now.ToString("HH:mm"));
                    break;
                case "my data":
                    IREX_Speaking.SpeakAsync("okay sir opening your data drive");
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                    break;
                case "open music folder":
                    IREX_Speaking.SpeakAsync("okay sir enjoy some music");
                   Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic));
                    break;
                case "download folder":
                    IREX_Speaking.SpeakAsync("Sir opening download folder for you , you have download many files");
                    Process.Start("shell:Downloads");
                    break;
                case "document file":
                    IREX_Speaking.SpeakAsync("Sir opening document folder for you , there are your assignments and some other important documents");
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
                    break;
                case "open idm":
                case "open i d m":
                    IREX_Speaking.SpeakAsync("Sir opening download folder for you , you have download many files");
                    if (File.Exists(@"C:\Program Files(x86)\Internet Download Manager\IDMan.exe"))
                    {
                        Process.Start(@"C:\Program Files (x86)\Internet Download Manager\IDMan.exe");
                    }
                    IREX_Speaking.SpeakAsync("Sir download manager is not install in this system");
                    break;
              /*  case "minimize window":
                    IREX_Speaking.SpeakAsync("I aplogize sir i am minimizing window");
                    this.WindowState = FormWindowState.Minimized;
                    break;*/
                case "weather condition":
                case "tell me room condition":
                    IREX_Speaking.SpeakAsync("OKay sir let me analyze weather");
                    int Out;
                   if (InternetGetConnectedState(out Out, 0) == true)
                    {
                        GetWeather();
                        IREX_Speaking.SpeakAsync("Sit i have calcualted weather condition , Weather condition of" + Town + ", is" + Condition + "and temprature is, " + Temperature + "fahrenheit");
                   }
                    else
                    {
                      MessageBox.Show("Internet connection not available");
                      IREX_Speaking.SpeakAsync("Sir your laptop not connected with internet connection, please connect with internet");
                    }
                   
                    break;
            /* case "tomorrow weather condition":
                    IREX_Speaking.SpeakAsync("sir analyzing tomorrow condition");
                    GetWeather();
                    IREX_Speaking.Speak("Weather condition of" + Town + "will" + TFCond + "and temprature" + TFHigh + "and" + TFLow);
                    break; */
                case "internet":
                    /*   IREX_Speaking.SpeakAsync("OK sir i am opening your developed browser for you");
                       if (brow.IsDisposed == true)
                       {
                           brow = new browser(this);
                       }
                       brow.Show();*/
                    port.Open();
                    port.Write("o");
                    port.Close();
                    break;
            }
            // my input. And second will output in the form of Human voice.
            // Voice input and computer voice output for browser coding start here...................................................
            switch (e.Result.Text)
            {
               case "find it":
                    IREX_Speaking.SpeakAsync("sir searching plaese wait some seconds");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.sear_ching();
                    break;
                    case "load again":
                    IREX_Speaking.SpeakAsync("Sir refreshing current page for you");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.refershing();
                    break;
                case "go to home":
                    IREX_Speaking.SpeakAsync("Sir going back to home");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.go_home();
                    break;
              /*  case "back":
                    IREX_Speaking.SpeakAsync("ok sir going back 1 page");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.go_back();
                    break; */
                case "forward":
                    IREX_Speaking.SpeakAsync("OK sir moving");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.go_forward();
                    break;
                case "open face book":
                    IREX_Speaking.SpeakAsync("OK sir have fun and update status, not forget to mention me in your fb status sir,  sorry just kidding");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.facebook();
                    break;
                case "out look mail":
                    IREX_Speaking.SpeakAsync("OK sir, there are some new mails in your inbox sir");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.outlook();
                    break;
                case "holy book":
                case "holly book":
                    IREX_Speaking.SpeakAsync("OK sir, I am Opening Islamic holy book");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.Quran_Pak();
                    break;
                case "login face book":
                    IREX_Speaking.SpeakAsync("OK sir, entering your facebook email address and password");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.login_facebook();
                    break;
                case "enter email and password":
                    IREX_Speaking.SpeakAsync("OK sir, entering your outlook email address and password");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.login_Outlook();
                    break;
            }
                    switch (e.Result.Text)
                    {
                        // case "open page":
                        //     IREX_Speaking.SpeakAsync("okay sir opening new tab");
                        //     brow.newtab();
                        //     break;
                        case "enter it":
                            IREX_Speaking.SpeakAsync("okay sir entering it");
                            SendKeys.Send("~");
                            break;
                       case "open ucp portal":
                            IREX_Speaking.SpeakAsync("sir opening university portal, You can check your subjects marks here");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.portal();
                            break;
                        case "detect my face":
                            IREX_Speaking.SpeakAsync("Sir i am under development,once this function completes, i will able to detect people faces and will able to tell you their name");
                            break;
                        case "check gmail":
                            IREX_Speaking.SpeakAsync("Sir opening google mail");
                    if (brow.IsDisposed == true)
                    {
                        brow = new browser(this);
                    }
                    brow.gMail();
                            break;
                  /*      case "name of window user":
                            System_Information sys = new System_Information();
                            sys.UserName();
                            IREX_Speaking.SpeakAsync("Sir computer user name is" + sys.user_name);
                            break;  */
                        case "operating system":
                            System_Information os = new System_Information();
                            os.o_s();
                            IREX_Speaking.SpeakAsync("Sir operating system is" + os.O_S);
                            break;
                        case "window run time":
                            System_Information TM = new System_Information();
                            TM.TotalMinutes();
                            IREX_Speaking.SpeakAsync("you started your system " + TM.total_minutes+ "minutes ago");
                            break;
                    }
            }
        //=====================================================================================================================================
        //========================================== IREX Main Window function defination======================================================
        //=====================================================================================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            voiceInput.RecognizeAsyncStop();
           
        }
        private void EnableIREX_Click(object sender, EventArgs e)
        {
            voiceInput.RecognizeAsync(RecognizeMode.Multiple);
        }
        private void folderbutton_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles));
        }

        private void data_file_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles));
        }
        private void weatherbutton_Click(object sender, EventArgs e)
        {
            IREX_Speaking.SpeakAsync("OKay sir let me analyze weather");
            GetWeather();
            IREX_Speaking.SpeakAsync("Sit i have calcualted weather condition , Weather condition of" + Town + ", is" + Condition + "and temprature is, " + Temperature + "fahrenheit");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        { 
       GetWeather();
       IREX_Speaking.SpeakAsync("Sir our Location is," +Town);
        }
        private void browserclick_Click(object sender, EventArgs e)
        {
            if (brow.IsDisposed == true)
            {
                brow = new browser(this);
            }
            brow.Show();
        }

    }
}

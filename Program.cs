using System;
using System.Diagnostics;
using System.Windows;
using NDesk.Options;



namespace Clippi_B
{
    class Program
    {
        public static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage:");
            p.WriteOptionDescriptions(Console.Out);
        }

        [STAThread]
        static void Main(string[] args)
        {
            Boolean text,help = false;
            string clipboardtext,previousclipboardtext = "";
            int interval = 5;
            int monitor = 480;
            var options = new OptionSet(){
                {"i|interval=","Intervaltimer in seconds to check if clipboard has contents (default every 5 seconds)",o=> interval =  Int32.Parse(o) },
                {"m|monitor=","How long this program should be ran in the background in minutes (default 8 hours)",o=> monitor = Int32.Parse(o)},
                {"h|?|help","Show Help", o => help = true}
            };

            try
            {
                options.Parse(args);

                if (help)
                {
                    ShowHelp(options);
                    return;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                ShowHelp(options);
                return;
            }

            Stopwatch monitortimer = new Stopwatch();
            Stopwatch intervaltimer = new Stopwatch();

            Info.Showbanner();
            Console.WriteLine("Monitoring the clipboard has been initialized! The clipboard will be analyzed every {0} seconds for {1} minutes ",interval,monitor);
            while (monitortimer.Elapsed.TotalMinutes < monitor)
            {
                intervaltimer.Start();
                if (intervaltimer.Elapsed.TotalSeconds == interval)
                {
                    text = Clipboard.ContainsText();
                    if (text)
                    {
                        clipboardtext = Clipboard.GetText(TextDataFormat.Text);
                        if(clipboardtext !=previousclipboardtext)
                        {Console.WriteLine("Clipboard contains text! contents:\n" + clipboardtext); }
                        previousclipboardtext=clipboardtext;
                        intervaltimer.Reset();
                    }
                }
            }
            monitortimer.Stop();
            intervaltimer.Stop();
        }
    }
}

using NDesk.Options;
using System;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Timers;
using System.Runtime.Remoting.Messaging;

namespace Clippi_B
{
    class Program
    {
    

        

        public static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage:");
            p.WriteOptionDescriptions(Console.Out);
        }

        static void Main(string[] args)
        {
            Boolean help = false;
            Info.Showbanner();
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

            //minutes * 60 (seconds) / interval
            int timestorun = (monitor * 60) / interval;
            string oldclipboardcontent = "";
            

            for (int i = 0; i < timestorun; i++)
            {
                Thread StaThread = new Thread(() => {
                oldclipboardcontent = Clippi(oldclipboardcontent);
            });
                StaThread.SetApartmentState(ApartmentState.STA);
                StaThread.Start();
                Thread.Sleep(interval * 1000);
            }
            
        }

        public static string Clippi(string previousclipboardtext)
        {
            Boolean text = false;
            string clipboardtext = "";
            if(String.IsNullOrEmpty(previousclipboardtext))
                previousclipboardtext = "";
            try
            {
                    text = Clipboard.ContainsText();
                if (text)
                {
                    clipboardtext = Clipboard.GetText(TextDataFormat.Text);
                    if (clipboardtext != previousclipboardtext)
                    { Console.WriteLine("Clipboard contains text! contents:\n" + clipboardtext); }
                    previousclipboardtext = clipboardtext;
                    return clipboardtext;
                }
                    
                }
            catch (Exception ex)
            { Console.WriteLine("ERROR:" + ex); } 
            return "";

        }
         

    }
        
}



using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using static System.Console;
using static System.Diagnostics.Process;
using static System.IO.Path;
using static System.Reflection.Assembly;
using static System.Threading.Thread;

namespace fixGame
{
    class Program
    {
        enum state
        {
            SUCCES,
            FAIL,
            UNDEFINED
        }

        static void cleanTheShit(string folder)
        {
            string fullPath = GetEntryAssembly().Location;
            string theDirectory = GetDirectoryName(fullPath) + "\\" + folder;
            System.IO.DirectoryInfo di = new DirectoryInfo(theDirectory);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }

        static void killTheShit()
        {
            WriteLine("Assassinat des processus fantômes...");
            foreach (Process proc in GetProcessesByName("farahlon64.game"))
                proc.Kill();
            foreach (Process proc in GetProcessesByName("farahlon32.game"))
                proc.Kill();
            Sleep(5 * 100);
            WriteLine("Maintenant, on attend 5 secondes parce que j'ai envie :D");
            Sleep(5 * 100);
            WriteLine("Non en fait c'est super important, parce que votre PC va trop vite pour moi >.<");
            Sleep(5 * 100);
            WriteLine("Allez, on compte ensemble !");
            for (int i = 5; i > 0; --i)
            {
                WriteLine(i);
                Sleep(1 * 1000);
            }
            WriteLine("Ouch, c'etait dur !");
            Sleep(5 * 100);
        }

        static state checkTheLogs()
        {
            Sleep(1 * 1000);
            string content;
            string fullPath = GetEntryAssembly().Location;
            string theDirectory = GetDirectoryName(fullPath) + "\\" + "Logs" + "\\" + "connection.log";
            FileStream logs;
            try
            {
                logs = File.Open(theDirectory, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch
            {
               return state.UNDEFINED;
            }
            var buffer_stream = new StreamReader(logs, Encoding.UTF8);
            content = buffer_stream.ReadToEnd();
            logs.Close();
            if (content.IndexOf("LOGIN_STATE_AUTHENTICATED") != -1)
                return state.SUCCES;
            else if (content.IndexOf("LOGIN_FAILED") != -1)
                return state.FAIL;
            return state.UNDEFINED;
        }


        static void Main(string[] args)
        {
            var status = state.UNDEFINED;
            do
            {
                WriteLine("----------------------------------------------");
                killTheShit();
                cleanTheShit("Cache");
                cleanTheShit("Logs");
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("Okay, ca devrait marcher, courrir meme !");
                WriteLine("Lancement du jeu ...");
                ResetColor();
                Start("FarahlonUpdater.exe");
                WriteLine("----------------------------------------------");
                ForegroundColor = ConsoleColor.Magenta;
                WriteLine("Laisse cette fenetre ouverte et connecte toi sur le jeu");
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("Attente de connexion .....");
                while ((status = checkTheLogs()) == state.UNDEFINED)
                    continue;
                if (status == state.FAIL)
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Aie Aie Aie. Ca n'a pas fonctionne, je quitte WoW");
                    ResetColor();
                }

            } while(status == state.FAIL);

            ForegroundColor = ConsoleColor.Green;
            WriteLine("YOUPIKAI ! Ca fonctionne :D");
            WriteLine("Tu peux fermer la fenêtre, bon jeu !");
            ResetColor();
            ReadKey();

        }
    }
}
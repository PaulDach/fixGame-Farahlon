using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace fixGame
{
    class Program
    {
        static void cleanTheShit(string folder)
        {
            string fullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string theDirectory = Path.GetDirectoryName(fullPath) + "\\" + folder;
            System.IO.DirectoryInfo di = new DirectoryInfo(theDirectory);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Assassinat des processus fantômes...");
            foreach (Process proc in Process.GetProcessesByName("farahlon64.game"))
                proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("farahlon32.game"))
                proc.Kill();
            Thread.Sleep(5 * 100);
            Console.WriteLine("C'est bon chef! Ils sont morts, on peut continuer è_é");
            Thread.Sleep(5 * 100);
            Console.WriteLine("Maintenant, on attend 5 secondes parce que j'ai envie :D");
            Thread.Sleep(5 * 100);
            Console.WriteLine("Non en fait c'est super important, parce que votre PC va trop vite pour moi >.<");
            Thread.Sleep(5 * 100);
            Console.WriteLine("Allez, on compte ensemble !");
            for (int i = 5; i > 0; --i)
            {
                Console.WriteLine(i);
                Thread.Sleep(1 * 1000);
            }
            Console.WriteLine("Ouch, c'etait dur !");
            Thread.Sleep(5 * 100);
            Console.WriteLine("Destructions du cache et des logs qui font tout bugger...");
            Thread.Sleep(1 * 100);
            cleanTheShit("Cache");
            cleanTheShit("Logs");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OUIIIIII C'EST FINI !");
            Console.WriteLine("Appuie sur Entrer pour lancer ton jeu !");
            Console.ResetColor();
            Console.ReadLine();
            System.Diagnostics.Process.Start("FarahlonUpdater.exe");
        }
    }
}
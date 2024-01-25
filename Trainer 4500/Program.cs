﻿/*
A program egy mozgas.txt állományba menti az adatokat, ebből dolgozik. Az állomány sorai
tabulátorral tagoltak, egy sorban a dátum (éééé.hh.nn formátumban) az edzés típusa(pl.
futás, úszás, kondi stb.) és az időtartam (percben) található.
A program által megvalósítandó funkciók
1. Új adat rögzítése:
Kérje be a dátumot, az edzés típusát és idejét a minta szerint:

(Választható extra: végezhet adatellenőrzést pl. a dátumformátumra)
2. Meglévő adatok kiíratása táblázatszerűen:
A forrásállományban szereplő adatokat írassa ki a minta szerint:

(Választható extra: rendezetten vagy adott érték szerint rendezetten)
3. Meglévő adatok kiíratása adott edzéstípusra szűrve
Az előző pont szerinti kiírást valósít meg, csak előbb megkérdezi, milyen edzéstípust
listázzon ki.
4. (Választható extra: statisztika kiíratása, vagyis edzéstípusonként adjuk meg az
edzések számát, összidejét, átlagos idejét, leghosszabb edzés idejét.)
Az egyes funkciók menüből választhatók legyenek. A feladat végrehajtása után térjünk
vissza a menübe. Az utolsó menüpont a kilépés legyen, ezzel lehessen kilépni a
programból.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Trainer_4500
{
    struct Edzes
    {
        public DateTime Date; // 2020. 10. 10. 10:10
        public string Type; // futás, kerékpár, úszás, súlyzós edzés
        public int Time; // perc
    }
    class Program
    {
        static List<Edzes> edzesList = new List<Edzes>();
        static void MainMenu()
        {
            string[] menuItems = { "Új edzés", "Összes adat", "Összes adat edzés szerint", "Exit" };
            int selectedOption = DisplayMenu(menuItems);
            Console.WriteLine($"You selected option {selectedOption + 1}");

            switch (selectedOption)
            {
                case 0:
                    NewTraining();
                    break;
                case 1:
                    AllData();
                    break;
                case 2:
                    AllDataFiltered();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        static void NewTraining()
        {
            Console.Clear();
            //Kérem adja meg a dátumot (éééé.hh.nn formátumban):
            //Kérem adja meg az edzés típusát:
            //Kérem adja meg az időtartamot (percben):
            DateTime date;
            string type;
            int time;
            Console.WriteLine("Új edzés");
            Console.WriteLine("Kérem adja meg a dátumot (éééé.hh.nn formátumban):");
            string input = Console.ReadLine();
            if (DateTime.TryParse(input, out date))
            {
                Console.WriteLine("Kérem adja meg az edzés típusát:");
                type = Console.ReadLine();
                Console.WriteLine("Kérem adja meg az időtartamot (percben):");
                input = Console.ReadLine();
                if (int.TryParse(input, out time))
                {
                    edzesList.Add(new Edzes() { Date = date, Type = type, Time = time });
                    Console.WriteLine("Sikeresen hozzáadva");
                    saveToFile(date, type, time);
                    Console.ReadKey();
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("Hibás időtartam");
                    Console.ReadKey();
                    MainMenu();
                }
            }
            else
            {
                Console.WriteLine("Hibás dátum");
                Console.ReadKey();
                MainMenu();
            }
        }

        static void saveToFile(DateTime date, string type, int time)
        {
            // Save data to file
            string path = @"C:\Users\krist\source\repos\Trainer 4500\Trainer 4500\bin\Debug\netcoreapp3.1\mozgas.txt";
            string[] lines = { date.ToString("yyyy.MM.dd"), type, time.ToString() };
            System.IO.File.AppendAllLines(path, lines);
        }

        static void AllData()
        {
            Console.Clear();
            // Show all data from edzesList in table
            Console.WriteLine("Összes adat \n");
            Console.WriteLine("{0,-20} {1,-20} {2,-20}", "Date", "Type", "Time");
            foreach (var edzes in edzesList)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-20}", edzes.Date, edzes.Type, edzes.Time);
            }
            Console.ReadKey();
            MainMenu();
        }

        static void AllDataFiltered()
        {
            Console.WriteLine("Összes adat edzés szerint");
            Console.ReadKey();
            MainMenu();
        }

        static void Main(string[] args)
        {
            //load data from mozgas.txt
            string path = @"mozgas.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (var line in lines)
            {
                string[] data = line.Split('\t');
                edzesList.Add(new Edzes() { Date = DateTime.Parse(data[0]), Type = data[1], Time = int.Parse(data[2]) });
            }
            
            MainMenu();
        }

        static int DisplayMenu(string[] menuItems)
        {
            int currentIndex = 0;

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < menuItems.Length; i++)
                {
                    Console.WriteLine((i == currentIndex ? ">" : " ") + menuItems[i]);
                }

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && currentIndex > 0)
                {
                    currentIndex--;
                } 
                else if (key == ConsoleKey.DownArrow && currentIndex < menuItems.Length - 1)
                {
                    currentIndex++;
                } 
                else if (key == ConsoleKey.Enter)
                {
                    return currentIndex;
                }
            }
        }
    }
}
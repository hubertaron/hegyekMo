using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hegyekmo
{
    class Program
    {
        struct hegyek
        {
            public string hegycsucsnev;
            public string hegysegnev;
            public int magassag;
        }
        static hegyek[] adatok = new hegyek[1000];
        static void Main(string[] args)
        {
            string[] fajlbol = File.ReadAllLines("hegyekMo.txt");
            int sorokszama = 0;
            int i, j;
            for (int k = 1; k < fajlbol.Count(); k++)
            {
                string[] egysordarabolva = fajlbol[k].Split(';');
                adatok[sorokszama].hegycsucsnev = egysordarabolva[0];
                adatok[sorokszama].hegysegnev = egysordarabolva[1];
                adatok[sorokszama].magassag = Convert.ToInt32(egysordarabolva[2]);
                sorokszama++;
            }
            int hegyekszama = sorokszama;
            Console.WriteLine("3. feladat: Hegycsúcsok száma: {0} db", hegyekszama);
            double atlagmagassag = 0;
            for (i = 0; i < hegyekszama; i++)
            {
                atlagmagassag += adatok[i].magassag;
            }
            Console.WriteLine("4. feladat: Hegycsúcsok átlagos magassága: {0} m", atlagmagassag / hegyekszama);


            int max = adatok[0].magassag;
            int maxi = 0;
            for (i = 0; i < hegyekszama; i++)
            {
                if (adatok[i].magassag > max)
                {
                    max = adatok[i].magassag;
                    maxi = i;
                }
            }

            Console.WriteLine("5. feladat: A legmagasabb hegycsúcs adatai:\n\tNév: {0}\n\tHegység: {1}\n\tMagasság: {2} m", adatok[maxi].hegycsucsnev, adatok[maxi].hegysegnev, adatok[maxi].magassag);

            int keresettmagassag;
            Console.Write("6. feladat: Kérek egy magasságot: ");
            keresettmagassag = Convert.ToInt32(Console.ReadLine());
            bool van;
            i = 0;
            while ((i < hegyekszama) && !(adatok[i].magassag > keresettmagassag))
            {
                i++;
            }



            van = i < hegyekszama ? true : false;
            if (van)
            {
                Console.WriteLine("\tVan {0} m-nél magasabb hegycsúcs.", keresettmagassag);
            }

            else
            {
                Console.WriteLine("\tNincs {0} m-nél magasabb hegycsúcs.", keresettmagassag);
            }

            int db = 0;
            for (i = 0; i < hegyekszama; i++)
            {
                if (adatok[i].magassag * 3.280839895 > 3000)
                {
                    db++;
                }
            }
            Console.WriteLine("7. feladat: 3000 lábnál magasabb hegycsúcsok száma: {0}", db);

            int kulonbozoelemekszama = 0;
            string[] hegysegnevek = new string[100];
            int[] hegysegekdb = new int[100];
            for (i = 0; i < 100; i++) hegysegekdb[i] = 0;
            for (i = 0; i < hegyekszama; i++)
            {
                j = 0;
                while ((j <= kulonbozoelemekszama) && (adatok[i].hegysegnev != hegysegnevek[j]))
                {
                    j++;
                }
                if (j > kulonbozoelemekszama)
                {
                    kulonbozoelemekszama++;
                    hegysegnevek[kulonbozoelemekszama] = adatok[i].hegysegnev;
                }
            }
            for (i = 0; i < hegyekszama; i++)
            {
                for (int k = 1; k <= kulonbozoelemekszama; k++)
                {
                    if (hegysegnevek[k] == adatok[i].hegysegnev) hegysegekdb[k]++;
                }
            }
            Console.WriteLine("8. feladat: Hegység statisztika");
            for (i = 1; i <= kulonbozoelemekszama; i++)
                Console.WriteLine("\t{0}: {1} db ", hegysegnevek[i], hegysegekdb[i]);

            Console.WriteLine("9. feladat: bukk-videk.txt");
            FileStream fnev = new FileStream("bukk-videk.txt", FileMode.Create);
            StreamWriter fajlbairo = new StreamWriter(fnev);
            double magassaglab;
            fajlbairo.WriteLine("Hegycsúcs neve;Magasság láb");
            for (i = 1; i <= hegyekszama; i++)
            {
                if (adatok[i].hegysegnev == "Bükk-vidék")
                {
                    magassaglab = Math.Round((adatok[i].magassag * 3.280839895), 1);
                    fajlbairo.Write("{0};", adatok[i].hegycsucsnev);
                    fajlbairo.Write("{0}", magassaglab);
                    fajlbairo.WriteLine("\n");
                }
            }
            fajlbairo.Close();
            fnev.Close();


            Console.ReadKey();

        }
    }
}

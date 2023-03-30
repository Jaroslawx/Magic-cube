using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Problem4
{
    class Program
    {
        // opis zmiennych: pat - wzór, tab - tablica, k - pomoc do indeksowania, m - ilosc znakow w pat, n - dlu/szer, x - miejsce w pat[], v - wersja
        static void evencomp(char[] pat, char[,] tab, int m, int k, int n, int x, int v) // - uzupełnianie kostki dla parz
        {
            if (n <= 0)
            {
                return;
            }
            // m, k, n | m, k, n
            // 9, 0, 5 | 9, 1, 3
            if (v == 0)
            {
                for (int j = k; j < n + k - 1; j++)
                {
                    tab[k, j] = pat[(x++) % m];
                }
                for (int i = k; i < n + k - 1; i++)
                {
                    tab[i, n - 1 + k] = pat[(x++) % m];
                }
                for (int j = n + k - 1; j > k - 1; j--)
                {
                    tab[n - 1 + k, j] = pat[(x++) % m];
                }
                for (int i = n + k - 2; i > k; i--)
                {
                    tab[i, k] = pat[(x++) % m];
                }
            }
            else if (v == 1)
            {
                for (int i = k; i < n + k - 1; i++)
                {
                    tab[i, n - 1 + k] = pat[(x++) % m];
                }
                for (int j = n + k - 1; j > k - 1; j--)
                {
                    tab[n - 1 + k, j] = pat[(x++) % m];
                }
                for (int i = n + k - 2; i > k; i--)
                {
                    tab[i, k] = pat[(x++) % m];
                }
                for (int j = k; j < n + k - 1; j++)
                {
                    tab[k, j] = pat[(x++) % m];
                }
            }
            else if (v == 2)
            {
                for (int j = n + k - 1; j > k; j--)
                {
                    tab[n - 1 + k, j] = pat[(x++) % m];
                }
                for (int i = n + k - 1; i > k; i--)
                {
                    tab[i, k] = pat[(x++) % m];
                }
                for (int j = k; j < n + k; j++)
                {
                    tab[k, j] = pat[(x++) % m];
                }
                for (int i = k + 1; i < n + k - 1; i++)
                {
                    tab[i, n - 1 + k] = pat[(x++) % m];
                }
            }
            else if (v == 3)
            {
                for (int i = n + k - 1; i > k; i--)
                {
                    tab[i, k] = pat[(x++) % m];
                }
                for (int j = k; j < n + k; j++)
                {
                    tab[k, j] = pat[(x++) % m];
                }
                for (int i = k + 1; i < n + k - 1; i++)
                {
                    tab[i, n - 1 + k] = pat[(x++) % m];
                }
                for (int j = n + k - 1; j > k; j--)
                {
                    tab[n - 1 + k, j] = pat[(x++) % m];
                }
            }

            evencomp(pat, tab, m, k + 1, n - 2, x, v);

        }
        // opis zmiennych: pat - wzór, tab - tablica, l - pomoc do indeksowania, m - ilosc znakow w pat, n - dlu/szer, x - miejsce w pat[]
        static void oddcomp(char[] pat, char[,] tab, int m, int l, int n, int h, int nhelp, int x) // - uzupełnianie kostki dla nparz
        {
            if (h >= nhelp)
            {
                return;
            }
            // m, l, n, h | m, l, n, h
            // 9, 2, 2, 2 | 9, 1, 4, 3
            for (int j = h; j >= l; j--)
            {
                tab[l, j] = pat[(x++) % m];
            }
            if (l > 0)
            {
                for (int i = l; i < h + 1; i++)
                {
                    tab[i, l - 1] = pat[(x++) % m];
                }

                for (int j = l - 1; j < h + 2; j++)
                {
                    tab[h + 1, j] = pat[(x++) % m];
                }
            }
            if (h < nhelp - 1)
            {
                for (int i = h; i >= l; i--)
                {
                    tab[i, h + 1] = pat[(x++) % m];
                }
            }

            oddcomp(pat, tab, m, l - 1, n + 2, h + 1, nhelp, x);

        }
        static void Main(string[] args)
        {
            int n, n2, m; // n - dlugosc i szerokosc tablicy; m - ilosc znakow w wzorze
            string help;

            FileStream outFile = new FileStream("Out0204.txt", FileMode.Truncate, FileAccess.Write); // otworzenie pliku i wyczyszczenie go
            using var writer = new StreamWriter(outFile);

            using (TextReader reader = File.OpenText("In0204.txt")) // sczytanie zmiennej n z pliku txt
            {
                n = int.Parse(reader.ReadLine());
                help = reader.ReadLine();
                help.ToCharArray();
            }

            char[,] tab = new char[n, n]; char[,] tab2 = new char[n, n];
            char[] pattern = new char[help.Length];
            pattern = help.ToCharArray();
            m = pattern.Length;

            writer.Write("T = ");
            foreach (char c in pattern)
            {
                writer.Write(c);
            }
            writer.Write(", n = {0}", n);
            writer.WriteLine(); writer.WriteLine();

            int k = 0, z = 0, h = 0; // k, z - do tablicy; index - pomoc do wypisywania z wzoru
            int index = 0, ver = 0; // zmienna do indexowania i wybierania wersji

            for (int w = 0; w < n; w++)
            {
                if (w % 2 == 0)
                {
                    evencomp(pattern, tab, m, k, n, index, 0); // zamiast 0 to w
                    writer.WriteLine("Tablica {0}", w);
                    for (int i = 0; i < n; i++)
                    {
                        //Console.Write('[');
                        writer.Write('[');
                        for (int j = 0; j < n; j++)
                        {
                            if (j == n - 1)
                            {
                                //Console.Write(tab[i, j]);
                                writer.Write(tab[i, j]);
                            }
                            else
                            {
                                //Console.Write(tab[i, j] + ", ");
                                writer.Write(tab[i, j] + ", ");
                            }
                            index++;
                        }
                        //Console.Write(']'); Console.WriteLine();
                        writer.Write(']'); writer.WriteLine();
                    }

                    ver++;

                }
                else if (w % 2 == 1)
                {
                    n2 = (n - 1) / 2;
                    h = (n - 1) / 2;
                    z = (n - 1) / 2;
                    oddcomp(pattern, tab2, m, z, n2, h, n, index);

                    writer.WriteLine("Tablica {0}", w);
                    for (int i = 0; i < n; i++)
                    {
                        //Console.Write('[');
                        writer.Write('[');
                        for (int j = 0; j < n; j++)
                        {
                            if (j == n - 1)
                            {
                                //Console.Write(tab2[i, j]);
                                writer.Write(tab2[i, j]);
                            }
                            else
                            {
                                //Console.Write(tab2[i, j] + ", ");
                                writer.Write(tab2[i, j] + ", ");
                            }
                            index++;
                        }
                        //Console.Write(']'); Console.WriteLine();
                        writer.Write(']'); writer.WriteLine();
                    }

                }

                writer.WriteLine();
                //Console.WriteLine();

                if (ver >= 4)
                {
                    ver = 0;
                }
            }

        }
    }
}

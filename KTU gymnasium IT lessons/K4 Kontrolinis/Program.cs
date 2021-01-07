using System;
using System.IO;
using System.Text;

namespace K4_Kontrolinis
{
    class Autorius
    {
        private string pavarde, vardas, knygosPavadinimas, leidykla;
        private double kaina;

        public Autorius()
        {
            pavarde = vardas = knygosPavadinimas = leidykla = "";
            kaina = -1;
        }

        public Autorius(string pavarde, string vardas, string knygosPavadinimas, string leidykla, double kaina)
        {
            this.pavarde = pavarde;
            this.vardas = vardas;
            this.knygosPavadinimas = knygosPavadinimas;
            this.leidykla = leidykla;
            this.kaina = kaina;
        }

        public string ImtiPavarde()
        {
            return pavarde;
        }

        public string ImtiVarda()
        {
            return vardas;
        }

        public string ImtiKnygosPavadinima()
        {
            return knygosPavadinimas;
        }

        public string ImtiLeidykla()
        {
            return leidykla;
        }

        public double ImtiKaina()
        {
            return kaina;
        }

        public static bool operator >=(Autorius a, Autorius b)
        {
            if (a.kaina == b.kaina)
            {
                if (a.pavarde == b.pavarde)
                {
                    return string.Compare(a.vardas, b.vardas) >= 0;
                }
                return string.Compare(a.pavarde, b.pavarde) >= 0;
            }
            return a.kaina >= b.kaina;
        }

        public static bool operator <=(Autorius a, Autorius b)
        {
            if (a.kaina == b.kaina && a.pavarde == b.pavarde && a.vardas == b.vardas)
            {
                return true;
            }
            return !(a >= b);
        }

        public static bool operator >(Autorius a, Autorius b)
        {
            if (a.kaina == b.kaina)
            {
                if (a.pavarde == b.pavarde)
                {
                    return string.Compare(a.vardas, b.vardas) > 0;
                }
                return string.Compare(a.pavarde, b.pavarde) > 0;
            }
            return a.kaina > b.kaina;
        }

        public static bool operator <(Autorius a, Autorius b)
        {
            if (a.kaina == b.kaina && a.pavarde == b.pavarde && a.vardas == b.vardas)
            {
                return false;
            }
            return !(a > b);
        }

        public override string ToString()
        {
            return string.Format("{0, 20} {1, 15} {2, 20} {3, 10} {4, 6}", pavarde, vardas, knygosPavadinimas, 
                leidykla, kaina);
        }
    }

    class Mazgas
    {
        public Autorius Duomenys
        {
            get; set;
        }

        public Mazgas Kitas
        {
            get; set;
        }

        public Mazgas()
        {
            Duomenys = null;
            Kitas = null;
        }

        public Mazgas(Autorius a, Mazgas m)
        {
            Duomenys = a;
            Kitas = m;
        }
    }

    class Autoriai
    {
        private Mazgas first, last, cur;

        public Autoriai()
        {
            first = last = cur = null;
        }

        public Autorius ImtiDuomenis()
        {
            return cur.Duomenys;
        }

        public string ImtiPaskutinioLeidykla()
        {
            return last.Duomenys.ImtiLeidykla();
        }

        public void Pradzia()
        {
            cur = first;
        }

        public bool ArYra()
        {
            return cur != null;
        }

        public void Kitas()
        {
            cur = cur.Kitas;
        }

        public void DetiDesinen(Autorius a)
        {
            Mazgas naujas = new Mazgas(a, null);
            if (first == null)
            {
                first = last = naujas;
            }
            else
            {
                last.Kitas = naujas;
                last = last.Kitas;
            }
        }

        public void Naikinti()
        {
            while (first != null)
            {
                Mazgas temp = first;
                first = first.Kitas;
                temp.Kitas = null;
                temp.Duomenys = null;
                temp = null;
            }
        }

        public void BubbleSort()
        {
            bool done = false;
            while (!done)
            {
                done = true;
                for (Pradzia(); ArYra() && cur != last; Kitas())
                {
                    if (cur.Duomenys > cur.Kitas.Duomenys)
                    {
                        Autorius pirmas = cur.Duomenys;
                        Autorius antras = cur.Kitas.Duomenys;
                        Autorius pirmoKopija = new Autorius(pirmas.ImtiPavarde(), pirmas.ImtiVarda(), 
                            pirmas.ImtiKnygosPavadinima(), pirmas.ImtiLeidykla(), pirmas.ImtiKaina());
                        Autorius antroKopija = new Autorius(antras.ImtiPavarde(), antras.ImtiVarda(), 
                            antras.ImtiKnygosPavadinima(), antras.ImtiLeidykla(), antras.ImtiKaina());
                        cur.Duomenys = antroKopija;
                        cur.Kitas.Duomenys = pirmoKopija;
                        done = false;
                    }
                }
            }
        }
    }

    class Program
    {
        static Autoriai Skaityti(string file)
        {
            Autoriai ret = new Autoriai();
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] dalys = line.Split();
                    string pavarde = dalys[0], vardas = dalys[1], pavadinimas = dalys[2], leidykla = dalys[3];
                    double kaina = double.Parse(dalys[4]);
                    Autorius naujas = new Autorius(pavarde, vardas, pavadinimas, leidykla, kaina);
                    ret.DetiDesinen(naujas);
                }
            }
            return ret;
        }

        static void Spausdinti(Autoriai aut, string line)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine("{0, 20} {1, 15} {2, 20} {3, 10} {4, 6}", "Pavardė", "Vardas", 
                "Knygos pavadinimas", "Leidykla", "Kaina");
            for (aut.Pradzia(); aut.ArYra(); aut.Kitas())
            {
                Console.WriteLine(aut.ImtiDuomenis());
            }
            Console.WriteLine();
        }

        static Autoriai FormuotiNauja(Autoriai aut, string ReikiamaLeidykla)
        {
            Autoriai ret = new Autoriai();
            for (aut.Pradzia(); aut.ArYra(); aut.Kitas())
            {
                if (aut.ImtiDuomenis().ImtiLeidykla() == ReikiamaLeidykla)
                {
                    Autorius pirmas = aut.ImtiDuomenis();
                    Autorius cur = new Autorius(pirmas.ImtiPavarde(), pirmas.ImtiVarda(), 
                        pirmas.ImtiKnygosPavadinima(), pirmas.ImtiLeidykla(), pirmas.ImtiKaina());
                    ret.DetiDesinen(cur);
                }
            }
            return ret;
        }

        static Autorius RastiBrangiausiaKnyga(Autoriai aut, string pavarde, string vardas)
        {
            Autorius ret = new Autorius();
            double maxKaina = -1;
            for (aut.Pradzia(); aut.ArYra(); aut.Kitas())
            {
                Autorius a = aut.ImtiDuomenis();
                if (a.ImtiPavarde() == pavarde && a.ImtiVarda() == vardas && a.ImtiKaina() > maxKaina)
                {
                    maxKaina = a.ImtiKaina();
                    ret = a;
                }
            }
            return ret;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Autoriai A = Skaityti("input.txt");
            Spausdinti(A, "Pradinis sąrašas");
            A.BubbleSort();
            Spausdinti(A, "Išrikiuotas sąrašas");
            Autoriai Naujas = FormuotiNauja(A, A.ImtiPaskutinioLeidykla());
            Spausdinti(Naujas, "Naujas sąrašas");
            Console.WriteLine("Įveskite norimo autoriaus pavardę ir vardą (atskirtus tarpu): ");
            string line = Console.ReadLine();
            string[] dalys = line.Split();
            string pavarde = dalys[0], vardas = dalys[1];
            Autorius brangiausia = RastiBrangiausiaKnyga(Naujas, pavarde, vardas);
            if (brangiausia.ImtiKaina() == -1)
            {
                Console.WriteLine("Sąraše nėra nei vienos autoriaus {0} {1} knygos.", pavarde, vardas);
            }
            else
            {
                Console.WriteLine("Informacija apie brangiausią autoriaus {0} {1} knygą: ", pavarde, vardas);
                Console.WriteLine("{0, 20} {1, 15} {2, 20} {3, 10} {4, 6}", "Pavardė", "Vardas", "Knygos pavadinimas",
                "Leidykla", "Kaina");
                Console.WriteLine(brangiausia);
            }
            Naujas.Naikinti();
        }
    }
}

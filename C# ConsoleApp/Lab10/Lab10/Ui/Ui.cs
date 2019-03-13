using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Service;
using Lab10.Domain;
using Lab10.Validation;

namespace Lab10.Ui
{
    class IUi
    {
        public IUi(IService Serv)
        {
            this.Serv = Serv;
        }

        private IService Serv { get; set; }

        public void Menu()
        {
            Console.WriteLine("\nAlegeti:");
            Console.WriteLine("0: Exit");
            Console.WriteLine("1: Operatii student");
            Console.WriteLine("2: Operatii tema");
            Console.WriteLine("3: Operatii nota");
            Console.WriteLine("4: Filtrari si Rapoarte");
            Console.WriteLine("Saptamana curenta: " + Serv.SaptCurenta);
            Console.WriteLine();
        }

        public void MenuStudent()
        {
            Console.WriteLine("\n0: Inapoi");
            Console.WriteLine("p: Print studenti");
            Console.WriteLine("1: Adauga student");
            Console.WriteLine("2: Modifica student");
            Console.WriteLine("3: Sterge student");
            Console.WriteLine("Saptamana curenta: " + Serv.SaptCurenta);
            Console.WriteLine();
        }

        public void MenuTema()
        {
            Console.WriteLine("\n0: Inapoi");
            Console.WriteLine("p: Print teme");
            Console.WriteLine("1: Adauga tema");
            Console.WriteLine("2: Modifica deadline");
            Console.WriteLine("Saptamana curenta: " + Serv.SaptCurenta);
            Console.WriteLine();
        }

        public void MenuNota()
        {
            Console.WriteLine("\n0: Inapoi");
            Console.WriteLine("p: Print note");
            Console.WriteLine("1: Adauga nota");
            Console.WriteLine("Saptamana curenta: " + Serv.SaptCurenta);
            Console.WriteLine();
        }

        public void MenuFilRap()
        {
            Console.WriteLine("\n0: Inapoi");
            Console.WriteLine("1: Notele la o anumita tema");
            Console.WriteLine("2: Notele unui student");
            Console.WriteLine("3: Notele studentilor dintr-o grupa, la o tema");
            Console.WriteLine("4: Notele studentilor dintr-o anumita grupa");
            Console.WriteLine();
        }


        public void AdaugaStudent()
        {
            Console.Write("Dati Id: ");
            string id = Console.ReadLine();
            Console.Write("Dati nume: ");
            string nume = Console.ReadLine();
            Console.Write("Dati grupa: ");
            string grupa = Console.ReadLine();
            Console.Write("Dati email: ");
            string email = Console.ReadLine();

            Serv.SaveS(int.Parse(id), nume, int.Parse(grupa), email);
            Console.WriteLine("Student adaugat cu succes!");
        }

        public void ModificaStudent()
        {
            Console.Write("dati id: ");
            string id = Console.ReadLine();
            Console.Write("dati noul nume: ");
            string nume = Console.ReadLine();
            Console.Write("dati noua grupa: ");
            string grupa = Console.ReadLine();
            Console.Write("dati noul email: ");
            string email = Console.ReadLine();

            Serv.UpdateS(int.Parse(id), nume, int.Parse(grupa), email);
            Console.WriteLine("Student modificat cu succes!");
        }

        public void StergeStudent()
        {
            Console.Write("Dati Id pentru stergere: ");
            string id = Console.ReadLine();
            Serv.DeleteS(int.Parse(id));
            Console.WriteLine("Student sters cu succes!");
        }

        public void PrintStudenti()
        {
            foreach (Student s in Serv.FindAllS())
            {
                Console.WriteLine(s);
            }
        }

        public void AdaugaTema()
        {
            Console.Write("Dati Id: ");
            string id = Console.ReadLine();
            Console.Write("Dati descriere: ");
            string desc = Console.ReadLine();
            Console.Write("Dati deadline: ");
            string deadline = Console.ReadLine();
            Console.Write("Dati saptamana primire: ");
            string primire = Console.ReadLine();

            Serv.SaveT(int.Parse(id), desc, int.Parse(deadline), int.Parse(primire));
            Console.WriteLine("Tema adaugata cu succes!");
        }

        public void ModificaDeadlineTema()
        {
            Console.Write("Dati Id: ");
            string id = Console.ReadLine();

            Console.Write("Dati nr de saptamani adaugate la deadline: ");
            string nr = Console.ReadLine();

            Serv.PrelungireTermen(int.Parse(id), int.Parse(nr));
            Console.WriteLine("Deadline modificat!");
        }

        public void PrintTeme()
        {
            foreach (Tema s in Serv.FindAllT())
            {
                Console.WriteLine(s);
            }
        }

        public void AdaugaNota()
        {
            Console.Write("Dati Id student: ");
            string idS = Console.ReadLine();
            Console.Write("Dati Id tema: ");
            string idT = Console.ReadLine();
            float notaMax = 10f;

            try
            {
                notaMax = Serv.AplicaIntarzieriN(int.Parse(idS), int.Parse(idT), 10);
                Console.Write("Nota maxima posibila este: " + notaMax);

                Console.Write("\nDati nota: ");
                string nota = Console.ReadLine();
                Console.Write("Dati nume cadru didactic: ");
                string numeC = Console.ReadLine();
                Console.Write("Dati un feedback: ");
                string feed = Console.ReadLine();

                Console.WriteLine("S-a adaugata cu succes nota " + Serv.SaveN(int.Parse(idS), int.Parse(idT), float.Parse(nota), numeC, feed) + " !");
            }
            catch (ServiceException)
            {
                Console.WriteLine("TerMenul limita este depasit! Se va trece nota 1!");
                Console.Write("Dati nume cadru didactic: ");
                string numeC = Console.ReadLine();
                Console.WriteLine("S-a adaugata cu succes nota " + Serv.SaveN(int.Parse(idS), int.Parse(idT), 1f, numeC, "S-a depasit terMenul de predare!") + " !");
            }
        }

        public void PrintNote()
        {
            foreach (Nota s in Serv.FindAllN())
            {
                Console.WriteLine(s);
            }
        }

        public void Filtru1()
        {
            Console.Write("Dati id-ul temei: ");
            int idTema = int.Parse(Console.ReadLine());
            foreach (Nota x in Serv.Filtru1(idTema))
            {
                Console.WriteLine(x);
            }
        }

        public void Filtru2()
        {
            Console.Write("Dati id-ul studentului: ");
            int idStud = int.Parse(Console.ReadLine());
            foreach (Nota x in Serv.Filtru2(idStud))
            {
                Console.WriteLine(x);
            }
        }

        public void Filtru3()
        {
            Console.Write("Dati grupa: ");
            int grupa = int.Parse(Console.ReadLine());
            Console.Write("Dati id-ul temei: ");
            int idTema = int.Parse(Console.ReadLine());
            foreach (Nota x in Serv.Filtru3(grupa,idTema))
            {
                Console.WriteLine(x);
            }
        }

        public void Filtru4()
        {
            Console.Write("Dati grupa: ");
            int grupa = int.Parse(Console.ReadLine());
            foreach (DTO x in Serv.Filtru4(grupa))
            {
                Console.WriteLine(x);
            }
        }

        public void Run()
        {
            try
            {
                Console.Write("Introduceti saptamana curenta: ");
                Serv.SaptCurenta = int.Parse(Console.ReadLine());
            }
            catch (System.IO.IOException) { }

            while (true)
            {
                try
                {
                    Menu();
                    string o = Console.ReadLine();

                    switch (o)
                    {
                        case "0":
                            return;
                        case "1":
                            MenuStudent();
                            string oo = Console.ReadLine();

                            switch (oo)
                            {
                                case "0":
                                    break;
                                case "1":
                                    AdaugaStudent();
                                    break;
                                case "2":
                                    ModificaStudent();
                                    break;
                                case "3":
                                    StergeStudent();
                                    break;
                                case "p":
                                    PrintStudenti();
                                    break;
                                default:
                                    Console.WriteLine("Alegere invalida!");
                                    break;
                            }
                            break;
                        case "2":
                            MenuTema();
                            string ooo = Console.ReadLine();

                            switch (ooo)
                            {
                                case "0":
                                    break;
                                case "1":
                                    AdaugaTema();
                                    break;
                                case "2":
                                    ModificaDeadlineTema();
                                    break;
                                case "p":
                                    PrintTeme();
                                    break;
                                default:
                                    Console.WriteLine("Alegere invalida!");
                                    break;
                            }
                            break;

                        case "3":
                            MenuNota();
                            string oooo = Console.ReadLine();

                            switch (oooo)
                            {
                                case "0":
                                    break;
                                case "1":
                                    AdaugaNota();
                                    break;
                                case "p":
                                    PrintNote();
                                    break;
                                default:
                                    Console.WriteLine("Alegere invalida!");
                                    break;
                            }

                            break;

                        case "4":
                            MenuFilRap();
                            string ooooo = Console.ReadLine();

                            switch (ooooo)
                            {
                                case "0":
                                    break;
                                case "1":
                                    Filtru1();
                                    break;
                                case "2":
                                    Filtru2();
                                    break;
                                case "3":
                                    Filtru3();
                                    break;
                                case "4":
                                    Filtru4();
                                    break;
                                default:
                                    Console.WriteLine("Alegere invalida!");
                                    break;
                            }

                            break;

                        default:
                            Console.WriteLine("Alegere invalida!");
                            break;
                    }
                }

                catch (System.IO.IOException)
                {
                    Console.Write("Valoare invalida!");
                }
                catch (MyException err)
                {
                    Console.Write(err.Message);
                }
                catch (ValidationException err)
                {
                    Console.Write(err.Message);
                }
                catch (Exception err)
                {
                    Console.Write(err.Message);
                }
            }
        }
    }
}

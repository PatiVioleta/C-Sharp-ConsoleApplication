using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Domain;
using Lab10.Repository;
using Lab10.Service;
using Lab10.Validation;
using Lab10.Ui;

namespace Lab10
{
    class Program
    {
        static AbstractRepo<int, Student> SRepo = new RepoStudent(new ValidatorStudent(), "C:\\Users\\pati\\Desktop\\UNIV\\SEM3\\MAP\\C# lab10\\f1.txt");
        static AbstractRepo<int, Tema> TRepo = new RepoTema(new ValidatorTema(), "C:\\Users\\pati\\Desktop\\UNIV\\SEM3\\MAP\\C# lab10\\f2.txt");
        static AbstractRepo<Tuple<Student, Tema>, Nota> NRepo = new RepoNota(new ValidatorNota(), "C:\\Users\\pati\\Desktop\\UNIV\\SEM3\\MAP\\C# lab10\\f3.txt", SRepo, TRepo);

        static IService Serv = new IService(SRepo, TRepo, NRepo);
        static IUi Ui;

        static void Main(string[] args)
        {
            Ui = new IUi(Serv);
            Ui.Run();
        }

    }
}

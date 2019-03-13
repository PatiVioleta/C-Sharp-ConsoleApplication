using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Repository;
using Lab10.Domain;
using Lab10.Validation;

namespace Lab10.Service
{
    class IService
    {
        public int SaptCurenta { get; set; }
        private AbstractRepo<int, Tema> repoT;
        private AbstractRepo<int, Student> repoS;
        private AbstractRepo<Tuple<Student, Tema>, Nota> repoN;

        public IService(AbstractRepo<int, Student> repoS, AbstractRepo<int, Tema> repoT, AbstractRepo<Tuple<Student, Tema>, Nota> repoN)
        {
            this.repoT = repoT;
            this.repoS = repoS;
            this.repoN = repoN;
        }

        public Student FindOneS(int id)
        {
            return repoS.FindOne(id);
        }

        public IEnumerable<Student> FindAllS()
        {
            return repoS.FindAll();
        }

        public void SaveS(int Id, string nume, int grupa, string email)
        {
            Student t = new Student(Id, nume, grupa, email);
            repoS.Save(t);
        }

        public Student DeleteS(int id)
        {
            return repoS.Delete(id);
        }

        public Student UpdateS(int Id, string nume, int grupa, string email)
        {
            Student t = new Student(Id, nume, grupa, email);
            return repoS.Update(t);
        }

        public void SaveT(int id, string descriere, int deadline, int primire)
        {
            Tema t = new Tema(id, descriere, deadline, primire);
            repoT.Save(t);
        }

        public Tema FindOneT(int id)
        {
            return repoT.FindOne(id);
        }

        public IEnumerable<Tema> FindAllT()
        {
            return repoT.FindAll();
        }

        public void PrelungireTermen(int id, int nrSaptamani)
        {
            Tema t = FindOneT(id);
            if (t == null)
            {
                throw new MyException("Nu exista tema cu acest ID!\n");
            }
            if (SaptCurenta <= t.Deadline && t.Deadline + nrSaptamani <= 14 && t.Deadline + nrSaptamani >= 1)
            {
                Tema tt = new Tema(t.Id, t.Descriere, t.Deadline + nrSaptamani, t.Primire);
                repoT.Update(tt);
            }
            else throw new MyException("Nu se poate modifica deadline-ul!\n");
        }

        public void ExistentaST(int idS, int idT)
        {
            string err = System.String.Empty;
            if (repoS.FindOne(idS) == null)
                err = err + "Studentul este inexistent!\n";
            if (repoT.FindOne(idT) == null)
                err = err + "Tema este inexistenta!\n";
            if (err.Length != 0)
                throw new ServiceException(err);
        }

        public float SaveN(int idS, int idT, float nota, string numeC, string feed)
        {
            Tuple<Student, Tema> id = Tuple.Create(FindOneS(idS), FindOneT(idT));

            Nota n = new Nota(id, nota, numeC);
            try
            {
                n.ValNota = AplicaIntarzieriN(idS, idT, n.ValNota);
            }
            catch (ServiceException)
            {
                n.ValNota = 1;
            }

            repoN.Save(n);

            return n.ValNota;
        }


        public float AplicaIntarzieriN(int idS, int idT, float nInitial)
        {
            ExistentaST(idS, idT);
            Tema t = repoT.FindOne(idT);
            float nota = nInitial;
            if (t.Deadline + 2 < SaptCurenta)
            {
                throw new ServiceException("Nu se mai poate preda aceasta tema!\n");
            }
            if (t.Deadline + 2 == SaptCurenta)
                nota -= 5;
            if (t.Deadline + 1 == SaptCurenta)
                nota -= 2.5f;
            return nota;
        }

        public IEnumerable<Nota> FindAllN()
        {
            return repoN.FindAll();
        }

        public IEnumerable<Nota> Filtru1(int idTema)
        {
            var rez = from x in repoN.FindAll()
                      where x.Id.Item2.Id == idTema
                      select x;
            return rez;
        }

        public IEnumerable<Nota> Filtru2(int idStud)
        {
            var rez = from x in repoN.FindAll()
                      where x.Id.Item1.Id == idStud
                      select x;
            return rez;
        }

        public IEnumerable<Nota> Filtru3(int grupa, int idTema)
        {
            var rez = from x in repoN.FindAll()
                      where x.Id.Item1.Grupa==grupa && x.Id.Item2.Id==idTema
                      select x;
            return rez;
        }

        public IEnumerable<DTO> Filtru4(int grupa)
        {
            var rez = from x in repoN.FindAll()
                      where x.Id.Item1.Grupa == grupa
                      group x by x.Id.Item1.Nume into g1
                      select new DTO(g1.Key,g1);
            return rez;
        }

    }

    class DTO
    {
        public DTO(string nume, IGrouping<string, Nota> l)
        {
            Nume = nume;

            int j = 0;
            L = new List<string>();
            for (j= 0; j < 14; j++)
                L.Add("-");

            foreach(Nota n in l)
            {
                L[n.Id.Item2.Id] = n.ValNota.ToString();
            }
        }

        public string Nume { get; set; }
        public List<string> L { get; set; }

        public override string ToString()
        {
            string s = "Nume: " + this.Nume +" | Note: ";
            for(int i=0;i<14;i++)
            {
                s += "N" + (i + 1) + "=" + L[i] + " ";
            }
            return s;
        }
    }
}

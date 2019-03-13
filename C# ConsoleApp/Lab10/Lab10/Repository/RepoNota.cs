using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Domain;
using Lab10.Validation;
using System.IO;

namespace Lab10.Repository
{
    class RepoNota : AbstractRepo<Tuple<Student, Tema>, Nota>
    {
        public IRepo<int, Student> RepoS { get; set; }
        public IRepo<int, Tema> RepoT { get; set; }
        public string File { get; set; }

        public RepoNota(IValidator<Nota> v, string file, IRepo<int,Student> repoS, IRepo<int,Tema> repoT) : base(v)
        {
            File = file;
            RepoS = repoS;
            RepoT = repoT;
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            using (StreamReader sr = new StreamReader(File))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    string[] linii = linie.Split(',');

                    Student e1 = RepoS.FindOne(int.Parse(linii[0]));
                    Tema e2 = RepoT.FindOne(int.Parse(linii[1]));

                    Nota entity = new Nota(new Tuple<Student, Tema>(e1,e2),float.Parse(linii[2]),linii[3]);
                    //(domain.Type)Enum.Parse(typeof(domain.Type), linii[3])
                    base.Save(entity);
                }
                sr.Close();
            }

        }

        private void StoreToFile()
        {
            using (StreamWriter sw = new StreamWriter(File))
            {
                foreach (var entity in base.FindAll())
                {
                    string s = entity.Id.Item1.Id + "," + entity.Id.Item2.Id + "," + entity.ValNota + "," + entity.Cadru;
                    sw.WriteLine(s);
                }
                sw.Flush();
            }
        }

        public override Nota Save(Nota entity)
        {
            Nota e = base.Save(entity);
            StoreToFile();
            return e;
        }

        public override Nota Delete(Tuple<Student, Tema> id)
        {
            Nota e = base.Delete(id);
            StoreToFile();
            return e;
        }

        public override Nota Update(Nota entity)
        {
            Nota e = base.Update(entity);
            StoreToFile();
            return e;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Domain;
using Lab10.Validation;
using System.IO;

namespace Lab10.Repository
{
    class RepoTema : AbstractRepo<int, Tema>
    {
        public string File { get; set; }

        public RepoTema(IValidator<Tema> v, string file) : base(v)
        {
            File = file;
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
                    Tema entity = new Tema(int.Parse(linii[0]), linii[1], int.Parse(linii[2]), int.Parse(linii[3]));
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
                    string s = entity.Id + "," + entity.Descriere + "," + entity.Deadline + "," + entity.Primire;
                    sw.WriteLine(s);
                }
                sw.Flush();
            }
        }

        public override Tema Save(Tema entity)
        {
            Tema e = base.Save(entity);
            StoreToFile();
            return e;
        }

        public override Tema Delete(int id)
        {
            Tema e = base.Delete(id);
            StoreToFile();
            return e;
        }

        public override Tema Update(Tema entity)
        {
            Tema e = base.Update(entity);
            StoreToFile();
            return e;
        }
    }
}

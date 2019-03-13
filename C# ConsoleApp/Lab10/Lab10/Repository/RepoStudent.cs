using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Domain;
using Lab10.Validation;
using System.IO;

namespace Lab10.Repository
{
    class RepoStudent : AbstractRepo<int, Student>
    {
        public string File { get; set; }

        public RepoStudent(IValidator<Student> v, string file) : base(v)
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
                    Student entity = new Student(int.Parse(linii[0]), linii[1], int.Parse(linii[2]),linii[3]);
                    //(domain.Type)Enum.Parse(typeof(domain.Type), linii[3])
                    base.Save(entity);
                }
                //sr.Close();
            }
        }

        private void StoreToFile()
        {
            using (StreamWriter sw = new StreamWriter(File))
            {
                foreach (var entity in base.FindAll())
                {
                    string s = entity.Id + "," + entity.Nume + "," + entity.Grupa + "," + entity.Email;
                    sw.WriteLine(s);

                }
                sw.Flush();
            }
        }

        public override Student Save(Student entity)
        {
            Student e = base.Save(entity);
            StoreToFile();
            return e;
        }

        public override Student Delete(int id)
        {
            Student e = base.Delete(id);
            StoreToFile();
            return e;
        }

        public override Student Update(Student entity)
        {
            Student e = base.Update(entity);
            StoreToFile();
            return e;
        }
    }
}

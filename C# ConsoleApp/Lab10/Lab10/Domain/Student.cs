using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Domain
{
    class Student : IHasID<int>
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Nume { get; set; }
        public int Grupa { get; set; }
        public string Email { get; set; }


        public Student(int Id, string nume, int grupa, string email)
        {
            this.Id = Id;
            this.Nume = nume;
            this.Grupa = grupa;
            this.Email = email;
        }

        public override bool Equals(object obj)
        {
            var student = obj as Student;
            return student != null &&
                   id == student.id;
        }

        public override string ToString()
        {
            return "Id: " + this.Id + "| Nume: " + this.Nume + "| Grupa: " + this.Grupa + "| Email: " + this.Email;
        }

        public override int GetHashCode()
        {
            var hashCode = 87000073;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nume);
            hashCode = hashCode * -1521134295 + Grupa.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }

    }
}

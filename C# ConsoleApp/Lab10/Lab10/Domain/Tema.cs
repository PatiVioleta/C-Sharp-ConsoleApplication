using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Domain
{
    class Tema : IHasID<int>
    {
        private int id;
        public int Id { get { return id; } set { id = value; } }
        public string Descriere { get; set; }
        public int Deadline { get; set; }
        public int Primire { get; set; }

        public Tema(int id, string descriere, int deadline, int primire)
        {
            Id = id;
            Descriere = descriere;
            Deadline = deadline;
            Primire = primire;
        }

        public override string ToString()
        {
            return "Id: " + this.Id + "| Descriere: " + this.Descriere + "| Primire: " + this.Primire + "| Deadline: " + this.Deadline;
        }

    }
}

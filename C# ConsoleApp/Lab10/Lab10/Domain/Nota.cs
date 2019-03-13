using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Domain
{
    class Nota : IHasID<Tuple<Student, Tema>>
    {
        private Tuple<Student, Tema> id;
        public Tuple<Student, Tema> Id
        {
            get { return id; }
            set { id = value; }
        }
        public float ValNota { get; set; }
        public string Cadru { get; set; }

        public Nota(Tuple<Student, Tema> id, float valNota, string cadru)
        {
            Id = id;
            ValNota = valNota;
            Cadru = cadru;
        }

        public override string ToString()
        {
            return "IdS: " + this.Id.Item1.Id + "| IdT: " + this.Id.Item2.Id + "| Nota: " + this.ValNota + "| Cadru: " + this.Cadru;
        }
    }
}

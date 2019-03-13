using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Domain;

namespace Lab10.Validation
{
    class ValidatorTema : IValidator<Tema>
    {
        public void Validate(Tema t)
        {
            string err = System.String.Empty;
            if (t.Id < 0)
                err = err + "ID invalid!\n";
            if (t.Descriere == "")
                err = err + "Descriere invalida!\n";
            if (t.Deadline < 1 || t.Deadline > 14)
                err = err + "Deadline invalid!\n";
            if (t.Primire < 1 || t.Primire > 14 || t.Primire > t.Deadline)
                err = err + "Saptamana primire invalida!\n";
            if (err.Length != 0)
                throw new ValidationException(err);
        }
    }
}

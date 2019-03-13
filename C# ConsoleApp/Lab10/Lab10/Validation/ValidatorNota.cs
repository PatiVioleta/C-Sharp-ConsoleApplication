using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Domain;

namespace Lab10.Validation
{
    class ValidatorNota : IValidator<Nota>
    {
        public void Validate(Nota t)
        {
            string err = System.String.Empty;
            if (t.Id.Item1 == null || t.Id.Item1.Id < 0)
                err = err + "ID student invalid!\n";
            if (t.Id.Item2 == null || t.Id.Item2.Id < 0)
                err = err + "ID tema invalid!\n";
            if (t.ValNota < 1 || t.ValNota > 10)
                err = err + "Nota invalida!\n";
            if (t.Cadru == "")
                err = err + "Cadru didactic invalid!\n";
            if (err.Length != 0)
                throw new ValidationException(err);
        }

    }
}

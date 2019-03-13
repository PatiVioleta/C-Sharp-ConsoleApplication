using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Domain;

namespace Lab10.Validation
{
    class ValidatorStudent : IValidator<Student>
    {
        public void Validate(Student s)
        {
            string err = System.String.Empty;
            if (s.Id < 0)
                err = err + "ID invalid!\n";
            if (s.Nume == "")
                err = err + "Nume invalid!\n";
            if (s.Grupa < 221 || s.Grupa > 227)
                err = err + "Grupa invalida!\n";
            if (s.Email == "")
                err = err + "Email invalid!\n";
            if (err.Length != 0)
                throw new ValidationException(err);

        }
    }
}

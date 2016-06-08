namespace Citizens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Humanizer;

    public class Citizen : ICitizen
    {
        public Citizen(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            this.FirstName = firstName.Transform(To.TitleCase);
            this.LastName = lastName.Transform(To.TitleCase);

            if (dateOfBirth.CompareTo(SystemDateTime.Now()) > 0)
            {
                throw new ArgumentException();
            }

            this.BirthDate = dateOfBirth.Date;

            if ((int)gender < 0 || (int)gender > 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.Gender = gender;
        }

        public DateTime BirthDate { get; }

        public string FirstName { get; }

        public Gender Gender{ get; }

        public string LastName { get; }

        public string VatId { get; set; }

        //private string CalcId()
        //{

        //}
    }
}

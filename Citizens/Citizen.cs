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
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private Gender gender;
        private string vatID;

        public Citizen(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = dateOfBirth;
            this.Gender = gender;
            this.VatId = string.Empty;
        }

        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                if (value.CompareTo(SystemDateTime.Now()) > 0)
                {
                    throw new ArgumentException();
                }

                this.birthDate = value.Date;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException();
                }

                this.firstName = value.Transform(To.TitleCase).Trim(' ');
            }
        }

        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                if ((int)value < 0 || (int)value > 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.gender = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException();
                }

                this.lastName = value.Transform(To.TitleCase).Trim(' ');
            }
        }

        public string VatId { get; set; }
    }
}

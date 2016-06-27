namespace Citizens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Humanizer;

    public class Registration : ICitizenRegistry
    {
        private readonly DateTime rootDate;
        private ICitizen[] registry;
        private DateTime? lastUpdate;

        public Registration()
        {
            this.registry = new ICitizen[0];
            this.lastUpdate = null;
            this.rootDate = new DateTime(1899, 12, 31);
            this.Count = 0;
        }

        public int Count { get; private set; }

        public ICitizen this[string id]
        {
            get
            {
                if (id == null)
                {
                    throw new ArgumentNullException();
                }

                return Array.Find(this.registry, (x) => x.VatId == id);
            }
        }

        public void Register(ICitizen citizen)
        {
            this.CheckForRepeats(citizen);
            if (this.Count == this.registry.Length)
            {
                Array.Resize(ref this.registry, this.FindNewCapacity());
            }

            if (string.IsNullOrWhiteSpace(citizen.VatId))
            {
                citizen.VatId = this.CalculateValidVatID(citizen);
            }

            this.registry[this.Count] =
                new Citizen(citizen.FirstName, citizen.LastName, citizen.BirthDate, citizen.Gender)
                { VatId = citizen.VatId };
            this.lastUpdate = SystemDateTime.Now();
            this.Count++;
        }

        public string Stats()
        {
            int countOfMen = this.registry.Count((x) => x.Gender == Gender.Male);

            string res = string.Format(
                "{0} and {1}",
                "man".ToQuantity(countOfMen),
                "woman".ToQuantity(this.registry.Length - countOfMen));

            if (this.lastUpdate != null)
            {
                res += string.Format(
                    ". Last registration was {0}",
                    this.lastUpdate.Humanize(true, SystemDateTime.Now()));
            }

            return res;
        }

        private void CheckForRepeats(ICitizen citizen)
        {
            if (this.registry.Count((x) =>
                        {
                            if (x == null)
                            {
                                return false;
                            }
                            else
                            {
                                return x.VatId == citizen.VatId;
                            }
                        }) > 0)
            {
                throw new InvalidOperationException();
            }
        }

        private string CalculateValidVatID(ICitizen citizen)
        {
            string res = string.Empty;
            res += string.Format(
                "{0:d5}",
                citizen.BirthDate.Subtract(this.rootDate).Days);
            int temp = ((citizen.Gender == Gender.Male) ? 1 : 0) +
                (this.CountOfPeopleWithSameBirthDateAndGender(citizen) * 2);
            if (temp > 9999)
            {
                throw new InvalidOperationException("System doesn't allow to registy more than 5k people with the same date and gender");
            }

            res += string.Format("{0:d4}", temp);
            res += this.FindKey(res).ToString();
            return res;
        }

        private int CountOfPeopleWithSameBirthDateAndGender(ICitizen citizen)
        {
            return this.registry.Count( (x) =>
                {
                    if (x == null || x.BirthDate == null || citizen.BirthDate == null)
                    {
                        return false;
                    }
                    else
                    {
                        return (x.BirthDate == citizen.BirthDate)
                               && (x.Gender == citizen.Gender);
                    }
                }
             );
        }

        private int FindKey(string code)
        {
            int temp =
               (int.Parse(code[0].ToString()) * (-1))
               + (int.Parse(code[1].ToString()) * 5)
               + (int.Parse(code[2].ToString()) * 7)
               + (int.Parse(code[3].ToString()) * 9)
               + (int.Parse(code[4].ToString()) * 4)
               + (int.Parse(code[5].ToString()) * 6)
               + (int.Parse(code[6].ToString()) * 10)
               + (int.Parse(code[7].ToString()) * 5)
               + (int.Parse(code[8].ToString()) * 7);
            return (temp % 11) % 10;
        }

        private int FindNewCapacity()
        {
            return ((this.registry.Length * 3) / 2) + 1;
        }
    }
}

namespace Citizens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Registration : ICitizenRegistry
    {
        private ICitizen[] registry;

        public Registration()
        {
            this.registry = new ICitizen[0];
        }

        public ICitizen this[string id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Register(ICitizen citizen)
        {
            this.CheckForRepeats(citizen);
            Array.Resize<ICitizen>(ref this.registry, this.registry.Length + 1);
            if (string.IsNullOrWhiteSpace(citizen.VatId))
            {
                citizen.VatId = this.CalculateValidVatID(citizen);
            }

            this.registry[this.registry.Length - 1] = citizen;
        }

        public string Stats()
        {
            throw new NotImplementedException();
        }

        private void CheckForRepeats(ICitizen citizen)
        {
            if (this.registry.Count((x) => x.VatId == citizen.VatId) > 0)
            {
                throw new InvalidOperationException();
            }
        }

        private string CalculateValidVatID(ICitizen citizen)
        {
            string res = string.Empty;
            res += string.Format("{0:d5}", citizen.BirthDate.Subtract(new DateTime(1899, 12, 31)).Days);
            int temp = ((citizen.Gender == Gender.Male) ? 1 : 0) +
                (this.CountOfPeopleWithSameBirthDateAndGender(citizen) * 2);
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
    }
}

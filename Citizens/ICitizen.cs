namespace Citizens
{
    using System;

    public interface ICitizen
    {
        string FirstName { get; }

        string LastName { get; }

        Gender Gender { get; }

        DateTime BirthDate { get; }

        string VatId { get; set; }
    }
}
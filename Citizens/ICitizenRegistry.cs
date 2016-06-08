namespace Citizens
{
    using System;

    public interface ICitizenRegistry
    {
        void Register(ICitizen citizen);

        ICitizen this[string id] { get; }

        string Stats();
    }
}

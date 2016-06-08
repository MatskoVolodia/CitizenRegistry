namespace Citizens
{
    using System;

    public static class SystemDateTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}

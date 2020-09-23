using System;

namespace IntegrationFactory.Domain.Tests.SeedWork
{
    public static class CSV
    {
        public static string Path = @"..\..\..\Files\Regiao.Csv";
        public static Func<string[], Region> Mapping => (c => new Region()
        {
            Id = Convert.ToInt32(c[0]),
            Initials = c[1],
            Name = c[2]
        });

        public static Func<string[], Region> MappingInválido => (c => new Region()
        {
            Id = Convert.ToInt32(c[0]),
            Initials = c[1],
            Name = c[3]
        });

    }
}
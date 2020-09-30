using System.Collections.Generic;
namespace IntegrationFactory.Domain.Tests.SeedWork
{
    public class SqlSeedData
    {
        public static string SeedRegion = @"..\..\..\Files\SeedRegiaoTest.sql";
        public static string SeedRegionExtens = @"..\..\..\Files\SeedRegiaoTestExtends.sql";
        public static string SeedRegionComDuasChaves = @"..\..\..\Files\SeedRegiaoTestComDuasChaves.sql";
       public static string SeedRegionMinus = @"..\..\..\Files\SeedRegiaoTestMinus.sql";
        public static List<string> Seeds = new List<string>() {
            SeedRegion,
            SeedRegionExtens,
            SeedRegionComDuasChaves,
            SeedRegionMinus
        };
    }
}
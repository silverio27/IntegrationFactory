using System;
using System.Xml.Linq;

namespace IntegrationFactory.Domain.Tests.SeedWork
{
    public static class XML
    {
        public static string Path = @"..\..\..\Files\Region.xml";
        public static string EmptyPath = @"..\..\..\Files\RegionEmpty.xml";

        public static Func<XElement, Region> Mapping => (c => new Region()
        {
            Id = Convert.ToInt32(c.Element("Id").Value),
            Initials = c.Element("Initials").Value,
            Name = c.Element("Name").Value
        });

         public static Func<XElement, Region> MappingInválido => (c => new Region()
        {
            Id = Convert.ToInt32(c.Element("Id").Value),
            Initials = c.Element("Initials").Value,
            Name = c.Element("XXXXX").Value
        });

        
    }
}
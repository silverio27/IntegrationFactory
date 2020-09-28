using System;

namespace IntegrationFactory.Domain.ConsoleApp.Model
{
    public class RegionTransformed
    {

        public int Id { get; set; }
        public string Initials { get; set; }
        public string Name { get; set; }
        public string Concat { get; set; }
        public DateTime When { get; private set; }
        public RegionTransformed()
        {
            When = DateTime.Now;
        }
    }
}
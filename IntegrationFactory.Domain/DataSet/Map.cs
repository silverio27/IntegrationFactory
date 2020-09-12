namespace IntegrationFactory.Domain.DataSet
{
    public class Map
    {
        public Map(string source, string target)
        {
            Source = source;
            Target = target;
        }

        public string Source { get; private set; }
        public string Target { get; private set; }
    }
}
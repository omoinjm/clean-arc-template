namespace Clean.Architecture.Template.Core.Specs
{
    public class DataList<T> where T : class
    {
        public DataList(int count, IReadOnlyList<T> data)
        {
            Count = count;
            Data = data;
        }

        public DataList()
        {
            Data = [];
        }

        public long Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
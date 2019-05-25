namespace Domain.Models
{
    public class ListResponse<T>
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public T Result { get; set; }
    }
}

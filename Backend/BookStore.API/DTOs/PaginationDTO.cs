namespace BookStore.API.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordsPerPage { get; set; } = 10;
        private readonly int maxAmount = 20;

        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set
            {
                recordsPerPage = (value > maxAmount) ? maxAmount : value;
            }
        }

    }
}

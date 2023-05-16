namespace Core.DTOs
{
    public class PaginationResult<T>
    {

        private List<T> _items;
        private int _currentPage;
        private int _itemsPerPage;
        private int _totalItems;
        private int _totalPages;

        public List<T> Items { get => _items; set => _items = value; }
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }
        public int ItemsPerPage { get => _itemsPerPage; set => _itemsPerPage = value; }
        public int TotalItems { get => _totalItems; set => _totalItems = value; }
        public int TotalPages { get => _totalPages; set => _totalPages = value; }
    }

}

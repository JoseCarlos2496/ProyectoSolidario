namespace BackendSolidario.Core.Response {
    public class ApiResponse<T> {
        public ApiResponse() {
            Success = false;
        }

        public bool Success { set; get; }
        public string Message { set; get; }
        public T? Data { set; get; }
        public List<string>? Errors { set; get; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public string PreviousPageUrl { get; set; }
        public string NextPageUrl { get; set; }
    }
}
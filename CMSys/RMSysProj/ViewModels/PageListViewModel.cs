namespace RMSysProj.ViewModels
{
    public class PageListViewModel
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public bool CanNext { get; set; }
        public bool CanPrevious { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}

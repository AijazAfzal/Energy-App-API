namespace Energie.Model.Response
{
    public class DepartmentEmployerHelpList
    {
        public int Id { get; set; }
        public string CompanyHelpCategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IList<DepartmentEmployerHelp> DepartmentEmployerHelps { get; set; }
    }

    public class DepartmentEmployerHelp
    {
        public int Id { get; set; }
        public int HelpCategoryId { get; set; }
        public string HelpCategoryName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contribution { get; set; }
        public string Requestvia { get; set; }
        public string MoreInformation { get; set; }
        public bool IsSelected { get; set; }
    }
}

namespace Energie.Model.Request
{
    public class DepartmentEnergyPlanRequest
    {
        public int FavouriteTipId { get; set; }
        public int ResponsiblePersonId { get; set; }
        public string TipBy { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsReminder { get; set; }
    }
}

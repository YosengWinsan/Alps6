namespace Alps.Domain.PurchaseMgr
{
    public class SupplierClass : EntityBase
    {
        public string Name { get; set; }
        public static SupplierClass Create(string name)
        {
            return new SupplierClass(){Name=name};
        }
    }
}
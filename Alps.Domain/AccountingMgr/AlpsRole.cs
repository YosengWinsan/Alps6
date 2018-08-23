namespace Alps.Domain.AccountingMgr
{
    public class AlpsRole : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public static AlpsRole Create(string name, string description)
        {
            return new AlpsRole() { Name = name, Description = description };
        }
    }
}

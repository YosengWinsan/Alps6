using System;

namespace Alps.Domain.Common
{
    public class Quantity : ValueObject
    {
        public decimal QuantityOfUnit { get; set; }
        public string UnitName { get; set; }
        public decimal Count { get; set; }
        public decimal Weight { get; set; }
        //public Unit Unit { get; set; }
        public Guid UnitID { get; set; }
        public Quantity(decimal quantity, Guid unitID)
        {
            this.QuantityOfUnit = quantity;
            this.UnitID = unitID;
        }
        public Quantity(decimal quantity,Guid unitID,string unitDisplayName) : this(quantity, unitID)
        {
            this.UnitName = unitDisplayName;
        }
        public static Quantity Create(decimal quantityOfUnit,Guid unitID,string unitDisplayName)
        {
            return new Quantity(quantityOfUnit, unitID, unitDisplayName);
        }
        public Quantity(decimal count, decimal weight)
            : base()
        {
            Count = count;
            Weight = weight;
        }
        public static Quantity operator +(Quantity q1, Quantity q2)
        {
            q1.Add(q2.Count, q2.Weight);
            return q1;
        }
        public static Quantity operator -(Quantity q1, Quantity q2)
        {
            q1.Count -= q2.Count;
            q1.Weight -= q2.Weight;
            return q1;
        }
        public Quantity Add(decimal count, decimal weight)
        {
            this.Count += count;
            this.Weight += weight;
            return this;
        }
        public Quantity Subtract(decimal count, decimal weight)
        {
            Add(-count, -weight);
            return this;
        }
        public bool IsNegative()
        {
            return this.Count < 0 || this.Weight < 0;
        }
    }
}

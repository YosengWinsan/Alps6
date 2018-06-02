using System;
using System.ComponentModel.DataAnnotations;
using Alps.Domain.Common;
namespace Alps.Domain.ProductMgr
{
    public class Product : EntityBase
    {
        [Display(Name = "品名")]
        public string Name { get; set; }
        //public Guid CatagoryID { get; set; }
        //[Display(Name = "类别")];
        //public virtual Catagory Catagory { get; set; }

        [Display(Name = "全名")]
        public string FullName { get; set; }
        [Display(Name="简介")]
        public string ShortDescription { get; set; }
        [Display(Name="详细介绍")]
        public string FullDescription { get; set; }

        //public ProductGrade ProductGrade { get; set; }
        [Display(Name="删除否")]
        public bool Deleted { get; set; }
        [Display(Name = "基本单位")]
        public Guid BaseUnitID { get; set; }
        [Display(Name="启用辅助单位")]
        public Boolean EnableAuxiliaryUnit { get; set; }
        [Display(Name ="辅助单位")]
        public Guid? AuxiliaryUnitID { get; set; }
        //[Display(Name = "类别")]
        // public virtual ICollection<ProductCatagorySetting> ProductCatagorySettings { get; set; }
        [Display(Name = "计价方式")]
        public PricingMethod PricingMethod { get; set; }
        [Display(Name = "定价")]
        public decimal ListPrice { get; set; }
        [Display(Name="类别")]
        public Guid CatagoryID { get;set; }
        public virtual Catagory Catagory { get; set; }
        //public ICollection<ProductAttributeCombination> ProductAttributeCombination { get; set; }
        public virtual Unit BaseUnit { get; set; }
        public static Product Create(string name,string shortDiscription,string fullDiscription,PricingMethod priceMethod,decimal price,Guid baseUnitID)
        {
            Product product = new Product();
            product.Name = name;
            product.FullName = shortDiscription;
            product.FullDescription = fullDiscription;
            product.ShortDescription = shortDiscription;
            //product.PackingQuantity = packingQuantity;
            //product.Weight = weight;
            product.PricingMethod = priceMethod;
            product.ListPrice = price;
            product.BaseUnitID = baseUnitID;
           // product.ProductCatagorySettings = new HashSet<ProductCatagorySetting>();
            return product;
        }
        public static Product Create(string name, string shortDiscription, string fullDiscription, PricingMethod priceMethod, decimal price, Guid baseUnitID,Guid catagoryID)
        {
            Product p= Create(name, shortDiscription, fullDiscription, priceMethod, price, baseUnitID);
            p.CatagoryID = catagoryID;
            //p.SetCatagory(catagoryID);
            return p;
        }
        public void SetCatagory(Catagory catagory,int displayOrder=0)
        {
            this.CatagoryID = catagory.ID;
            //var productCatagorySetting = ProductCatagorySetting.Create(this.ID,catagory.ID,catagory.Name,displayOrder);
            //this.ProductCatagorySettings.Add(productCatagorySetting);
        }
        
        public void SetDeleted()
        {
            this.Deleted = true;
        }
        public PurchaseMgr.ProductInOrder GetProductInOrder()
        {
            return new PurchaseMgr.ProductInOrder(this.ID, this.FullName, this.PricingMethod);
        }
    }
}

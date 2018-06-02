using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alps.Domain.Common;

namespace Alps.Domain.ProductMgr
{
    public partial class Catagory : EntityBase
    {
        [Display(Name = "类别名")]
        public string Name { get; set; }
        [Display(Name = "全名")]
        public string FullName { get; set; }
        [Display(Name = "上级")]
        public virtual Catagory Parent { get; set; }
        [Display(Name = "上级ID")]
        public Guid? ParentID { get; set; }
        public string Code { get; set; }
        public bool IsParentCatagory { get; set; }
        [ForeignKey("ParentID")]
        public virtual ICollection<Catagory> Children { get; set; }
        public Catagory()
        {
            Children = new HashSet<Catagory>();
            Parent = null;
        }
        public static Catagory Create(string name)
        {
            var newCatagory = new Catagory ();
            newCatagory.Name = name;
            newCatagory.FullName = name;
            newCatagory.IsParentCatagory = false;
            return newCatagory;
        }
        public void ChangeName(string name)
        {
            this.Name = name;
            this.FullName = name;
        }
        public Catagory AddChildCatagory(Catagory catagory)
        {
            catagory.FullName = this.FullName + "-" + catagory.Name;
            this.Children.Add(catagory);
            this.IsParentCatagory = true;
            return this;
        }
        public Catagory RemoveChildCatagory(Catagory catagory)
        {
            this.Children.Remove(catagory);
            if (this.Children.Count() == 0)
                this.IsParentCatagory = false;
            return this;
        }
        public void BelongTo(Catagory parentCatagory)
        {
            this.Parent = parentCatagory;
            this.ParentID = parentCatagory.ID;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Model
{
  
  public class PurchaseOrderListDto
  {
    public DateTime CreateTime { get; set; }
    public string Creater { get; set; }
    public DateTime ConfirmTime { get; set; }
    public string Confirmer { get; set; }
    public Guid ID { get; set; }
    public Guid OrderID { get; set; }
    public string Supplier { get; set; }
    public decimal TotalQuantity { get; set; }
    public int State { get; set; }

  }

  public class PurchaseOrderItemForEditDto
  {
    public Guid ID { get; set; }
    public Guid SKUID { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
  }
  public class PurchaseOrderForEditDto
  {

    public Guid ID { get; set; }
    public string Supplier { get; set; }
    public string Department { get; set; }
    public Guid SupplierID { get; set; }
    public Guid DepartmentID { get; set; }
    public string Creater { get; set; }
    public string CreateTime { get; set; }

    public IEnumerable<PurchaseOrderItemForEditDto> Items { get; set; }
  }
  public class PurchaseOrderEditModel
  {
    public IEnumerable<AlpsSelectorItemDto> SupplierOptions { get; set; }
    public PurchaseOrderForEditDto Order { get; set; }

  }

}

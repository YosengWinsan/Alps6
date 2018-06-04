using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Model
{
    public class StockListDto
    {
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public decimal AuxiliaryQuantity { get; set; }
    public string Warehouse { get; set; }
    public string Owner { get; set; }
    public string SerialNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Model
{
    public class CatagoryEditDto
    {
    public Guid ID { get; set; }
    public Guid? ParentID { get; set; }
    public string Name { get; set; }
    }
}

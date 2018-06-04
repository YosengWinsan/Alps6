using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Model
{
  public class AlpsSelectorItemDto
  {
    public Guid Value { get; set; }
    public string DisplayValue { get; set; }
    public IEnumerable<AlpsSelectorItemDto> Children { get; set; }
  }
}

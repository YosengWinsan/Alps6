using System.Linq;

namespace Alps.Domain.Service
{
    public class AddressService
    {
        readonly AlpsContext _context;
        public AddressService(AlpsContext context)
        {
            this._context = context;
        }
        public void UpdateChildrenFullName(City city)
        {
            foreach (var county in _context.Counties.Where(p => p.CityID == city.ID))
            {
                county.FullName = city.FullName + "/" + county.Name;
            }
        }
        public void UpdateChildrenFullName(Province province)
        {
            foreach (var city in _context.Cities.Where(p => p.ProvinceID == province.ID))
            {
                city.FullName = province.FullName + "/" + city.Name;
                UpdateChildrenFullName(city);
            }
        }
                public void UpdateChildrenFullName(Country country)
        {
            foreach (var province in _context.Provinces.Where(p => p.CountryID == country.ID))
            {
                province.FullName = country.Name + "/" + province.Name;
                UpdateChildrenFullName(province);
            }
        }
    }
}
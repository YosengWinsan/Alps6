using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
//using Alps.Web.Service.Model;
namespace Alps.Web.Service.Extensions
{
  public class AlpsEnumSelectorItem
  {
    public int Value { get; set; }
    public string DisplayValue { get; set; }
  }

  public class EnumHelper
  {
    public static IList<AlpsEnumSelectorItem> GetEnumSelectList(Type type)
    {
      if (type == null)
      {
        throw new Exception("type参数无效");
      }
      if (!EnumHelper.IsValidForEnumHelper(type))
      {
        throw new Exception("type参数类型不符合");
      }
      IList<AlpsEnumSelectorItem> list = new List<AlpsEnumSelectorItem>();
      Type type2 = Nullable.GetUnderlyingType(type) ?? type;
      if (type2 != type)
      {
        list.Add(new AlpsEnumSelectorItem
        {
          DisplayValue = string.Empty,
          Value = 0
        });
      }
      FieldInfo[] fields = type2.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
      for (int i = 0; i < fields.Length; i++)
      {
        FieldInfo fieldInfo = fields[i];
        object rawConstantValue = fieldInfo.GetRawConstantValue();
        list.Add(new AlpsEnumSelectorItem
        {
          DisplayValue = EnumHelper.GetDisplayName(fieldInfo),
          Value = (int)rawConstantValue
        });
      }
      return list;
    }
    private static string GetDisplayName(FieldInfo field)
    {
      DisplayAttribute customAttribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute), false) as DisplayAttribute;
      if (customAttribute != null)
      {
        string name = customAttribute.GetName();
        if (!string.IsNullOrEmpty(name))
        {
          return name;
        }
      }
      return field.Name;
    }
    public static string GetDisplayValue(Enum enumValue)
    {
      FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
      return EnumHelper.GetDisplayName(fi);
    }
    public static string GetDisplayValue(Type enumType,string value)
    {
      return EnumHelper.GetDisplayName(enumType.GetField(value));
    }
    public static bool IsValidForEnumHelper(Type type)
    {
      bool result = false;
      if (type != null)
      {
        Type type2 = Nullable.GetUnderlyingType(type) ?? type;
        if (type2.IsEnum)
        {
          result = true;
        }
      }
      return result;
    }

  }
}

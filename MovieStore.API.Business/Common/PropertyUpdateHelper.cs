using System;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Common
{
    public static class PropertyUpdateHelper
    {
        public static void Update(object oldEnt, object newEnt)
        {
            foreach (var propInfo in newEnt.GetType().GetProperties())
            {
                var newValue = propInfo.GetValue(newEnt);
                var oldProp = oldEnt.GetType().GetProperty(propInfo.Name);
                var oldValue = oldProp.GetValue(oldEnt);
                if(propInfo.PropertyType == typeof(string))
                {
                    oldProp.SetValue(oldEnt, newValue is null || string.Empty == newValue.ToString() ? oldValue : newValue);
                }
                else if (propInfo.PropertyType == typeof(int))
                {
                    oldProp.SetValue(oldEnt, (int)newValue == 0 ? oldValue : newValue);
                }
                else if (propInfo.PropertyType == typeof(DateTime))
                {
                    oldProp.SetValue(oldEnt, (DateTime)newValue == DateTime.MinValue ? oldValue : newValue);
                }
            }
        }
    }
}
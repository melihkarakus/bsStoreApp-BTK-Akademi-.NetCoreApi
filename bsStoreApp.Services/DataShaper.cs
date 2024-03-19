using bsStoreApp.Services.Contract;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        // Tiplerin özelliklerini tutacak dizi
        public PropertyInfo[] properties { get; set; }

        // DataShaper sınıfının yapıcı metodu
        public DataShaper()
        {
            // Tiplerin özelliklerini almak için refleksiyon kullanılır
            properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        // Belirli özellikleri içeren bir koleksiyonu şekillendirmek için kullanılan yöntem
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            // Gerekli özellikleri al
            var requiredFields = GetRequiredProperties(fieldsString);
            // Verileri şekillendirilmiş bir şekilde döndür
            return FetchData(entities, requiredFields);
        }

        // Belirli özellikleri içeren bir nesneyi şekillendirmek için kullanılan yöntem
        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            // Gerekli özellikleri al
            var requiredProperties = GetRequiredProperties(fieldsString);
            // Belirtilen nesneyi şekillendir
            return FetchDataForEntity(entity, requiredProperties);
        }

        // Belirli özellikleri almak için kullanılan özel bir yöntem
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFields = new List<PropertyInfo>();
            // fieldsString boş değilse, belirli alanları ayır
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in fields)
                {
                    // Alan adına sahip özelliği al
                    var property = properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                    // Özellik bulunamazsa geç
                    if (property is null)
                    {
                        continue;
                    }
                    else
                    {
                        requiredFields.Add(property);
                    }
                }
            }
            // fieldsString boşsa, tüm özellikleri al
            else
            {
                requiredFields = properties.ToList();
            }

            return requiredFields;
        }

        // Bir nesneyi belirli özelliklerle şekillendirmek için kullanılan özel bir yöntem
        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            // Şekillendirilmiş nesne oluştur
            var shapedObject = new ExpandoObject();

            // Belirli özellikler üzerinde dön ve değerleri al
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objectPropertyValue);
            }
            return shapedObject;
        }

        // Bir koleksiyonu belirli özelliklerle şekillendirmek için kullanılan özel bir yöntem
        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();
            // Tüm nesneler üzerinde dön
            foreach (var entity in entities)
            {
                // Her bir nesneyi belirli özelliklerle şekillendir ve şekillenmiş veriyi koleksiyona ekle
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}

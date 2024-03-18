using bsStoreApp.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Repositories.Extensions
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            // Sıralama dizgesini virgülle ayırarak parametreleri al.
            var orderParams = orderByQueryString.Trim().Split(',');

            // Kitap özelliklerini (properties) al.
            var propertyInfos = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Sıralama sorgusunu oluşturacak olan StringBuilder nesnesi.
            var orderQueryBuilder = new StringBuilder();

            // Her bir sıralama parametresini kontrol et.
            foreach (var param in orderParams)
            {
                // Eğer parametre boş ise devam et.
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                // Parametre içerisindeki özellik adını ve sıralama yönünü ayır.
                var propertyFromQueryName = param.Split(' ')[0];

                // Parametrenin özellik adı ile kitap özelliklerini eşleştir.
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                // Eğer özellik bulunamazsa devam et.
                if (objectProperty is null)
                {
                    continue;
                }

                // Sıralama yönünü belirle (azalan mı artan mı?).
                var direction = param.EndsWith(" desc") ? "desc" : "ascending";

                // Sıralama sorgusuna özellik adını ve sıralama yönünü ekle.
                orderQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }


            // Oluşturulan sıralama sorgusunu virgülle ve boşlukla bitişik şekilde temizle.
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;
        }
    }
}

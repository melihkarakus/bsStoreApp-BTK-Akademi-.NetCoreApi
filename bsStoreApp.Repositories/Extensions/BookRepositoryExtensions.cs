using bsStoreApp.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace bsStoreApp.Repositories.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            // Verilen fiyat aralığına göre kitapları filtrelemek için kullanılan fonksiyon.
            // Belirtilen fiyat aralığı içindeki kitapları döndürür.

            return books.Where(book => book.Price >= minPrice && book.Price <= maxPrice);
        }

        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            // Verilen arama terimine göre kitapları filtrelemek için kullanılan fonksiyon.
            // Arama terimi boş ise tüm kitapları döndürür.

            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var lowerCaseTerm = searchTerm.Trim().ToLower(); // Küçük harfle de aramak için
            return books.Where(b => b.Title.ToLower().Contains(searchTerm));
        }

        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            // Verilen sorgu dizgesine (orderByQueryString) göre kitapları sıralamak için kullanılan fonksiyon.
            // orderByQueryString parametresi, sıralama için kullanılacak özellikleri ve sıralama yönlerini belirtir.

            // Eğer sıralama dizgesi boş ise, kitapları ID'ye göre artan şekilde sırala.
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return books.OrderBy(b => b.ID);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            // Eğer sıralama sorgusu hala boş ise, kitapları ID'ye göre artan şekilde sırala.
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return books.OrderBy(b => b.ID);
            }

            // Oluşturulan sıralama sorgusunu kullanarak kitapları sırala.
            return books.OrderBy(orderQuery);
        }
    }
}

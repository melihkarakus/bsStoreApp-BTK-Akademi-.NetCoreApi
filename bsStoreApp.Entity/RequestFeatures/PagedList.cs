using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Entity.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        // PagedList sınıfı, sayfalama işlemi için kullanılır. Belirli sayfa numarası ve boyutuna göre öğeleri gruplar.
        public MetaData MetaData { get; set; }

        // PagedList sınıfının kurucu metodu. Sayfalı bir liste oluşturur.
        // items: Sayfalanmış öğelerin listesi
        // count: Toplam öğe sayısı
        // pageNumber: Mevcut sayfa numarası
        // pageSize: Sayfa boyutu
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            // Meta verileri oluşturulur ve atanır
            MetaData = new MetaData()
            {
                TotalCount = count, // Toplam öğe sayısı
                PageSize = pageSize, // Sayfa boyutu
                CurrentPage = pageNumber, // Mevcut sayfa numarası
                TotalPage = (int)Math.Ceiling(count / (double)pageSize) // Toplam sayfa sayısı
            };

            // Öğeler koleksiyona eklenir
            AddRange(items);
        }

        // Belirli bir IEnumerable koleksiyonunu sayfalı bir liste olarak dönüştürür.
        // source: Dönüştürülecek kaynak koleksiyon
        // pageNumber: İstenen sayfa numarası
        // pageSize: Sayfa boyutu
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count(); // Toplam öğe sayısı alınır
            var items = source
                .Skip((pageNumber - 1) * pageSize) // Belirtilen sayfa numarasına göre öğeler atlanır
                .Take(pageSize) // Sayfa boyutu kadar öğe alınır
                .ToList(); // Alınan öğeler listeye dönüştürülür

            // Yeni bir PagedList örneği oluşturulur ve döndürülür
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}

using System.Collections.Generic;

namespace SemidSDF
{
    // Ödəniş məlumatları (Ad və Məbləğ)
    public class Payment
    {
        public string Title { get; set; }   // Məsələn: Yanvar maaşı
        public double Amount { get; set; }  // Məbləğ
    }

    // İşçi məlumatları (Kod, Ad, Soyad, Yaş və ödənişlər siyahısı)
    public class Employee
    {
        public string Code { get; set; }     // İşçi kodu
        public string FirstName { get; set; }// Ad
        public string LastName { get; set; } // Soyad
        public int Age { get; set; }         // Yaş
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
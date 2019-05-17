using System;

namespace Freet.DTO
{
    public class UzytkownikAddDTO
    {        
        public string Login { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Haslo { get; set; }
        public Int64 ZespolId { get; set; }
    }
}
using System;

namespace FreeT.DTO
{
  public class UrlopAddDTO
  {
    public Int64 Id { get; set; }
    public DateTime Data_Od { get; set; }
    public DateTime Data_Do { get; set; }
    public Int64 Uzytkownik_Id { get; set; }
  }
}
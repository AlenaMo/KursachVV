//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocumentHall.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public Nullable<int> UserId_ { get; set; }
        public Nullable<int> Numer { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TypeOrder { get; set; }
        public string Control { get; set; }
    
        public virtual Document Document { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
    }
}

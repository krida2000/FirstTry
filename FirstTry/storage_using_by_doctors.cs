//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirstTry
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class storage_using_by_doctors
    {
        public string stamp { get; set; }
        public int id { get; set; }
        [Display(Name = "ФІО")]
        public string FIO { get; set; }
        [Display(Name = "Назва предмету")]
        public string Item_name { get; set; }
        [Display(Name = "Кількість")]
        public Nullable<int> Count { get; set; }
        [Display(Name = "Процент")]
        public string Percent { get; set; }
    }
}

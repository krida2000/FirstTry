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

    public partial class doctor_visits
    {
        public int id { get; set; }

        [Display(Name = "Лікар")]
        public int doctor_id { get; set; }

        [Display(Name = "Паціент")]
        public int patient_id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public System.DateTime date { get; set; }

        [Display(Name = "Причина")]
        public string reason { get; set; }

        [Display(Name = "Результат обстеження")]
        public string result { get; set; }

        [Display(Name = "Лікування")]
        public string treatment { get; set; }
    
        public virtual doctors doctors { get; set; }

        public virtual patients patients { get; set; }
    }
}

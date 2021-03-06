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

    public partial class placing_in_hospital
    {
        public int id { get; set; }

        [Display(Name = "Відповідальний лікар")]
        public int attending_doctor_id { get; set; }

        [Display(Name = "Паціент")]
        public int patient_id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Дата початку")]
        public System.DateTime date_start { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Дата закінчення")]
        public Nullable<System.DateTime> date_end { get; set; }

        [Display(Name = "Причина")]
        public string reason { get; set; }

        [Display(Name = "Лікування")]
        public string treatment { get; set; }
    
        public virtual doctors doctors { get; set; }
        public virtual patients patients { get; set; }
    }
}

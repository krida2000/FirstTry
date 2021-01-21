﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class polyclinicEntities : DbContext
    {
        public polyclinicEntities()
            : base("name=polyclinicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<doctor_visits> doctor_visits { get; set; }
        public virtual DbSet<doctors> doctors { get; set; }
        public virtual DbSet<patients> patients { get; set; }
        public virtual DbSet<placing_in_hospital> placing_in_hospital { get; set; }
        public virtual DbSet<storage> storage { get; set; }
        public virtual DbSet<storage_using> storage_using { get; set; }
        public virtual DbSet<items_using_by_doctors> items_using_by_doctors { get; set; }
        public virtual DbSet<storage_using_by_doctors> storage_using_by_doctors { get; set; }
    
        public virtual int del_doctor_visits(Nullable<int> id, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("del_doctor_visits", idParameter, rez);
        }
    
        public virtual int del_doctors(Nullable<int> id, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("del_doctors", idParameter, rez);
        }
    
        public virtual int del_patients(Nullable<int> id, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("del_patients", idParameter, rez);
        }
    
        public virtual int del_placing_in_hospital(Nullable<int> id, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("del_placing_in_hospital", idParameter, rez);
        }
    
        public virtual int del_storage(Nullable<int> item_id, ObjectParameter rez)
        {
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("del_storage", item_idParameter, rez);
        }
    
        public virtual int del_storage_using(Nullable<int> id, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("del_storage_using", idParameter, rez);
        }
    
        public virtual int ins_doctor_visits(Nullable<int> doctor_id, Nullable<int> patient_id, Nullable<System.DateTime> date, string reason, string result, string treatment, ObjectParameter rez)
        {
            var doctor_idParameter = doctor_id.HasValue ?
                new ObjectParameter("doctor_id", doctor_id) :
                new ObjectParameter("doctor_id", typeof(int));
    
            var patient_idParameter = patient_id.HasValue ?
                new ObjectParameter("patient_id", patient_id) :
                new ObjectParameter("patient_id", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            var reasonParameter = reason != null ?
                new ObjectParameter("reason", reason) :
                new ObjectParameter("reason", typeof(string));
    
            var resultParameter = result != null ?
                new ObjectParameter("result", result) :
                new ObjectParameter("result", typeof(string));
    
            var treatmentParameter = treatment != null ?
                new ObjectParameter("treatment", treatment) :
                new ObjectParameter("treatment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_doctor_visits", doctor_idParameter, patient_idParameter, dateParameter, reasonParameter, resultParameter, treatmentParameter, rez);
        }
    
        public virtual int ins_doctors(string fIO, string phone, Nullable<int> salary, Nullable<System.DateTime> date_start_work, Nullable<System.DateTime> date_end_work, string position, Nullable<int> qualification, string address, ObjectParameter rez)
        {
            var fIOParameter = fIO != null ?
                new ObjectParameter("FIO", fIO) :
                new ObjectParameter("FIO", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("phone", phone) :
                new ObjectParameter("phone", typeof(string));
    
            var salaryParameter = salary.HasValue ?
                new ObjectParameter("salary", salary) :
                new ObjectParameter("salary", typeof(int));
    
            var date_start_workParameter = date_start_work.HasValue ?
                new ObjectParameter("date_start_work", date_start_work) :
                new ObjectParameter("date_start_work", typeof(System.DateTime));
    
            var date_end_workParameter = date_end_work.HasValue ?
                new ObjectParameter("date_end_work", date_end_work) :
                new ObjectParameter("date_end_work", typeof(System.DateTime));
    
            var positionParameter = position != null ?
                new ObjectParameter("position", position) :
                new ObjectParameter("position", typeof(string));
    
            var qualificationParameter = qualification.HasValue ?
                new ObjectParameter("qualification", qualification) :
                new ObjectParameter("qualification", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_doctors", fIOParameter, phoneParameter, salaryParameter, date_start_workParameter, date_end_workParameter, positionParameter, qualificationParameter, addressParameter, rez);
        }
    
        public virtual int ins_patients(string fIO, string phone, Nullable<System.DateTime> date_birth, string address, ObjectParameter rez)
        {
            var fIOParameter = fIO != null ?
                new ObjectParameter("FIO", fIO) :
                new ObjectParameter("FIO", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("phone", phone) :
                new ObjectParameter("phone", typeof(string));
    
            var date_birthParameter = date_birth.HasValue ?
                new ObjectParameter("date_birth", date_birth) :
                new ObjectParameter("date_birth", typeof(System.DateTime));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_patients", fIOParameter, phoneParameter, date_birthParameter, addressParameter, rez);
        }
    
        public virtual int ins_placing_in_hospital(Nullable<int> attending_doctor_id, Nullable<int> patient_id, Nullable<System.DateTime> date_start, Nullable<System.DateTime> date_end, string reason, string treatment, ObjectParameter rez)
        {
            var attending_doctor_idParameter = attending_doctor_id.HasValue ?
                new ObjectParameter("attending_doctor_id", attending_doctor_id) :
                new ObjectParameter("attending_doctor_id", typeof(int));
    
            var patient_idParameter = patient_id.HasValue ?
                new ObjectParameter("patient_id", patient_id) :
                new ObjectParameter("patient_id", typeof(int));
    
            var date_startParameter = date_start.HasValue ?
                new ObjectParameter("date_start", date_start) :
                new ObjectParameter("date_start", typeof(System.DateTime));
    
            var date_endParameter = date_end.HasValue ?
                new ObjectParameter("date_end", date_end) :
                new ObjectParameter("date_end", typeof(System.DateTime));
    
            var reasonParameter = reason != null ?
                new ObjectParameter("reason", reason) :
                new ObjectParameter("reason", typeof(string));
    
            var treatmentParameter = treatment != null ?
                new ObjectParameter("treatment", treatment) :
                new ObjectParameter("treatment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_placing_in_hospital", attending_doctor_idParameter, patient_idParameter, date_startParameter, date_endParameter, reasonParameter, treatmentParameter, rez);
        }
    
        public virtual int ins_storage(string item_name, Nullable<int> count, Nullable<int> price, Nullable<System.DateTime> expiration_date, Nullable<bool> is_written_off, ObjectParameter rez)
        {
            var item_nameParameter = item_name != null ?
                new ObjectParameter("item_name", item_name) :
                new ObjectParameter("item_name", typeof(string));
    
            var countParameter = count.HasValue ?
                new ObjectParameter("count", count) :
                new ObjectParameter("count", typeof(int));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(int));
    
            var expiration_dateParameter = expiration_date.HasValue ?
                new ObjectParameter("expiration_date", expiration_date) :
                new ObjectParameter("expiration_date", typeof(System.DateTime));
    
            var is_written_offParameter = is_written_off.HasValue ?
                new ObjectParameter("is_written_off", is_written_off) :
                new ObjectParameter("is_written_off", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_storage", item_nameParameter, countParameter, priceParameter, expiration_dateParameter, is_written_offParameter, rez);
        }
    
        public virtual int ins_storage_using(Nullable<int> doctor_id, Nullable<int> item_id, Nullable<int> count, Nullable<System.DateTime> date, ObjectParameter rez)
        {
            var doctor_idParameter = doctor_id.HasValue ?
                new ObjectParameter("doctor_id", doctor_id) :
                new ObjectParameter("doctor_id", typeof(int));
    
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            var countParameter = count.HasValue ?
                new ObjectParameter("count", count) :
                new ObjectParameter("count", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ins_storage_using", doctor_idParameter, item_idParameter, countParameter, dateParameter, rez);
        }
    
        public virtual ObjectResult<string> me_proc(Nullable<int> id_doc, Nullable<int> id_item)
        {
            var id_docParameter = id_doc.HasValue ?
                new ObjectParameter("id_doc", id_doc) :
                new ObjectParameter("id_doc", typeof(int));
    
            var id_itemParameter = id_item.HasValue ?
                new ObjectParameter("id_item", id_item) :
                new ObjectParameter("id_item", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("me_proc", id_docParameter, id_itemParameter);
        }
    
        public virtual ObjectResult<test_proc_Result> test_proc(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<test_proc_Result>("test_proc", idParameter);
        }
    
        public virtual int upd_doctor_visits(Nullable<int> doctor_id, Nullable<int> patient_id, Nullable<System.DateTime> date, string reason, string result, string treatment, ObjectParameter rez)
        {
            var doctor_idParameter = doctor_id.HasValue ?
                new ObjectParameter("doctor_id", doctor_id) :
                new ObjectParameter("doctor_id", typeof(int));
    
            var patient_idParameter = patient_id.HasValue ?
                new ObjectParameter("patient_id", patient_id) :
                new ObjectParameter("patient_id", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            var reasonParameter = reason != null ?
                new ObjectParameter("reason", reason) :
                new ObjectParameter("reason", typeof(string));
    
            var resultParameter = result != null ?
                new ObjectParameter("result", result) :
                new ObjectParameter("result", typeof(string));
    
            var treatmentParameter = treatment != null ?
                new ObjectParameter("treatment", treatment) :
                new ObjectParameter("treatment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("upd_doctor_visits", doctor_idParameter, patient_idParameter, dateParameter, reasonParameter, resultParameter, treatmentParameter, rez);
        }
    
        public virtual int upd_doctors(Nullable<int> id, string fIO, string phone, Nullable<int> salary, Nullable<System.DateTime> date_start_work, Nullable<System.DateTime> date_end_work, string position, Nullable<int> qualification, string address, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var fIOParameter = fIO != null ?
                new ObjectParameter("FIO", fIO) :
                new ObjectParameter("FIO", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("phone", phone) :
                new ObjectParameter("phone", typeof(string));
    
            var salaryParameter = salary.HasValue ?
                new ObjectParameter("salary", salary) :
                new ObjectParameter("salary", typeof(int));
    
            var date_start_workParameter = date_start_work.HasValue ?
                new ObjectParameter("date_start_work", date_start_work) :
                new ObjectParameter("date_start_work", typeof(System.DateTime));
    
            var date_end_workParameter = date_end_work.HasValue ?
                new ObjectParameter("date_end_work", date_end_work) :
                new ObjectParameter("date_end_work", typeof(System.DateTime));
    
            var positionParameter = position != null ?
                new ObjectParameter("position", position) :
                new ObjectParameter("position", typeof(string));
    
            var qualificationParameter = qualification.HasValue ?
                new ObjectParameter("qualification", qualification) :
                new ObjectParameter("qualification", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("upd_doctors", idParameter, fIOParameter, phoneParameter, salaryParameter, date_start_workParameter, date_end_workParameter, positionParameter, qualificationParameter, addressParameter, rez);
        }
    
        public virtual int upd_patients(Nullable<int> id, string fIO, string phone, Nullable<System.DateTime> date_birth, string address, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var fIOParameter = fIO != null ?
                new ObjectParameter("FIO", fIO) :
                new ObjectParameter("FIO", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("phone", phone) :
                new ObjectParameter("phone", typeof(string));
    
            var date_birthParameter = date_birth.HasValue ?
                new ObjectParameter("date_birth", date_birth) :
                new ObjectParameter("date_birth", typeof(System.DateTime));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("upd_patients", idParameter, fIOParameter, phoneParameter, date_birthParameter, addressParameter, rez);
        }
    
        public virtual int upd_placing_in_hospital(Nullable<int> id, Nullable<int> attending_doctor_id, Nullable<int> patient_id, Nullable<System.DateTime> date_start, Nullable<System.DateTime> date_end, string reason, string treatment, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var attending_doctor_idParameter = attending_doctor_id.HasValue ?
                new ObjectParameter("attending_doctor_id", attending_doctor_id) :
                new ObjectParameter("attending_doctor_id", typeof(int));
    
            var patient_idParameter = patient_id.HasValue ?
                new ObjectParameter("patient_id", patient_id) :
                new ObjectParameter("patient_id", typeof(int));
    
            var date_startParameter = date_start.HasValue ?
                new ObjectParameter("date_start", date_start) :
                new ObjectParameter("date_start", typeof(System.DateTime));
    
            var date_endParameter = date_end.HasValue ?
                new ObjectParameter("date_end", date_end) :
                new ObjectParameter("date_end", typeof(System.DateTime));
    
            var reasonParameter = reason != null ?
                new ObjectParameter("reason", reason) :
                new ObjectParameter("reason", typeof(string));
    
            var treatmentParameter = treatment != null ?
                new ObjectParameter("treatment", treatment) :
                new ObjectParameter("treatment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("upd_placing_in_hospital", idParameter, attending_doctor_idParameter, patient_idParameter, date_startParameter, date_endParameter, reasonParameter, treatmentParameter, rez);
        }
    
        public virtual int upd_storage(Nullable<int> item_id, string item_name, Nullable<int> count, Nullable<int> price, Nullable<System.DateTime> expiration_date, Nullable<bool> is_written_off, ObjectParameter rez)
        {
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            var item_nameParameter = item_name != null ?
                new ObjectParameter("item_name", item_name) :
                new ObjectParameter("item_name", typeof(string));
    
            var countParameter = count.HasValue ?
                new ObjectParameter("count", count) :
                new ObjectParameter("count", typeof(int));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(int));
    
            var expiration_dateParameter = expiration_date.HasValue ?
                new ObjectParameter("expiration_date", expiration_date) :
                new ObjectParameter("expiration_date", typeof(System.DateTime));
    
            var is_written_offParameter = is_written_off.HasValue ?
                new ObjectParameter("is_written_off", is_written_off) :
                new ObjectParameter("is_written_off", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("upd_storage", item_idParameter, item_nameParameter, countParameter, priceParameter, expiration_dateParameter, is_written_offParameter, rez);
        }
    
        public virtual int upd_storage_using(Nullable<int> id, Nullable<int> doctor_id, Nullable<int> item_id, Nullable<int> count, Nullable<System.DateTime> date, ObjectParameter rez)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var doctor_idParameter = doctor_id.HasValue ?
                new ObjectParameter("doctor_id", doctor_id) :
                new ObjectParameter("doctor_id", typeof(int));
    
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            var countParameter = count.HasValue ?
                new ObjectParameter("count", count) :
                new ObjectParameter("count", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("upd_storage_using", idParameter, doctor_idParameter, item_idParameter, countParameter, dateParameter, rez);
        }
    }
}

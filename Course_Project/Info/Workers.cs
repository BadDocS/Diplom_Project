//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Course_Project.Info
{
    using System;
    using System.Collections.Generic;
    
    public partial class Workers
    {
        public int id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Mid_name { get; set; }
        public Nullable<int> Pos_work { get; set; }
        public Nullable<int> Area_work { get; set; }
        public string FIO { get; set; }
    
        public virtual Position Position { get; set; }
        public virtual Work_Areas Work_Areas { get; set; }
    }
}

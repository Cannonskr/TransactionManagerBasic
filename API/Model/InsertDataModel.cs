using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class InsertDataModel
    {
        public TableADataModel TableAData { get; set; }
        public TableBDataModel TableBData { get; set; }
        
    }

    public class TableADataModel 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class TableBDataModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NickName { get; set; }
    }


}
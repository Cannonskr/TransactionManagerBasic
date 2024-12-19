using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.Service
{
    public interface ITableAService
    {
        Task InsertDataAsync(TableADataModel data,DbTransaction transaction);
    }
}
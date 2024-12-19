using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controller
{
    [Route("[controller]")]
    public class DataController : ControllerBase
{
    private readonly ITableAService _tableAService;
    private readonly ITableBService _tableBService;
    private readonly DbConnection _connection;

    public DataController(ITableAService tableAService, ITableBService tableBService, DbConnection connection)
    {
        _tableAService = tableAService;
        _tableBService = tableBService;
        _connection = connection; 
    }

    [HttpPost("insert")]
    public async Task<IActionResult> InsertData([FromBody] InsertDataModel request)
    {
        DbTransaction transaction = null;

        try
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                await _connection.OpenAsync();
            transaction = _connection.BeginTransaction();

            await _tableAService.InsertDataAsync(request.TableAData, transaction);
            await _tableBService.InsertDataAsync(request.TableBData, transaction);

            transaction.Commit();

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            transaction?.Rollback();
            return BadRequest(new { success = false, message = ex.Message });
        }
        finally
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
}
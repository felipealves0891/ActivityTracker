using System.Text;
using ActivityTracker.Core;
using Dapper;
using Npgsql;

namespace ActivityTracker.Api.Services.RepositoryService;

public sealed class Repository : IRepository
{
    private readonly NpgsqlConnection connection;

    public Repository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgresql") ??
            throw new ArgumentNullException("ConnectionString:Postgresql");

        connection = new NpgsqlConnection(connectionString);
    }

    public async Task<IEnumerable<ActivityEntity>> GetActivitiesAsync()
    {
        var query = @"select 
                        id as Id, 
                        uuid as Uuid, 
                        machine_name as MachineName, 
                        process_name as ProcessName, 
                        window_title as WindowTitle, 
                        start_time as Start, 
                        end_time as End 
                      from activities.activity";
                      
        return await connection.QueryAsync<ActivityEntity>(query);
    }

    public async Task<ActivityEntity> GetActivityByIdAsync(int id)
    {
        var query =$@"select 
                        id as Id, 
                        uuid as Uuid, 
                        machine_name as MachineName, 
                        process_name as ProcessName, 
                        window_title as WindowTitle, 
                        start_time as Start, 
                        end_time as End 
                      from activities.activity
                      where id = {id}";
                      
        return await connection.QueryFirstAsync<ActivityEntity>(query);
    }

    public async Task<ActivityEntity> GetActivityLastetAsync()
    {
        var query =$@"select 
                        id as Id, 
                        uuid as Uuid, 
                        machine_name as MachineName, 
                        process_name as ProcessName, 
                        window_title as WindowTitle, 
                        start_time as Start, 
                        end_time as End 
                      from activities.activity
                      where id = (select max(id) from activities.activity)";
                      
        return await connection.QueryFirstAsync<ActivityEntity>(query);
    }

    public void Dispose()
    {
        if (connection is not null)
            connection.Dispose();
    }

}

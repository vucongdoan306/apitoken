using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace apitoken.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{

    private IConfiguration _configuration;
    private string connectionString;

    public LoginController(IConfiguration config)
    {
        this._configuration = config;
        this.connectionString = config.GetConnectionString("Default");
    }


    [HttpGet(Name = "GetToken/{id}")]
    public string GetToken([FromQuery]string id)
    {
        var connection = new MySqlConnection(connectionString);
        var users = connection.Query<string>("select user_code from user_login where user_id = @userId", new { userId = id}).FirstOrDefault();
        return users;
    }
}

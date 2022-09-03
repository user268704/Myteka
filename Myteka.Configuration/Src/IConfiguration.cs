using Myteka.Configuration.Models;

namespace Myteka.Configuration;

public interface IConfiguration
{
    ConfigModel GetConfig();
}
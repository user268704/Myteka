using System.Security.AccessControl;
using Myteka.Configuration.Models.Points;
using Myteka.Configuration.Models.Url;

namespace Myteka.Configuration.Models;

public class ConfigModel
{
    public EndPoints EndPoints { get; set; }
    public Urls Urls { get; set; }
    public Global Global { get; set; }
}
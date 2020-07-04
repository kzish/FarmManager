using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

public class Globals
{
    public static string log_file = "C:\\rubiem\\simba\\simba_logs.txt";
    public static string uploads_folder = @"c:\rubiem\telepathy\uploads\";
    //config file path
    public static string simba_config = @"c:\rubiem\simba\simba_config.json";//this is keyvalue pairs json file with configuration



    public static void log_data_to_file(string source, object data)
    {
        try
        {
            dynamic obj = new JObject();
            obj.source = source;
            obj.msg = data.ToString();
            var logdata = JsonConvert.SerializeObject(obj);
            System.IO.File.AppendAllText(log_file, logdata+Environment.NewLine);
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText(log_file, ex.Message+Environment.NewLine);
        }
    }


    //use the application name as is verbatim for the microservices names
    /// <summary>
    /// return the key from the config file
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static dynamic read_config(string key)
    {
        var file = System.IO.File.ReadAllText(simba_config);
        dynamic obj = JsonConvert.DeserializeObject(file);
        return obj.key;
    }
    /// <summary>
    ///write the key to the file
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void write_config(string key, string value)
    {

        if (!File.Exists(simba_config))
        {
            var path = Path.GetDirectoryName(simba_config);
            Directory.CreateDirectory(path);
            File.Create(simba_config).Close();
        };
        var file = File.ReadAllText(simba_config);
        dynamic obj = JsonConvert.DeserializeObject(file);
        if (obj == null)
        {
            obj = new JObject();
        }
        obj[key] = value;
        File.WriteAllText(simba_config, obj.ToString());
    }


}

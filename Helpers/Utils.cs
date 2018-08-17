using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

public class Utils
{    
    public static byte[] ConvertToByteArray(object value)
    {
        var objSession = JsonConvert.SerializeObject(value);
        var bytSession = Encoding.ASCII.GetBytes(objSession);

        return bytSession;
    }

    public static string ConvertBytesToString(byte[] value)
    {
        var strSession = Encoding.ASCII.GetString(value);
        var str = JsonConvert.DeserializeObject<string>(strSession);
        return str;
    }
}
 
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
        if(value == null) { return string.Empty; }

        var strSession = Encoding.ASCII.GetString(value);
        var str = JsonConvert.DeserializeObject<string>(strSession);
        return str;
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
 
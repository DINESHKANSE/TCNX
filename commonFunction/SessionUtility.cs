using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TCNX.commonFunction
{
    public class SessionUtility
    {
      
            private readonly IHttpContextAccessor HttpContextAccessor;
            public SessionUtility(IHttpContextAccessor httpContextAccessor)
            {
                HttpContextAccessor = httpContextAccessor;
            }

        public SessionUtility()
        {
        }

        public void SetSession(string key, string value)
            {
                HttpContextAccessor.HttpContext.Session.SetString(key, value);
            }
            public string GetSession(string key)
            {
                return HttpContextAccessor.HttpContext.Session.GetString(key);
            }
    }
}

public static class SessionExtensions
{


    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }

    public static void SetSession(this ISession session, string key, string value)
    {
        session.SetString(key, value);
    }
    public static string GetSession(this ISession session, string key)
    {
        return session.GetString(key);
    }
}
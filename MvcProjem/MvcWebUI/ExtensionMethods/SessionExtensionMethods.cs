using Newtonsoft.Json;

namespace MvcWebUI.ExtensionMethods
{
    public static class SessionExtensionMethods
    {
        //Sessionları burada tutacagız.
        //Extension Method yazacagım
        public static void SetObject(this ISession session,string key,object value)
        {
            string objectString=JsonConvert.SerializeObject(value);
            session.SetString(key,objectString);
        }

        public static T GetObject<T>(this ISession session,string key) where T : class
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            T value = JsonConvert.DeserializeObject<T>(objectString);
            //yani object string i T türünde nesne haline getir ve Object türünde aktar .
            return value;
        }
    }
}

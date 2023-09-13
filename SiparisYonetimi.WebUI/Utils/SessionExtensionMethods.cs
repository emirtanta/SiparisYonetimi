using Newtonsoft.Json;

namespace SiparisYonetimi.WebUI.Utils
{
    /// <summary>
    /// kart işlemleri için tanımlandı
    /// </summary>
    public static class SessionExtensionMethods
    {
        public static void SetJson(this ISession session,string key,object value)
        {
            string objectString=JsonConvert.SerializeObject(value); //string bir değişken oluşturup parametreden gelen value değerini json tipine çevirip değişkene aktardık


            // session a json nesnesini string olarak yükledik
            session.SetString(key,objectString);
        }

        // sessiondaki veriyi T(yani generic olarak) tutan ve bize kullanmak istediğimiz yerde getirecek metodumuz
        public static T GetJson<T>(this ISession session,string key)where T : class 
        {
            string objectString=session.GetString(key);

            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }

            // json verimizi tekrardan nesneye çevirip çağrıldığı yere gönderdik.
            T value =JsonConvert.DeserializeObject<T>(objectString);

            return value;
        }
    }
}

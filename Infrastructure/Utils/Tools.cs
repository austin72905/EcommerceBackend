namespace Infrastructure.Utils
{
    public class Tools
    {
        /// <summary>
        ///時間戳_10位(秒)
        /// </summary>
        public static long TimeStamp()
        {
            var dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((System.DateTime.Now - dateStart).TotalSeconds);
            return timeStamp;
        }

        
    }
}

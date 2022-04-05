namespace healthcare_visuzlier25.Util
{
    public class Time
    {
        public static long GetEpochTime()
        {
            long epoch = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            return epoch;
        }
    }
}

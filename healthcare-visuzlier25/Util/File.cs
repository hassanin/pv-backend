namespace healthcare_visuzlier25.Util
{
    public class FileTools
    {
        private static string downloadsSubDir = "downloads";
        public static string GetNewFile(string fileSuffix = "")
        {
            var currentDir = Path.GetTempPath();
            var downloadsDir = Path.Combine(currentDir, downloadsSubDir);

            if (!Directory.Exists(downloadsDir))
            {
                Directory.CreateDirectory(downloadsDir);
            }

            var fileName = $"{Guid.NewGuid()}-{fileSuffix}";
            var finalFileName = Path.Combine(downloadsDir, fileName);
            return finalFileName;

        }

    }
}

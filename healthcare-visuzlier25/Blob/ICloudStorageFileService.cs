namespace healthcare_visuzlier25.Blob
{
    public interface ICloudStorageFileService
    {
        /// <summary>
        /// Uploadsa file to the cloud
        /// </summary>
        /// <param name="path"> a local path where the file that is desired to be uploaded is located</param>
        /// <returns> the URL of the uploaded blob that can be used later to retreive the file</returns>
        public Task<string> UploadFile(string path);

        /// <summary>
        /// Downloads a file using its URL
        /// </summary>
        /// <param name="url">a URL representing the path of the file to be downloaded </param>
        /// <returns> a local temp path where the downloaded file is located</returns>
        public Task<string> DownloadFile(string url);

        public Task<string> GenerateSASToken(TimeSpan timeSpan);
        public async Task<string> GenerateSASToken()
        {
            TimeSpan span = TimeSpan.FromMinutes(15); // default is 15 minutes
            return await GenerateSASToken(span);
        }
    }
}

namespace healthcare_visuzlier25.Storage
{
    public interface IFileService
    {
        //TODO:
        // Later will add support for streaming file content and saving file as a stream
        // For now we will just treat the file store as a Simple Key Value store
        public Task<string> UploadFile(string fileName, string base64EncodedContent);
        /// <summary>
        /// returns the base64 encodded string 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<string> DownloadFile(string fileName);
        public Task<string> GenerateSASToken(TimeSpan timeSpan);
        public async Task<string> GenerateSASToken()
        {
            TimeSpan span = TimeSpan.FromMinutes(15); // default is 15 minutes
            return await GenerateSASToken(span);
        }
        public string BaseContainerUrl();

    }
}

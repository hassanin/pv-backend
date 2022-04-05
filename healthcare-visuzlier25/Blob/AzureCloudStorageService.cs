using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace healthcare_visuzlier25.Blob
{
    public class AzureCloudStorageService : ICloudStorageFileService
    {

        public async Task<string> DownloadFile(string blobName)
        {
            blobContainerClient.GetBlobsAsync(prefix: "");
            // List all blobs in the container
            await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
            {
                BlobClient blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                logger.LogDebug($" blob name is  {blobItem.Name}");
                var fileName = Util.FileTools.GetNewFile(blobItem.Name);
                var response = await blobClient.DownloadToAsync(fileName);
                return fileName;
            }
            throw new Exception($"file {blobName} not found in the Azure blob");

        }

        public async Task<string> UploadFile(string path)
        {
            string extension = System.IO.Path.GetExtension(path);
            string blobName = $"{Guid.NewGuid()}{extension}";
            // Get a reference to a blob
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

            // Upload data from the local file
            var res = await blobClient.UploadAsync(path, true);

            return blobClient.Uri.ToString();

        }
        /// <summary>
        /// Returns the SAS token with the question mark in front
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public async Task<string> GenerateSASToken(TimeSpan timeSpan)
        {
            var fullBlobSASURI = GetServiceSasUriForContainer(this.blobContainerClient, timeSpan);
            return fullBlobSASURI.Query; // includes the question mark
            //return GetServiceSasUriForContainer(this.blobContainerClient,timeSpan).ToString();
        }

        internal static Uri GetServiceSasUriForContainer(BlobContainerClient containerClient, TimeSpan span,
                                          string storedPolicyName = null)
        {
            // Check whether this BlobContainerClient object has been authorized with Shared Key.
            if (containerClient.CanGenerateSasUri)
            {
                // Create a SAS token that's valid for one hour.
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = containerClient.Name,
                    Resource = "c"
                };

                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow + span;
                    sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }

                Uri sasUri = containerClient.GenerateSasUri(sasBuilder);
                Console.WriteLine("SAS URI for blob container is: {0}", sasUri);
                Console.WriteLine();

                return sasUri;
            }
            else
            {
                throw new Exception(@"BlobContainerClient must be authorized with Shared Key 
                          credentials to create a service SAS.");
            }
        }

        public AzureCloudStorageService(ILogger<AzureCloudStorageService> logger, BlobContainerClient blobContainerClient)
        {
            this.logger = logger;
            this.blobContainerClient = blobContainerClient;
            logger.LogInformation("Constructed UploadFile");

        }

        private ILogger<AzureCloudStorageService> logger;
        private BlobContainerClient blobContainerClient;
    }
}

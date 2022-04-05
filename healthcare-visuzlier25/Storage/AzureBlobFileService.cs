using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using healthcare_visuzlier25.Config;
using Microsoft.Extensions.Options;

namespace healthcare_visuzlier25.Storage
{
    public class AzureBlobFileService : IFileService
    {
        private readonly ILogger<AzureBlobFileService> _logger;
        private readonly BlobContainerClient _blobContainerClient;
        private readonly BlobConfig _blobConfig;
        public AzureBlobFileService(ILogger<AzureBlobFileService> logger, IOptions<BlobConfig> blobConfigOptions)
        {
            _logger = logger;
            _blobConfig = blobConfigOptions.Value;
            _blobContainerClient = new BlobContainerClient(_blobConfig.ConnectionString, _blobConfig.ContainerName);

        }
        public Task<string> DownloadFile(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            //TODO : GET FILE size and abort if file is bigger than a certain threshold (2 MB) maby
            //blobClient.GetProperties().Value.ContentLength
            var content = blobClient.DownloadContent();
            var res = content.Value.Content.ToString();
            return Task.FromResult(res);
        }

        public Task<string> GenerateSASToken(TimeSpan timeSpan)
        {
            var fullBlobSASURI = GetServiceSasUriForContainer(_blobContainerClient, timeSpan);
            return Task.FromResult(fullBlobSASURI.Query); // includes the question mark
        }

        public Task<string> UploadFile(string fileName, string base64EncodedContent)
        {
            _blobContainerClient.UploadBlobAsync(fileName, new BinaryData(base64EncodedContent));
            return Task.FromResult(fileName);
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

        public string BaseContainerUrl()
        {
            return _blobContainerClient.Uri.ToString();
        }
    }
}

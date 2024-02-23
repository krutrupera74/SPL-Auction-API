using auction.Models.Domain;
using auction.Models.Enums;
using auction.Repositories.Interface;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static auction.Models.Domain.FileUploadModel;
using static auction.Models.Enums.EnumUtils;

namespace auction.Helpers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadHelper : IFileUploadHelper
    {

        private readonly IConfiguration _configuration;

        public FileUploadHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<string> UploadImageToBlobStorage(IFormFile image)
        {
            // Get connection string and container name from configuration
            string connectionString = _configuration.GetValue<string>("AzureBlobStorage:ConnectionString");
            string containerName = _configuration.GetValue<string>("AzureBlobStorage:ContainerName");

            // Create a BlobServiceClient
            var blobServiceClient = new BlobServiceClient(connectionString);

            // Get a reference to a container
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Upload image to Blob Storage
            var blobClient = blobContainerClient.GetBlobClient(Guid.NewGuid().ToString() + ".jpg");
            await blobClient.UploadAsync(image.OpenReadStream(), true);

            // Return the URL of the uploaded image
            return blobClient.Uri.ToString();
        }
    }
}

using auction.Models.Domain;
using auction.Models.Enums;
using auction.Repositories.Interface;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static auction.Models.Domain.FileUploadModel;
using static auction.Models.Enums.EnumUtils;

namespace auction.Helpers
{
    public class FileUploadHelper : IFileUploadHelper
    {

        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;

        public FileUploadHelper(IConfiguration configuration, BlobServiceClient blobServiceClient)
        {
            _configuration = configuration;
            this._blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            if (file.ContentType.ToLower() != "image/jpeg" && file.ContentType.ToLower() != "image/png")
            {
                throw new ArgumentException("Only JPEG and PNG images are allowed");
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureBlobStorage:ContainerName"]);
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            // Get the URL of the uploaded image
            return blobClient.Uri.AbsoluteUri;
        }

    }
}

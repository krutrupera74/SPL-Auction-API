﻿using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface IFileUploadHelper
    {
        Task<string> UploadImage(IFormFile file, string folderName);
    }
}

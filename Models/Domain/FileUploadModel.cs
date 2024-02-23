namespace auction.Models.Domain
{
    public class FileUploadModel
    {

        public string Blob { get; set; }
        public UploadFiles File { get; set; }

        public class UploadFiles
        {
            public string FileName { get; set; }
            public string FileData { get; set; }
        }
    }
}

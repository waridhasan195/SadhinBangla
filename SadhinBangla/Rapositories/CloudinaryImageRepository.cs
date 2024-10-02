
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SadhinBangla.Rapositories
{
    public class CloudinaryImageRepository : IImageRapository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudnary")["CloudName"],
                configuration.GetSection("Cloudnary")["ApiKey"],
                configuration.GetSection("Cloudnary")["ApiSecret"]
                );
        }

        public async Task<string> UploadAsunc(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            }; 

            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadParams != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
 
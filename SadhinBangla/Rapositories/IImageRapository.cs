namespace SadhinBangla.Rapositories
{
    public interface IImageRapository
    {
        Task<string> UploadAsunc(IFormFile file);
    }
}

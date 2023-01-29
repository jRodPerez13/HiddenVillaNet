using HiddenVilla.Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla.Server.Service;

public class FileUpload : IFileUpload
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }
    public bool DeleteFile(string fileName)
    {
        try
        {
            var path = $"{_webHostEnvironment.WebRootPath}\\RoomImages\\{fileName}";
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> UploadFile(IBrowserFile file)
    {
        try
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\RoomImages";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RoomImages", fileName);

            var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);

            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fs);
            }

            var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";

            var fullpath = $"{url}RoomImages/{fileName}";
            return fullpath;
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
}
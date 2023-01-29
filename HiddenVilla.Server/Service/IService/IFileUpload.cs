using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla.Server.Service.IService;
public interface IFileUpload
{
    Task<string> UploadFile(IBrowserFile file);

    bool DeleteFile(string fileName);
}

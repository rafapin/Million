using Microsoft.AspNetCore.Mvc;
using Million.Domain.Features.Shared.Constants;
using Million.Domain.Features.Shared.Entities;

namespace Million.WebAPI.Extensions
{
    public static class FormFileExtension
    {
        public static async Task<CustomFile?> ToCustomFile(this IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                if (file == null)
                    return null;
                await file.CopyToAsync(memoryStream);

                byte[] fileBytes = memoryStream.ToArray();

                return new CustomFile(fileBytes, file.FileName, file.ContentType, file.Length);
            }
        }

        public static FileContentResult ToFileContentResult(this CustomFile customFile)
        {
            var fileContent = customFile.Type ?? ApplicationTypes.OctectStream;
            var fileContentResult = new FileContentResult(customFile.File, fileContent)
            {
                FileDownloadName = customFile.FullName,
            };
            return fileContentResult;
        }
    }
}

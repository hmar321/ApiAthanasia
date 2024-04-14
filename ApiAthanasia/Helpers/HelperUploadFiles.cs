using ApiAthanasia.Helpers;

namespace ApiAthanasia.Helpers
{
    public class HelperUploadFiles
    {
        private HelperPathProvider helperPathProvider;

        public HelperUploadFiles(HelperPathProvider helperPathProvider)
        {
            this.helperPathProvider = helperPathProvider;
        }

        public async Task<string> UpdateFileAsync(IFormFile file, FoldersImages folder)
        {
            string fileName = file.FileName;
            string path = this.helperPathProvider.MapPath(fileName, folder);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}

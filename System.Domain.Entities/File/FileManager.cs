using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace System.Domain.Entities.FileManager
{
    public class FileManager
    {
        private HttpPostedFileBase[] files;
        private string folderPath;
        public string FullFilePath { get; private set; }
        public FileManager(HttpPostedFileBase[] files, string folderPath)
        {
            this.files = files;
            this.folderPath = folderPath;
        }

        public void UploadFile()
        {
            if (files != null)
            {
                CreateFolder();
                foreach (var file in files)
                {
                    SetFullPathFile(file.FileName);
                    SaveFile(file);
                }
            }
        }

        private void CreateFolder()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
        private void SetFullPathFile(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            var fileNameRandom = $"{Path.GetRandomFileName()}";
            FullFilePath = $@"{folderPath}\{fileNameRandom}.{ext}";
            //FullFilePath = HostingEnvironment.MapPath(Path.Combine(folderPath, fileNameRandom));
        }
        private void SaveFile(HttpPostedFileBase file)
        {
            file.SaveAs(FullFilePath);
        }
    }
}

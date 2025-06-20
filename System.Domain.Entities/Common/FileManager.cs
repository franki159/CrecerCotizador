using System.IO;
using System.Web;

namespace System.Domain.Entities.Common
{
    public class FileManager
    {
        private string pathFile;
        private HttpPostedFileBase[] files;
        private Int64 idSolicitud;

        public string FullPathFile { get; private set; }
        public string FileName { get; private set; }
        public string Extension { get; private set; }
        public int Size { get; private set; }
        public FileManager(string pathFile, FileRequest fileRequest)
        {
            this.pathFile = pathFile;
            this.files = fileRequest.Files;
            idSolicitud = Convert.ToInt64(fileRequest.scodigosolicitud);
            Directory.CreateDirectory(pathFile);
        }

        public void Save()
        {
            foreach (HttpPostedFileBase file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    FileName = file.FileName;
                    Extension = Path.GetExtension(FileName).ToUpper();
                    Size = file.ContentLength;
                    ValidateFileType();
                    //ValidateSize();
                    FullPathFile = Path.Combine(pathFile, $@"{idSolicitud}-{FileName}");
                    file.SaveAs(FullPathFile);
                }
            }
        }

        public bool ExixstFile()
        {
            return files != null;
        }

        private void ValidateFileType()
        {
            bool xRes = true;
            switch (Extension)
            {
                case ".EXE":
                    xRes = false;
                    break;
                case ".DLL":
                    xRes = false;
                    break;
                case ".COM":
                    xRes = false;
                    break;
                case ".VBS":
                    xRes = false;
                    break;
                case ".JS":
                    xRes = false;
                    break;
            }
            if (!xRes)
            {
                throw new Exception("El tipo de archivo no está permitido");
            }
        }

        private void ValidateSize()
        {
            // 1 mg 1048576
            if (Size > 3048576)
            {
                throw new Exception("El archivo adjunto es muy grande.\r\nEl tamañó maximo permitido es 3.0 Mb.");
            }
        }
        public void Delete()
        {
            File.Delete(FullPathFile);
        }
    }
}

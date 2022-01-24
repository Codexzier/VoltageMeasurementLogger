using System.IO;

namespace VoltageMeasurementLogger.Views.LogData
{
    public class FileItem
    {
        private readonly FileInfo _fileInfo;

        public FileItem()
        {
            this.Filename = "No Files";
            this._fileInfo = null;
        }

        public FileItem(FileInfo fileInfo)
        {
            this._fileInfo = fileInfo;

            this.Filename = this._fileInfo.Name;
        }

        public string Filename { get; set; }

        public bool IsFile => this._fileInfo != null;
    }
}
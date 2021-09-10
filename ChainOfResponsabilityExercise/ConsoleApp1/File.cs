namespace ConsoleApp1
{
    public class File
    {
        public string fileName { set; get; }
        public string fileType { set; get; }
        public string filePath { set; get; }

        public File(string fileName, string fileType, string filePath)
        {
            this.fileName = fileName;
            this.fileType = fileType;
            this.filePath = filePath;
        }

        public string GetFileName()
        {
            return this.fileName;
        }

        public string GetFileType()
        {
            return this.fileType;
        }
        public string GetFilePath()
        {
            return this.filePath;
        }
    }
}

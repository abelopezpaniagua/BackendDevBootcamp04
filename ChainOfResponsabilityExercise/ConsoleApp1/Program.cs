using System;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            File file = null;
            Handler textHandler = new TextFileHandler("Text Handler");
            Handler docHandler = new DocFileHandler("Doc Handler");
            Handler excelHandler = new ExcelFileHandler("Excel Handler");
            Handler audioHandler = new AudioFileHandler("Audio Handler");
            Handler videoHandler = new VideoFileHandler("Video Handler");
            Handler imageHandler = new ImageFileHandler("Image Handler");

            textHandler.SetHandler(docHandler);
            docHandler.SetHandler(excelHandler);
            excelHandler.SetHandler(audioHandler);
            audioHandler.SetHandler(videoHandler);
            videoHandler.SetHandler(imageHandler);

            file = new File("Abc.jpg", "image", "C:");
            textHandler.Process(file);

            file = new File("Abc.mp3", "audio", "C:");
            textHandler.Process(file);

            file = new File("Abc.jpg", "video", "C:");
            textHandler.Process(file);

            file = new File("Abc.doc", "doc", "C:");
            textHandler.Process(file);

            file = new File("Abc.bat", "bat", "C:");
            textHandler.Process(file);

            file = new File("Abc.xls", "excel", "C:");
            textHandler.Process(file);
        }
    }
}

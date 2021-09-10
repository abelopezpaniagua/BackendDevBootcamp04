using System;

namespace ConsoleApp1
{
    public class VideoFileHandler : Handler
    {
        public Handler handler { set; get; }
        public string handlerName { set; get; }

        public VideoFileHandler(string handlerName)
        {
            this.handlerName = handlerName;
        }

        public void SetHandler(Handler handler)
        {
            this.handler = handler;
        }

        public void Process(File file)
        {
            if (file.fileType.Equals("video"))
            {
                Console.WriteLine("VideoFileHandler = " + this.handlerName);
            }
            else
            {
                if (this.handler != null)
                {
                    this.handler.Process(file);
                }
                else
                {
                    Console.WriteLine("Error is not support a VideoFileHandler = " + this.handlerName);
                }
            }
        }

        public string GetHandlerName()
        {
            return this.handlerName;
        }
    }
}

using System;

namespace ConsoleApp1
{
    public class ImageFileHandler : Handler
    {
        public Handler handler { set; get; }
        public string handlerName { set; get; }

        public ImageFileHandler(string handlerName)
        {
            this.handlerName = handlerName;
        }

        public void SetHandler(Handler handler)
        {
            this.handler = handler;
        }

        public void Process(File file)
        {
            if (file.fileType.Equals("image"))
            {
                Console.WriteLine("ImageFileHandler = " + this.handlerName);
            }
            else
            {
                if (this.handler != null)
                {
                    this.handler.Process(file);
                }
                else
                {
                    Console.WriteLine("Error is not support a ImageFileHandler = " + this.handlerName);
                }
            }
        }

        public string GetHandlerName()
        {
            return this.handlerName;
        }
    }
}

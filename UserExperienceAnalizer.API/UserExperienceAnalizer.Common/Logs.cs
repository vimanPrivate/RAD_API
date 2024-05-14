using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UserExperienceAnalizer.API.UserExperienceAnalizer.Common
{
    public class Logs
    {
        static string filePath = @"D:\Viman\Projects\LuggageLogistic\RRRRRRRR" + "/";

        public static void InfoLog(string message, bool isProcessStartingLog = false)
        {
            try
            {
                if (Directory.Exists(filePath))
                {
                    string logTypeName = "Info";

                    if (!Directory.Exists(filePath + "/" + logTypeName))
                        Directory.CreateDirectory(filePath + "/" + logTypeName);

                    string logFile = filePath + logTypeName + "/Info - " + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                    if (!System.IO.File.Exists(logFile))
                        System.IO.File.Create(logFile).Dispose();

                    using (StreamWriter sw = System.IO.File.AppendText(logFile))
                    {
                        DateTime now = DateTime.Now;
                        string msg = "";

                        if (isProcessStartingLog)
                            msg = "\n\n\n\n" + now + "\n" + message;
                        else
                            msg = "\n ************************ \n " + now + "\n " + message;

                        sw.WriteLine(msg);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public static void ErrorLog(ErrorMessage error)
        //{
        //    try
        //    {
        //        if (Directory.Exists(filePath))
        //        {
        //            string logTypeName = "Error";

        //            if (!Directory.Exists(filePath + "/" + logTypeName))
        //                Directory.CreateDirectory(filePath + "/" + logTypeName);

        //            string logFile = filePath + logTypeName + "/Error - " + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

        //            if (!System.IO.File.Exists(logFile))
        //                System.IO.File.Create(logFile).Dispose();

        //            using (StreamWriter sw = System.IO.File.AppendText(logFile))
        //            {
        //                sw.WriteLine(error.GetErrorMessage());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;


namespace MyToolNetStandard
{
    public static class IOWriter
    {
        /// <summary>
        /// 寫入error_{date}.log到程式所在資料夾。用家可以MethodBase.GetCurrentMethod().DeclaringType獲取當前class名。
        /// </summary>
        /// <param name="header"></param>
        /// <param name="content"></param>
        /// <exception cref="IOException"></exception>
        public static void ErrorLog(string header, string content)
        {
            // sample code
            //IOWriter.ErrorLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + "." + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex.ToString());
            string folder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "log");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, string.Format(@"error_{0}.log", DateTime.Now.ToString("yyyyMMdd")));
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(string.Format(@"[{0}],[{1}],{2}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), header, content));
            }
        }
        /// <summary>
        /// 寫入log_{date}.log到程式所在資料夾。
        /// </summary>
        /// <param name="header"></param>
        /// <param name="content"></param>
        /// <exception cref="IOException"></exception>
        public static void Log(string header, string content)
        {
            string folder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "log");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, string.Format(@"log_{0}.log", DateTime.Now.ToString("yyyyMMdd")));
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(string.Format(@"[{0}],[{1}],{2}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), header, content));
            }
        }
        /// <summary>
        /// 寫入log格式字串到指定檔案。如輸出目標路徑未存在，會按路徑新增資料夾。
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="header"></param>
        /// <param name="content"></param>
        /// <exception cref="IOException"></exception>
        public static void Log(string fullPath, string header, string content)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.WriteLine(string.Format(@"[{0}],[{1}],{2}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), header, content));
            }
        }
        /// <summary>
        /// FullPath是連檔案名輸出路徑，將content寫入檔案，檔案已存在時選擇append或overwrite。如輸出目標路徑未存在，會按路徑新增資料夾。
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <param name="append"></param>
        /// <param name="addDateSuffix"></param>
        /// <exception cref="IOException"></exception>
        public static void Write(string fullPath, string content, bool append)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            using (StreamWriter sw = new StreamWriter(fullPath, append))
            {
                sw.Write(content);
            }
        }
        /// <summary>
        /// Write byte至指定檔案。如輸出目標路徑未存在，會按路徑新增資料夾。
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <param name="append"></param>
        /// <param name="addDateSuffix"></param>
        /// <param name="encoding"></param>
        /// <exception cref="IOException"></exception>
        public static void Write(string fullPath, string content, bool append, Encoding encoding)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            byte[] isoBytes = null;
            if (encoding == Encoding.Unicode)
            {
                byte[] utf8Bytes = Encoding.UTF8.GetBytes(content);
                isoBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes);
            }
            using (FileStream fs = new FileStream(fullPath, append ? FileMode.Append : FileMode.Create))
            {
                fs.Write(isoBytes, 0, isoBytes.Length);
            }
        }
        /// <summary>
        /// FullPath是連檔案名輸出路徑，將content寫入檔案，檔案已存在時選擇append或overwrite。如輸出目標路徑未存在，會按路徑新增資料夾。
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <param name="append"></param>
        /// <param name="addDateSuffix"></param>
        /// <exception cref="IOException"></exception>
        public static void WriteLine(string fullPath, string content, bool append)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            using (StreamWriter sw = new StreamWriter(fullPath, append))
            {
                sw.WriteLine(content);
            }
        }

    }
}

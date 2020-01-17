using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GetMd5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入 mods 文件夹");
            string directory = Console.ReadLine();
            CheckMd5 checkMd5 = new CheckMd5(directory);
            string md5 = checkMd5.GetDirectoryMd5(".jar");
            string[] filesPath = Directory.GetFiles(directory);
            File.WriteAllText("md5.txt", md5);
            Console.WriteLine("已在目录创建 md5.txt ,按任意键退出");
            Console.ReadKey();
        }

        public class CheckMd5
        {
            private List<string> Files = new List<string>();
            private string DirPath { get; set; }

            public CheckMd5(string dirPath)
            {
                DirPath = dirPath;
            }

            /// <summary>
            /// 获取文件夹下所有文件的 md5
            /// </summary>
            /// <returns></returns>
            public string GetDirectoryMd5()
            {
                listDirectory(DirPath);
                string[] filesPath = Files.ToArray();
                List<string> filesMd5 = new List<string>();
                for (int i = 0; i < filesPath.Length; i++)
                {
                    //这里引用了作者在 MD5Checker 类里的方法
                    //获取 md5
                    FileStream file = new FileStream(filesPath[i], FileMode.Open);
                    MD5 md5 = new MD5CryptoServiceProvider();//创建SHA1对象
                    byte[] md5Bytes = md5.ComputeHash(file);//Hash运算
                    md5.Dispose();//释放当前实例使用的所有资源
                    file.Dispose();
                    string result = BitConverter.ToString(md5Bytes);//将运算结果转为string类型
                    result = result.Replace("-", "");
                    filesMd5.Add(result);
                }
                return String.Join(",", filesMd5.ToArray());
            }

            /// <summary>
            /// 获取文件夹下所有文件的 md5
            /// </summary>
            /// <param name="extension">扩展名</param>
            /// <returns></returns>
            public string GetDirectoryMd5(string extension)
            {
                listDirectory(DirPath);
                string[] filesPath = Files.ToArray();
                List<string> filesMd5 = new List<string>();
                for (int i = 0; i < filesPath.Length; i++)
                {
                    if(Path.GetExtension(filesPath[i]) == extension)
                    {
                        //这里引用了作者在 MD5Checker 类里的方法
                        //获取 md5
                        FileStream file = new FileStream(filesPath[i], FileMode.Open);
                        MD5 md5 = new MD5CryptoServiceProvider();//创建SHA1对象
                        byte[] md5Bytes = md5.ComputeHash(file);//Hash运算
                        md5.Dispose();//释放当前实例使用的所有资源
                        file.Dispose();
                        string result = BitConverter.ToString(md5Bytes);//将运算结果转为string类型
                        result = result.Replace("-", "");
                        filesMd5.Add(result);
                    }
                }
                return String.Join(",", filesMd5.ToArray());
            }
            /// <summary>
            /// 获取文件夹下所有文件
            /// </summary>
            /// <param name="path"></param>
            public void listDirectory(string path)
            {
                DirectoryInfo theFolder = new DirectoryInfo(@path);
                //遍历文件
                foreach (FileInfo file in theFolder.GetFiles())
                {
                    Files.Add(file.FullName);
                }
                //遍历文件夹
                foreach (DirectoryInfo folder in theFolder.GetDirectories())
                {
                    listDirectory(folder.FullName);
                }
            }
        }
    }
}

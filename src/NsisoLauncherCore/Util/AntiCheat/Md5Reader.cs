using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NsisoLauncherCore.Util.AntiCheat
{
    public class Md5Reader
    {
        private List<string> Files = new List<string>();
        private string DirPath { get; set; }

        public Md5Reader(string dirPath)
        {
            DirPath = dirPath;
        }

        /// <summary>
        /// 获取文件夹下所有文件的 md5
        /// </summary>
        /// <param name="dirPath"></param>
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
                if (Path.GetExtension(filesPath[i]) == extension)
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

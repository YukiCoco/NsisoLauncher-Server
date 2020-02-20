using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "琉璃Craft v1.1.3更新程序";
            Console.WriteLine("更新程序开始运行，请别关闭我！");
            if (!Directory.Exists(".minecraft"))
            {
                Console.WriteLine("请将我复制到与启动器同级的目录下再运行！");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("开始删除旧文件");
            //File.Delete(".minecraft/mods/gokiStats-1.2.6.jar");
            Console.WriteLine("开始下载更新文件，请勿关闭程序...");
            using (WebClient webClient = new WebClient())
            {
                if (File.Exists(".minecraft/mods/gokiStats-1.2.7.2.jar"))
                {
                    File.Delete(".minecraft/mods/gokiStats-1.2.7.2.jar");
                    webClient.DownloadFile("https://yukino.nos-eastchina1.126.net/nsiso/gokiStats-1.2.6.jar", ".minecraft/mods/gokiStats-1.2.6.jar");
                }
                else
                {
                    //webClient.DownloadFile("https://yukino.nos-eastchina1.126.net/nsiso/gokiStats-1.2.7.2.jar", ".minecraft/mods/gokiStats-1.2.7.2.jar");
                    webClient.DownloadFile("https://yukino.nos-eastchina1.126.net/nsiso/CTM-MC1.12.2-1.0.1.30.jar", ".minecraft/mods/CTM-MC1.12.2-1.0.1.30.jar");
                    webClient.DownloadFile("https://yukino.nos-eastchina1.126.net/nsiso/Chisel-MC1.12.2-1.0.1.44.jar", ".minecraft/mods/Chisel-MC1.12.2-1.0.1.44.jar");
                    webClient.DownloadFile("https://yukino.nos-eastchina1.126.net/nsiso/mod_chatBubbles-1.0.1_for_1.12.2.litemod", ".minecraft/mods/mod_chatBubbles-1.0.1_for_1.12.2.litemod");
                }
            }

            Console.WriteLine("更新安装完毕");
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}

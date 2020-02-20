using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisoLauncher.Utils
{
    public class Helper
    {
        /// <summary>
        /// 比较版本号大小
        /// </summary>
        /// <param name="localVersion">本地版本号</param>
        /// <param name="version">要比较的版本号</param>
        /// <returns>大于本地为true，小于为false</returns>
        public static bool CompareVersion(string localVersion,string version)
        {
            string[] localVersionArray = localVersion.Split('.');
            string[] versionArray = version.Split('.');
            for (int i = 0; i < localVersion.Split('.').Length; i++)
            {
                int versionItem = Convert.ToInt32(versionArray[i]);
                int localVersionItem = Convert.ToInt32(localVersionArray[i]);
                if (localVersionItem < versionItem)
                    return true;
                else if (localVersionItem > versionItem)
                    return false;
            }
            return false;
        }
    }
}

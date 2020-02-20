using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NsisoLauncherCore.Net.Server;

namespace NsisoLauncher.Views.Controls
{
    /// <summary>
    /// TileControl.xaml 的交互逻辑
    /// </summary>
    public partial class TileControl : UserControl
    {
        public TileControl()
        {
            InitializeComponent();
            if (!App.Config.MainConfig.Server.ServerMode)
                this.Visibility = System.Windows.Visibility.Collapsed;
            if (!string.IsNullOrEmpty(App.Config.MainConfig.Server.Address))
            {
                Task.Run(() =>
                {
                    ServerInfo serverInfo = new ServerInfo(App.Config.MainConfig.Server.Address, App.Config.MainConfig.Server.Port);
                    serverInfo.StartGetServerInfo();
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        this.CurrentOnline.Count = Convert.ToString(serverInfo.CurrentPlayerCount);
                    });
                });
            }
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(App.Config.MainConfig.Server.OfficialUrl);
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(App.Config.MainConfig.Server.SkinUrl);
        }
    }
}

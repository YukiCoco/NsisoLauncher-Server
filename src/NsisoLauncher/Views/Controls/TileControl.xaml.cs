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

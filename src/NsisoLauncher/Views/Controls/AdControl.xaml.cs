using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;

namespace NsisoLauncher.Views.Controls
{
    /// <summary>
    /// AdControl.xaml 的交互逻辑
    /// </summary>
    public partial class AdControl : UserControl
    {
        public AdControl()
        {
            InitializeComponent();
            if(!String.IsNullOrEmpty(skinUrl))
                AdsInit();
            if (!App.Config.MainConfig.Server.ServerMode)
                this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private class Ad
        {
            private int id;
            private string title;
            private string content;
            private string img;

            public int Id { get => id; set => id = value; }
            public string Title { get => title; set => title = value; }
            public string Content { get => content; set => content = value; }
            public string Img { get => img; set => img = value; }
        }

        List<Grid> grids = new List<Grid>();
        IList<Ad> ads = new List<Ad>();
        string skinUrl = App.Config.MainConfig.Server.SkinUrl;
        private async void AdsInit()
        {
            flipView.IsEnabled = false;
            using(WebClient webClient = new WebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(skinUrl + "/nsiso/ad");
                //反序列化 Json
                JArray jArray = JArray.Parse(response);
                IList<JObject> results = jArray.OfType<JObject>().ToList();
                foreach(JObject result in results)
                {
                    Ad ad = result.ToObject<Ad>();
                    ads.Add(ad);
                }
                //图片操作
                string localPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                Directory.CreateDirectory("adimgs");
                foreach (Ad ad in ads)
                {
                    string imgPath = localPath + "/adimgs/" + Path.GetFileName(ad.Img);
                    if (!File.Exists("adimgs/" + Path.GetFileName(ad.Img)))
                        await webClient.DownloadFileTaskAsync(ad.Img, imgPath);
                    Grid grid = new Grid();
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(imgPath));
                    image.Stretch = System.Windows.Media.Stretch.UniformToFill;
                    grid.Children.Add(image);
                    grids.Add(grid);
                }
                this.flipView.ItemsSource = grids;
                flipView.IsEnabled = true;
            }
        }

        private void FlipView_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChildWindow childWindow = new ChildWindow();
            childWindow.Viewer.Markdown = ads[flipView.SelectedIndex].Content;
            Views.Windows.MainWindow.MainGrid.Children.Add(childWindow);
        }

        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            flipView.BannerText = ads[flipView.SelectedIndex].Title;
        }
    }
}

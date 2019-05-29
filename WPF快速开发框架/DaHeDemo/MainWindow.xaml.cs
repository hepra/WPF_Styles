using DaHeDemo.Model;
using DaHeDemo.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DaHeDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {
        ViewModel.MainViewModel _viewModel = new ViewModel.MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            //Canvas.SetLeft(playListCanvas, playListCanvas.Width - 20);
          //  SetCanvesAutoEnter_Right_SreenWhenMouseLeave(playListCanvas, 40);
          //  SetCanvesAutoOut_Right_SreenWhenMouseEnter(playListCanvas);
         //   var zipRepertory = new ZipRepertory();
           // var filename = @"G:\Work\bak\9月-2无logo.zip";
           var files = GetAllFiles(@"G:\Res");
            var Videos = (from a in files
                          where a.Contains(".dat")||a.Contains(".mov")||a.Contains("Video")
                          select a).ToList<string>();
            for (int i = 0; i < Videos.Count; i++)
            {
                _viewModel.PlayLists.Add(new PlayListNode(Videos[i]));
            }

          //  CurrentStream = zipRepertory.GetVideoStream(filename, ZipHashCode.HashKey);
            VlcHelper.Initialize(VlcPlayer);
         //   var temp = CurrentStream.Stream.Entries.FirstOrDefault(CurrentStream.check);
          //  VlcPlayer.SourceProvider.MediaPlayer.Play(temp.OpenEntryStream());
            VlcPlayer.SourceProvider.MediaPlayer.Playing += MediaPlayer_Playing; ;
            VlcPlayer.SourceProvider.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;
            this.DataContext = _viewModel;

        }

        private void MediaPlayer_PositionChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerPositionChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                _viewModel.CurrentPos = TransTimeSecondIntToString((long)(VlcPlayer.SourceProvider.MediaPlayer.Position * _viewModel.PlayTime));
                _viewModel.CurrentTime = VlcPlayer.SourceProvider.MediaPlayer.Position * _viewModel.PlayTime;
                _viewModel.Progress = VlcPlayer.SourceProvider.MediaPlayer.Position * 100;
            }));
        }

        private void PlayListCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            SetCanvesAutoEnter_Right_SreenWhenMouseLeave(sender as Canvas, 40);
        }

        private void PlayListCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            SetCanvesAutoOut_Right_SreenWhenMouseEnter(sender as Canvas);
        }

        List<string> GetAllFiles(string rootDirectoryPath)
        {
            List<string> temp = new List<string>();
            string[] newPath = Directory.GetDirectories(rootDirectoryPath);
            if (newPath.Length > 0)
            {
                for (int i = 0; i < newPath.Length; i++)
                {
                    temp.AddRange(GetAllFiles(newPath[i]));
                }
            }
            temp.AddRange(Directory.GetFiles(rootDirectoryPath));
            return temp;
        }
        bool isSame;
        private void MediaPlayer_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
        {
            if(!isSame)
            {
                _viewModel.CurrentPos = VlcPlayer.SourceProvider.MediaPlayer.GetMedia() + "";
                _viewModel.PlayTime = (long)VlcPlayer.SourceProvider.MediaPlayer.GetMedia().Duration.TotalSeconds;
                _viewModel.MaxPos = TransTimeSecondIntToString((long)VlcPlayer.SourceProvider.MediaPlayer.GetMedia().Duration.TotalSeconds);
                _viewModel.MinPos = "00:00:00";
                ////       _viewModel.VideoTitle = VlcPlayer.SourceProvider.MediaPlayer.GetMedia().Title;
                //Thread thGetPos = new Thread(GetPos);
                //thGetPos.IsBackground = true;
                //thGetPos.Start();
            }
           
        }
        string TransTimeSecondIntToString(long second)
        {
            string str = "";
            long hour = second / 3600;
            long min = second % 3600 / 60;
            long sec = second % 60;
            if (hour < 10)
            {
                str += "0" + hour.ToString();
            }
            else
            {
                str += hour.ToString();
            }
            str += ":";
            if (min < 10)
            {
                str += "0" + min.ToString();
            }
            else
            {
                str += min.ToString();
            }
            str += ":";
            if (sec < 10)
            {
                str += "0" + sec.ToString();
            }
            else
            {
                str += sec.ToString();
            }
            return str;
        }
        void GetPos()
        {
            while (true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    _viewModel.CurrentPos = TransTimeSecondIntToString((long)(VlcPlayer.SourceProvider.MediaPlayer.Position * _viewModel.PlayTime));
                    _viewModel.CurrentTime = VlcPlayer.SourceProvider.MediaPlayer.Position * _viewModel.PlayTime;
                    _viewModel.Progress = VlcPlayer.SourceProvider.MediaPlayer.Position * 100;
                }));

                Thread.Sleep(100);

            }
        }
        private void MediaPlayer_Opening(object sender, Vlc.DotNet.Core.VlcMediaPlayerOpeningEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // VlcPlayer.SourceProvider.MediaPlayer.Position = _viewModel.CurrentTime / 100;

        }

        private void Slider_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        public byte[] StreamToBytes(Stream stream, long size, int offset = 0)
        {
            int count = (int)(size - offset);
            byte[] bytes = new byte[count];

            stream.Read(bytes, offset, count);
            // 设置当前流的位置为流的开始
            // stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        // 将 byte[] 转成 Stream
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        private void Slider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            int nMin = 0;
            int nMax = 0;

            // VlcPlayer.SourceProvider.MediaPlayer.Position = _viewModel.CurrentTime;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //VlcPlayer.SourceProvider.MediaPlayer.se
            VlcPlayer.SourceProvider.MediaPlayer.Rate += 1.0f;
            if (VlcPlayer.SourceProvider.MediaPlayer.Rate >= 4)
            {
                VlcPlayer.SourceProvider.MediaPlayer.Rate = 1;
            }
            // VlcPlayer.SourceProvider.MediaPlayer.Position += 0.1f;
            //VlcPlayer.SourceProvider.MediaPlayer.Play();
        }
        string FileName = "";
        private  void PlaySilder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            double x = e.GetPosition(playSilder).X;
            double postion = x / playSilder.ActualWidth;
            isSame = true;
            //playSilder.Value = x / playSilder.ActualWidth * 100;
            // var media =  VlcPlayer.SourceProvider.MediaPlayer.GetMedia();
            //media.URL;
            _viewModel.Progress = x / playSilder.ActualWidth * 100;
            
            int pos = Math.Abs((int)((postion - VlcPlayer.SourceProvider.MediaPlayer.Position) * CurrentFiloInfo.Length));
            CurrentPlayStream.Position = pos;

            //StreamReader sr = new StreamReader(CurrentPlayStream);
            //CurrentPlayStream.Seek(pos, SeekOrigin.Begin);
            //using (Stream temp = new MemoryStream())
            //{
            //    CurrentPlayStream.CopyTo(temp);
            //    VlcPlayer.SourceProvider.MediaPlayer.Play(temp);
            //}

            VlcPlayer.SourceProvider.MediaPlayer.Position = (float)postion;
                
            //var temp = CurrentStream.Stream.Entries.FirstOrDefault(CurrentStream.check);
            // int offset = (int)(temp.Size * _viewModel.Progress / 100);
            //VlcPlayer.SourceProvider.MediaPlayer.Play(BytesToStream(StreamToBytes(temp.OpenEntryStream(), temp.Size, offset)));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VlcPlayer.SourceProvider.MediaPlayer.Play();
        }
        #region 获取行号
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }
        #endregion

        #region Canvas
        #region 设置鼠标移动Canvas
        void SetCanvasFollowMouse(Canvas temp)
        {
            temp.MouseMove += (MouseEventHandler)delegate (object send, MouseEventArgs mouse) {
                if (mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Canvas.SetTop(temp, mouse.GetPosition(playListCanvas).Y - temp.Height / 2);
                    Canvas.SetLeft(temp, mouse.GetPosition(playListCanvas).X - temp.Width / 2);
                }
            };

        }
        #endregion

        #region 设置Canves自动进入左边屏幕
        void SetCanvesAutoEnter_Left_SreenWhenMouseLeave(Canvas targetCanves, int outWidth)
        {
            targetCanves.MouseLeave += (MouseEventHandler)delegate (object send, MouseEventArgs mouse) {
                Canvas temp = send as Canvas;
                double currentLeft = Canvas.GetLeft(temp);
                if (currentLeft < outWidth)
                {
                    while (currentLeft >= -temp.Width + outWidth + 10)
                    {
                        Canvas.SetLeft(temp, currentLeft - 10);
                        currentLeft -= 10;
                    }
                }
            };
        }
        #endregion

        #region 设置Canves自动从左边屏幕显示出来
        void SetCanvesAutoOut_Left_SreenWhenMouseEnter(Canvas targetCanves)
        {
            targetCanves.MouseEnter += (MouseEventHandler)delegate (object send, MouseEventArgs mouse) {
                Canvas temp = send as Canvas;
                double currentLeft = Canvas.GetLeft(temp);
                if (currentLeft < 0)
                {
                    while (currentLeft < 0)
                    {
                        Canvas.SetLeft(temp, currentLeft + 10);
                        currentLeft += 10;
                    }
                }
            };
        }
        #endregion

        #region 设置Canves自动进入右边屏幕
        void SetCanvesAutoEnter_Right_SreenWhenMouseLeave(Canvas targetCanves, int outWidth)
        {
            targetCanves.MouseLeave += (MouseEventHandler)delegate (object send, MouseEventArgs mouse) {
                Canvas temp = send as Canvas;
                double ssss = Canvas.GetLeft(temp);
                double currentRight = playListCanvas.Width - Canvas.GetLeft(temp) - temp.Width;
                while (currentRight <= playListCanvas.Width - outWidth)
                {
                    Canvas.SetLeft(temp, currentRight + 10);
                    currentRight += 10;
                }
            };
        }
        #endregion

        #region 设置Canves自动从右边屏幕显示出来
        void SetCanvesAutoOut_Right_SreenWhenMouseEnter(Canvas targetCanves)
        {
            targetCanves.MouseEnter += (MouseEventHandler)delegate (object send, MouseEventArgs mouse) {
                Canvas temp = send as Canvas;
                double currentRight = playListCanvas.Width - Canvas.GetLeft(temp) - temp.Width;
                if (currentRight < 0)
                {
                    while (currentRight < 0)
                    {
                        Canvas.SetLeft(temp, currentRight + 10);
                        currentRight += 10;
                    }
                }
            };
        }
        #endregion
        #endregion


        #region 保存图片
        public void save_image(string path)
        {
            //  Steema.TeeChart.WPF.Export.ImageExport image_export = new Steema.TeeChart.WPF.Export.ImageExport(Tchart1.Chart);
            //  image_export.JPEG.Save(path);
        }
        #endregion

        Stream CurrentPlayStream;
        FileInfo CurrentFiloInfo;
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var info = e.AddedItems[0] as PlayListNode;
            isSame = false;
            var filename = info.VideoPath;
            if (FileName == filename)
            {
                isSame = true;
            }
            FileName = filename;
            //CurrentStream = zipRepertory.GetVideoStream(filename, ZipHashCode.HashKey);
            //var temp = CurrentStream.Stream.Entries.FirstOrDefault(CurrentStream.check);
            CurrentPlayStream = new FileStream(filename, FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
            CurrentFiloInfo = new FileInfo(filename);
            VlcPlayer.SourceProvider.MediaPlayer.Play(CurrentPlayStream);
            VlcPlayer.SourceProvider.MediaPlayer.Playing += MediaPlayer_Playing; ;
            _viewModel.VideoTitle = filename;
        }

        private void DMSkinWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //playListCanvas.Width = this.ActualWidth - 250;
            //playListCanvas.Height = this.ActualHeight - 180;
        }

        private void DMSkinWindow_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.Key.ToString());
        }
    }
}

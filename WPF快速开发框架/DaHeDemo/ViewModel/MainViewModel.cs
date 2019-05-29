using DMSkin.Core;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace DaHeDemo.ViewModel
{
    public class MainViewModel : DMSkin.Core.ViewModelBase
    {
        public  MainViewModel()
        {
            _PlayLists = new ObservableCollection<Model.PlayListNode>();
        }
        public long PlayTime { get; set; }
        private double _Progress;
        public double Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        private string _CurrentPos;
        public string CurrentPos
        {
            get { return _CurrentPos; }
            set
            {
                _CurrentPos = value;
                OnPropertyChanged(nameof(CurrentPos));
            }
        }
        private float _CurrentTime;
        public float CurrentTime
        {
            get { return _CurrentTime; }
            set
            {
                _CurrentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        private string _MinPos;
        public string MinPos
        {
            get { return _MinPos; }
            set
            {
                _MinPos = value;
                OnPropertyChanged(nameof(MinPos));
            }
        }

        private string _MaxPos;
        public string MaxPos
        {
            get { return _MaxPos; }
            set
            {
                _MaxPos = value;
                OnPropertyChanged(nameof(MaxPos));
            }
        }
        private string _VideoTitle;
        public string VideoTitle
        {
            get { return _VideoTitle; }
            set
            {
                _VideoTitle = value;
                OnPropertyChanged(nameof(VideoTitle));
            }
        }
        private ObservableCollection<Model.PlayListNode> _PlayLists;
        public ObservableCollection<Model.PlayListNode> PlayLists
        {
            get { return _PlayLists; }
            set
            {
                _PlayLists = value;
                OnPropertyChanged(nameof(PlayLists));
            }
        }

        #region Command
        public ICommand btnDeletePlayListNode => new DelegateCommand(obj =>
        {
            TextBlock deleteInfo = obj as TextBlock;
            for (int i = 0; i < PlayLists.Count; i++)
            {
                if(PlayLists[i].VideoPath== deleteInfo.Text)
                {
                    PlayLists.RemoveAt(i);
                    break;
                }
            }
           // this.PlayLists.Remove()
            //写入缓存中的配置
          //  MessageBox.Show("delete");
        });
        #endregion
    }
}

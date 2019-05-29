using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaHeDemo.Model
{
    public class PlayListNode:DMSkin.Core.ViewModelBase
    {
        public PlayListNode(string value)
        {
            _VideoPath = value;
        }
        private string _VideoPath;
        public string VideoPath
        {
            get { return _VideoPath; }
            set
            {
               _VideoPath = value;
                OnPropertyChanged(nameof(VideoPath));
            }
        }
    }
}

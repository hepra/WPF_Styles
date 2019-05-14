using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaHeDemo.Model
{
    public class SampleModel:MVVM.MvvmBase
    {
        private string _name;
        /// <summary>
        /// 文件上传的名字 preview
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
    }
}

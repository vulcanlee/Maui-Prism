using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismMonkey.Models
{
    /// <summary>
    /// 猴子資料模型類別
    /// </summary>
    public class Monkey : INotifyPropertyChanged
    {
        /// <summary>
        /// 實作 INoifyPropertyChanged 介面內的事件成員
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地點
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 明細說明
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// 圖片網址
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 分布數量
        /// </summary>
        public int Population { get; set; }
        /// <summary>
        /// 緯度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public double Longitude { get; set; }

    }
}

namespace PrismMonkey.ViewModels
{
    using System.ComponentModel;
    using Prism.Navigation;
    public class MonkeyListPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        // 這裡是實作 INotifyPropertyChanged 介面需要用到的事件成員
        // 這是要用於屬性變更的時候，將會觸發這個事件通知
        public event PropertyChangedEventHandler PropertyChanged;

        #region 透過建構式注入的服務
        // 這是透過建構式注入的頁面導航的實作執行個體
        private readonly INavigationService navigationService;
        #endregion

        #region 在此設計要進行資料綁定的屬性

        #endregion

        #region 在此設計要進行命令物件綁定的屬性

        #endregion

        public MonkeyListPageViewModel(INavigationService navigationService)
        {
            #region 將透過建構式注入進來的物件，指派給這個類別內的欄位或者屬性
            this.navigationService = navigationService;
            #endregion

            #region 在此將命令屬性進行初始化，建立命令物件與指派委派方法

            #endregion
        }

        #region 在此設計該 ViewModel 的其他商業邏輯程式碼

        #endregion

        #region 頁面導航將會觸發的方法
        // 因為實作 INavigationAware 介面，需要建立這個方法
        // 該方法將會用於當要離開此頁面的時候，會被觸發執行
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        // 因為實作 INavigationAware 介面，需要建立這個方法
        // 該方法將會用於當要導航到此頁面的時候，會被觸發執行
        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        #endregion

    }
}

namespace PrismMonkey.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using Prism.Navigation;
    using Prism.Services.Dialogs;
    using PrismMonkey.Helpers;
    using PrismMonkey.Models;
    using PrismMonkey.Services;

    public class MonkeyListPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        // 這裡是實作 INotifyPropertyChanged 介面需要用到的事件成員
        // 這是要用於屬性變更的時候，將會觸發這個事件通知
        public event PropertyChangedEventHandler PropertyChanged;

        #region 透過建構式注入的服務
        // 這是透過建構式注入的頁面導航的實作執行個體
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly MonkeyService monkeyService;
        private readonly IConnectivity connectivity;
        #endregion

        #region 在此設計要進行資料綁定的屬性
        /// <summary>
        /// 要瀏覽的所有猴子集合紀錄
        /// </summary>
        public ObservableCollection<Monkey> Monkeys { get; set; } = new();
        /// <summary>
        /// 是否有觸發 下拉更新 手勢條件
        /// </summary>
        public bool IsRefreshing { get; set; }
        /// <summary>
        /// 是否正在忙碌要從網路上下載猴子清單的 JSON 內容
        /// </summary>
        public bool IsBusy { get; set; }
        /// <summary>
        /// 是否沒有正在忙碌要從網路上下載猴子清單的 JSON 內容
        /// </summary>
        public bool IsNotBusy => !IsBusy;
        #endregion

        #region 在此設計要進行命令物件綁定的屬性
        public DelegateCommand<Monkey> GoToDetailsCommand { get; set; }
        public DelegateCommand GetMonkeysCommand { get; set; }
        #endregion

        public MonkeyListPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            MonkeyService monkeyService, IConnectivity connectivity)
        {
            #region 將透過建構式注入進來的物件，指派給這個類別內的欄位或者屬性
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.monkeyService = monkeyService;
            this.connectivity = connectivity;
            #endregion

            #region 在此將命令屬性進行初始化，建立命令物件與指派委派方法

            #region 點選某個猴子之後，要進行頁面切換的命令
            GoToDetailsCommand = new DelegateCommand<Monkey>(async monkey =>
            {
                // 若沒有取得猴子資訊，則不會有任何動作
                if (monkey == null)
                    return;

                NavigationParameters parameters = new();
                parameters.Add(ConstantHelper.NavigationKeyMonkey, monkey);

                #region 舊的頁面導航用法
                //await navigationService.NavigateAsync(ConstantHelper.MonkeyDetailPage, parameters);
                #endregion

                #region 採用 Navigation Builder 的用法
                // 參考文章 : https://github.com/PrismLibrary/Prism/issues/2283
                await navigationService.CreateBuilder()
                .WithParameters(parameters)
                .AddNavigationSegment(ConstantHelper.MonkeyDetailPage)
                .NavigateAsync();
                #endregion
            });
            #endregion

            #region 取得網路上最新猴子清單資訊的命令
            GetMonkeysCommand = new DelegateCommand(async () =>
            {
                await ReloadMonkey();
            });
            #endregion

            #endregion
        }

        #region 在此設計該 ViewModel 的其他商業邏輯程式碼
        // 使用注入的 MonkeyService 物件，來取得遠端所有猴子 JSON 紀錄
        private async Task ReloadMonkey()
        {
            // 若已經觸發這個命令委派方法，則無需繼續往下執行
            if (IsBusy)
                return;

            // 保持良好習慣，對於使用 await 呼叫非同步方法，要捕捉例外異常，避免程式崩潰
            try
            {
                // 透過 .NET MAUI Essentials 的 Connectivity 類別可讓您監視裝置網路狀況的變更、
                // 檢查目前的網路存取，以及目前連線方式。
                // 若現在無法連上 Internet ，則顯示與通知使用者，操作錯誤訊息
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    // 使用 Prism.Maui 提供的對話窗警訊物件，顯示此錯誤訊息
                    await dialogService.DisplayAlertAsync("No connectivity!",
                         $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                // 透過之前設計的猴子讀取遠端服務端點服務類別
                // 取得網路上的所有猴子 JSON 內容
                var monkeys = await monkeyService.GetMonkeysAsync();

                if (Monkeys.Count != 0)
                    Monkeys.Clear();

                foreach (var monkey in monkeys)
                    Monkeys.Add(monkey);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await dialogService.DisplayAlertAsync("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
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

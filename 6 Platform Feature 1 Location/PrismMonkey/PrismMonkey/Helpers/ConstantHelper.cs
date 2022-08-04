namespace PrismMonkey.Helpers
{
    /// <summary>
    /// 管理該專案使用到的字串資源
    /// </summary>
    public static class ConstantHelper
    {
        #region 該應用程式中的頁面名稱字串
        /// <summary>
        /// 猴子集合物件清單的頁面名稱
        /// </summary>
        public static readonly string MonkeyListPage = nameof(MonkeyListPage);
        /// <summary>
        /// 猴子明細資訊的頁面名稱
        /// </summary>
        public static readonly string MonkeyDetailPage = nameof(MonkeyDetailPage);
        #endregion

        #region 頁面導航用到的參數鍵值
        /// <summary>
        /// 傳入猴子參數的鍵值名稱
        /// </summary>
        public static readonly string NavigationKeyMonkey = "Monkey";
        #endregion
    }
}

namespace Domain.Enums
{
    public enum RespCode
    {
        /// <summary>
        /// 失敗
        /// </summary>
        FAIL = -1,
        /// <summary>
        /// 未授權 (未登入)
        /// </summary>
        UN_AUTHORIZED = 0,
        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 1
    }
}

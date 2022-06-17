using System.Collections.Generic;

namespace CardGame.Infomation
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string? ID { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }

        /// <summary>
        /// 卡片
        /// </summary>
        public List<string> Card { get; set; }
    }
}
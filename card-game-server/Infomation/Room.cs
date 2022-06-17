using System.Collections.Generic;

namespace CardGame.Infomation
{
    /// <summary>
    /// 房间
    /// </summary>
    public class Room
    {
        /// <summary>
        /// 房间名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 房主id
        /// </summary>
        public string Masterid { get; set; }

        /// <summary>
        /// 当前出牌人
        /// </summary>
        public int Curr { get; set; }

        /// <summary>
        /// 当前卡片
        /// </summary>
        public List<string> CurrCard { get; set; } = new List<string>();

        /// <summary>
        /// 当前卡片打出人
        /// </summary>
        public string ExistingCardClient { get; set; }

        /// <summary>
        /// 房间成员列表
        /// </summary>
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}

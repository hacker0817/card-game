using System.Collections.Generic;
using System.Linq;

namespace CardGame.Infomation
{
    /// <summary>
    /// 用户操作
    /// </summary>
    public static class CustomerAction
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        private static List<Customer> cusList = new List<Customer>();

        /// <summary>
        /// 不存在则新增，存在则修改昵称
        /// </summary>
        /// <param name="customer"></param>
        public static void Create(Customer customer)
        {
            Customer curr = null;

            if (cusList.Count > 0)
                curr = cusList.Where(x => x.ID == customer.ID).FirstOrDefault();

            if (curr is null)
                cusList.Add(customer);
            else
            {
                curr.NickName = customer.NickName;

                Up4ID(curr);
            }
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetList()
        {
            return cusList;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer GetOne(string id)
        {
            return cusList.Where(x => x.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(string id)
        {
            cusList.RemoveAll(x => x.ID == id);
        }

        /// <summary>
        /// 增加卡片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cards"></param>
        public static void InCard(string id, List<string> cards)
        {
            Customer customer = cusList.Where(x => x.ID == id).FirstOrDefault();

            if (customer.Card is null)
                customer.Card = cards;
            else
                customer.Card.AddRange(cards);

            Up4ID(customer);
        }

        /// <summary>
        /// 扣除卡片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cards"></param>
        /// <param name="group"></param>
        /// <returns>1成功、-1失败、2游戏结束</returns>
        public static int OutCard(string id, List<string> cards, Room group)
        {
            Customer client = cusList.Where(x => x.ID == id).FirstOrDefault();

            if (client is null)
                return -1;

            //卡片不匹配直接失败
            if (client.Card.Where(x => cards.Contains(x)).ToList().Count != cards.Count)
                return -1;

            //不符合出牌规则直接失败
            if (!new Game.WithCard().Rule(group.CurrCard, cards, group.ExistingCardClient is null || group.ExistingCardClient == id))
                return -1;

            foreach (var item in cards)
            {
                client.Card.Remove(item);
            }

            if (client.Card.Count == 0)
                return 2;

            group.CurrCard = cards;

            group.ExistingCardClient = id;

            Up4ID(client);

            RoomAction.Up4Name(group);

            return 1;
        }

        /// <summary>
        /// 更新（根据ID）
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static bool Up4ID(Customer customer)
        {
            if (cusList.Count == 0)
                return false;

            cusList.RemoveAll(x => x.ID == customer.ID);

            cusList.Add(customer);

            return true;
        }
    }
}
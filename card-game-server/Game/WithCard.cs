using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Game
{
    /// <summary>
    /// 卡片相关
    /// </summary>
    public class WithCard
    {
        /// <summary>
        /// 黑桃-S、红桃-H、梅花-C、方块-D
        /// BG大王，SG小王，14-A，15-2
        /// </summary>
        readonly List<string> Cards = new List<string>()
        {
            "S-14","S-15","S-3","S-4","S-5","S-6","S-7","S-8","S-9","S-10","S-11","S-12","S-13",
            "H-14","H-15","H-3","H-4","H-5","H-6","H-7","H-8","H-9","H-10","H-11","H-12","H-13",
            "C-14","C-15","C-3","C-4","C-5","C-6","C-7","C-8","C-9","C-10","C-11","C-12","C-13",
            "D-14","D-15","D-3","D-4","D-5","D-6","D-7","D-8","D-9","D-10","D-11","D-12","D-13",
            "BG-99","SG-88"
        };

        /// <summary>
        /// 发牌
        /// </summary>
        public List<List<string>> DrawCard()
        {
            List<string> a = new List<string>();
            List<string> b = new List<string>();
            List<string> c = new List<string>();

            Random ran = new Random();

            //剩3张底牌
            for (int i = 0; i < 51; i++)
            {
                //随机抽取一张牌
                string item = Cards[ran.Next(Cards.Count)];

                switch (i % 3)
                {
                    case 0:
                        a.Add(item);
                        break;
                    case 1:
                        b.Add(item);
                        break;
                    case 2:
                        c.Add(item);
                        break;
                }

                Cards.Remove(item);
            }

            return new List<List<string>>()
            {
                a,b,c,Cards
            };
        }

        /// <summary>
        /// 规则
        /// </summary>
        /// <param name="existingCard"></param>
        /// <param name="newCard"></param>
        /// <param name="isSelf"></param>
        /// <returns></returns>
        public bool Rule(List<string> existingCard, List<string> newCard, bool isSelf)
        {
            //现有牌号
            List<int> existingCardNo = existingCard.Select(x => Convert.ToInt32(x.Split('-')[1])).ToList().OrderBy(x => x).ToList();

            //新出牌号
            List<int> newCardNo = newCard.Select(x => Convert.ToInt32(x.Split('-')[1])).ToList().OrderBy(x => x).ToList();

            //上一手是王炸，禁止其他人出牌
            if (existingCardNo.All(x => x > 50) && existingCardNo.Count == 2)
            {
                if (isSelf)
                    return true;
                else
                    return false;
            }

            //王炸最大
            if (newCardNo.All(x => x > 50) && newCard.Count == 2)
                return true;

            //单张
            if (newCardNo.Count == 1)
            {
                if (existingCardNo.Count == 0)
                    return true;

                if ((existingCardNo.Count == 1 && newCardNo[0] > existingCardNo[0]) || isSelf)
                    return true;
            }

            //对子/三只
            if (newCardNo.Count == 2 || newCardNo.Count == 3)
            {
                if (existingCardNo.Count == 0 && newCardNo.All(x => x == newCardNo[0]))
                    return true;

                if (newCardNo.All(x => x == newCardNo[0]) && (isSelf || newCardNo.Count == existingCardNo.Count && newCardNo[0] > existingCardNo[0]))
                    return true;
            }

            if (newCard.Count == 4)
            {
                //炸
                if (newCardNo.All(x => x == newCardNo[0]))
                {
                    if (existingCardNo.Count == 0 || isSelf)
                        return true;

                    if (existingCardNo.All(x => x == existingCardNo[0]) && existingCardNo.Count == 4)
                    {
                        if (newCardNo[0] > existingCardNo[0])
                            return true;
                    }

                    return true;
                }

                //三带一
                {
                    List<int> flagA = newCardNo.Distinct().ToList();

                    //超过2种牌直接失败
                    if (flagA.Count > 2)
                        return false;

                    //没有上一手牌，或者上一手是自己出的牌
                    if (existingCardNo.Count == 0 || isSelf)
                        return true;

                    int newCardFlag = 0;

                    if (newCardNo.Where(x => x == flagA[0]).ToList().Count() > 1)
                    {
                        newCardFlag = flagA[0];
                    }
                    else
                        newCardFlag = flagA[1];

                    List<int> flagB = existingCardNo.Distinct().ToList();

                    //上一手牌不是三带一
                    if (flagB.Count > 2)
                        return false;

                    int existingCardFlag = 0;

                    if (existingCardNo.Where(x => x == flagB[0]).ToList().Count() > 1)
                    {
                        existingCardFlag = flagB[0];
                    }
                    else
                        existingCardFlag = flagB[1];

                    if (newCardFlag > existingCardFlag)
                        return true;
                }
            }

            if (newCard.Count >= 5)
            {
                bool flag = true;

                for (int i = 0; i < newCardNo.Count - 1; i++)
                {
                    if (newCardNo[i] + 1 != newCardNo[i + 1])
                    {
                        flag = false;
                        break;
                    }
                }

                //顺子
                if (flag)
                {
                    if (existingCardNo.Count == 0 || (newCardNo[0] > existingCardNo[0] && newCardNo.Count == existingCardNo.Count) || isSelf)
                        return true;
                }
            }

            return false;
        }
    }
}
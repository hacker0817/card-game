using System.Collections.Generic;
using System.Linq;

namespace CardGame.Infomation
{
    /// <summary>
    /// 房间操作
    /// </summary>
    public static class RoomAction
    {
        /// <summary>
        /// 房间列表
        /// </summary>
        private static List<Room> roomList = new List<Room>();

        /// <summary>
        /// 新增房间
        /// 如果房间已存在则不新增
        /// </summary>
        /// <param name="group"></param>
        public static void Create(Room group)
        {
            if (!roomList.Where(x => x.Name == group.Name).Any())
                roomList.Add(group);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<Room> GetList()
        {
            return roomList;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="masterid">房主id</param>
        /// <param name="roomName">房间名称</param>
        /// <returns></returns>
        public static Room GetOne(string masterid = null, string roomName = null)
        {
            if (roomList.Count == 0)
                return null;

            if (masterid != null)
                return roomList.Where(x => x.Masterid == masterid).FirstOrDefault();

            if (roomName != null)
                return roomList.Where(x => x.Name == roomName).FirstOrDefault();

            return null;
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="client"></param>
        /// <param name="roomName"></param>
        public static bool Join(Customer client, string roomName)
        {
            if (roomList.Count == 0)
                return false;

            var room = roomList.Where(x => x.Name == roomName).FirstOrDefault();

            if (room is null)
                return false;

            if (room.Customers.Count == 3)
                return false;

            room.Customers.Add(client);

            Up4Name(room);

            return true;
        }

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="masterid">房主id</param>
        public static bool Delete(string masterid)
        {
            if (roomList.Count == 0)
                return false;

            var room = roomList.Where(x => x.Masterid == masterid).FirstOrDefault();

            if (room == null)
                return false;

            roomList.Remove(room);

            return true;
        }

        /// <summary>
        /// 更新（根据房名）
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public static bool Up4Name(Room room)
        {
            if (roomList.Count == 0)
                return false;

            roomList.RemoveAll(x => x.Name == room.Name);

            roomList.Add(room);

            return true;
        }

        /// <summary>
        /// 更新当前出牌人
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="index">传入则强制修改，不传按规则走</param>
        public static Customer ChangeCurr(string roomName, int index = -1)
        {
            var room = roomList.Where(x => x.Name == roomName).FirstOrDefault();

            if (index != -1)
                room.Curr = index;
            else
                room.Curr = (room.Curr + 1) % 3;

            Up4Name(room);

            return room.Customers[room.Curr];
        }
    }
}
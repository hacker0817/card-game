using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CardGame.Server
{
    /// <summary>
    /// hub类
    /// </summary>
    public class SignalRHub : Hub
    {
        /// <summary>
        /// 客户连接成功时触发
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var cid = Context.ConnectionId;

            Infomation.CustomerAction.Create(new Infomation.Customer() { ID = cid });

            await SendClient(cid, "Self", cid);

            await SendAll("Send", new
            {
                cid,
                msg = "加入"
            });
        }

        /// <summary>
        /// 客户链接断开后
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            var cid = Context.ConnectionId;

            var customerInfo = Infomation.CustomerAction.GetOne(cid);

            Infomation.CustomerAction.Delete(cid);

            var room = Infomation.RoomAction.GetOne(cid);

            if (room != null)
            {
                if (Infomation.RoomAction.Delete(cid))
                    await SendGroup(room.Name, "SendToRoom", "close now");
            }

            await SendAll("Send", new
            {
                cid,
                customerInfo.NickName,
                msg = "离开"
            });
        }

        /// <summary>
        /// 发送给全部人
        /// </summary>
        /// <param name="method"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task<bool> SendAll(string method, object msg)
        {
            //向所有用户发送消息
            await Clients.All.SendAsync(method, msg);
            return true;
        }

        /// <summary>
        /// 发送消息到客户端
        /// </summary>
        /// <param name="id"></param>
        /// <param name="method"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task<bool> SendClient(string id, string method, object msg)
        {
            await Clients.Client(id).SendAsync(method, msg);
            return true;
        }

        /// <summary>
        /// 发送消息到群组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="method"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task<bool> SendGroup(string groupName, string method, object msg)
        {
            await Clients.Group(groupName).SendAsync(method, msg);

            return true;
        }
    }
}
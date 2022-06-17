using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CardGame.Server
{
    public class SRHelper
    {
        private static IHubContext<SignalRHub> _countHub;

        public SRHelper(IHubContext<SignalRHub> countHub)
        {
            _countHub = countHub;
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
            await _countHub.Clients.All.SendAsync(method, msg);
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
            await _countHub.Clients.Client(id).SendAsync(method, msg);
            return true;
        }

        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public async Task<bool> AddGroup(string id, string roomName)
        {
            await _countHub.Groups.AddToGroupAsync(id, roomName);
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
            await _countHub.Clients.Group(groupName).SendAsync(method, msg);

            return true;
        }
    }
}
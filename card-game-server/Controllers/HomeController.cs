using CardGame.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.Controllers
{
    /// <summary>
    /// 基本操作
    /// </summary>
    [ApiController, Route("[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "client")]
    public class HomeController : Controller
    {
        private readonly SRHelper _srHelper;

        public HomeController(IHubContext<SignalRHub> countHub)
        {
            _srHelper = new SRHelper(countHub);
        }

        /// <summary>
        /// 公聊
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Send([FromForm] string id, [FromForm] string msg)
        {
            var customer = Infomation.CustomerAction.GetOne(id);

            await _srHelper.SendAll("Send", new
            {
                customer.ID,
                customer.NickName,
                msg
            });
        }

        /// <summary>
        /// 私聊
        /// </summary>
        /// <param name="send"></param>
        /// <param name="to"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task PrivateSend([FromForm] string send, [FromForm] string to, [FromForm] string msg)
        {
            var customer = Infomation.CustomerAction.GetOne(send);

            await _srHelper.SendClient(to, "PrivateSend", new
            {
                sendID = customer.ID,
                sendName = customer.NickName,
                to,
                msg
            });
        }

        /// <summary>
        /// 绑定信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nickName"></param>
        /// <returns>返回当前在线的所有用户</returns>
        [HttpPost]
        public IActionResult BindInfo([FromForm] string id, [FromForm] string nickName)
        {
            Infomation.CustomerAction.Create(new Infomation.Customer()
            {
                ID = id,
                NickName = nickName
            });

            return Ok(Infomation.CustomerAction.GetList());
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomName"></param>
        /// <returns></returns>
        [HttpPost]
        public void NewRoom([FromForm] string id, [FromForm] string roomName)
        {
            Infomation.RoomAction.Create(new Infomation.Room()
            {
                Masterid = id,
                Name = roomName,
                Customers = new System.Collections.Generic.List<Infomation.Customer>()
            });
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RoomList()
        {
            return Ok(Infomation.RoomAction.GetList());
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JoinRoom([FromForm] string id, [FromForm] string roomName)
        {
            var customer = Infomation.CustomerAction.GetOne(id);

            if (!Infomation.RoomAction.Join(customer, roomName))
                return BadRequest("加入房间失败");

            await _srHelper.AddGroup(id, roomName);

            await _srHelper.SendGroup(roomName, "SendToRoom", new
            {
                customer.ID,
                customer.NickName,
                msg = "加入房间"
            });

            await _srHelper.SendGroup(roomName, "RoomState", Infomation.RoomAction.GetOne(null, roomName));

            return Ok("成功加入房间");
        }

        /// <summary>
        /// 房间内发送消息
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SendToRoom([FromForm] string roomName, [FromForm] string id, [FromForm] string msg)
        {
            var customer = Infomation.CustomerAction.GetOne(id);

            await _srHelper.SendGroup(roomName, "SendToRoom", new
            {
                customer.ID,
                customer.NickName,
                msg
            });
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DrawCard([FromForm] string id)
        {
            var room = Infomation.RoomAction.GetOne(id);

            if (room is null)
                return BadRequest("不是房主");

            //手牌
            var cards = new Game.WithCard().DrawCard();

            //地主下标
            int lord = new Random().Next(1, 3);

            //更新出牌人
            Infomation.RoomAction.ChangeCurr(room.Name, lord);

            for (int i = 0; i < 3; i++)
            {
                await _srHelper.SendClient(room.Customers[i].ID, "PrivateSend", new
                {
                    sendID = "官方",
                    sendName = "官方",
                    to = room.Customers[i].ID,
                    msg = cards[i]
                });

                Infomation.CustomerAction.InCard(room.Customers[i].ID, cards[i]);
            }

            await _srHelper.SendGroup(room.Name, "SendToRoom", new
            {
                room.Customers[lord].ID,
                room.Customers[lord].NickName,
                msg = "成为地主",
                card = cards[3],
                curr = room.Customers[lord].ID
            });

            Infomation.CustomerAction.InCard(room.Customers[lord].ID, cards[3]);

            return Ok("发牌成功");
        }

        /// <summary>
        /// 出牌
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="id"></param>
        /// <param name="cards">多张以英文逗号分割</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OutCard([FromForm] string roomName, [FromForm] string id, [FromForm] string cards)
        {
            var room = Infomation.RoomAction.GetOne(null, roomName);

            if (room is null)
                return BadRequest("房间不存在");

            var customer = Infomation.CustomerAction.GetOne(id);

            if (!room.Customers.Where(x => x.ID == id).Any())
                return BadRequest("您不在此房间");

            int result = Infomation.CustomerAction.OutCard(id, cards.Split(',').ToList(), room);

            if (result == -1)
                return BadRequest("出牌失败");

            //更新出牌人
            string currID = Infomation.RoomAction.ChangeCurr(roomName).ID;

            await _srHelper.SendGroup(roomName, "SendToRoom", new
            {
                id,
                customer.NickName,
                msg = "打出了牌",
                card = cards.Split(',').ToList(),
                conut = room.Customers.Where(x => x.ID == id).First().Card.Count,
                curr = currID,
                over = (result == 2)
            });

            return Ok();
        }

        /// <summary>
        /// 过（放弃出牌）
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Pass([FromForm] string roomName, [FromForm] string id)
        {
            var room = Infomation.RoomAction.GetOne(null, roomName);

            if (room is null)
                return BadRequest("房间不存在");

            var client = Infomation.CustomerAction.GetOne(id);

            if (!room.Customers.Where(x => x.ID == id).Any())
                return BadRequest("您不在此房间");

            string currID = Infomation.RoomAction.ChangeCurr(roomName).ID;

            await _srHelper.SendGroup(roomName, "SendToRoom", new
            {
                id,
                client.NickName,
                msg = "跳过了出牌",
                conut = room.Customers.Where(x => x.ID == id).First().Card.Count,
                curr = currID
            });

            return Ok();
        }
    }
}
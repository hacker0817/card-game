<template>
	<view class="">
		<view class="homepage zongCenter" v-if="liaotian">
			<view style="font-size: 30rpx;margin-top: 20rpx;margin-bottom: 20rpx;">房间列表</view>
			<view @click="goToPages('../index/index',item)" class="zongCenter" v-for="item in homeList" style="width: 80%;height: 80rpx;border: 1px solid #ccc;border-radius: 10rpx;margin-bottom: 20rpx;">
				{{item.name}}
			</view>
			<view v-if="homeList.length == 0" style="margin-top: 20rpx;margin-bottom: 20rpx;" @click="shuaxin()">
				暂无房间
			</view>
			<view class="">
				<view @click="newRoom()" style="width: 200rpx;height: 60rpx;background-color: #4CD964;text-align: center;line-height: 60rpx;border-radius: 10rpx;">
					新建房间
				</view>
			</view>
			<!-- <view class="">
				<view @click="fapai()" style="width: 200rpx;height: 60rpx;background-color: #4CD964;text-align: center;line-height: 60rpx;border-radius: 10rpx;margin-top: 20rpx;">
					发牌
				</view>
			</view> -->
		</view>
		<view class="index" v-if="!liaotian" style="background-image: url(../../static/beijing.jpg);background-repeat: no-repeat;background-size: 100% 100%;">
			<view style="width: 100%;height: 70vh;display: flex;align-items: center;justify-content: space-between;">
				<view v-if="playerleft!=''" class="" style="width: 30%;height: 100%;position: relative;padding: 20rpx;">
					<image v-if="dizhuId == playerleft.id&&playGame" style="position: absolute;top: 10vh;width: 30%;right: 5%;" src="../../static/SE/dizhu.png"
					 mode="widthFix"></image>
					<image v-else-if="playGame" style="position: absolute;top: 10vh;width: 30%;right: 5%;" src="../../static/SE/nonmin.png"
					 mode="widthFix"></image>
					<view style="position: absolute;top: 10vh;width: auto;text-align: center;left: 3vw;color: #FFFFFF;font-size: 40rpx;font-weight: 800;">
						<view class="">
							{{playerleft!=''?playerleft.nickName:'玩家未加入'}}
						</view>
						<image src="../../static/SE/tou1.png" mode="widthFix" style="width: 10vw;"></image>
					</view>
					<view v-if="playerleft!=''" style="width: 100%;height: 10vh;position: absolute;top: 23vh;left: 1vw;display: flex;align-items: center;justify-content: center;"
					 :style="{backgroundColor: playGame?'':'#DD524D'}">
						<view v-if="playGame" style="display: flex;align-items: center;width: 100%;flex-wrap: wrap;">
							<image v-for="index of playerleftNum" src="../../static/SE/cardBack.png" mode="widthFix" style="width: 2vh;margin-right: 10rpx;margin-bottom: 10rpx;"></image>
						</view>
						<view v-else>
							游戏未开始
						</view>
					</view>
				</view>
				<view v-else style="width: 30%;height: 100%;position: relative;padding: 20rpx;border-right: 1px solid #ccc;"></view>
				<view style="display: flex;width: 40%;height: 70vh;align-items: flex-start;flex-direction: column;">
					<view style="width: 100%;display: flex;align-items: center;justify-content: space-between;padding: 20rpx;">
						<view v-if="threeCard[0]" style="width: 4vh;height: 6vh;text-align: center;">
							<!-- {{threeCard[0].name}} -->
							<image :src="`../../static/${threeCard[0].id}.jpg`" style="width: 100%;" mode="widthFix"></image>
						</view>
						<view v-if="threeCard[1]" style="width: 4vh;height: 6vh;text-align: center;">
							<image :src="`../../static/${threeCard[1].id}.jpg`" style="width: 100%;" mode="widthFix"></image>
						</view>
						<view v-if="threeCard[2]" style="width: 4vh;height: 6vh;text-align: center;">
							<image :src="`../../static/${threeCard[2].id}.jpg`" style="width: 100%;" mode="widthFix"></image>
						</view>
					</view>
					<view style="width: 100%;text-align: center;margin-top: 30vh;margin-bottom: 1vh;color: #fff;">
						{{`当前出牌:${publicName}`}}
					</view>
					<view style="width: 100%;display: flex;align-items: center;padding: 30rpx;flex-wrap: wrap;">
						<image v-if="publicList[0]" v-for="(item,index) in publicList" :src="`../../static/${item.id}.jpg`" style="width: 3vh;height: 4vh;margin-right: 10rpx;margin-top: 20rpx;"
						 mode="widthFix"></image>
					</view>
				</view>
				<view v-if="playerright!=''" class="" style="width: 30%;height: 100%;position: relative;padding: 20rpx;">
					<image v-if="dizhuId == playerright.id &&playGame" style="position: absolute;top: 10vh;width: 30%;right: 5%;" src="../../static/SE/dizhu.png"
					 mode="widthFix"></image>
					<image v-else-if="playGame" style="position: absolute;top: 10vh;width: 30%;right: 5%;" src="../../static/SE/nonmin.png"
					 mode="widthFix"></image>
					<view style="position: absolute;top: 10vh;width: auto;text-align: center;left: 0;color: #FFFFFF;font-size: 40rpx;font-weight: 800;">
						<view class="">
							{{playerright!=''?playerright.nickName:'玩家未加入'}}
						</view>
						<image src="../../static/SE/tou5.png" mode="widthFix" style="width: 10vw;"></image>
					</view>
					<view v-if="playerright!=''" style="width: 100%;height: 10vh;position: absolute;top: 23vh;left: 0;display: flex;align-items: center;justify-content: center;"
					 :style="{backgroundColor: playGame?'':'#DD524D'}">
						<view v-if="playGame" style="display: flex;align-items: center;width: 100%;flex-wrap: wrap;">
							<image v-for="index of playerrightNum" src="../../static/SE/cardBack.png" mode="widthFix" style="width: 2vh;margin-right: 10rpx;margin-bottom: 10rpx;"></image>
						</view>
						<view v-else>
							游戏未开始
						</view>
					</view>
				</view>
				<view v-else style="width: 30%;height: 100%;position: relative;padding: 20rpx;border-left: 1px solid #ccc;"></view>
			</view>
			<view style="width: 100%;height: 30vh;padding: 20rpx;border-top: 1px solid #ccc;display: flex;flex-wrap: wrap;background-repeat: no-repeat;background-size:100% 100%;"
			 :style="{backgroundImage: id==dizhuId?'url(../../static/SE/mydizhu.png)':'url(../../static/SE/mynongmin.png)'}">
				<image @click="changecard(index)" v-for="(item,index) in cardList" :src="`../../static/${item.id}.jpg`" style="width: 4vh;height: 6vh;margin-right: 20rpx;text-align: center;"
				 :style="{border:OutCard.findIndex((v) => {return v.id == item.id})>-1?'3px solid #007AFF':''}" mode="widthFix"></image>
			</view>
			<image v-if="myroom&&fapai1" @click="fapai()" src="../../static/SE/kaishiyouxi.png" mode="widthFix" style="width:220rpx;margin-right: 20rpx;position: fixed;bottom: 30vh;left: 35%;"></image>
			<view v-else-if="dapaiBtn" style="position: fixed;bottom: 32vh;left: 30%;display: flex;">
				<image @click="dapai()" src="../../static/SE/chupai.png" mode="widthFix" style="width:140rpx;margin-right: 20rpx;"></image>
				<image v-if="buchu" @click="pass()" src="../../static/SE/buchu.png" mode="widthFix" style="width: 140rpx;"></image>
			</view>
		</view>
		<!-- 炸弹 -->
		<view v-if="playAnimation" style="width: 100vw;height: 70vh;position: fixed;left: 0;top: 0;" class="animation-bomb">

		</view>
		<!-- 游戏结束 -->
		<view v-if="over" style="width: 100vw;height: 70vh;position: fixed;left: 10%;top: 45vh;" class="animate bounceInDown">
			<image v-if="dizhuId==id&&winImg" src="../../static/SE/dizhuWin.png" mode="widthFix"></image>
			<image v-if="dizhuId==id&&!winImg" src="../../static/SE/dizhuLost.png" mode="widthFix"></image>
			<image v-if="dizhuId!=id&&winImg" src="../../static/SE/nongminWin.png" mode="widthFix"></image>
			<image v-if="dizhuId!=id&&!winImg" src="../../static/SE/nongminLost.png" mode="widthFix"></image>
		</view>
		<!-- 顺子 -->
		<image v-if="playAnimationSunzi" src="../../static/SE/suanzi.png" mode="widthFix" class="animated bounceInLeft" style="width: 200rpx;position: fixed;right: 30vw;top: 50vh;"></image>
		<!-- 飞机 -->
		<!-- <image v-if="playAnimationfeiji" src="../../static/SE/feiji.png" mode="widthFix" style="position: fixed;left: 50vw;transform: translateX(-50%);top: -40vh;" class="animation-feiji"></image> -->
	</view>
</template>

<script>
	import {
		post,
		put,
		get
	} from '@/api/http';
	let signalR = require('../../tools/signalr.min.js')
	export default {
		data() {
			return {
				liaotian: true,
				homeList: [],
				id: '',
				roomName: '',
				msgList: [],
				title: '',
				roomid: '',
				myroom: true,
				fapai1: true,
				playerleft: {},
				playerright: {},
				playerleftNum: 17,
				playerrightNum: 17,
				playGame: false,
				cardList: [],
				threeCard: [],
				OutCard: [],
				dizhuId: '',
				dapaiBtn: false,
				publicList: [],
				publicName: '',
				publicID: '',
				playAnimation: false,
				playAnimationfeiji: true,
				playAnimationSunzi: false,
				buchu: false,
				touxiangleft: null,
				touxiangright: null,
				over: false,
				winImg: false
			}
		},
		onLoad() {
			const conn = new signalR.HubConnectionBuilder()
				.withUrl("http://192.168.1.52:9090/mj")
				.build();
			// 接收公聊消息
			conn.on("Send", this.Send);
			// 加入房间消息
			conn.on("JoinRoom", this.JoinRoom);
			// 加入房间消息
			conn.on("RoomState", this.RoomState);
			// 私聊消息
			conn.on("PrivateSend", this.PrivateSend);
			// 房间内消息
			conn.on("NewRoom", this.newRoom);
			// 房间内消息
			conn.on("SendToRoom", this.SendToRoom);
			conn.on("Finished", () => {
				conn.stop();
			});
			conn.on("Self", (obj) => {
				this.id = obj
				console.log(obj);
			});
			conn.start().catch(err => console.log(err));
			this.showNickName()
			// this.liaotian = false
			// this.dapaiBtn = true
			// this.fapai1 = false
		},
		methods: {
			Send(obj) {
				if (obj.msg.indexOf("离开") != -1) {
					obj.type = 3
				}
				this.msgList.push(obj)
				console.log(obj);
			},
			newRoom(obj) {
				console.log(obj);
			},
			RoomState(obj) {
				console.log(obj);
				if (obj.masterid == this.id) {
					this.myroom = true
				} else {
					this.myroom = false
				}
				let index1 = null
				obj.customers.forEach((e, index) => {
					if (e.id == this.id) {
						index1 = index
					}
				})
				if (index1 == 0) {
					this.playerleft = obj.customers[2] ? obj.customers[2] : ''
					this.playerright = obj.customers[1] ? obj.customers[1] : ''
				} else if (index1 == 1) {
					this.playerleft = obj.customers[0] ? obj.customers[0] : ''
					this.playerright = obj.customers[2] ? obj.customers[2] : ''
				} else if (index1 == 2) {
					this.playerleft = obj.customers[1] ? obj.customers[1] : ''
					this.playerright = obj.customers[0] ? obj.customers[0] : ''
				}
			},
			JoinRoom(obj) {
				console.log(obj);
			},
			PrivateSend(obj) {
				console.log(obj);
				this.cardList = obj.msg
				let list = []
				this.$card.cards().forEach(e => {
					if (this.cardList.indexOf(e.id) > -1) {
						list.push(e)
					}
				})
				this.cardList = list
			},
			SendToRoom(obj) {
				console.log(obj);
				if (obj == 'close now') {
					this.liaotian = true
					this.msgList = []
					this.getRoomList()
					return
				}
				if (obj.over) {
					this.over = true
					if (obj.id == this.dizhuId) {
						if (this.dizhuId == this.id) {
							this.winImg = true
						} else {
							this.winImg = false
						}
					} else {
						if (this.dizhuId == this.id) {
							this.winImg = false
						} else {
							this.winImg = true
						}
					}
					this.playGame = false
					this.dapaiBtn = false
					this.fapai1 = true
					setTimeout(e => {
						this.over = false
					}, 2000)
				} else {
					this.over = false
				}
				if (obj.msg == "打出了牌") {
					if (obj.curr == this.id) {
						this.dapaiBtn = true
					} else {
						this.dapaiBtn = false
					}
					if (obj.id == this.id) {
						this.buchu = false
					} else {
						this.buchu = true
					}
					if (this.playerleft.id == this.dizhuId) {
						this.playerleftNum = obj.conut
					}
					if (this.playerright.id == this.dizhuId) {
						this.playerrightNum = obj.conut
					}
					let list = []
					this.$card.cards().forEach(e => {
						if (obj.card.indexOf(e.id) > -1) {
							list.push(e)
						}
					})
					this.publicList = list
					this.publicName = obj.nickName
					this.publicID = obj.id
					let list1 = []
					list.forEach(e => {
						list1.push(e.id)
					})
					if (list1.length == 2 && list1.toString() == 'SG-88,BG-99') {
						this.playAnimation = true
					}
					if (list1.length == 4 && this.$tools.judgeFun(list1)) {
						this.playAnimation = true
					}
					if (list1.length >= 5 && this.$tools.sunziFun(list1)) {
						this.playAnimationSunzi = true
					}
					setTimeout(() => {
						this.playAnimation = false
						this.playAnimationSunzi = false
					}, 1000)
				}
				if (obj.msg == "跳过了出牌") {
					if (obj.curr == this.id) {
						this.dapaiBtn = true
					} else {
						this.dapaiBtn = false
					}
					if (obj.curr == this.publicID) {
						this.buchu = false
					} else {
						this.buchu = true
					}
				}
				if (obj.msg == "成为地主") {
					this.playGame = true
					this.dizhuId = obj.id
					if (obj.curr == this.id) {
						this.dapaiBtn = true
					} else {
						this.dapaiBtn = false
					}
					let list = []
					this.$card.cards().forEach(e => {
						if (obj.card.indexOf(e.id) > -1) {
							list.push(e)
						}
					})
					this.threeCard = list
					if (this.id == obj.id) {
						this.cardList = [...this.cardList, ...list]
						this.cardList.sort(function(a, b) {
							return a.num - b.num;
						})

					}
					if (this.playerleft.id == this.dizhuId) {
						this.playerleftNum = this.playerleftNum + 3
					}
					if (this.playerright.id == this.dizhuId) {
						this.playerrightNum = this.playerrightNum + 3
					}
				}
				if (obj.msg.indexOf("加入") != -1) {
					obj.type = 3
				} else if (obj.id == this.id) {
					obj.type = 2
				} else {
					obj.type = 1
				}
				this.msgList.push(obj)
			},
			async fapai() {
				let res = await post('home/DrawCard', {
					id: this.id
				})
				this.fapai1 = false
				console.log(res.data);
			},
			changecard(e) {
				if (this.OutCard.findIndex((v) => {
						return v.id == this.cardList[e].id
					}) == -1) {
					this.OutCard.push(this.cardList[e])
				} else {
					this.OutCard.splice(this.OutCard.findIndex((v) => {
						return v.id == this.cardList[e].id
					}), 1);
				}
			},
			async pass() {
				let res = await post('home/pass', {
					id: this.id,
					roomName: this.roomName
				})
				this.OutCard = []
				console.log(res);
			},
			async dapai() {
				if (this.OutCard.length == 0) {
					uni.showToast({
						icon: 'error',
						title: '您未选择牌'
					})
					return
				}
				let list = []
				this.OutCard.forEach(e => {
					list.push(e.id)
				})
				let res = await post('home/OutCard', {
					id: this.id,
					roomName: this.roomName,
					cards: list
				})
				if (list.length == 2 && list.toString() == 'SG-88,BG-99') {
					this.playAnimation = true
				}
				if (list.length == 4 && this.$tools.judgeFun(list)) {
					this.playAnimation = true
				}
				let list1 = []
				this.cardList.forEach((e, index) => {
					if (!this.OutCard.find((v) => {
							return v.id == this.cardList[index].id
						})) {
						list1.push(e)
					}
				})
				this.cardList = list1
				this.OutCard = []
				setTimeout(() => {
					this.playAnimation = false
				}, 1000)
			},
			async RoomMsg() {
				let res = await post('home/SendToRoom', {
					id: this.id,
					msg: this.title,
					roomName: this.roomName
				})
				this.title = ''
			},
			shuaxin() {
				this.homeList = []
				this.getRoomList()
			},
			async goToPages(a, b) {
				await this.inRoom(b.name)
				// uni.navigateTo({
				// 	url:a + '?b=' + b.name
				// })
				this.roomName = b.name
				this.roomid = b.masterid
				this.liaotian = false
			},
			showNickName() {
				let that = this
				uni.showModal({
					title: '提示',
					placeholderText: '请输入昵称',
					editable: true,
					success: function(res) {
						if (res.content == '') {
							that.showNickName()
						} else {
							that.nickName = res.content
							that.setNickName()
						}
					}
				});
			},
			async setNickName() {
				let res = await post('home/bindInfo', {
					id: this.id,
					nickName: this.nickName
				})
				console.log(res.data);
				this.getRoomList()
			},
			async getRoomList() {
				let res = await post('home/RoomList')
				this.homeList = res.data
			},
			async newRoom() {
				let res = await post('home/newRoom', {
					id: this.id,
					roomName: this.nickName + '的房间'
				})
				this.getRoomList()
				// this.goToPages('../index/index',{
				// 	name:this.nickName+'的房间'
				// })
			},
			async inRoom(e) {
				let res = await post('home/JoinRoom', {
					id: this.id,
					roomName: e
				})
			}
		}
	}
</script>

<style>
</style>

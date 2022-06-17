<template>
	<view class="index">
		<view style="width: 100%;height: 70vh;">
			<view v-for="item in msgList" class="zongCenter" style="padding: 0 20rpx 0 20rpx;">
				<view v-if="item.type == 1" style="max-width: 70%;align-self: flex-start;margin-top: 20rpx;background-color: #fff;padding: 20rpx;border-radius: 10rpx;">
					{{item.msg}}
				</view>
				<view v-if="item.type == 2" style="max-width: 70%;align-self: flex-end;margin-top: 20rpx;background-color: #129611;padding: 20rpx;border-radius: 10rpx;">
					{{item.msg}}
				</view>
				<view v-if="item.type == 3" style="font-size: 20rpx;padding: 0 20rpx 0 20rpx;background-color: #ccc;color: #fff;border-radius: 20rpx;margin-top: 20rpx;">
					{{item.msg}}
				</view>
			</view>
		</view>
		<view style="width: 100%;height: 30vh;background-color: #fff;padding: 20rpx;border-top: 1px solid #ccc;">
			<textarea style="width: 100%;height: 24vh;" v-model="title" placeholder="输入信息" />
			<view style="width: 100%;display: flex;justify-content: flex-end;">
				<text style="background-color: #129611;color: #fff;padding: 10rpx 20rpx 10rpx 20rpx;border-radius: 5rpx;" @click="RoomMsg()">发送</text>
			</view>
		</view>
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
				title: '',
				id:'',
				msgList:[],
				roomName:''
			}
		},
		onLoad(e) {
			this.roomName = e.b
			const conn = new signalR.HubConnectionBuilder()
						.withUrl("http://192.168.1.52:9090/mj")
						.build();
			// 接收公聊消息
			conn.on("Send",this.Send);
			// 加入房间消息
			conn.on("JoinRoom", this.JoinRoom);
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
			});
			conn.start().catch(err => console.log(err));
		},
		methods: {
			Send(obj){
				console.log(obj);
				// if(obj.msg == "加入"||obj.msg == "离开"){
				// 	obj.type = 3
				// }else if(obj.id == this.id){
				// 	obj.type = 1
				// }else{
				// 	obj.type = 2
				// }
				// this.msgList.push(obj)
			},
			newRoom(obj){
				console.log(obj);
			},
			JoinRoom(obj){
				console.log(obj);
			},
			PrivateSend(obj){
				console.log(obj);
			},
			SendToRoom(obj){
				console.log(obj);
				if(obj.msg.indexOf("加入")!=-1||obj.msg.indexOf("离开")!=-1){
					obj.type = 3
				}else if(obj.id == this.id){
					obj.type = 2
				}else{
					obj.type = 1
				}
				this.msgList.push(obj)
			},
			async addmasg() {
				let res = await post('home/send',{
					id:this.id,
					msg:this.title
				})
			},
			async RoomMsg() {
				let res = await post('home/SendToRoom',{
					id:this.id,
					msg:this.title,
					roomName:this.roomName
				})
			}
		}
	}
</script>

<style>
</style>

<script>
	export default {
		onLaunch: function() {
			console.log('App Launch')
		},
		onShow: function() {
			console.log('App Show')
		},
		onHide: function() {
			console.log('App Hide')
		}
	}
</script>

<style>
	@import url("./tools/animation.css");
	@import url("./tools/animate.min.css");
	/*每个页面公共css */
	body {
		background-color: #F7F5FD;
		font-size: 28upx;
		color: #333333;
		font-family: Helvetica Neue, Helvetica, sans-serif;
	}
	
	view,
	scroll-view,
	swiper,
	button,
	input,
	textarea,
	label,
	navigator,
	image {
		box-sizing: border-box;
	}
	* {
		padding: 0;
		margin: 0;
		box-sizing: border-box;
		font-family: PingFang SC;
	}
	.pages{
		display: flex;
		align-items: center;
		flex-direction: column;
	}
	/* 横向居中 */
	.hengCenter{
		display: flex;
		align-items: center;
	}
	/* 纵向居中 */
	.zongCenter{
		display: flex;
		align-items: center;
		flex-direction: column;
	}
	/* 全局中1-内容横向 */
	.allHengCenter{
		display: flex;
		align-items: center;
		justify-content: center;
	}
	/* 全局中2-内容纵向*/
	.allZongCenter{
		display: flex;
		align-items: center;
		justify-content: center;
		flex-direction: column;
	}
	/* 禁用样式 */
	.disabled {
	    pointer-events: none;
	    cursor: default;
	    opacity: 0.6;
	}
	/* 1溢出隐藏页面 */
	.oneLineNoWrap {
		overflow: hidden;
		-ms-text-overflow: ellipsis;
		text-overflow: ellipsis;
		white-space: nowrap;
	}
	/* 2溢出隐藏页面 */
	.towLineNoWrap {
		text-overflow: -o-ellipsis-lastline;
		  overflow: hidden;
		  text-overflow: ellipsis;
		  display: -webkit-box;
		  -webkit-line-clamp: 2;
		  line-clamp: 2;
		  -webkit-box-orient: vertical;
	}
	
</style>

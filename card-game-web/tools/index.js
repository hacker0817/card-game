export default {
	// 判断数组里面是否有重复项
	judgeFun(e){
		let str = e[0].split('-')[1]
		let state = true
		e.forEach(element=>{
			if(str != element.split('-')[1]){
				state = false
			}
		})
		return state
	},
	// 判断数组里面是否是飞机
	feijiFun(e){
		let state = true
		if(e.length>=8){
			let list = []
			e.forEach(element=>{
				list.push(element.split('-')[1])
			})
			list.forEach(e=>{
				
			})
		}else{
			state = false
		}
		return state
	},
	// 判断数组里面是否是顺子
	sunziFun(e){
		let state = true
		if(e.length>=5){
			let list = []
			e.forEach(element=>{
				list.push(element.split('-')[1])
			})
			list.sort(function(a,b){
				return a - b;
			})
			for (var i = 0; i < list.length; i++) {
				if(i+1<list.length){
					if(Number(list[i]) + 1 !=list[i+1]){
						state = false
					}
				}
			}
		}else{
			state = false
		}
		return state
	},
	//获取两个日期之间的天数间隔
	getDaysDiffBetweenDates(dateInitial, dateFinal) {
		(dateFinal - dateInitial) / (1000 * 3600 * 24);
	},
	//格式化毫秒
	formatDuration(ms) {
		if (ms < 0) ms = -ms;
		const time = {
			'天': Math.floor(ms / 86400000),
			'小时': Math.floor(ms / 3600000) % 24,
			'分钟': Math.floor(ms / 60000) % 60,
			'秒': Math.floor(ms / 1000) % 60,
			'毫秒': Math.floor(ms) % 1000
		};
		return Object.entries(time)
			.filter(val => val[1] !== 0)
			.map(([key, val]) => `${val} ${key}${val !== 1 ? '' : ''}`)
			.join(', ');
	},
	//延迟
	delay(fn, wait, ...args) {
		setTimeout(fn, wait, ...args)
	},
	//获取前一页面的数据
	prePage() {
		let pages = getCurrentPages();
		let prePage = pages[pages.length - 2];
		// #ifdef H5
		return prePage;
		// #endif
		return prePage.$vm;
	},
	showToast(txt, time = 2000) {
		uni.showToast({
			title: txt,
			icon: "none",
			duration: time
		})
	},
	getUrlDateFather(variable) {
		var query = window.location.search;
		var vars = query.split("&");

		for (var i = 0; i < vars.length; i++) {
			var pair = vars[i].split("=");
			if (pair[0].indexOf(variable) != -1) {
				return pair[1];
			}
		}
		return (false);
	},
	getUrlDate(variable) {
		var after = window.location.search;
		after = after.substr(1) || window.location.hash.split("?")[1];
		if (after) {
			var reg = new RegExp("(^|&)" + variable + "=([^&]*)(&|$)");
			var r = after.match(reg);
			if (r != null) {
				return decodeURIComponent(r[2]);
			} else {
				return null;
			}
		}
	},
	nameUnescape(val) {
		var val = val
		return unescape(val.replace(/\\u/g, "%u"));
	},
	// 保留两位小数(补0)
	Fixed(count, fl) {
		count += ''
		fl == undefined ? fl = 2 : ''
		let rs = count.indexOf('.')
		if (rs < 0) {
			count += '.'
			for (let i = 0; i < fl; i++) {
				count += '0'
			}
			return count
		}
		while (count.length <= rs + fl) {
			count += '0'
		}
		return count.substr(0, rs + (Number(fl) + 1))
	},
	//转换时间格式
	resolveDate(time, type) {
		let date = new Date(time);
		let month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
		let day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
		let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
		let min = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
		let sec = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
		let times = '';
		if (type == 2) {
			times = date.getFullYear() + '-' + month + '-' + day;

		} else if (type == 1) {
			times = date.getFullYear() + '/' + month + '/' + day + ' ' + hours + ':' + min + ':' + sec;

		} else {
			times = date.getFullYear() + '年' + month + '月' + day + '日 ' + hours + ':' + min + ':' + sec;

		}

		return times
	},
	//补0
	PrefixInteger(num, length) {
		return (Array(length).join('0') + num).slice(-length);
	},
	//获取周一到周日的日期
	getWeek() {
		let now = new Date();
		let nowTime = now.getTime();
		let day = now.getDay();
		let oneDayLong = 24 * 60 * 60 * 1000;
		let MondayTime = nowTime - (day - 1) * oneDayLong;
		let TuesdayTime = nowTime - (day - 2) * oneDayLong;
		let WednesdayTime = nowTime - (day - 3) * oneDayLong;
		let ThursdayTime = nowTime - (day - 4) * oneDayLong;
		let FridayTime = nowTime - (day - 5) * oneDayLong;
		let SaturdayTime = nowTime - (day - 6) * oneDayLong;
		let SundayTime = nowTime + (7 - day) * oneDayLong;
		let monday = new Date(MondayTime);
		let tuesday = new Date(TuesdayTime);
		let wednesday = new Date(WednesdayTime);
		let thursday = new Date(ThursdayTime);
		let friday = new Date(FridayTime);
		let saturday = new Date(SaturdayTime);
		let sunday = new Date(SundayTime);

		monday = monday.getFullYear() + '-' + this.PrefixInteger((monday.getMonth() + 1), 2) + '-' + monday.getDate() + ' ';
		tuesday = tuesday.getFullYear() + '-' + this.PrefixInteger((tuesday.getMonth() + 1), 2) + '-' + tuesday.getDate() +
			' ';
		wednesday = wednesday.getFullYear() + '-' + this.PrefixInteger((wednesday.getMonth() + 1), 2) + '-' + wednesday.getDate() +
			' ';
		thursday = thursday.getFullYear() + '-' + this.PrefixInteger((thursday.getMonth() + 1), 2) + '-' + thursday.getDate() +
			' ';
		friday = friday.getFullYear() + '-' + this.PrefixInteger((friday.getMonth() + 1), 2) + '-' + friday.getDate() + ' ';
		saturday = saturday.getFullYear() + '-' + this.PrefixInteger((saturday.getMonth() + 1), 2) + '-' + saturday.getDate() +
			' ';
		sunday = sunday.getFullYear() + '-' + this.PrefixInteger((sunday.getMonth() + 1), 2) + '-' + sunday.getDate() + ' ';
		let arr = [monday, tuesday, wednesday, thursday, friday, saturday, sunday];
		arr.forEach(item => {
			item = item.trim()
		})
		return arr;
	},
	// 获取当前日期
	getDate() {
		let date = new Date();
		let month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
		let day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
		let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
		let min = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
		let sec = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
		let times = '';
		times = date.getFullYear() + '-' + month + '-' + day;

		return times;
	},
	// 获取当前日期
	newgetDate() {
		let date = new Date();
		let month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
		let day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
		let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
		let min = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
		let sec = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
		let times = '';
		times = date.getFullYear() + '-' + month;
	
		return times;
	},
	// 获取当前日期
	newgetDateHMS() {
		let date = new Date();
		let month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
		let day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
		let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
		let min = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
		let sec = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
		let times = '';
		times = `${date.getFullYear()}-${month}-${day} ${hours}:${min}:${sec}`;
	
		return times;
	},
	// 获取当前日期开始结束
	newgetDateHMSend() {
		let date = new Date();
		let month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
		let day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
		let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
		let min = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
		let sec = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
		let times = '';
		let times1 = '';
		times = `${date.getFullYear()}-${month}-${day} 00:00:00`;
		times1 = `${date.getFullYear()}-${month}-${day} 23:59:59`;
	
		return {times,times1};
	},
	//格局化日期：yyyy-MM-dd
	formatDate(e) {
		let date = new Date(e);
		var myyear = date.getFullYear();
		var mymonth = date.getMonth() + 1;
		var myweekday = date.getDate();
		var myweekday = date.getDate();
		
		let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
		let min = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
		let sec = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();

		if (mymonth < 10) {
			mymonth = "0" + mymonth;
		}
		if (myweekday < 10) {
			myweekday = "0" + myweekday;
		}
		return (myyear + "-" + mymonth + "-" + myweekday+' '+hours+':'+min+':'+sec);
	},
	//获得某月的天数
	getMonthDays(myMonth) {
		var now = new Date(); //当前日期
		var nowYear = now.getYear(); //当前年
		var monthStartDate = new Date(nowYear, myMonth, 1);
		var monthEndDate = new Date(nowYear, myMonth + 1, 1);
		var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
		return days;
	},
	//获得本月的开端日期
	getMonthStartDate() {
		var now = new Date(); //当前日期
		var nowDayOfWeek = now.getDay(); //今天本周的第几天
		var nowDay = now.getDate(); //当前日
		var nowMonth = now.getMonth(); //当前月
		var nowYear = now.getYear(); //当前年
		nowYear += (nowYear < 2000) ? 1900 : 0; //
		var monthStartDate = new Date(nowYear, nowMonth, 1);
		return this.formatDate(monthStartDate);
	},

	//获得本月的停止日期
	getMonthEndDate() {
		var now = new Date(); //当前日期
		var nowDayOfWeek = now.getDay(); //今天本周的第几天
		var nowDay = now.getDate(); //当前日
		var nowMonth = now.getMonth(); //当前月
		var nowYear = now.getYear(); //当前年
		nowYear += (nowYear < 2000) ? 1900 : 0; //
		var monthEndDate = new Date(nowYear, nowMonth, this.getMonthDays(nowMonth));
		return this.formatDate(monthEndDate);
	},
	//获取某月的开始时间和结束时间戳
	getstartenddate(e){
		let time = new Date(e);//当前月 要计算其他时间点自己传入即可
		let year = time.getFullYear();
		let month = parseInt( time.getMonth() + 1 );
		//开始时间 时间戳
		let start = new Date( year + "-" + month + "-01 00:00:00" ).getTime()
		//结束时间 时间戳
		if( month == 12 ){
			 //十二月的时候进位，这里直接用加减法算了  
			 //也可以用 time.setMonth( month + 1 )去计算并获取结束时间的月份和年份
			month = 0;
			year += 1;
		}
		let end = new Date( year + "-" + ( month + 1 )  + "-01 00:00:00" ).getTime()-1
		return {
			start,
			end
		}
	},
	//计算时间差
	shjijiancha(date1,date2){
			if(date2 == 0){
			date2 = new Date().getTime()
			}
		    var date3 = new Date(date2).getTime() - new Date(date1).getTime();   //时间差的毫秒数        
		    //------------------------------  
		    //计算出相差天数  
		    var days=Math.floor(date3/(24*3600*1000))  
		    //计算出小时数  
		    var leave1=date3%(24*3600*1000)    //计算天数后剩余的毫秒数  
		    var hours=Math.floor(leave1/(3600*1000))  
		    //计算相差分钟数  
		    var leave2=leave1%(3600*1000)        //计算小时数后剩余的毫秒数  
		    var minutes=Math.floor(leave2/(60*1000))  
		    //计算相差秒数  
		    var leave3=leave2%(60*1000)      //计算分钟数后剩余的毫秒数  
		    var seconds=Math.round(leave3/1000)
			
			return days+"天"+hours+"时"+minutes+"分"+seconds+"秒"
	},
	//验证手机号
	checkPhone(phone) {
		let res = true;
		if (!(/^1[3456789]\d{9}$/.test(phone))) {
			res = false;
			uni.showToast({
				icon:"none",
				title:"请输入正确的手机号"
			})
		}
		return res;
	}
}

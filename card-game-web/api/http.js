import {
	baseUrl
} from './base.js'

export const get = (url, data) => {
	uni.showLoading({
		mask:true
	});
	return new Promise((resolve, reject) => {
		const token = uni.getStorageSync('token');
		let header = {};
		if (token) {
			header = {
				...header,
				...{
					'Authorization': 'Bearer ' + token
				}
			}
		}
		uni.request({
			url: baseUrl + url,
			data,
			method: 'GET',
			header,
			success: res => {
				if (res.statusCode == 200 || res.statusCode == 204) {
					uni.hideLoading()
					resolve(res)
				} else {
					if (res.statusCode == 401) {
						uni.removeStorageSync('token');
						uni.showToast({
							mask:true,
							title: '登陆失效',
							icon: 'none'
						})
						setTimeout(() => {
							uni.reLaunch({
								url: '/pages/login/index'
							})
						}, 1500)
					} else if (res.statusCode == 502) {
						uni.showToast({
							mask:true,
							title: '网络请求失败，请重试',
							icon: 'none',
							duration: 2000
						})
					} else if (res.statusCode == 400) {
						uni.showToast({
							mask:true,
							title: res.data,
							icon: 'none',
							duration: 2000
						})
					}
				}
			},
			fail: err => {
				uni.hideLoading()
				uni.showToast({
					mask:true,
					title: err,
					icon: 'none',
					duration: 2000
				})
				reject(err)
			}
		});
	})
}

export const post = (url, data, responseType = 'text') => {
	uni.showLoading({
		mask:true
	});
	return new Promise((resolve, reject) => {
		const token = uni.getStorageSync('token')
		// 请求前修改请求数据
		let ret = ''
		for (let it in data) {
			ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
		}
		ret = ret.trim('&');
		let header = {
			'Cache-Control': 'no-cache',
			'Content-Type': 'application/x-www-form-urlencoded',
		}
		if (token) {
			header = {
				...{
					'Authorization': 'Bearer ' + token
				},
				...header
			}
		}
		// 发请求
		uni.request({
			url: baseUrl + url,
			data: ret,
			method: 'POST',
			responseType: responseType,
			header,
			success: res => {
				if (res.statusCode == 200 || res.statusCode == 204) {
					uni.hideLoading()
					resolve(res)
				} else {
					if (res.statusCode == 401) {
						uni.removeStorageSync('token');
						uni.showToast({
							mask:true,
							title: '登陆失效',
							icon: 'none'
						})
						setTimeout(() => {
							uni.reLaunch({
								url: '/pages/login/index'
							})
						}, 1500)
					} else if (res.statusCode == 502) {
						uni.showToast({
							mask:true,
							title: '网络请求失败，请重试',
							icon: 'none',
							duration: 2000
						})
					} else if (res.statusCode == 400) {
						uni.showToast({
							mask:true,
							title: res.data,
							icon: 'none',
							duration: 2000
						})
					}
				}
			},
			fail: err => {
				uni.hideLoading()
				uni.showToast({
					mask:true,
					title: err,
					icon: 'none',
					duration: 2000
				})
				reject(err)
			}
		});
	})
}

export const put = (url, data, responseType = 'text') => {
	uni.showLoading({
		mask:true
	});
	return new Promise((resolve, reject) => {
		const token = uni.getStorageSync('token')
		// 请求前修改请求数据
		let ret = ''
		for (let it in data) {
			ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
		}
		let header = {
			'Cache-Control': 'no-cache',
			'Content-Type': 'application/x-www-form-urlencoded',
		}
		if (token) {
			header = {
				...{
					'Authorization': 'Bearer ' + token
				},
				...header
			}
		}
		// 发请求
		uni.request({
			url: baseUrl + url,
			data: ret,
			method: 'PUT',
			responseType: responseType,
			header,
			success: res => {
				if (res.statusCode == 200 || res.statusCode == 204) {
					uni.hideLoading()
					resolve(res)
				} else {
					if (res.statusCode == 401) {
						uni.removeStorageSync('token');
						uni.showToast({
							mask:true,
							title: '登陆失效',
							icon: 'none'
						})
						setTimeout(() => {
							uni.reLaunch({
								url: '/pages/login/index'
							})
						}, 1500)
					} else if (res.statusCode == 502) {
						uni.showToast({
							mask:true,
							title: '网络请求失败，请重试',
							icon: 'none',
							duration: 2000
						})
					} else if (res.statusCode == 400) {
						uni.showToast({
							mask:true,
							title: res.data,
							icon: 'none',
							duration: 2000
						})
					}
				}
			},
			fail: err => {
				uni.hideLoading()
				uni.showToast({
					mask:true,
					title: err,
					icon: 'none',
					duration: 2000
				})
				reject(err)
			}
		});
	})
}

export const del = (url, data, responseType = 'text') => {
	uni.showLoading({
		mask:true
	});
	return new Promise((resolve, reject) => {
		const token = uni.getStorageSync('token')
		// 请求前修改请求数据
		let ret = ''
		for (let it in data) {
			ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
		}
		let header = {
			'Cache-Control': 'no-cache',
			'Content-Type': 'application/x-www-form-urlencoded',
		}
		if (token) {
			header = {
				...{
					'Authorization': 'Bearer ' + token
				},
				...header
			}
		}
		// 发请求
		uni.request({
			url: baseUrl + url,
			data: ret,
			method: 'DELETE',
			responseType: responseType,
			header,
			success: res => {
				if (res.statusCode == 200 || res.statusCode == 204) {
					uni.hideLoading()
					resolve(res)
				} else {
					if (res.statusCode == 401) {
						uni.removeStorageSync('token');
						uni.showToast({
							mask:true,
							title: '登陆失效',
							icon: 'none'
						})
						setTimeout(() => {
							uni.reLaunch({
								url: '/pages/login/index'
							})
						}, 1500)
					} else if (res.statusCode == 502) {
						uni.showToast({
							mask:true,
							title: '网络请求失败，请重试',
							icon: 'none',
							duration: 2000
						})
					} else if (res.statusCode == 400) {
						uni.showToast({
							mask:true,
							title: res.data,
							icon: 'none',
							duration: 2000
						})
					}
				}
			},
			fail: err => {
				uni.hideLoading()
				uni.showToast({
					mask:true,
					title: err,
					icon: 'none',
					duration: 2000
				})
				reject(err)
			}
		});
	})
}

// 倍电接口post

export const bdpost = (url, data, responseType = 'text') => {
	return new Promise((resolve, reject) => {
		let timestamp = new Date().getTime()
		let arr1 = Object.keys(data)
		let stringb = ''
		arr1.forEach(e => {
			stringb += e + '=' + data[e] + '&'
		})
		let stringA = "appid=2021051800000105&" + stringb + "timestamp=" + timestamp
		let stringSignTemp = stringA + "&key=WQ323t7xrk5qwcGfAxjPaA"
		console.log(stringSignTemp)
		let sign = md5.hex_md5(stringSignTemp).toUpperCase()
		data = {
			...data,
			// ...{
			// 	timestamp:timestamp,
			// 	appid:2021051800000105,
			// 	sign: sign
			// }
		}
		// 请求前修改请求数据
		let ret = ''
		for (let it in data) {
			ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
		}
		ret = ret.trim('&');
		let header = {
			'Cache-Control': 'no-cache',
			'Content-Type': 'application/x-www-form-urlencoded',
		}
		uni.showLoading();
		// 发请求
		uni.request({
			url: 'https://admin.rrzuji.com/' + url,
			data: ret,
			method: 'POST',
			responseType: responseType,
			header,
			success: res => {
				resolve(res)
			},
			fail: err => {
				reject(err)
			}
		});
	})
}

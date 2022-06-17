import App from './App'
import Vue from 'vue'
import card from './tools/cards.js'
import tools from './tools/index.js'

Vue.config.productionTip = false
Vue.prototype.$card = card;
Vue.prototype.$tools = tools;
App.mpType = 'app'
const app = new Vue({
    ...App
})
app.$mount()
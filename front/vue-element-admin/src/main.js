import Vue from 'vue'

import Cookies from 'js-cookie'

import 'normalize.css/normalize.css' // a modern alternative to CSS resets

import Element from 'element-ui'
import './styles/element-variables.scss'

import '@/styles/index.scss' // global css

import App from './App'
import store from './store'
import router from './router'

import i18n from './lang' // internationalization
import './icons' // icon
import './permission' // permission control
import './utils/error-log' // error log
import permission from '@/directive/permission/index.js'

import * as filters from './filters' // global filters

import dict from './config/dict'

import eIconPicker from 'e-icon-picker'
import 'e-icon-picker/lib/symbol.js' // 基本彩色图标库
import 'e-icon-picker/lib/index.css' // 基本样式，包含基本图标
import 'font-awesome/css/font-awesome.min.css' // font-awesome 图标库
import 'element-ui/lib/theme-chalk/icon.css' // element-ui 图标库

/**
 * If you don't want to use mock-server
 * you want to use MockJs for mock api
 * you can execute: mockXHR()
 *
 * Currently MockJs will be used in the production environment,
 * please remove it before going online ! ! !
 */
if (process.env.NODE_ENV === 'production') {
  const { mockXHR } = require('../mock')
  mockXHR()
}

Vue.use(Element, {
  size: Cookies.get('size') || 'small', // set element-ui default size
  i18n: (key, value) => i18n.t(key, value)
})

// register global utility filters
Object.keys(filters).forEach(key => {
  Vue.filter(key, filters[key])
})

Vue.config.productionTip = false

new Vue({
  el: '#app',
  router,
  store,
  i18n,
  render: h => h(App)
})

permission.install(Vue)

Vue.prototype.$dict = dict

// 全局删除增加图标
Vue.use(eIconPicker, {
  FontAwesome: false,
  ElementUI: true,
  eIcon: false, // 自带的图标，来自阿里妈妈
  eIconSymbol: false, // 是否开启彩色图标
  addIconList: [],
  removeIconList: [],
  zIndex: 3100// 选择器弹层的最低层,全局配置
})

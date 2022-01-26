import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import { getToken } from '@/utils/auth'
import i18n from '@/lang'
import { getRequestLanguage } from '@/lang'

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 30000 // request timeout
})

// request interceptor
service.interceptors.request.use(
  config => {
    // do something before request is sent

    if (store.getters.token) {
      // 判断token是否快过期
      const expireTime = store.state.user.expireTime || 0
      const leftTime = expireTime - new Date().getTime()
      if (!config.url.endsWith('login/refreshToken') && leftTime < 5 * 60 * 1000) {
        store.dispatch('user/refreshToken')
      }
      // let each request carry token
      // ['Authorization'] is a custom headers key
      // please modify it according to the actual situation
      config.headers['Authorization'] = 'Bearer ' + getToken()
    }

    config.headers['Accept-Language'] = getRequestLanguage() === 'en' ? 'en' : 'zh-Hans'
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    if (response.status === 200 && response.headers['content-type'] !== 'application/json; charset=utf-8') {
      return response
    }

    const res = response.data

    // if the custom code is not 20000, it is judged as an error.
    if (res.code !== '200') {
      Message({
        message: res.msg || 'Error',
        type: 'error',
        duration: 5 * 1000
      })

      // 100106: Illegal token; 100107: Other clients logged in; 100108: Token expired;
      if (res.code === '100106' || res.code === '100107' || res.code === '100108') {
        // to re-login
        MessageBox.confirm(i18n.t('login.需要重新登录'), i18n.t('login.确认退出'), {
          confirmButtonText: i18n.t('login.重新登录'),
          cancelButtonText: i18n.t('login.取消'),
          type: 'warning'
        }).then(() => {
          store.dispatch('user/resetToken').then(() => {
            location.reload()
          })
        })
      }
      return Promise.reject(new Error(res.msg || 'Error'))
    } else {
      return res
    }
  },
  error => {
    console.log('err' + error) // for debug
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)

export default service

import request from '@/utils/request'
import baseUrl from './service-config'

// 登录方法
export function login(data) {
  return request({
    url: `${baseUrl}api/systemManagement/login`,
    headers: {
      isToken: false
    },
    method: 'post',
    data: data
  })
}

export function refreshToken() {
  return request({
    url: `${baseUrl}api/systemManagement/login/refreshToken`,
    method: 'post'
  })
}

export function logout() {
  return request({
    url: '/vue-element-admin/user/logout',
    method: 'post'
  })
}

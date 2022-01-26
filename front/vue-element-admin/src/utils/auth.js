import Cookies from 'js-cookie'

const TokenKey = 'Admin-Token'
const TokenExpireTimeKey = 'Admin-Token-ExpireTime'

export function getToken() {
  return Cookies.get(TokenKey)
}

export function getTokenExpireTime() {
  return Cookies.get(TokenExpireTimeKey)
}

export function setToken(token) {
  Cookies.set(TokenKey, token)
  const expireTime = new Date().getTime() + 10 * 60 * 1000
  Cookies.set(TokenExpireTimeKey, expireTime)
  return expireTime
}

export function removeToken() {
  Cookies.remove(TokenKey)
  return Cookies.remove(TokenExpireTimeKey)
}

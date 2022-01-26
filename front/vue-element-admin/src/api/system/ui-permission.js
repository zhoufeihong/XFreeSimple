import request from '@/utils/request'
import baseUrl from '../service-config'

// 获取信息
export function get(id) {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/${id}`,
    method: 'get'
  })
}

// 创建
export function create(data) {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission`,
    method: 'post',
    data: data
  })
}

// 修改
export function update(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/${id}`,
    method: 'put',
    data: data
  })
}

// 删除
export function deleteResource(id) {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/${id}`,
    method: 'delete'
  })
}

// 获取信息
export function query(parentId) {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/query`,
    method: 'get',
    params: {
      parentId
    }
  })
}

// 查询全部
export function getAll() {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/getAll`,
    method: 'get'
  })
}

// 查询全部后端权限数据
export function getAllBackgroundApi() {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/getAllBackgroundApi`,
    method: 'get'
  })
}

// 获取权限绑定的后台服务
export function queryPermissionCodes(id) {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/${id}/permissionCodes`,
    method: 'get'
  })
}

// 绑定后台服务
export function bindBackgroundApi(id, permissionCodes) {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/${id}/permissionCodes`,
    method: 'put',
    data: permissionCodes
  })
}

// 刷新接口资源
export function refreshBackgroundApi() {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/refreshBackgroundApi`,
    method: 'put'
  })
}

// 清除本地缓存
export function removeAllMemoryCache() {
  return request({
    url: `${baseUrl}api/systemManagement/uiPermission/removeAllMemoryCache`,
    method: 'put'
  })
}

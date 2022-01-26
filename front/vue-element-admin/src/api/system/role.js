import request from '@/utils/request'
import baseUrl from '../service-config'

// 获取角色信息列表
export function getListPaged(query) {
  return request({
    url: `${baseUrl}api/systemManagement/role`,
    method: 'get',
    params: query
  })
}

// 获取信息列表
export function getAll(query) {
  return request({
    url: `${baseUrl}api/systemManagement/role/all`,
    method: 'get'
  })
}

// 创建角色
export function create(data) {
  return request({
    url: `${baseUrl}api/systemManagement/role`,
    method: 'post',
    data: data
  })
}

// 修改角色信息
export function update(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/role/${id}`,
    method: 'put',
    data: data
  })
}

// 删除
export function deleteResource(id) {
  return request({
    url: `${baseUrl}api/systemManagement/role/${id}`,
    method: 'delete'
  })
}

// 修改状态
export function updateStatus(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/role/${id}/status`,
    method: 'put',
    data: data
  })
}

// 获取角色权限信息列表
export function getUiPermissionIds(id) {
  return request({
    url: `${baseUrl}api/systemManagement/role/${id}/uiPermissionIds`,
    method: 'get'
  })
}

// 修改角色权限
export function grantUiPermission(id, permissionIds) {
  return request({
    url: `${baseUrl}api/systemManagement/role/${id}/grant`,
    method: 'put',
    data: permissionIds
  })
}

// 获取角色用户列表
export function getUsers(id) {
  return request({
    url: `${baseUrl}api/systemManagement/role/${id}/users`,
    method: 'get'
  })
}

import request from '@/utils/request'
import baseUrl from '../service-config'

// 获取用户信息
export function getInfo(token) {
  return request({
    url: `${baseUrl}api/systemManagement/user/getUserPermisionInfo`,
    method: 'get',
    params: { userId: '0' }
  })
}

// 通过token获取信息
export function getByToken() {
  return request({
    url: `${baseUrl}api/systemManagement/user/getByToken`,
    method: 'get'
  })
}

// 修改密码
export function updatePassword(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/user/${id}/password`,
    method: 'put',
    data: data
  })
}

// 获取操作记录信息
export function getOperationInfoListPaged(pageIndex, pageSize) {
  return request({
    url: `${baseUrl}api/systemManagement/user/getOperationInfoListPaged`,
    method: 'get',
    params: {
      pageIndex,
      pageSize
    }
  })
}

// 获取用户信息列表
export function getListPaged(query) {
  return request({
    url: `${baseUrl}api/systemManagement/user`,
    method: 'get',
    params: query
  })
}

// 创建用户
export function create(data) {
  return request({
    url: `${baseUrl}api/systemManagement/user`,
    method: 'post',
    data: data
  })
}

// 修改用户信息
export function update(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/user/${id}`,
    method: 'put',
    data: data
  })
}

// 删除
export function deleteResource(id) {
  return request({
    url: `${baseUrl}api/systemManagement/user/${id}`,
    method: 'delete'
  })
}

// 修改状态
export function updateStatus(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/user/${id}/status`,
    method: 'put',
    data: data
  })
}

// 导出
export function exportResource(query) {
  return request({
    url: `${baseUrl}api/systemManagement/user/export`,
    method: 'get',
    params: query,
    headers: {
      accept: 'application/octet-stream'
    },
    responseType: 'blob'
  })
}

// 获取用户菜单权限列表
export function getUiPermission() {
  return request({
    url: `${baseUrl}api/systemManagement/user/getUiPermission`,
    method: 'get'
  })
}

// 下载模板
export function downLoadTemplate() {
  return request({
    url: `${baseUrl}api/systemManagement/user/downLoadTemplate`,
    method: 'get',
    responseType: 'blob'
  })
}

// 重置密码
export function resetPassword(id) {
  return request({
    url: `${baseUrl}api/systemManagement/user/${id}/resetPassword`,
    method: 'put'
  })
}

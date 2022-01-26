import request from '@/utils/request'
import baseUrl from '../service-config'

// 获取信息列表
export function getListPaged(query) {
  return request({
    url: `${baseUrl}api/systemManagement/tenant`,
    method: 'get',
    params: query
  })
}

// 获取信息列表
export function getAll(query) {
  return request({
    url: `${baseUrl}api/systemManagement/tenant/all`,
    method: 'get'
  })
}

// 创建
export function create(data) {
  return request({
    url: `${baseUrl}api/systemManagement/tenant`,
    method: 'post',
    data: data
  })
}

// 修改
export function update(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/tenant/${id}`,
    method: 'put',
    data: data
  })
}

// 删除
export function deleteResource(id) {
  return request({
    url: `${baseUrl}api/systemManagement/tenant/${id}`,
    method: 'delete'
  })
}

// 修改状态
export function updateStatus(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/tenant/${id}/status`,
    method: 'put',
    data: data
  })
}

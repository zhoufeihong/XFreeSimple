import request from '@/utils/request'
import baseUrl from '../service-config'

// 获取信息列表
export function getListPaged(query) {
  return request({
    url: `${baseUrl}api/systemManagement/depart`,
    method: 'get',
    params: query
  })
}

// 获取信息列表
export function getAll(query) {
  return request({
    url: `${baseUrl}api/systemManagement/depart/all`,
    method: 'get'
  })
}

// 获取信息
export function get(id) {
  return request({
    url: `${baseUrl}api/systemManagement/depart/${id}`,
    method: 'get'
  })
}

// 创建
export function create(data) {
  return request({
    url: `${baseUrl}api/systemManagement/depart`,
    method: 'post',
    data: data
  })
}

// 修改
export function update(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/depart/${id}`,
    method: 'put',
    data: data
  })
}

// 删除
export function deleteResource(id) {
  return request({
    url: `${baseUrl}api/systemManagement/depart/${id}`,
    method: 'delete'
  })
}

// 获取部门用户信息列表
export function getUserListPaged(departId, query) {
  return request({
    url: `${baseUrl}api/systemManagement/depart/${departId}/getUserListPaged`,
    method: 'get',
    params: query
  })
}

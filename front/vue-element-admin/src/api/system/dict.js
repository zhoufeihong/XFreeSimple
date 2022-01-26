import request from '@/utils/request'
import baseUrl from '../service-config'

// 获取字典信息列表
export function getListPaged(query) {
  return request({
    url: `${baseUrl}api/systemManagement/dict`,
    method: 'get',
    params: query
  })
}

// 获取信息列表
export function getAll(query) {
  return request({
    url: `${baseUrl}api/systemManagement/dict/all`,
    method: 'get'
  })
}

// 创建字典
export function create(data) {
  return request({
    url: `${baseUrl}api/systemManagement/dict`,
    method: 'post',
    data: data
  })
}

// 修改字典信息
export function update(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/dict/${id}`,
    method: 'put',
    data: data
  })
}

// 删除
export function deleteResource(id) {
  return request({
    url: `${baseUrl}api/systemManagement/dict/${id}`,
    method: 'delete'
  })
}

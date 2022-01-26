import request from '@/utils/request'
import baseUrl from '../service-config'

// 获取信息
export function getByDictCode(dictCode) {
  return request({
    url: `${baseUrl}api/systemManagement/dictItem/get`,
    method: 'get',
    params: {
      dictCode
    }
  })
}

// 获取信息列表
export function getListPaged(query) {
  return request({
    url: `${baseUrl}api/systemManagement/dictItem`,
    method: 'get',
    params: query
  })
}

// 创建字典项
export function create(data) {
  return request({
    url: `${baseUrl}api/systemManagement/dictItem`,
    method: 'post',
    data: data
  })
}

// 修改字典项信息
export function update(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/dictItem/${id}`,
    method: 'put',
    data: data
  })
}

// 修改状态
export function updateStatus(id, data) {
  return request({
    url: `${baseUrl}api/systemManagement/dictItem/${id}/status`,
    method: 'put',
    data: data
  })
}

// 删除字典项目
export function deleteResource(id) {
  return request({
    url: `${baseUrl}api/systemManagement/dictItem/${id}`,
    method: 'delete'
  })
}

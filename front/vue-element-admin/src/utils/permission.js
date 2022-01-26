import store from '@/store'

/**
 * @param {Array} value
 * @returns {Boolean}
 * @example
 */
export function checkPermission(value) {
  const permissionPerms = store.getters && store.getters.permissionPerms
  if (value) {
    if (value.length > 0) {
      const hasPermission = permissionPerms[value]
      if (hasPermission) {
        return true
      }
      return false
    }
  } else {
    throw new Error(`需要设置权限编码`)
  }
  return false
}


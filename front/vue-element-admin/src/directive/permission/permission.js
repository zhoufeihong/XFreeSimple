import { checkPermission } from '@/utils/permission'

/**
 * @param {Array} value
 * @returns {Boolean}
 * @example
 */
export function bindPermission(el, binding) {
  const { value } = binding
  if (!checkPermission(value)) {
    // el.parentNode && el.parentNode.removeChild(el)
  }
}

export default {
  inserted(el, binding) {
    bindPermission(el, binding)
  },
  update(el, binding) {
    bindPermission(el, binding)
  }
}

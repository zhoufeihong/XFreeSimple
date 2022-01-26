import store from '@/store'

export default {
  sexType: [
    { id: 0, name: '未知', enName: 'Unknown' },
    { id: 1, name: '男', enName: 'Male' },
    { id: 2, name: '女', enName: 'Female' }
  ],
  userType: [
    { id: 1, name: '租户用户', enName: 'Tenant user' },
    { id: 2, name: '平台用户', enName: 'Platform user' }
  ],
  uiMenuType: [
    { id: 0, name: '一级菜单', enName: 'Level 1 menu' },
    { id: 1, name: '子菜单', enName: 'Sub menu' },
    { id: 2, name: '权限', enName: 'Permissions' }
  ],
  RoleAccessType: [
    { id: 1, name: '公开的', enName: 'Public' },
    { id: 2, name: '管理员用户可访问', enName: 'The administrator can access it' },
    { id: 3, name: '按授权编码分配', enName: 'Assigned by authorization code' }
  ],
  DatabaseProviderType: [
    { id: 1, name: 'EntityFrameworkCore', enName: 'EntityFrameworkCore' },
    { id: 2, name: 'MongoDb', enName: 'MongoDb' }
  ],
  Language: [
    { id: 'zh-cn', name: '简体中文', enName: 'Simplified Chinese' },
    { id: 'en', name: '英文', enName: 'English' }
  ],
  EntryType: [
    { id: 'Tenant', name: '租户', enName: 'Tenant' },
    { id: 'Host', name: '管理后台', enName: 'Management' }
  ],
  getNameById: function(dict, valueId) {
    var vals = dict.filter(f => f.id === valueId)
    if (vals.length > 0) {
      if (store.getters.language === 'en') {
        return vals[0].enName
      }
      return vals[0].name
    }
    return ''
  }
}

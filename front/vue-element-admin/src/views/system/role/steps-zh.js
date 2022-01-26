const steps = [
  {
    element: '#btn-add',
    popover: {
      title: '添加角色',
      description: '添加角色信息，给角色授予相应权限。给用户分配角色，用户即被赋予角色的权限。',
      position: 'bottom'
    }
  },
  {
    element: '#btn-permission',
    popover: {
      title: '给角色分配权限',
      description: '权限树节点操作功能介绍<br>点击箭头: 展开/收缩树<br>点击复选框: 勾选/不勾选当前节点<br>点击节点内容: 勾选/不勾选当前节点及全部子节点',
      position: 'left'
    }
  }
]

export default steps

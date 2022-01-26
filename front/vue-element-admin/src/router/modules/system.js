/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const systemRouter = {
  path: '/system',
  component: Layout,
  redirect: '/system/user',
  name: '系统管理',
  meta: {
    title: '系统管理',
    icon: 'el-icon-setting'
  },
  children: [
    {
      path: 'user',
      component: () => import('@/views/system/user/index'),
      name: 'User',
      meta: { title: '用户管理' }
    },
    {
      path: 'role',
      component: () => import('@/views/system/role/index'),
      name: 'Role',
      meta: { title: '角色管理' }
    },
    {
      path: 'depart',
      component: () => import('@/views/system/depart/index'),
      name: 'Depart',
      meta: { title: '部门管理' }
    },
    {
      path: 'post',
      component: () => import('@/views/system/post/index'),
      name: 'Post',
      meta: { title: '职务管理' }
    },
    {
      path: 'uiPermission',
      component: () => import('@/views/system/ui-permission/index'),
      name: 'UiPermission',
      meta: { title: '菜单权限管理' }
    }
  ]
}
export default systemRouter

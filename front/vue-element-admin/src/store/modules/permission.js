import { constantRoutes } from '@/router'
import { getUiPermission } from '@/api/system/user'
import Layout from '@/layout'
import { getLanguage } from '@/lang/index'

/**
 * Filter asynchronous routing tables by recursion
 * @param roles
 */
async function filterAsyncRoutes(roles) {
  const response = await getUiPermission()
  const menus = listRouter(response.data)
  const perms = listPerms(response.data)
  return { menus, perms }
}

function listRouter(menus) {
  const getMenuName = (menu) => {
    if (getLanguage() === 'en') {
      return menu.enName
    }
    return menu.name
  }
  // 转换菜单
  const getRoute = (menu) => {
    if (menu.openMode) {
      return {
        id: menu.id,
        path: menu.url,
        name: menu.url,
        hidden: menu.hidden,
        meta: { title: getMenuName(menu), icon: menu.icon, noCache: true }
      }
    }
    if (menu.uiMenuType === 0) {
      return {
        id: menu.id,
        path: '/' + menu.url,
        name: menu.url,
        hidden: menu.hidden,
        redirect: menu.redirect,
        component: Layout,
        meta: { title: getMenuName(menu), icon: menu.icon, noCache: !menu.keepAlive },
        children: []
      }
    } else {
      return {
        id: menu.id,
        path: menu.url,
        name: menu.componentName,
        hidden: menu.hidden,
        component: (resolve) => {
          require(['@/views/' + menu.component + '.vue'], resolve)
        },
        meta: { title: getMenuName(menu), icon: menu.icon, noCache: !menu.keepAlive }
      }
    }
  }

  const dataRoot = {
    isRoot: true,
    id: '',
    children: []
  }

  menus.sort((d1, d2) => {
    return d1.sortOrder - d2.sortOrder
  })

  const findP = dataOption => {
    menus.forEach(element => {
      if ((dataOption.isRoot && element.uiMenuType === 0) || (element.parentId === dataOption.id && element.uiMenuType !== 2)) {
        const tempOption = getRoute(element)
        findP(tempOption, element.id)
        if (!dataOption.children) {
          dataOption.children = []
        }
        dataOption.children.push(tempOption)
      }
    })
  }

  findP(dataRoot)
  return dataRoot.children
}

function listPerms(menus) {
  // 查找权限
  const perms = {}
  menus.filter(f => f.uiMenuType === 2).forEach(perm => {
    perms[perm.perms] = perm
  })
  return perms
}

const state = {
  routes: [],
  addRoutes: [],
  permissionPerms: {}
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  },
  SET_PERMISSION_PERMS: (state, permissionPerms) => {
    state.permissionPerms = permissionPerms
  }
}

const actions = {
  generateRoutes({ commit }, roles) {
    return new Promise((resolve) => {
      filterAsyncRoutes(roles).then(res => {
        commit('SET_ROUTES', res.menus)
        commit('SET_PERMISSION_PERMS', res.perms)
        resolve(res.menus)
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

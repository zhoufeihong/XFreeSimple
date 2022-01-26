import { login, refreshToken } from '@/api/login'
import { getInfo } from '@/api/system/user'
import { getToken, getTokenExpireTime, setToken, removeToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'

const state = {
  token: getToken(),
  expireTime: getTokenExpireTime(),
  userId: '',
  supperUser: false,
  name: '',
  avatar: '',
  departId: '',
  introduction: '',
  roles: []
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_EXPIRE_TIME: (state, expireTime) => {
    state.expireTime = expireTime
  },
  SET_INTRODUCTION: (state, introduction) => {
    state.introduction = introduction
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  },
  SET_USER_ID: (state, userId) => {
    state.userId = userId
  },
  SET_DEPART_ID: (state, departId) => {
    state.departId = departId
  },
  SET_IS_ADMIN: (state, supperUser) => {
    state.supperUser = supperUser
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    const { loginName, password, tenantCode } = userInfo
    return new Promise((resolve, reject) => {
      login({ loginName: loginName.trim(), password: password, returnUserToken: true, tenantCode: tenantCode }).then(response => {
        const { data } = response
        commit('SET_TOKEN', data.userToken)
        const expireTime = setToken(data.userToken)
        commit('SET_EXPIRE_TIME', expireTime)
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  refreshToken({ commit }, state) {
    return new Promise((resolve, reject) => {
      refreshToken().then(response => {
        console.log('refreshToken')
        const { data } = response
        commit('SET_TOKEN', data.userToken)
        const expireTime = setToken(data.userToken)
        commit('SET_EXPIRE_TIME', expireTime)
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },
  // get user info
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {
      getInfo(state.token).then(response => {
        const { data } = response

        if (!data) {
          reject('请求异常,请重新登录.')
        }

        const { roles, name, avatar, introduction, id, departId, supperUser } = data

        commit('SET_ROLES', roles)
        commit('SET_NAME', name)
        commit('SET_AVATAR', avatar)
        commit('SET_INTRODUCTION', introduction)
        commit('SET_USER_ID', id)
        commit('SET_DEPART_ID', departId)
        commit('SET_IS_ADMIN', supperUser)
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
      try {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        commit('SET_USER_ID', '')
        removeToken()
        resetRouter()
        // reset visited views and cached views
        // to fixed https://github.com/PanJiaChen/vue-element-admin/issues/2485
        dispatch('tagsView/delAllViews', null, { root: true })
        resolve(state)
      } catch (error) {
        reject(error)
      }
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      commit('SET_ROLES', [])
      commit('SET_USER_ID', '')
      removeToken()
      resolve()
    })
  },

  // dynamically modify permissions
  async changeRoles({ commit, dispatch }, role) {
    const token = role + '-token'

    commit('SET_TOKEN', token)
    setToken(token)

    const { roles } = await dispatch('getInfo')

    resetRouter()

    // generate accessible routes map based on roles
    const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })
    // dynamically add accessible routes
    router.addRoutes(accessRoutes)

    // reset visited views and cached views
    dispatch('tagsView/delAllViews', null, { root: true })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

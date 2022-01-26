<template>
  <div class="app-container">
    <div v-if="user">
      <el-row :gutter="20">

        <el-col :span="6" :xs="24">
          <user-card :user="user" />
        </el-col>

        <el-col :span="18" :xs="24">
          <el-card>
            <el-tabs v-model="activeTab">
              <el-tab-pane :label="$t('profile.详细信息')" name="account">
                <account :user="user" />
              </el-tab-pane>
              <el-tab-pane :label="$t('profile.近期记录')" name="timeline">
                <timeline />
              </el-tab-pane>
              <el-tab-pane :label="$t('profile.修改密码')" name="updatePassword">
                <update-password />
              </el-tab-pane>
            </el-tabs>
          </el-card>
        </el-col>

      </el-row>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import UserCard from './components/UserCard'
import Timeline from './components/Timeline'
import Account from './components/Account'
import UpdatePassword from './components/UpdatePassword'
import { getByToken } from '@/api/system/user'

export default {
  name: 'Profile',
  components: { UserCard, Timeline, Account, UpdatePassword },
  data() {
    return {
      user: {},
      activeTab: 'account'
    }
  },
  computed: {
    ...mapGetters([
      'name',
      'avatar',
      'roles'
    ])
  },
  created() {
    this.getUser()
  },
  methods: {
    async getUser() {
      const { data } = await getByToken()
      data.roles = this.roles
      this.user = data
    }
  }
}
</script>

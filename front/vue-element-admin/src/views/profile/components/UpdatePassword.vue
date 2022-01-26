<template>
  <el-form ref="updatePasswordForm" :model="userForm" :rules="updatePasswordRules" label-width="220px">
    <el-form-item :label="$t('profile.原密码')" prop="oldPassword">
      <el-input v-model.trim="userForm.oldPassword" style="width: 50%;" show-password />
    </el-form-item>
    <el-form-item :label="$t('profile.新密码')" prop="newPassword">
      <el-input v-model.trim="userForm.newPassword" style="width: 50%;" show-password />
    </el-form-item>
    <el-form-item :label="$t('profile.确认新密码')" prop="newPassword1">
      <el-input v-model.trim="userForm.newPassword1" style="width: 50%;" show-password />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit">{{ $t('profile.确认修改') }}</el-button>
    </el-form-item>
  </el-form>
</template>

<script>

import { mapGetters } from 'vuex'
import { updatePassword } from '@/api/system/user'

export default {
  data() {
    return {
      userForm: {
        name: '',
        oldPassword: '',
        newPassword: '',
        newPassword1: ''
      },
      updatePasswordRules: {
        oldPassword: [
          { required: true, message: this.$t('profile.请输入原密码'), trigger: 'blur' },
          { min: 6, message: this.$t('profile.密码至少为6个字符'), trigger: 'blur' }
        ],
        newPassword: [
          { required: true, message: this.$t('profile.请输入新密码'), trigger: 'blur' },
          { min: 6, message: this.$t('profile.密码至少为6个字符'), trigger: 'blur' }
        ],
        newPassword1: [
          { required: true, message: this.$t('profile.请确认新密码'), trigger: 'blur' },
          { min: 6, message: this.$t('profile.密码至少为6个字符'), trigger: 'blur' }
        ]
      }
    }
  },
  computed: {
    ...mapGetters([
      'userId'
    ])
  },
  methods: {
    submit() {
      const that = this
      this.$refs.updatePasswordForm.validate((valid) => {
        if (valid) {
          if (this.userForm.newPassword !== this.userForm.newPassword1) {
            this.$message({
              message: this.$t('profile.新密码前后输入不一致'),
              type: 'error',
              duration: 5 * 1000
            })
            return
          }
          if (this.userForm.oldPassword === this.userForm.newPassword) {
            this.$message({
              message: this.$t('profile.新旧密码一致，不需要修改'),
              type: 'error',
              duration: 5 * 1000
            })
            return
          }
          updatePassword(that.userId, { oldPassword: that.userForm.oldPassword, newPassword: that.userForm.newPassword }).then(response => {
            this.$message({
              message: this.$t('profile.修改成功,请重新登录!'),
              type: 'success',
              duration: 5 * 1000
            })
            this.$store.dispatch('user/logout')
            this.$router.push(`/login?redirect=${this.$route.fullPath}`)
          })
        }
      })
    }
  }
}
</script>

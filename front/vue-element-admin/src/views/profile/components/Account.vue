<template>
  <el-form label-width="180px">
    <el-form-item :label="$t('profile.账号')">
      <el-input v-model.trim="user.loginName" :readonly="true" />
    </el-form-item>
    <el-form-item :label="$t('commonUse.工号')">
      <el-input v-model.trim="user.employeeIDNumber" :readonly="true" />
    </el-form-item>
    <el-form-item :label="$t('profile.姓名')">
      <el-input v-model.trim="user.nickname" :readonly="true" />
    </el-form-item>
    <el-form-item :label="$t('profile.性别')">
      <el-select v-model="user.sex">
        <el-option
          v-for="item in $dict.sexType"
          :key="item.id"
          :label="$dict.getNameById($dict.sexType, item.id)"
          :value="item.id"
        />
      </el-select>
    </el-form-item>
    <el-form-item :label="$t('profile.生日')">
      <el-input v-model.trim="user.birthday" :readonly="true" />
    </el-form-item>
    <el-form-item :label="$t('profile.手机号')">
      <el-input v-model.trim="user.phone" :readonly="true" />
    </el-form-item>
    <el-form-item :label="$t('profile.账号类型')">
      <el-select v-model="user.userType">
        <el-option
          v-for="item in $dict.userType"
          :key="item.id"
          :label="$dict.getNameById($dict.userType, item.id)"
          :value="item.id"
        />
      </el-select>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  props: {
    user: {
      type: Object,
      default: () => {
        return {
          name: '',
          email: ''
        }
      }
    }
  },
  methods: {
  }
}
</script>

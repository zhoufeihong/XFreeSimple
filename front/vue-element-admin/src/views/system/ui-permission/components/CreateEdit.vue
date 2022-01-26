<template>
  <div>
    <el-drawer :title="textMap[currentDialogStatus]" :visible.sync="currentDialogFormVisible" size="38%">
      <div class="drawer_content">
        <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="180px">
          <el-form-item :label="$t('uiPermission.菜单类型')">
            <el-radio-group v-model="temp.uiMenuType" @change="onChangeUiMenuType">
              <el-radio-button :label="0">{{ $t('uiPermission.一级菜单') }}</el-radio-button>
              <el-radio-button :label="1">{{ $t('uiPermission.子菜单') }}</el-radio-button>
              <el-radio-button :label="2">{{ $t('uiPermission.按钮/权限') }}</el-radio-button>
            </el-radio-group>
          </el-form-item>
          <el-form-item v-show="temp.uiMenuType !== 0" :label="$t('uiPermission.上级菜单')" prop="parentId">
            <ui-permission-select v-model="temp.parentId" :ignore-values="[temp.id]" :placeholder="$t('uiPermission.请选择上级')" />
          </el-form-item>
          <el-form-item :label="menuLabel" prop="name">
            <el-input v-model.trim="temp.name" :placeholder="$t('uiPermission.请输入名称')" />
          </el-form-item>
          <el-form-item :label="menuEnLabel" prop="enName">
            <el-input v-model="temp.enName" :placeholder="$t('uiPermission.请输入英文名称')" />
          </el-form-item>
          <el-form-item v-show="show" :label="$t('uiPermission.图标')">
            <e-icon-picker v-model="temp.icon" />
          </el-form-item>
          <el-form-item :label="$t('uiPermission.菜单路径')">
            <el-input v-model.trim="temp.url" :placeholder="$t('uiPermission.请输入')" />
          </el-form-item>
          <el-form-item v-show="show" :label="$t('uiPermission.前端组件名称')">
            <el-input v-model.trim="temp.componentName" :placeholder="$t('uiPermission.请输入')" />
          </el-form-item>
          <el-form-item v-show="show" :label="$t('uiPermission.前端组件')">
            <el-input v-model.trim="temp.component" :placeholder="$t('uiPermission.请输入')" />
          </el-form-item>
          <el-form-item v-show="temp.uiMenuType==0" :label="$t('uiPermission.默认跳转地址')">
            <el-input v-model.trim="temp.redirect" :placeholder="$t('uiPermission.请输入')" />
          </el-form-item>
          <el-form-item v-show="!show" :label="$t('uiPermission.权限编码')" prop="perms">
            <el-input v-model.trim="temp.perms" :placeholder="$t('uiPermission.请输入权限编码')" />
          </el-form-item>
          <el-form-item :label="$t('table.status')">
            <el-switch v-model="temp.enabled" />
          </el-form-item>
          <el-form-item :label="$t('uiPermission.排序')">
            <el-input-number v-model="temp.sortOrder" :placeholder="$t('uiPermission.请选择')" />
          </el-form-item>
          <el-form-item v-show="show" :label="$t('uiPermission.是否路由菜单')">
            <el-switch v-model="temp.isRoute" />
          </el-form-item>
          <el-form-item v-show="show" :label="$t('uiPermission.是否缓存该页面')">
            <el-switch v-model="temp.keepAlive" />
          </el-form-item>
          <el-form-item v-show="show" :label="$t('uiPermission.是否隐藏路由')">
            <el-switch v-model="temp.hidden" />
          </el-form-item>
          <el-form-item v-show="show" :label="$t('uiPermission.外部打开')">
            <el-switch v-model="temp.openMode" />
          </el-form-item>
        </el-form>
        <div class="drawer_footer">
          <el-button @click="currentDialogFormVisible = false">
            {{ $t('commonUse.取消') }}
          </el-button>
          <el-button type="primary" @click="currentDialogStatus==='create'?createData():updateData()">
            {{ $t('commonUse.保存') }}
          </el-button>
        </div>
      </div>
    </el-drawer>
  </div>

</template>
<script>
import UiPermissionSelect from '../../components/UiPermissionSelect'
import { create, update } from '@/api/system/ui-permission'

export default {
  name: 'CreateEdit',
  components: { UiPermissionSelect },
  props: {
    row: {
      type: Object,
      default: null
    },
    dialogStatus: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      currentDialogFormVisible: false,
      show: true,
      menuLabel: this.$t('uiPermission.菜单名称'),
      menuEnLabel: this.$t('uiPermission.菜单英文名称'),
      temp: {
        id: '',
        status: 1,
        uiMenuType: 0
      },
      excution: false,
      textMap: {
        update: this.$t('table.edit'),
        create: this.$t('table.add')
      },
      rules: {
        name: [{ required: true, message: this.$t('uiPermission.必填'), trigger: 'change' }]
      }
    }
  },
  computed: {
    currentDialogStatus: {
      get() {
        return this.dialogStatus
      },
      set(val) {
        this.$emit('update:dialogStatus', val)
      }
    }
  },
  watch: {
    row(val) {
      this.currentDialogFormVisible = true
      if (this.currentDialogStatus === 'create') {
        this.temp = {
          enabled: true,
          parentId: val.parentId,
          uiMenuType: val.parentId ? 1 : 0
        }
        this.handleCreate()
      } else {
        this.temp = val
        this.handleUpdate(val)
      }
    }
  },
  created() {

  },
  methods: {
    handleUpdate(row) {
      this.onChangeUiMenuType()
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    handleCreate() {
      this.onChangeUiMenuType()
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    checkMenuType() {
      return true
    },
    createData() {
      if (!this.checkMenuType()) {
        return
      }
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          this.excution = true
          create(this.temp).then(() => {
            this.$notify({
              title: this.$t('operation.result.successTitle'),
              message: this.$t('operation.result.addSuccessTitle'),
              type: 'success',
              duration: 2000
            })
            this.success()
            this.currentDialogFormVisible = false
            this.excution = false
          }, () => {
            this.excution = false
          })
        }
      })
    },
    onChangeUiMenuType() {
      if (this.temp.uiMenuType === 2) {
        this.show = false
        this.menuLabel = this.$t('uiPermission.按钮/权限')
        this.menuEnLabel = this.$t('uiPermission.按钮/权限')
      } else {
        this.show = true
        this.menuLabel = this.$t('uiPermission.菜单名称')
        this.menuEnLabel = this.$t('uiPermission.菜单英文名称')
      }
    },
    updateData() {
      if (!this.checkMenuType()) {
        return
      }
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          update(tempData.id, tempData).then(() => {
            this.success()
            this.$notify({
              title: this.$t('operation.result.successTitle'),
              message: this.$t('operation.result.editSuccessTitle'),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    success() {
      this.$emit('success', this.temp)
    }
  }
}
</script>

<style scoped>
.drawer_content {
    display: flex;
    flex-direction: column;
    height: 100%;
}
.drawer_content form {
    flex: 1;
}
.drawer_footer {
    display: flex;
}
.drawer_footer button {
    flex: 1;
}
</style>

<style>
.el-drawer__header {
  padding: 10px;
  margin-bottom: 5px;
}
.el-drawer__body {
    padding: 20px;
    flex: 1;
    overflow: auto;
}
.el-scrollbar__wrap {
  min-height: 204px;
}
.drawer_content .form_contet {
  flex: 1;
  overflow:scroll;
}
</style>

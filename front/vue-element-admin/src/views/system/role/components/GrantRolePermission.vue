<template>
  <div>
    <el-drawer :title="$t('role.权限管理')" :visible.sync="currentDialogFormVisible" size="39%">
      <div class="drawer_content">
        <div class="form_contet">
          <ui-permission-tree v-model="selectdPermissions" />
        </div>
        <div class="drawer_footer">
          <el-button @click="currentDialogFormVisible = false">
            {{ $t('commonUse.取消') }}
          </el-button>
          <el-button type="primary" @click="updateData()">
            {{ $t('commonUse.保存') }}
          </el-button>
        </div>
      </div>
    </el-drawer>
  </div>

</template>
<script>
import { getUiPermissionIds, grantUiPermission } from '@/api/system/role'
import UiPermissionTree from '../../components/UiPermissionTree'
export default {
  name: 'GrantRolePermission',
  components: { UiPermissionTree },
  props: {
    rowId: {
      type: String,
      default: ''
    },
    dialogFormVisible: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      selectdPermissions: []
    }
  },
  computed: {
    currentDialogFormVisible: {
      get() {
        return this.dialogFormVisible
      },
      set(val) {
        this.$emit('update:dialogFormVisible', val)
      }
    }
  },
  watch: {
    rowId(val) {
      getUiPermissionIds(val).then(response => {
        this.selectdPermissions = response.data
      })
    }
  },
  created() {

  },
  methods: {
    updateData() {
      if (!this.selectdPermissions) {
        this.selectdPermissions = []
      }
      grantUiPermission(this.rowId, this.selectdPermissions).then(() => {
        this.$notify({
          title: '成功',
          message: '保存成功',
          type: 'success',
          duration: 2000
        })
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
.drawer_content .form_contet {
    flex: 1;
}
.drawer_footer {
    display: flex;
}
.drawer_footer button {
    flex: 1;
}
.drawer_content .form_contet {
  flex: 1;
  overflow:scroll;
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
</style>

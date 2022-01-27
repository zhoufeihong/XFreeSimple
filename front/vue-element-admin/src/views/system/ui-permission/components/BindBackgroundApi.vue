<template>
  <div>
    <el-drawer :title="$t('uiPermission.接口资源绑定')" :visible.sync="currentDialogFormVisible" size="35%">
      <div class="drawer_content">
        <div class="form_contet">
          <background-api-tree v-model="selectdBackgroundApis" />
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
import { queryPermissionCodes, bindBackgroundApi } from '@/api/system/ui-permission'
import BackgroundApiTree from './BackgroundApiTree'
export default {
  name: 'BindBackgroundApi',
  components: { BackgroundApiTree },
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
      selectdBackgroundApis: []
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
      queryPermissionCodes(val).then(response => {
        this.selectdBackgroundApis = response.data
      })
    }
  },
  created() {

  },
  methods: {
    updateData() {
      if (!this.selectdBackgroundApis) {
        this.selectdBackgroundApis = []
      }
      bindBackgroundApi(this.rowId, this.selectdBackgroundApis).then(() => {
        this.$notify({
          title: this.$t('operation.result.successTitle'),
          message: this.$t('operation.successMsg'),
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

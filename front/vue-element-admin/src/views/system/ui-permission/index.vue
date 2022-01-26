<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button v-permission="'system:uiPermission:create'" class="filter-item" type="primary" icon="el-icon-edit" @click="handleCreate">
        {{ $t('table.add') }}
      </el-button>
      <el-button class="filter-item" type="primary" icon="el-icon-refresh" @click="handleFilter">
        {{ $t('uiPermission.刷新') }}
      </el-button>
      <el-button v-permission="'system:uiPermission:refreshBackgroundApi'" class="filter-item" type="danger" icon="el-icon-refresh" @click="handleRefreshBackgroundApi">
        {{ $t('uiPermission.刷新接口资源') }}
      </el-button>
      <el-button v-permission="'system:uiPermission:refreshBackgroundApi'" class="filter-item" type="danger" icon="el-icon-refresh" @click="handleRemoveAllMemoryCache">
        {{ $t('uiPermission.清除服务本地缓存') }}
      </el-button>
    </div>
    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      lazy
      highlight-current-row
      row-key="id"
      :load="load"
      style="width: 100%;"
      :tree-props="{children: 'children', hasChildren: 'hasChildren'}"
    >
      >
      <el-table-column :label="$t('commonUse.名称')" min-width="150" align="left">
        <template slot-scope="{row}">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('commonUse.英文名称')" min-width="150" align="left">
        <template slot-scope="{row}">
          <span>{{ row.enName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('uiPermission.图标')" min-width="100" align="center">
        <template slot-scope="{row}">
          <i :class="row.icon" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('uiPermission.类型')" min-width="100" align="center">
        <template slot-scope="{row}">
          <span>{{ row.uiMenuType | uiMenuTypeFilter }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('uiPermission.组件')" min-width="130" align="center">
        <template slot-scope="{row}">
          <span>{{ row.component }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('uiPermission.菜单路径')" min-width="100" align="center">
        <template slot-scope="{row}">
          <span>{{ row.url }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('uiPermission.排序')" min-width="50" align="center">
        <template slot-scope="{row}">
          <span>{{ row.sortOrder }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.creationTime')" min-width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.actions')" align="center" min-width="400" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button v-permission="'system:uiPermission:update'" type="primary" size="mini" @click="handleCreateFromRow(row)">
            {{ $t('uiPermission.添加下级') }}
          </el-button>
          <el-button v-permission="'system:uiPermission:update'" type="primary" size="mini" @click="handleUpdate(row)">
            {{ $t('table.edit') }}
          </el-button>
          <el-button v-if="row.status!='deleted'" v-permission="'system:uiPermission:delete'" size="mini" type="danger" @click="handleDelete(row,$index)">
            {{ $t('table.delete') }}
          </el-button>
          <el-button v-if="row.status!='deleted'" v-permission="'system:uiPermission:update'" size="mini" type="danger" @click="bindBackgroundApi.rowId=row.id;bindBackgroundApi.dialogFormVisible=true">
            {{ $t('uiPermission.绑定接口资源') }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <create-edit :row="temp" :dialog-status.sync="dialogStatus" @success="handleFilter()" />
    <bind-background-api :row-id="bindBackgroundApi.rowId" :dialog-form-visible.sync="bindBackgroundApi.dialogFormVisible" />
  </div>
</template>

<script>
import { query, deleteResource, refreshBackgroundApi, removeAllMemoryCache } from '@/api/system/ui-permission'
import dict from '@/config/dict'
import CreateEdit from './components/CreateEdit'
import BindBackgroundApi from './components/BindBackgroundApi'

export default {
  name: 'UiPermission',
  components: { CreateEdit, BindBackgroundApi },
  filters: {
    uiMenuTypeFilter(uiMenuType) {
      return dict.getNameById(dict.uiMenuType, uiMenuType)
    }
  },
  data() {
    return {
      tableKey: 0,
      list: [],
      total: 0,
      listLoading: true,
      excution: false,
      temp: {
      },
      dialogStatus: '',
      bindBackgroundApi: {
        dialogFormVisible: false,
        rowId: ''
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      query('').then(response => {
        this.list = response.data.items
        if (this.list) {
          this.list.forEach(f => {
            f.hasChildren = !f.isLeaf
          })
        }
        setTimeout(() => {
          this.listLoading = false
        }, 0.2 * 1000)
      })
    },
    load(tree, treeNode, resolve) {
      query(tree.id).then(response => {
        var items = response.data.items
        if (items) {
          items.forEach(f => {
            f.hasChildren = !f.isLeaf
          })
          resolve(items)
        }
      })
    },
    handleFilter() {
      this.tableKey++
      this.getList()
    },
    handleCreate() {
      this.dialogStatus = 'create'
      this.temp = {}
    },
    handleCreateFromRow(row) {
      this.dialogStatus = 'create'
      this.temp = { parentId: row.id }
    },
    handleUpdate(row) {
      this.dialogStatus = 'update'
      this.temp = Object.assign({}, row) // copy obj
    },
    handleDelete(row, index) {
      var that = this
      this.$confirm(this.$t('operation.confirmDeleteMessage'), this.$t('operation.confirmDeleteTitle'), {
        confirmButtonText: this.$t('operation.confirmButtonText'),
        cancelButtonText: this.$t('operation.cancelButtonText'),
        type: 'warning'
      }).then(() => {
        deleteResource(row.id).then(() => {
          this.handleFilter()
          that.$notify({
            title: this.$t('operation.result.successTitle'),
            message: this.$t('operation.result.deleteSuccessTitle'),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    handleRefreshBackgroundApi() {
      refreshBackgroundApi().then(() => {
        this.$notify({
          title: this.$t('operation.result.successTitle'),
          message: this.$t('operation.successMsg'),
          type: 'success',
          duration: 2000
        })
      })
    },
    handleRemoveAllMemoryCache() {
      removeAllMemoryCache().then(() => {
        this.$notify({
          title: this.$t('operation.result.successTitle'),
          message: this.$t('operation.successMsg'),
          type: 'success',
          duration: 2000
        })
      })
    }
  }
}
</script>
<style scoped>
.drawer_content .form_contet {
  flex: 1;
  overflow:scroll;
}
</style>

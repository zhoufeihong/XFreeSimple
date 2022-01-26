<template>
  <div v-loading.fullscreen.lock="saveLoading" element-loading-spinner="el-icon-loading" :element-loading-text="$t('operation.processing')" class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.code" :placeholder="$t('commonUse.编码')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.name" :placeholder="$t('commonUse.名称')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        {{ $t('table.search') }}
      </el-button>
      <el-button id="btn-add" v-permission="'system:role:create'" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        {{ $t('table.add') }}
      </el-button>
      <el-button icon="el-icon-question" class="filter-item" type="primary" @click.prevent.stop="guide">
        {{ $t('driverGuid.操作指南') }}
      </el-button>
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%;"
      @sort-change="sortChange"
    >
      <el-table-column :label="$t('table.id')" prop="id" type="index" align="center" width="80">
        <template slot-scope="scope">
          {{ (listQuery.pageIndex - 1) * listQuery.pageSize + scope.$index + 1 }}
        </template>
      </el-table-column>
      <el-table-column :label="$t('role.角色编码')" min-width="150" align="center" sortable="custom" prop="code">
        <template slot-scope="{row}">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('role.角色名称')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('role.描述')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.memo }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.status')" min-width="150" align="center">
        <template slot-scope="{row}">
          <el-switch v-model="row.status" :active-value="1" :inactive-value="2" @change="changeStatus(row)" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.creationTime')" min-width="150" align="center" sortable="custom" prop="creationTime">
        <template slot-scope="{row}">
          <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.actions')" align="center" min-width="230" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button v-permission="'system:role:update'" type="primary" @click="handleUpdate(row)">
            {{ $t('table.edit') }}
          </el-button>
          <el-button v-if="row.status!='deleted'" v-permission="'system:role:delete'" type="danger" @click="handleDelete(row,$index)">
            {{ $t('table.delete') }}
          </el-button>
          <el-button id="btn-permission" v-permission="'system:role:update'" type="primary" @click="rowId=row.id;grantDialogFormVisible=true">
            {{ $t('role.授权') }}
          </el-button>
          <el-button type="primary" @click="showUsers(row)">
            {{ $t('role.用户列表') }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageIndex" :limit.sync="listQuery.pageSize" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="120px" style="">
        <el-row>
          <el-col :span="24">
            <el-row>
              <el-col :span="12">
                <el-form-item :label="$t('commonUse.编码')" prop="code">
                  <el-input v-model.trim="temp.code" :placeholder="$t('commonUse.不输入自动生成')" :readonly="dialogStatus!='create'" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item :label="$t('commonUse.名称')" prop="name">
                  <el-input v-model.trim="temp.name" :placeholder="$t('role.请输入名称')" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-col :span="8">
              <el-form-item :label="$t('role.访问类型')">
                <el-select v-model="temp.roleAccessType">
                  <el-option
                    v-for="item in $dict.RoleAccessType"
                    :key="item.id"
                    :label="$dict.getNameById($dict.RoleAccessType, item.id)"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item v-if="temp.roleAccessType===3" :label="$t('role.权限编码')">
                <el-input v-model.trim="temp.accessValue" :placeholder="$t('role.请输入权限编码')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('role.描述')">
                <el-input v-model.trim="temp.memo" type="textarea" :rows="4" :placeholder="$t('role.请输入描述')" />
              </el-form-item>
            </el-col>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t('table.cancel') }}
        </el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">
          {{ $t('table.confirm') }}
        </el-button>
      </div>
    </el-dialog>
    <grant-role-permission :row-id="rowId" :dialog-form-visible.sync="grantDialogFormVisible" />
    <user-list-dialog :dialog-visible.sync="userDialogVisible" :user-list="userList" />
  </div>
</template>

<script>
import Driver from 'driver.js' // import driver.js
import 'driver.js/dist/driver.min.css' // import driver.js css
import stepsEn from './steps-en'
import stepsZh from './steps-zh'
import { getListPaged, create, update, deleteResource, updateStatus, getUsers } from '@/api/system/role'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import dict from '@/config/dict'
import GrantRolePermission from './components/GrantRolePermission'
import UserListDialog from '../components/UserListDialog'
import { getLanguage } from '@/lang'

export default {
  name: 'Role',
  components: { Pagination, GrantRolePermission, UserListDialog },
  directives: { waves },
  filters: {
    sexFilter(sex) {
      return dict.getNameById(dict.sexType, sex)
    }
  },
  data() {
    return {
      saveLoading: false,
      userDialogVisible: false,
      userList: [],
      grantDialogFormVisible: false,
      rowId: '',
      tableKey: 0,
      list: [],
      total: 0,
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 20,
        code: '',
        name: '',
        sorting: ''
      },
      temp: {
        id: '',
        code: null,
        name: null,
        memo: null,
        status: 1,
        roleAccessType: 1,
        accessValue: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: this.$t('table.edit'),
        create: this.$t('table.add')
      },
      rules: {
        name: [{ required: true, message: this.$t('role.名称必填'), trigger: 'change' }]
      },
      downloadLoading: false
    }
  },
  created() {
    this.getList()
  },
  mounted() {
    this.driver = new Driver({
      doneBtnText: this.$t('driverGuid.完成'),
      closeBtnText: this.$t('driverGuid.关闭'),
      stageBackground: '#fff',
      nextBtnText: this.$t('driverGuid.下一步'),
      prevBtnText: this.$t('driverGuid.上一步') })
  },
  methods: {
    guide() {
      const language = getLanguage()
      if (language === 'en') {
        this.driver.defineSteps(stepsEn)
      } else {
        this.driver.defineSteps(stepsZh)
      }
      this.driver.start()
    },
    getList() {
      this.listLoading = true
      getListPaged(this.listQuery).then(response => {
        this.list = response.data.data
        this.total = response.data.total
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false
        }, 0.2 * 1000)
      })
    },
    handleFilter() {
      this.getList()
    },
    sortChange(data) {
      const { prop, order } = data
      if (prop) {
        this.sortByProp(prop, order)
      }
    },
    sortByProp(prop, order) {
      if (order === 'ascending') {
        this.listQuery.sorting = `${prop} asc`
      } else if (order) {
        this.listQuery.sorting = `${prop} desc`
      } else {
        this.listQuery.sorting = ''
      }
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        id: '',
        code: null,
        name: null,
        memo: null,
        status: 1,
        roleAccessType: 1,
        accessValue: ''
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          this.saveLoading = true
          create(this.temp).then(() => {
            this.list.unshift(this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$t('operation.result.successTitle'),
              message: this.$t('operation.result.addSuccessTitle'),
              type: 'success',
              duration: 2000
            })
            this.getList()
          }).finally(() => {
            this.saveLoading = false
          })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          this.saveLoading = true
          update(tempData.id, tempData).then(() => {
            const index = this.list.findIndex(v => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$t('operation.result.successTitle'),
              message: this.$t('operation.result.editSuccessTitle'),
              type: 'success',
              duration: 2000
            })
            this.getList()
          }).finally(() => {
            this.saveLoading = false
          })
        }
      })
    },
    changeStatus(row) {
      this.saveLoading = true
      updateStatus(row.id, { status: row.status }).then(() => {
        this.$notify({
          title: this.$t('operation.result.successTitle'),
          message: this.$t('operation.result.updateStatusSuccessTitle'),
          type: 'success',
          duration: 2000
        })
      }).finally(() => {
        this.saveLoading = false
      })
    },
    handleDelete(row, index) {
      var that = this
      this.$confirm(this.$t('operation.confirmDeleteMessage'), this.$t('operation.confirmDeleteTitle'), {
        confirmButtonText: this.$t('operation.confirmButtonText'),
        cancelButtonText: this.$t('operation.cancelButtonText'),
        type: 'warning'
      }).then(() => {
        this.saveLoading = true
        deleteResource(row.id).then(() => {
          const index = that.list.findIndex(v => v.id === row.id)
          that.list.splice(index, 1)
          that.dialogFormVisible = false
          that.$notify({
            title: this.$t('operation.result.successTitle'),
            message: this.$t('operation.result.deleteSuccessTitle'),
            type: 'success',
            duration: 2000
          })
        }).finally(() => {
          this.saveLoading = false
        })
      })
    },
    handleDownload() {
      this.downloadLoading = true
      this.downloadLoading = false
    },
    showUsers(row) {
      this.userDialogVisible = true
      getUsers(row.id).then(response => {
        this.userList = response.data
      })
    }
  }
}
</script>

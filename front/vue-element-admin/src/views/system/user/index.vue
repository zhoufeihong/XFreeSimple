<template>
  <div v-loading.fullscreen.lock="saveLoading" element-loading-spinner="el-icon-loading" :element-loading-text="$t('operation.processing')" class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.nickname" :placeholder="$t('user.姓名')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.loginName" :placeholder="$t('user.账号')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.employeeIDNumber" :placeholder="$t('commonUse.工号')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        {{ $t('table.search') }}
      </el-button>
      <el-button v-permission="'system:user:create'" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        {{ $t('table.add') }}
      </el-button>
      <el-button v-permission="'system:user:export'" v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
        {{ $t('table.export') }}
      </el-button>
      <el-button v-permission="'system:user:import'" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="showUpload=true">
        {{ $t('table.import') }}
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
      <el-table-column :label="$t('user.账号')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.loginName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('user.姓名')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.nickname }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('commonUse.工号')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.employeeIDNumber }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('user.性别')" min-width="150" align="center">
        <template slot-scope="{row}">
          <el-tag>
            {{ row.sex | sexFilter }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('user.部门')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.departName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('user.职务')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.postName }}</span>
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
      <el-table-column :label="$t('table.actions')" align="center" min-width="280" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button v-permission="'system:user:update'" type="primary" size="mini" @click="handleUpdate(row)">
            {{ $t('table.edit') }}
          </el-button>
          <el-button v-if="row.status!='deleted'" v-permission="'system:user:delete'" size="mini" type="danger" @click="handleDelete(row,$index)">
            {{ $t('table.delete') }}
          </el-button>
          <el-button id="btn-resetPassword" v-permission="'system:user:resetPassword'" size="mini" type="danger" @click="handleResetPassword(row,$index)">
            {{ $t('user.重置密码') }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageIndex" :limit.sync="listQuery.pageSize" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" width="70%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="145px" style="">
        <el-row>
          <el-col :span="24">
            <el-row>
              <el-col :span="8">
                <el-form-item :label="$t('user.账号')" prop="loginName">
                  <el-input v-model.trim="temp.loginName" :placeholder="$t('user.请输入账号')" :readonly="dialogStatus!='create'" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item :label="$t('commonUse.工号')" prop="employeeIDNumber">
                  <el-input v-model.trim="temp.employeeIDNumber" :placeholder="$t('user.请输入工号')" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item :label="$t('user.姓名')" prop="nickname">
                  <el-input v-model.trim="temp.nickname" :placeholder="$t('user.请输入姓名')" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row v-if="dialogStatus=='create'">
              <el-col :span="8">
                <el-form-item :label="$t('user.密码')" prop="password">
                  <el-input v-model.trim="temp.password" show-password />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item :label="$t('user.确认密码')" prop="password1">
                  <el-input v-model.trim="temp.password1" show-password />
                </el-form-item>
              </el-col>
              <el-tag>{{ $t('user.不修改默认密码为') }}:123456</el-tag>
            </el-row>
            <el-form-item :label="$t('user.性别')">
              <el-select v-model="temp.sex">
                <el-option
                  v-for="item in $dict.sexType"
                  :key="item.id"
                  :label="$dict.getNameById($dict.sexType, item.id)"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
            <el-form-item :label="$t('user.生日')">
              <el-date-picker v-model="temp.birthday" type="date" :placeholder="$t('user.请选择生日')" value-format="yyyy-MM-dd HH:mm:ss" :format="`yyyy-MM-dd`" />
            </el-form-item>
            <el-col :span="8">
              <el-form-item :label="$t('user.手机号')">
                <el-input v-model.trim="temp.phone" :placeholder="$t('user.请输入手机号')" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item :label="$t('user.账号类型')">
                <el-select v-model="temp.userType" disabled>
                  <el-option
                    v-for="item in $dict.userType"
                    :key="item.id"
                    :label="$dict.getNameById($dict.userType, item.id)"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item :label="$t('user.职务')">
                <post-select v-model="temp.postId" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('user.部门')" prop="departId">
                <depart-select v-model="temp.departId" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('user.角色')">
                <role-select v-model="temp.roleIds" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('user.描述')">
                <el-input v-model.trim="temp.memo" type="textarea" :rows="4" :placeholder="$t('user.请输入描述')" />
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
    <upload-excel :show-dialog.sync="showUpload" action-url="api/systemManagement/user/import" :download-template-request="downLoadTemplate" />
  </div>
</template>

<script>
import { getListPaged, create, update, deleteResource, updateStatus, exportResource, downLoadTemplate, resetPassword } from '@/api/system/user'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import dict from '@/config/dict'
import { toExcelfile } from '@/utils/excel-helper'
import PostSelect from '../components/PostSelect'
import DepartSelect from '@/components/DepartSelect'
import RoleSelect from '@/components/RoleSelect'
import UploadExcel from '@/components/UploadExcel'

export default {
  name: 'User',
  components: { Pagination, PostSelect, DepartSelect, RoleSelect, UploadExcel },
  directives: { waves },
  filters: {
    sexFilter(sex) {
      return dict.getNameById(dict.sexType, sex)
    }
  },
  data() {
    return {
      saveLoading: false,
      tableKey: 0,
      list: [],
      total: 0,
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 20,
        nickname: '',
        loginName: '',
        employeeIDNumber: '',
        sorting: ''
      },
      temp: {
        id: '',
        departId: null,
        postId: null,
        loginName: null,
        nickname: null,
        password: null,
        password1: null,
        employeeIDNumber: null,
        birthday: null,
        sex: 1,
        userType: 2,
        email: '',
        phone: '',
        memo: '',
        supperUser: false,
        status: 1,
        roleIds: []
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: this.$t('table.edit'),
        create: this.$t('table.add')
      },
      rules: {
        loginName: [{ required: true, message: this.$t('user.账号必填'), trigger: 'change' }],
        nickname: [{ required: true, message: this.$t('user.姓名必填'), trigger: 'change' }],
        employeeIDNumber: [{ required: true, message: this.$t('user.工号必填'), trigger: 'change' }]
      },
      downloadLoading: false,
      downLoadTemplate: downLoadTemplate,
      showUpload: false
    }
  },
  created() {
    this.getList()
  },
  methods: {
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
        departId: null,
        postId: null,
        loginName: null,
        nickname: null,
        password: '123456',
        password1: '123456',
        employeeIDNumber: null,
        birthday: null,
        sex: 1,
        userType: 1,
        email: '',
        phone: '',
        memo: '',
        supperUser: false,
        status: 1,
        roleIds: []
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
    checkForm() {
      console.log(this.temp)
      if ((!this.temp.departId || this.temp.departId === '') && !this.temp.supperUser) {
        this.$notify({
          title: this.$t('operation.result.failedTitle'),
          message: this.$t('user.请选择部门'),
          type: 'error',
          duration: 2000
        })
        return false
      }
      return true
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          if (this.temp.password !== this.temp.password1) {
            this.$notify({
              title: this.$t('operation.result.failedTitle'),
              message: this.$t('user.确认密码不一致'),
              type: 'error',
              duration: 2000
            })
            return
          }
          if (!this.temp.password || this.temp.password.length < 6) {
            this.$notify({
              title: this.$t('operation.result.failedTitle'),
              message: this.$t('user.密码不能小于6位'),
              type: 'error',
              duration: 2000
            })
            return
          }
          if (!this.checkForm()) {
            return
          }
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
      if (!this.checkForm()) {
        return
      }
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
      exportResource(this.listQuery).then(response => {
        toExcelfile(response)
        this.downloadLoading = false
      })
    },
    handleResetPassword(row, index) {
      var that = this
      this.$confirm(`${that.$t('user.确认重置用户密码')}?`, that.$t('operation.confirmDeleteTitle'), {
        confirmButtonText: that.$t('operation.confirmButtonText'),
        cancelButtonText: that.$t('operation.cancelButtonText'),
        type: 'warning'
      }).then(() => {
        this.saveLoading = true
        resetPassword(row.id).then(() => {
          that.dialogFormVisible = false
          that.$notify({
            title: that.$t('operation.result.successTitle'),
            message: that.$t('user.重置成功，重置后密码为123456'),
            type: 'success',
            duration: 2000
          })
        }).finally(() => {
          this.saveLoading = false
        })
      })
    }
  }
}
</script>

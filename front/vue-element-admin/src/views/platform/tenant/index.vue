<template>
  <div v-loading.fullscreen.lock="saveLoading" element-loading-spinner="el-icon-loading" :element-loading-text="$t('operation.processing')" class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.code" :placeholder="$t('commonUse.编码')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.name" :placeholder="$t('commonUse.名称')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        {{ $t('table.search') }}
      </el-button>
      <el-button v-permission="'system:post:create'" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        {{ $t('table.add') }}
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
      :default-sort="{prop: 'sortOrder', order: 'descending'}"
      @sort-change="sortChange"
    >
      <el-table-column :label="$t('table.id')" prop="id" type="index" align="center" width="80">
        <template slot-scope="scope">
          {{ (listQuery.pageIndex - 1) * listQuery.pageSize + scope.$index + 1 }}
        </template>
      </el-table-column>
      <el-table-column :label="$t('commonUse.编码')" min-width="120" align="center" sortable="custom" prop="code">
        <template slot-scope="{row}">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('commonUse.名称')" min-width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('tenant.语言')" min-width="100" align="center">
        <template slot-scope="{row}">
          <span>{{ row.language | languageFilter }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('tenant.描述')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.memo }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('tenant.手机号')" min-width="120" align="center" sortable="custom" prop="phone">
        <template slot-scope="{row}">
          <span>{{ row.phone }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('tenant.邮箱')" min-width="120" align="center" sortable="custom" prop="email">
        <template slot-scope="{row}">
          <span>{{ row.email }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('tenant.采用独立数据库')" min-width="80" align="center" sortable="custom" prop="isStandaloneDatabase">
        <template slot-scope="{row}">
          <span>{{ row.isStandaloneDatabase | isStandaloneDatabaseFilter }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('tenant.数据库连接配置名称')" min-width="100" align="center" sortable="custom" prop="defaultConnectionStringName">
        <template slot-scope="{row}">
          <span v-if="!row.isStandaloneDatabase">{{ row.defaultConnectionStringName }}</span>
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
      <el-table-column :label="$t('table.actions')" align="center" min-width="180" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button v-permission="'system:post:update'" type="primary" size="mini" @click="handleUpdate(row)">
            {{ $t('table.edit') }}
          </el-button>
          <el-button v-if="row.status!='deleted'" v-permission="'system:post:delete'" size="mini" type="danger" @click="handleDelete(row,$index)">
            {{ $t('table.delete') }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageIndex" :limit.sync="listQuery.pageSize" @pagination="getList" />

    <el-dialog :key="dialogKey" :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" width="65%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="200px" style="">
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
                  <el-input v-model.trim="temp.name" :placeholder="$t('tenant.请输入名称')" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-col :span="12">
              <el-form-item :label="$t('tenant.手机号')" prop="phone">
                <el-input v-model.trim="temp.phone" :placeholder="$t('tenant.请输入手机号')" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item :label="$t('tenant.邮箱')" prop="email">
                <el-input v-model.trim="temp.email" :placeholder="$t('tenant.请输入邮箱')" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item :label="$t('tenant.采用独立数据库')">
                <el-select v-model="temp.isStandaloneDatabase">
                  <el-option
                    label="No"
                    :value="false"
                  />
                  <el-option
                    label="Yes"
                    :value="true"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col v-if="!temp.isStandaloneDatabase" :span="12">
              <el-form-item :label="$t('tenant.数据库连接配置名称')" prop="defaultConnectionStringName">
                <database-connection-select v-model="temp.defaultConnectionStringName" :tenant-id="temp.id" style="width:100%" />
              </el-form-item>
            </el-col>
            <el-col v-if="temp.isStandaloneDatabase" :span="24">
              <el-form-item :label="$t('tenant.独立的数据库连接字符串')" prop="standaloneDatabaseConnectionString">
                <el-input v-model.trim="temp.standaloneDatabaseConnectionString" :placeholder="$t('tenant.请输入独立的数据库连接字符串')" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item :label="$t('tenant.语言')" prop="language">
                <el-select v-model="temp.language">
                  <el-option
                    v-for="item in $dict.Language"
                    :key="item.id"
                    :label="$dict.getNameById($dict.Language, item.id)"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('tenant.描述')">
                <el-input v-model.trim="temp.memo" type="textarea" :rows="4" :placeholder="$t('tenant.请输入描述')" />
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
  </div>
</template>

<script>
import { getListPaged, create, update, deleteResource, updateStatus } from '@/api/platform/tenant'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import DatabaseConnectionSelect from '../components/DatabaseConnectionSelect'
import dict from '@/config/dict'

const defaultSort = 'creationTime desc'
const getTemp = () => {
  return {
    id: '',
    code: null,
    name: null,
    memo: null,
    status: 1,
    isStandaloneDatabase: false,
    defaultConnectionStringName: '',
    standaloneDatabaseConnectionString: '',
    language: ''
  }
}
export default {
  name: 'Tenant',
  components: { Pagination, DatabaseConnectionSelect },
  directives: { waves },
  filters: {
    isStandaloneDatabaseFilter(isStandaloneDatabase) {
      return isStandaloneDatabase ? 'Yes' : 'No'
    },
    languageFilter(language) {
      return dict.getNameById(dict.Language, language)
    }
  },
  data() {
    return {
      tableKey: 0,
      saveLoading: false,
      dialogKey: 100000,
      list: [],
      total: 0,
      listLoading: true,
      listQuery: {
        pageIndex: 1,
        pageSize: 20,
        code: '',
        name: '',
        sorting: defaultSort
      },
      temp: getTemp(),
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: this.$t('table.edit'),
        create: this.$t('table.add')
      },
      rules: {
        name: [{ required: true, message: this.$t('tenant.名称必填'), trigger: 'change' }],
        email: [{ type: 'email', message: this.$t('tenant.请输入正确的邮箱地址'), trigger: ['blur', 'change'] }],
        language: [{ required: true, message: this.$t('tenant.语言必选'), trigger: 'change' }]
      },
      downloadLoading: false
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
        this.listQuery.sorting = defaultSort
      }
      this.handleFilter()
    },
    resetTemp() {
      this.temp = getTemp()
    },
    handleCreate() {
      this.dialogKey++
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
      this.dialogKey++
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
      this.saveLoading = true
      var that = this
      this.$confirm(this.$t('operation.confirmDeleteMessage'), this.$t('operation.confirmDeleteTitle'), {
        confirmButtonText: this.$t('operation.confirmButtonText'),
        cancelButtonText: this.$t('operation.cancelButtonText'),
        type: 'warning'
      }).then(() => {
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
    }
  }
}
</script>

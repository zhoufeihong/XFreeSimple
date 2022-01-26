<template>
  <div v-loading.fullscreen.lock="saveLoading" element-loading-spinner="el-icon-loading" :element-loading-text="$t('operation.processing')" class="app-container">
    <div class="filter-container">
      <dict-select v-model="listQuery.dictId" class="filter-item" />
      <el-input v-model="listQuery.itemValue" :placeholder="$t('commonUse.编码')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        {{ $t('table.search') }}
      </el-button>
      <el-button v-permission="'system:dict:update'" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
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
      <el-table-column :label="$t('commonUse.编码')" min-width="150" align="center" sortable="custom" prop="itemValue">
        <template slot-scope="{row}">
          <span>{{ row.itemValue }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('commonUse.名称')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.itemText }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('commonUse.英文名称')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.itemEnText }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('dict.描述')" min-width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('dict.排序')" min-width="150" align="center" sortable="custom" prop="sortOrder">
        <template slot-scope="{row}">
          <span>{{ row.sortOrder }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.status')" min-width="150" align="center">
        <template slot-scope="{row}">
          <el-switch v-model="row.enabled" :active-value="true" :inactive-value="false" @change="changeStatus(row)" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.creationTime')" min-width="150" align="center" sortable="custom" prop="creationTime">
        <template slot-scope="{row}">
          <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.actions')" align="center" min-width="230" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button v-permission="'system:dict:update'" type="primary" size="mini" @click="handleUpdate(row)">
            {{ $t('table.edit') }}
          </el-button>
          <el-button v-if="row.status!='deleted'" v-permission="'system:dict:update'" size="mini" type="danger" @click="handleDelete(row,$index)">
            {{ $t('table.delete') }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageIndex" :limit.sync="listQuery.pageSize" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" width="50%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="120px">
        <el-row>
          <el-col :span="24">
            <el-row>
              <el-col :span="8">
                <el-form-item v-if="dialogStatus==='create'" :label=" $t('dict.父级字典')" prop="dictId">
                  <dict-select v-model="temp.dictId" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item :label=" $t('commonUse.编码')" prop="itemValue">
                  <el-input v-model.trim="temp.itemValue" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item :label=" $t('commonUse.名称')" prop="itemText">
                  <el-input v-model.trim="temp.itemText" :placeholder=" $t('dict.请输入名称')" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item :label=" $t('commonUse.英文名称')" prop="itemText">
                  <el-input v-model.trim="temp.itemEnText" :placeholder=" $t('dict.请输入名称')" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-col :span="24">
              <el-form-item :label=" $t('dict.描述')">
                <el-input v-model.trim="temp.description" type="textarea" :rows="4" :placeholder=" $t('dict.请输入描述')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label=" $t('dict.排序')">
                <el-input-number v-model="temp.sortOrder" />
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
import { getListPaged, create, update, deleteResource, updateStatus } from '@/api/system/dictItem'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import DictSelect from './DictSelect'

const defaultSort = 'itemText desc'

export default {
  name: 'DictItem',
  components: { Pagination, DictSelect },
  directives: { waves },
  data() {
    return {
      saveLoading: false,
      tableKey: 0,
      list: [],
      total: 0,
      listLoading: true,
      listQuery: {
        itemValue: '',
        dictId: '',
        pageIndex: 1,
        pageSize: 20,
        sorting: defaultSort
      },
      temp: {
        id: '',
        dictId: '',
        itemValue: null,
        itemText: null,
        itemEnText: null,
        description: null,
        sortOrder: 0
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: this.$t('table.edit'),
        create: this.$t('table.add')
      },
      rules: {
        itemValue: [{ required: true, message: this.$t('dict.必填'), trigger: 'change' }],
        itemText: [{ required: true, message: this.$t('dict.必填'), trigger: 'change' }]
      },
      downloadLoading: false
    }
  },
  created() {
    this.listQuery.dictId = this.$route.query.dictId
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
      this.temp = {
        id: '',
        dictId: this.listQuery.dictId,
        itemValue: null,
        itemText: null,
        itemEnText: null,
        description: null,
        sortOrder: 0
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
              message: this.$t('operation.result.updateStatusSuccessTitle'),
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
      updateStatus(row.id, { enabled: row.enabled }).then(() => {
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
    }
  }
}
</script>

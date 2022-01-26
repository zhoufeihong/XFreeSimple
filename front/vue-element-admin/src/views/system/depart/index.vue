<template>
  <div v-loading.fullscreen.lock="saveLoading" element-loading-spinner="el-icon-loading" :element-loading-text="$t('operation.processing')" class="app-container">
    <el-col :span="10">
      <el-card>
        <div>
          <el-row style="margin-left: 14px;padding-bottom: 10px;">
            <el-button v-permission="'system:depart:create'" type="primary" @click="handleCreate(0)">{{ $t('depart.添加部门') }}</el-button>
            <el-button v-permission="'system:depart:addSub'" type="primary" @click="handleCreate(1)">{{ $t('depart.添加下级') }}</el-button>
            <el-button v-permission="'system:depart:delete'" type="danger" @click="handleDelete()">{{ $t('table.delete') }}</el-button>
          <!--<a-button @click="refresh" type="default" icon="reload" :loading="loading">刷新</a-button>-->
          </el-row>
          <depart-tree ref="departTree" @node-click="treeNodeClick" />
        </div>
      </el-card>
    </el-col>
    <el-col :span="14">
      <el-card>
        <el-tabs active-name="first">
          <el-tab-pane :label="$t('depart.部门基础信息')" name="first">
            <depart-edit :depart-id="selectedDepartId" @success="refreshTree" />
          </el-tab-pane>
          <el-tab-pane :label="$t('depart.用户信息')" name="two">
            <div v-permission="'system:depart:queryUser'">
              <depart-user :depart-id="selectedDepartId" />
            </div>
          </el-tab-pane>
        </el-tabs>
      </el-card>
    </el-col>
    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="180px" width="70%">
        <el-row>
          <el-col :span="24">
            <el-row>
              <el-col :span="24">
                <el-form-item :label="$t('depart.上级部门')" label-width="180px">
                  <el-input :value="temp.orgLevelType === 1 ? '' : selectedParentOrgName" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item :label="$t('commonUse.编码')" prop="orgCode">
                  <el-input v-model.trim="temp.orgCode" :placeholder="$t('commonUse.不输入自动生成')" :readonly="dialogStatus!='create'" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item :label="$t('commonUse.名称')" prop="orgName">
                  <el-input v-model.trim="temp.orgName" :placeholder="$t('depart.请输入名称')" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-col :span="24">
              <el-form-item :label="$t('depart.排序')" prop="sortOrder">
                <el-input-number v-model.trim="temp.sortOrder" :placeholder="$t('depart.请输入排序')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.联系方式')" prop="contact">
                <el-input v-model.trim="temp.contact" :placeholder="$t('depart.请输入联系方式')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.类型')" prop="orgType">
                <dict-item-select v-model="temp.orgType" dict-code="Inside_SystemMangement_OrgType" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.地址')" prop="address">
                <el-input v-model.trim="temp.address" :placeholder="$t('depart.请输入地址')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.描述')">
                <el-input v-model.trim="temp.memo" type="textarea" :rows="4" :placeholder="$t('depart.请输入描述')" />
              </el-form-item>
            </el-col>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t('table.cancel') }}
        </el-button>
        <el-button type="primary" @click="createData()">
          {{ $t('table.confirm') }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { create, deleteResource } from '@/api/system/depart'
import waves from '@/directive/waves' // waves directive
import DepartTree from './components/DepartTree'
import DepartEdit from './components/DepartEdit'
import DepartUser from './components/DepartUser'
import DictItemSelect from '@/components/DictItemSelect'
import { mapGetters } from 'vuex'

export default {
  name: 'Depart',
  components: { DepartTree, DictItemSelect, DepartEdit, DepartUser },
  directives: { waves },
  data() {
    return {
      saveLoading: false,
      tableKey: 0,
      listLoading: true,
      selectedParentOrgName: '',
      selectedDepartId: '',
      temp: {
        id: '',
        parentId: '',
        orgName: '',
        orgNameEn: '',
        orgCategory: 1,
        orgLevelType: 1,
        orgType: 'Structure',
        orgCode: '',
        contact: '',
        memo: '',
        status: 1,
        sortOrder: 0,
        address: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: this.$t('table.edit'),
        create: this.$t('table.add')
      },
      rules: {
        orgName: [{ required: true, message: this.$t('depart.名称必填'), trigger: 'change' }]
      },
      downloadLoading: false
    }
  },
  computed: {
    ...mapGetters([
      'supperUser'
    ])
  },
  created() {
  },
  methods: {
    resetTemp() {
      this.temp = {
        id: '',
        parentId: '',
        orgName: '',
        orgNameEn: '',
        orgCategory: 1,
        orgLevelType: 1,
        orgType: 'Structure',
        orgCode: '',
        contact: '',
        memo: '',
        status: 1,
        sortOrder: 0,
        address: ''
      }
    },
    handleCreate(level) {
      this.resetTemp()
      this.temp.orgLevelType = 1
      if (level === 1) {
        if (!this.selectedDepartId) {
          this.$notify({
            title: this.$t('operation.result.failedTitle'),
            message: this.$t('depart.请先选择部门'),
            type: 'warning',
            duration: 2000
          })
          return
        }
        // 设置父节点
        this.temp.orgLevelType = 2
        this.temp.parentId = this.selectedDepartId
      }
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    refreshTree() {
      this.$refs.departTree.refresh()
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          this.saveLoading = true
          create(this.temp).then(() => {
            this.dialogFormVisible = false
            this.$notify({
              title: this.$t('operation.result.successTitle'),
              message: this.$t('operation.result.addSuccessTitle'),
              type: 'success',
              duration: 2000
            })
            this.refreshTree()
          }).finally(() => {
            this.saveLoading = false
          })
        }
      })
    },
    handleDelete() {
      var that = this
      if (!that.selectedDepartId) {
        this.$notify({
          title: this.$t('operation.result.failedTitle'),
          message: this.$t('depart.请先选择部门'),
          type: 'error',
          duration: 2000
        })
      }
      this.$confirm(this.$t('operation.confirmDeleteMessage'), this.$t('operation.confirmDeleteTitle'), {
        confirmButtonText: this.$t('operation.confirmButtonText'),
        cancelButtonText: this.$t('operation.cancelButtonText'),
        type: 'warning'
      }).then(() => {
        this.saveLoading = true
        deleteResource(that.selectedDepartId).then(() => {
          that.selectedDepartId = null
          that.refreshTree()
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
    treeNodeClick(row) {
      this.selectedParentOrgName = row.label
      this.selectedDepartId = row.value
    }
  }
}
</script>

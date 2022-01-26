<template>
  <div>
    <el-card v-if="tempUpdate" :bordered="false">
      <el-form ref="dataUpdateForm" :rules="rules" :model="tempUpdate" label-position="right" label-width="120px" style="">
        <el-row>
          <el-col :span="24">
            <el-row>
              <el-col :span="8">
                <el-form-item :label="$t('commonUse.编码')" prop="orgCode">
                  <el-input v-model.trim="tempUpdate.orgCode" :placeholder="$t('commonUse.不输入自动生成')" :readonly="true" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item :label="$t('commonUse.名称')" prop="orgName">
                  <el-input v-model.trim="tempUpdate.orgName" :placeholder="$t('depart.请输入名称')" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-col :span="12">
              <el-form-item :label="$t('depart.排序')" prop="sortOrder">
                <el-input-number v-model.trim="tempUpdate.sortOrder" :placeholder="$t('depart.请输入排序')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.联系方式')" prop="contact">
                <el-input v-model.trim="tempUpdate.contact" :placeholder="$t('depart.请输入联系方式')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('table.status')" prop="status">
                <el-switch v-model="tempUpdate.status" :active-value="1" :inactive-value="2" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.类型')" prop="orgType">
                <dict-item-select v-model="tempUpdate.orgType" dict-code="Inside_SystemMangement_OrgType" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.地址')" prop="address">
                <el-input v-model.trim="tempUpdate.address" :placeholder="$t('depart.请输入地址')" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item :label="$t('depart.描述')">
                <el-input v-model.trim="tempUpdate.memo" type="textarea" :rows="4" :placeholder="$t('depart.请输入描述')" />
              </el-form-item>
            </el-col>
          </el-col>
        </el-row>
      </el-form>
      <div class="dialog-footer">
        <el-button v-permission="'system:depart:update'" type="primary" :disabled="excution" @click="updateData()">
          {{ $t('commonUse.保存') }}
        </el-button>
      </div>
    </el-card>
  </div>
</template>
<script>
import { get, update } from '@/api/system/depart'
import DictItemSelect from '@/components/DictItemSelect'

export default {
  name: 'DepartEdit',
  components: { DictItemSelect },
  props: {
    departId: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      tempUpdate: null,
      excution: false,
      rules: {
        orgName: [{ required: true, message: this.$t('depart.名称必填'), trigger: 'change' }]
      }
    }
  },
  watch: {
    departId(val) {
      this.loadDepart()
    }
  },
  created() {

  },
  methods: {
    loadDepart() {
      if (this.departId) {
        get(this.departId).then(res => {
          this.tempUpdate = res.data
        })
      }
    },
    updateData() {
      this.$refs['dataUpdateForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.tempUpdate)
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
      this.$emit('success', this.tempData)
    } }
}
</script>

<template>
  <div>
    <el-dialog
      v-loading="loading"
      :visible.sync="showUpload"
      :title="$t('uploadExcel.上传')"
      width="40%"
      :element-loading-text="$t('uploadExcel.上传中')"
    >
      <el-upload
        ref="upload"
        :headers="uploadHeaders"
        :action="`${baseUrl}${actionUrl}`"
        :multiple="false"
        :limit="1"
        :on-success="onUploadSuccess"
        :on-error="onUploadError"
        list-type="xls/xlsx"
        :auto-upload="false"
        :on-exceed="onExceed"
      >
        <el-button
          slot="trigger"
          size="small"
          type="primary"
        > {{ $t('uploadExcel.选取文件') }}</el-button>
        <el-button
          style="margin-left: 10px"
          size="small"
          type="success"
          @click="submitUpload"
        >{{ $t('uploadExcel.上传') }}</el-button>
        <el-button
          style="margin-left: 10px"
          size="small"
          type="success"
          @click="downLoadTemplate"
        >{{ $t('uploadExcel.下载模板') }}</el-button>

        <div slot="tip" class="el-upload__tip">
          {{ $t('uploadExcel.只能上传Excel文件，模板可以通过导出下载') }}.
        </div>
      </el-upload>
      <br>
      <el-alert v-if="showResult" :title="$t('uploadExcel.结果')" :type="uploadResult.failedCount === 0 ? 'success':'warning'" :description="resultDescription" show-icon />
      <div v-if="showResult && uploadResult.itemErrors && uploadResult.itemErrors.length > 0">
        <el-divider content-position="left">{{ $t('uploadExcel.错误信息列表') }}:</el-divider>
        <el-table
          :data="uploadResult.itemErrors"
          border
          fit
          highlight-current-row
          style="width: 100%;"
        >
          <el-table-column :label="$t('table.id')" prop="id" type="index" align="center" width="80">
            <template slot-scope="scope">
              {{ scope.$index + 1 }}
            </template>
          </el-table-column>
          <el-table-column :label="errorKeyName" align="center">
            <template slot-scope="{row}">
              <span>{{ row.key }}</span>
            </template>
          </el-table-column>
          <el-table-column :label="$t('uploadExcel.错误信息')" align="center" min-width="180">
            <template slot-scope="{row}">
              {{ row.message }}
            </template>
          </el-table-column>
        </el-table>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { toExcelfile } from '@/utils/excel-helper'
import baseUrl from '@/api/service-config'
import store from '@/store'
import { getRequestLanguage } from '@/lang'

export default {
  props: {
    downloadTemplateRequest: {
      type: Function,
      default: () => { }
    },
    actionUrl: {
      type: String,
      default: ''
    },
    showDialog: {
      type: Boolean,
      default: false
    },
    errorKeyName: {
      type: String,
      default: 'Key'
    }
  },
  data() {
    return {
      loading: false,
      downloadLoading: false,
      baseUrl: baseUrl,
      fileName: '',
      showResult: false,
      uploadResult: {
        successfulCount: 0,
        failedCount: 0,
        createdCount: 0,
        updatedCount: 0,
        itemErrors: []
      }
    }
  },
  computed: {
    uploadHeaders() {
      return {
        'Authorization': `Bearer ${store.getters.token}`,
        'Accept-Language': getRequestLanguage()
      }
    },
    showUpload: {
      get() {
        return this.showDialog
      },
      set(val) {
        this.$emit('update:showDialog', val)
      }
    },
    resultDescription() {
      return `${this.$t('uploadExcel.选取文件')}: ${this.fileName} ${this.$t('uploadExcel.导入成功数量')}:${this.uploadResult.successfulCount} ${this.$t('uploadExcel.导入失败数量')}:${this.uploadResult.failedCount}`
    }
  },
  methods: {
    resetResult() {
      this.showResult = false
    },
    submitUpload() {
      if (
        this.$refs.upload.uploadFiles === null ||
        this.$refs.upload.uploadFiles.length === 0
      ) {
        this.$message({
          showClose: true,
          message: this.$t('uploadExcel.请先选择文件'),
          type: 'error'
        })
        return
      }
      this.fileName = this.$refs.upload.uploadFiles[0].name
      this.loading = true
      this.resetResult()
      this.$refs.upload.submit()
    },
    onExceed(files, fileList) {
      this.$message({
        showClose: true,
        message: this.$t('uploadExcel.一次只能上传一个文件，请先移除选中文件再进行添加'),
        type: 'error'
      })
    },
    onUploadSuccess(response, file, fileList) {
      this.loading = false
      if (response.success) {
        this.$refs.upload.clearFiles()
        this.showResult = true
        this.uploadResult = response.data
      } else {
        this.$notify({
          title: this.$t('operation.result.failedTitle'),
          message: response.msg,
          type: 'error',
          duration: 5000
        })
        this.$refs.upload.clearFiles()
      }
    },
    onUploadError(err, file, fileList) {
      this.loading = false
      this.$notify({
        title: this.$t('operation.result.failedTitle'),
        message: err,
        type: 'error',
        duration: 5000
      })
    },
    downLoadTemplate() {
      this.downloadLoading = true
      this.downloadTemplateRequest().then(response => {
        toExcelfile(response)
      })
      this.downloadLoading = false
    }
  }
}
</script>

<template>
  <el-dialog :title="$t('user.用户列表')" :visible.sync="currentDialogVisible" width="80%">
    <el-table
      :data="userList"
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
          <el-switch v-model="row.status" :active-value="1" :inactive-value="2" disabled />
        </template>
      </el-table-column>
      <el-table-column :label="$t('table.creationTime')" min-width="150" align="center" sortable="custom" prop="creationTime">
        <template slot-scope="{row}">
          <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>
    </el-table>
  </el-dialog>
</template>
<script>
import dict from '@/config/dict'
export default {
  name: 'UserListDialog',
  filters: {
    sexFilter(sex) {
      return dict.getNameById(dict.sexType, sex)
    }
  },
  props: {
    dialogVisible: {
      type: Boolean,
      default: false
    },
    userList: {
      type: Array,
      default: () => []
    }
  },
  computed: {
    currentDialogVisible: {
      get() {
        return this.dialogVisible
      },
      set(val) {
        this.$emit('update:dialogVisible', val)
      }
    }
  }
}
</script>

<template>
  <div>
    <el-card v-if="departId" :bordered="false">
      <el-table
        :data="list"
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
      <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageIndex" :limit.sync="listQuery.pageSize" @pagination="getList" />
    </el-card>
  </div>
</template>
<script>
import { getUserListPaged } from '@/api/system/depart'
import Pagination from '@/components/Pagination'
import dict from '@/config/dict'

export default {
  name: 'DepartUser',
  components: { Pagination },
  filters: {
    sexFilter(sex) {
      return dict.getNameById(dict.sexType, sex)
    }
  },
  props: {
    departId: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      list: [],
      total: 0,
      listQuery: {
        pageIndex: 1,
        pageSize: 20,
        sorting: ''
      }
    }
  },
  watch: {
    departId(val) {
      this.resetListQuery()
      this.getList()
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      if (this.departId) {
        getUserListPaged(this.departId, this.listQuery).then(response => {
          this.list = response.data.data
          this.total = response.data.total
        })
      }
    },
    resetListQuery() {
      this.listQuery.pageIndex = 1
    }
  }
}
</script>

<template>
  <div>
    <div style="background: #fff;padding-left:16px;height: 100%; margin-top: 5px;">
      <el-tag style="margin-bottom: 3px">
        <span style="font-size:15px;">  {{ $t('depart.当前选择') }}： {{ currSelected.label }} </span>
      </el-tag>
      <!-- 树-->
      <el-col :span="24">
        <template>
          <span style="user-select: none">
            <div class="mytree">
              <el-tree
                ref="tree"
                :data="treeData"
                default-expand-all
                node-key="value"
                :check-on-click-node="false"
                :expand-on-click-node="false"
                highlight-current
                :props="defaultProps"
                :filter-node-method="filterNode"
                @check="handleChange"
                @node-click="nodeClick"
              />
            </div>
          </span>
        </template>
      </el-col>
    </div>
  </div>
</template>

<script>
import { getAll } from '@/api/system/depart'
import { mapGetters } from 'vuex'

export default {
  name: 'DepartTree',
  data() {
    return {
      treeData: [],
      defaultProps: {
        children: 'children',
        label: 'label'
      },
      list: [],
      currSelected: { label: '' },
      total: 0
    }
  },
  computed: {
    ...mapGetters([
      'supperUser',
      'departId'
    ])
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getAll().then(response => {
        this.list = response.data.items
        this.total = response.data.items.length
        this.treeData = this.transformationTree(this.list)
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false
        }, 0.2 * 1000)
      })
    },
    filterNode(value, treeData) {
      if (!value) return true
      return treeData.label.indexOf(value) !== -1
    },
    transformationTree(data) {
      const dataRoot = {
        isRoot: true,
        value: '-1',
        children: []
      }
      data.sort((d1, d2) => {
        return d1.sortOrder - d2.sortOrder
      })
      const findP = (dataOption) => {
        data.forEach(element => {
          if ((this.supperUser && dataOption.isRoot && element.orgLevelType === 1) ||
           (!this.supperUser && dataOption.isRoot && element.id === this.departId) ||
           element.parentId === dataOption.value) {
            const tempOption = {
              value: element.id,
              label: element.orgName,
              isRoot: false
            }
            findP(tempOption, element.id)
            if (!dataOption.children) {
              dataOption.children = []
            }
            dataOption.children.push(tempOption)
          }
        })
      }
      findP(dataRoot)
      return dataRoot.children
    },
    handleChange(data) {
      const val = this.$refs.tree.getCheckedKeys()
      if (val && val instanceof Array) {
        if (val.length > 0) {
          this.$emit('input', val)
          return
        }
      }
      this.$emit('input', [])
    },
    nodeClick(data) {
      this.currSelected.label = data.label
      this.$emit('node-click', data)
    },
    refresh() {
      this.getList()
    }
  }
}
</script>
<style scoped>
.el-tree /deep/ .el-tree-node .el-tree-node__content{
  height: 43px;
  background: #dee9fa;
  margin: 1px 0;
}
.el-tree /deep/ .is-current>.el-tree-node__content{
  background: #d1e2fc !important;
}

</style>
<style>
.el-tree-node__expand-icon {
  color: rgb(42, 108, 206) !important;
}
.el-tree-node__expand-icon.is-leaf {
  color: transparent !important;
}
.el-tree-node__content .el-icon-caret-right:before {
    font-size: 22px;
}
.el-tree-node__content .el-tree-node__expand-icon.expanded.el-icon-caret-right:before {
     font-size: 22px;
}
</style>

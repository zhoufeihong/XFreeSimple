<template>
  <div>
    <el-input v-if="showSearch" v-model="filterText" :placeholder="$t('commonUse.输入关键字进行过滤')" />
    <el-tree
      ref="tree"
      :data="treeData"
      show-checkbox
      default-expand-all
      check-on-click-node
      :expand-on-click-node="false"
      check-strictly
      node-key="value"
      highlight-current
      :props="defaultProps"
      :filter-node-method="filterNode"
      @check="handleChange"
      @current-change="currentChange"
    />
  </div>
</template>

<script>
import { getAll } from '@/api/system/ui-permission'

export default {
  name: 'UiPermissionTree',
  props: {
    value: {
      required: true,
      type: Array
    },
    showSearch: {
      required: false,
      default: true,
      type: Boolean
    }
  },
  data() {
    return {
      list: [],
      filterText: '',
      treeData: [],
      defaultProps: {
        children: 'children',
        label: 'label'
      }
    }
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val)
    },
    value(val) {
      this.$refs.tree.setCheckedKeys(val)
    }
  },
  created() {
    this.listPermissionResource()
  },
  methods: {
    filterNode(value, treeData) {
      if (!value) return true
      return treeData.label.indexOf(value) !== -1
    },
    listPermissionResource() {
      getAll().then(
        response => {
          this.list = response.data.items
          this.treeData = this.handleListPermissionResource(this.list)
          if (this.value) {
            this.$refs.tree.setCheckedKeys(this.value)
          }
        }
      )
    },
    handleListPermissionResource(data) {
      const dataRoot = {
        isRoot: true,
        value: '',
        children: []
      }
      data.sort((d1, d2) => {
        return d1.sortOrder - d2.sortOrder
      })
      const findP = (dataOption) => {
        data.forEach(element => {
          if (!element.enabled) {
            return
          }
          if ((dataOption.isRoot && element.uiMenuType === 0) || element.parentId === dataOption.value) {
            const tempOption = {
              value: element.id,
              label: element.name,
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
    currentChange(data, node) {
      const check = !node.checked
      const val = this.$refs.tree.getCheckedKeys()
      if (data.children && check) {
        const childrenIds = []
        this.getChildrenIds(data, childrenIds)
        // 合并子节点集合
        this.$refs.tree.setCheckedKeys(val.concat(childrenIds))
      } else if (data.children && !check) {
        const childrenIds = []
        this.getChildrenIds(data, childrenIds)
        // 选中节点集合和子节点集合的差集
        const checkIds = val.filter(f => childrenIds.indexOf(f) === -1)
        this.$refs.tree.setCheckedKeys(checkIds)
      }
    },
    getChildrenIds(data, childrenIds) {
      if (data.children) {
        data.children.forEach(children => {
          childrenIds.push(children.value)
          this.getChildrenIds(children, childrenIds)
        })
      }
    }
  }
}
</script>

<template>
  <div>
    <el-input v-if="showSearch" v-model="filterText" :placeholder="$t('commonUse.输入关键字进行过滤')" />
    <el-tree
      ref="tree"
      :data="treeData"
      show-checkbox
      default-expand-all
      check-strictly
      check-on-click-node
      :expand-on-click-node="false"
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
import { getAllBackgroundApi } from '@/api/system/ui-permission'
import store from '@/store'

export default {
  name: 'BackgroundApiTree',
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
    this.listBackgroundApiiResource()
  },
  methods: {
    filterNode(value, treeData) {
      if (!value) return true
      return treeData.label.indexOf(value) !== -1
    },
    listBackgroundApiiResource() {
      getAllBackgroundApi().then(
        response => {
          this.list = response.data
          this.treeData = this.handleListBackgroundApiiResource(this.list)
          if (this.value) {
            this.$refs.tree.setCheckedKeys(this.value)
          }
        }
      )
    },
    handleListBackgroundApiiResource(data) {
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
          if ((dataOption.isRoot && element.primaryNode) || element.parentPermissionCode === dataOption.value) {
            const tempOption = {
              value: element.permissionCode,
              label: this.getName(element),
              isRoot: false
            }
            findP(tempOption, element.permissionCode)
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
    getName(item) {
      if (store.getters.language === 'en') {
        return item.enName
      }
      return item.name
    },
    currentChange(data, node) {
      const check = !node.checked
      const val = this.$refs.tree.getCheckedKeys()
      if (data.children && check) {
        const childrenIds = []
        this.getChildrenIds(data, childrenIds)
        console.log(childrenIds)
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
<style>
.drawer_content .form_contet {
  flex: 1;
  overflow:scroll;
}
</style>

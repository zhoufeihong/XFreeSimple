<template>
  <el-cascader
    v-model="dataValue"
    :options="options"
    filterable
    :props="{ checkStrictly: true }"
    :clearable="true"
    @change="handleChange"
  />
</template>

<script>
import { getAll } from '@/api/system/depart'
import { mapGetters } from 'vuex'

export default {
  name: 'DepartSelect',
  props: {
    value: {
      require: false,
      type: String,
      default: ''
    },
    ignoreValues: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {
      dataValue: [],
      list: [],
      options: []
    }
  },
  computed: {
    ...mapGetters([
      'supperUser',
      'departId'
    ])
  },
  watch: {
    value: function(newValue, oldValue) {
      this.setOptions(newValue)
    },
    ignoreValues: function(newValue, oldVale) {
      this.options = this.transformationTree(this.list)
    }
  },
  created() {
    this.getList()
  },
  methods: {
    transformationTree(data) {
      var tempData = data.filter(item => {
        return !this.ignoreValues.includes(item.id)
      })
      const dataRoot = {
        isRoot: true,
        value: '-1',
        children: []
      }
      tempData.sort((d1, d2) => {
        return d1.sortOrder - d2.sortOrder
      })
      const findP = (dataOption) => {
        tempData.forEach(element => {
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
    getList() {
      getAll().then(response => {
        this.list = response.data.items
        this.options = this.transformationTree(this.list)
        this.setOptions(this.value)
      })
    },
    handleChange(val) {
      if (val && val instanceof Array) {
        if (val.length > 0) {
          this.$emit('input', val[val.length - 1])
          return
        }
      }
      this.$emit('input', '')
    },
    setOptions(newValue) {
      const selectItems = []
      const getSelectItems = id => {
        if (id) {
          this.list.forEach(f => {
            if (f.id === id) {
              selectItems.push(f.id)
              if (f.id !== this.departId) {
                getSelectItems(f.parentId)
              }
            }
          })
        }
      }
      getSelectItems(newValue)
      selectItems.reverse()
      this.dataValue = selectItems
    }
  }
}
</script>
<style>
.el-cascader-panel {
  height: 100%;
}
</style>

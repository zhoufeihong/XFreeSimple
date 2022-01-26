<template>
  <el-select v-model="dictItemId" :placeholder="$t('components.请选择')">
    <el-option
      v-for="item in options"
      :key="item.value"
      :label="item.label"
      :value="item.value"
    />
  </el-select>
</template>

<script>
import { getByDictCode } from '@/api/system/dictItem'
import store from '@/store'

export default {
  name: 'DictItemSelect',
  props: {
    value: {
      required: true,
      type: String
    },
    dictCode: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      options: []
    }
  },
  computed: {
    dictItemId: {
      get() {
        return this.value
      },
      set(val) {
        this.$emit('input', val)
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      getByDictCode(this.dictCode).then(response => {
        this.options = response.data.items.map(m => {
          return {
            value: m.itemValue,
            label: this.getItemText(m)
          }
        })
      })
    },
    getItemText(item) {
      if (store.getters.language === 'en') {
        return item.itemEnText
      }
      return item.itemText
    }
  }
}
</script>

<template>
  <el-select v-model="dictId" :placeholder="$t('components.请选择')">
    <el-option
      v-for="item in options"
      :key="item.value"
      :label="item.label"
      :value="item.value"
      :disabled="item.disabled"
    />
  </el-select>
</template>

<script>
import { getAll } from '@/api/system/dict'

export default {
  name: 'DictSelect',
  props: {
    value: {
      required: false,
      type: String,
      default: ''
    }
  },
  data() {
    return {
      options: []
    }
  },
  computed: {
    dictId: {
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
      getAll().then(response => {
        this.options = response.data.items.map(m => { return { value: m.id, label: m.dictName } })
      })
    }
  }
}
</script>
